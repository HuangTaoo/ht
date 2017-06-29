using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using BWP.B3Butchery.Rpcs.RpcObject;
using BWP.B3Butchery.Utils;
using BWP.B3Frameworks;
using BWP.B3Frameworks.BO;
using BWP.B3Frameworks.BO.NamedValueTemplate;
using BWP.B3Frameworks.Utils;
using Forks.EnterpriseServices.BusinessInterfaces;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using Forks.EnterpriseServices.JsonRpc;
using Forks.EnterpriseServices.SqlDoms;
using Forks.Utils;
using TSingSoft.WebPluginFramework;

namespace BWP.B3Butchery.Rpcs
{
  [Rpc]
  public static class ProduceOutputRpc
  {
    [Rpc]
    public static List<GoodsInfoDto> GetTodayGoodsByStore(long accountUnitId,long departId,long  storeId)
    {
      var list=new List<GoodsInfoDto>();
      var bill=new JoinAlias(typeof(ProduceOutput));
      var detail=new JoinAlias(typeof(ProduceOutput_Detail));
      var query=new DQueryDom(bill);
      query.From.AddJoin(JoinType.Inner, new DQDmoSource(detail),DQCondition.EQ(bill,"ID",detail, "ProduceOutput_ID"));
      query.Where.Conditions.Add(DQCondition.EQ("AccountingUnit_ID",accountUnitId));

//      query.Where.Conditions.Add(DQCondition.EQ("Department_ID", departId));

      //DQCondition.EQ(string.Format("Department_TreeDeep{0}ID", context.Department_Depth), context.Department_ID)

      query.Where.Conditions.Add(DQCondition.EQ(bill, "Domain_ID", DomainContext.Current.ID));
      query.Where.Conditions.Add(DQCondition.EQ("BillState", 单据状态.已审核));
      query.Where.Conditions.Add(DQCondition.GreaterThanOrEqual(bill, "Time", DateTime.Today));
      query.Where.Conditions.Add(DQCondition.LessThan(bill, "Time", DateTime.Today.AddDays(1)));
      query.Where.Conditions.Add(DQCondition.EQ("FrozenStore_ID",storeId));
      query.Where.Conditions.Add(DQCondition.NotInSubQuery(DQExpression.Field(detail,"Goods_ID"),GetTodayGoodsByStoreSubQuery(accountUnitId,departId,storeId)));


      OrganizationUtil.AddOrganizationLimit(query, typeof(ProduceOutput));


      query.Columns.Add(DQSelectColumn.Field("Goods_ID",detail));
      query.Columns.Add(DQSelectColumn.Field("Goods_Name", detail));
//      query.Columns.Add(DQSelectColumn.Field("Goods_InnerPackingPer", detail));
      query.Columns.Add(DQSelectColumn.Sum(detail, "Number"));
      query.Columns.Add(DQSelectColumn.Create(DQExpression.Divide( DQExpression.Sum(DQExpression.Field(detail,"Number")), DQExpression.Field(detail, "Goods_InnerPackingPer")), "InnerPackingPer"));
      query.Columns.Add(DQSelectColumn.Field("Goods_InnerPackingPer", detail));
      //query.Columns.Add(DQSelectColumn.Create(DQExpression.Divide(DQExpression.Field(detail,"Number"),DQExpression.Field(detail, "Goods_InnerPackingPer")),"包装数"));


      query.GroupBy.Expressions.Add(DQExpression.Field(detail, "Goods_ID"));
      query.GroupBy.Expressions.Add(DQExpression.Field(detail, "Goods_Name"));
      query.GroupBy.Expressions.Add(DQExpression.Field(detail, "Goods_InnerPackingPer"));

      using (var session=Dmo.NewSession())
      {
        using (var reader=session.ExecuteReader(query))
        {
          while (reader.Read())
          {
            var goods=new GoodsInfoDto();
            goods.Goods_ID = (long) reader[0];
            goods.Goods_Name = (string) reader[1];
            goods.Number = (decimal?)((Money<decimal>?) reader[2]);
            goods.InnerPackingPer = (decimal?) reader[3];
            goods.Goods_InnerPackingPer = (decimal?) reader[4];
            list.Add(goods);
          }
        }
      }
      return list;
    }

    private static DQueryDom GetTodayGoodsByStoreSubQuery(long accountUnitId, long departId, long storeId)
    {
      var bill=new JoinAlias("instore",typeof(FrozenInStore));
      var detail=new JoinAlias("instoredetail",typeof(FrozenInStore_Detail));
      var query = new DQueryDom(bill);
      query.From.AddJoin(JoinType.Inner, new DQDmoSource(detail), DQCondition.EQ(bill, "ID", detail, "FrozenInStore_ID"));
      query.Where.Conditions.Add(DQCondition.EQ("AccountingUnit_ID", accountUnitId));
      query.Where.Conditions.Add(DQCondition.EQ("Department_ID", departId));
      //DQCondition.EQ(string.Format("Department_TreeDeep{0}ID", context.Department_Depth), context.Department_ID)
      query.Where.Conditions.Add(DQCondition.EQ(bill, "Domain_ID", DomainContext.Current.ID));
      query.Where.Conditions.Add(DQCondition.EQ("BillState",单据状态.已审核));
      query.Where.Conditions.Add(DQCondition.EQ("Store_ID", storeId));
      query.Where.Conditions.Add(DQCondition.GreaterThanOrEqual(bill, "Date", DateTime.Today));
      query.Where.Conditions.Add(DQCondition.LessThan(bill, "Date", DateTime.Today.AddDays(1)));
      query.Distinct = true;
      query.Columns.Add(DQSelectColumn.Field("Goods_ID",detail));
      return query;
    }

    [Rpc]
    public static long PdaInsertAndCheck(ProduceOutput dmo)
    {
      SetSecondNumberByNumber(dmo);
      using (var context = new TransactionContext())
      {
        var bl = BIFactory.Create<IProduceOutputBL>(context.Session);
        dmo.Time = dmo.Time ?? DateTime.Today;

        dmo.Employee_ID = B3ButcheryUtil.GetCurrentBindingEmployeeID(context.Session);
        bl.InitNewDmo(dmo); 
        bl.Insert(dmo);
        bl.Check(dmo);
        context.Commit();
      }

      return dmo.ID;
    }

    static void SetSecondNumberByNumber(ProduceOutput dmo)
    {
      foreach (ProduceOutput_Detail detail in dmo.Details)
      {
        DmoUtil.RefreshDependency(detail,"Goods_ID");
        if (detail.Goods_UnitConvertDirection == null)
        {
          continue;
        }
        
        if (detail.Goods_UnitConvertDirection == 主辅转换方向.双向转换 || detail.Goods_UnitConvertDirection == 主辅转换方向.由主至辅)
        {
          //辅单位数量
          if (detail.Goods_MainUnitRatio != null && detail.Goods_SecondUnitRatio != null)
          {
            detail.SecondNumber =detail.Number * detail.Goods_SecondUnitRatio / detail.Goods_MainUnitRatio ;
          }
          //辅单位Ⅱ数量
          if (detail.Goods_SecondUnitII_MainUnitRatio != null && detail.Goods_SecondUnitII_SecondUnitRatio != null)
          {
            detail.SecondNumber2= detail.Number * detail.Goods_SecondUnitII_SecondUnitRatio / detail.Goods_SecondUnitII_MainUnitRatio;
          }
        }
      }
    }

    [Rpc]
    public static long PdaInsert(ProduceOutput dmo)
    {
      using (var context=new TransactionContext())
      {
        var bl= BIFactory.Create<IProduceOutputBL>(context.Session);
        dmo.Time = DateTime.Today;
        dmo.IsHandsetSend = true;
        dmo.Employee_ID = B3ButcheryUtil.GetCurrentBindingEmployeeID(context.Session);
        //        bl.InitNewDmo(dmo); 板块信息由手持机传入
        bl.Insert(dmo);
        bl.Check(dmo);
        context.Commit();
      }

      return dmo.ID;
    }


    [Rpc]
    public static long Insert(ProduceOutput dmo)
    {
      var bl = BIFactory.Create<IProduceOutputBL>();
      bl.InitNewDmo(dmo);
      bl.Insert(dmo);
      return dmo.ID;
    }



  }
}
