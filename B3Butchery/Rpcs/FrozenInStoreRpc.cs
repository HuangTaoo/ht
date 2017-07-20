using System.Collections.Generic;
using BWP.B3Butchery.BO;
using BWP.B3Frameworks;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using Forks.EnterpriseServices.JsonRpc;
using System;
using BWP.B3Butchery.BL;
using BWP.B3Butchery.Utils;
using BWP.B3Frameworks.BO.NamedValueTemplate;
using BWP.B3Frameworks.Utils;
using Forks.EnterpriseServices.BusinessInterfaces;
using Forks.EnterpriseServices.SqlDoms;
using Forks.Utils;
using TSingSoft.WebPluginFramework;

namespace BWP.B3Butchery.Rpcs
{
  [Rpc]
  public static class FrozenInStoreRpc
  {

    [Rpc]
    public static long PdaInsertAndCheck(FrozenInStore dmo)
    {
      if (!BLContext.User.IsInRole("B3Butchery.产出单.新建"))
      {
        return 0;
      }
      SetSecondNumberByNumber(dmo);
      using (var context = new TransactionContext())
      {
        var bl = BIFactory.Create<IFrozenInStoreBL>(context.Session);
        dmo.Date = dmo.Date ?? DateTime.Today;

        dmo.Employee_ID = B3ButcheryUtil.GetCurrentBindingEmployeeID(context.Session);
        bl.InitNewDmo(dmo);
        bl.Insert(dmo);
        bl.Check(dmo);
        context.Commit();
      }

      return dmo.ID;
    }
    static void SetSecondNumberByNumber(FrozenInStore dmo)
    {
      foreach (FrozenInStore_Detail detail in dmo.Details)
      {
        DmoUtil.RefreshDependency(detail, "Goods_ID");
        if (detail.Goods_UnitConvertDirection == null)
        {
          continue;
        }

        if (detail.Goods_UnitConvertDirection == 主辅转换方向.双向转换 || detail.Goods_UnitConvertDirection == 主辅转换方向.由主至辅)
        {
          //辅单位数量
          if (detail.Goods_MainUnitRatio != null && detail.Goods_SecondUnitRatio != null)
          {
            detail.SecondNumber = detail.Number * detail.Goods_SecondUnitRatio / detail.Goods_MainUnitRatio;
          }
          //辅单位Ⅱ数量
          if (detail.Goods_SecondUnitII_MainUnitRatio != null && detail.Goods_SecondUnitII_SecondUnitRatio != null)
          {
            detail.SecondNumber2 = detail.Number * detail.Goods_SecondUnitII_SecondUnitRatio / detail.Goods_SecondUnitII_MainUnitRatio;
          }
        }
      }
    }

    [Rpc]
    public static long? UnCheck(long id)
    {
      if (!BLContext.User.IsInRole("B3Butchery.速冻入库.撤销"))
      {
        return 0;
      }
      using (var context = new TransactionContext())
      {
        var bl = BIFactory.Create<IFrozenInStoreBL>(context.Session);
        var dmo = bl.Load(id);
        bl.UnCheck(dmo);
        context.Commit();
        return dmo.ID;
      }
    }

    [Rpc]
    public static long? Nullify(long id)
    {
      if (!BLContext.User.IsInRole("B3Butchery.速冻入库.作废"))
      {
        return 0;
      }
      using (var context = new TransactionContext())
      {
        var bl = BIFactory.Create<IFrozenInStoreBL>(context.Session);
        var dmo = bl.Load(id);
        bl.Nullify(dmo);
        context.Commit();
        return dmo.ID;
      }
    }

    [Rpc]
    public static List<FrozenInStoreObj> GetFrozenInStore(bool? positiveNumber)
    {
      var bill = new JoinAlias(typeof(FrozenInStore));
      var detail = new JoinAlias(typeof(FrozenInStore_Detail));
      var query = new DQueryDom(bill);
      query.From.AddJoin(JoinType.Left, new DQDmoSource(detail), DQCondition.EQ(bill, "ID", detail, "FrozenInStore_ID"));
      query.Columns.Add(DQSelectColumn.Field("ID", bill));
      query.Columns.Add(DQSelectColumn.Field("Date", bill));
      query.Columns.Add(DQSelectColumn.Field("Goods_ID", detail));
      query.Columns.Add(DQSelectColumn.Field("Goods_Name", detail));
      query.Columns.Add(DQSelectColumn.Field("Number", detail));
      query.Where.Conditions.Add(DQCondition.EQ(bill, "Domain_ID", DomainContext.Current.ID));
      query.Where.Conditions.Add(DQCondition.Or(DQCondition.EQ(bill, "BillState", 单据状态.已审核), DQCondition.EQ(bill, "BillState", 单据状态.未审核)));
      query.OrderBy.Expressions.Add(DQOrderByExpression.Create(bill, "ID", true));
      OrganizationUtil.AddOrganizationLimit(query, typeof(FrozenInStore));
      var list = new List<FrozenInStoreObj>();
      using (var session = Dmo.NewSession())
      {
        using (var reader = session.ExecuteReader(query))
        {
          while (reader.Read())
          {
            if (positiveNumber == true)
            {
              if ((Money<decimal>?)reader[4] < 0)
                continue;
            }
            else
            {
              if ((Money<decimal>?)reader[4] >= 0)
                continue;
            }
            list.Add(new FrozenInStoreObj
            {
              ID = (long)reader[0],
              Date = (DateTime?)reader[1],
              Goods_ID = (long?)reader[2],
              Goods_Name = (string)reader[3],
              Number = (Money<decimal>?)reader[4]
            });
          }
        }
      }
      return list;
    }
  }

  [RpcObject]
  public class FrozenInStoreObj
  {
    public long? ID { get; set; }
    public DateTime? Date { get; set; }
    public long? Goods_ID { get; set; }
    public string Goods_Name { get; set; }
    public Money<decimal>? Number { get; set; }
  }
}
