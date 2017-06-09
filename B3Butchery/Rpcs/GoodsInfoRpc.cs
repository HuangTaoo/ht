using System;
using System.Collections.Generic;
using System.Linq;
using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using BWP.B3Butchery.Rpcs.RpcObject;
using BWP.B3Butchery.Utils;
using BWP.B3Frameworks;
using BWP.B3Frameworks.BO.NamedValueTemplate;
using BWP.B3UnitedInfos.BO;
using BWP.Web.Utils;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using Forks.EnterpriseServices.JsonRpc;
using Forks.EnterpriseServices.SqlDoms;
using Forks.Utils;

namespace BWP.B3Butchery.Rpcs
{
  [Rpc]
  public static class GoodsInfoRpc {

    [Rpc]
    public static List<GoodsInfoDto> GetAllGoods(string input="")
    {
      var list = new List<GoodsInfoDto>();
      var joinGoods = new JoinAlias(typeof(Goods));
//      var goodsProperty = new JoinAlias(typeof(GoodsProperty));
//      var goodsPropertyCatalog = new JoinAlias(typeof(GoodsPropertyCatalog));
      var query=new DQueryDom(joinGoods);
     
      query.Where.Conditions.Add(DQCondition.EQ("Stopped",false));
      if (!string.IsNullOrWhiteSpace(input))
      {
        query.Where.Conditions.Add(DQCondition.Or(DQCondition.Like("Name",input),DQCondition.Like("Code", input),DQCondition.Like("Spell", input)));
      }
      query.OrderBy.Expressions.Add(DQOrderByExpression.Create("Name"));
      query.OrderBy.Expressions.Add(DQOrderByExpression.Create("Code"));
      //query.Where.Conditions.Add(DQCondition.EQ("Domain_ID", DomainContext.Current.ID));

      query.Columns.Add(DQSelectColumn.Field("ID"));
      query.Columns.Add(DQSelectColumn.Field("Name"));
      query.Columns.Add(DQSelectColumn.Field("MainUnit"));
      query.Columns.Add(DQSelectColumn.Field("SecondUnit"));
      query.Columns.Add(DQSelectColumn.Field("UnitConvertDirection"));
      query.Columns.Add(DQSelectColumn.Field("MainUnitRatio"));
      query.Columns.Add(DQSelectColumn.Field("SecondUnitRatio"));
      query.Columns.Add(DQSelectColumn.Field("Code"));
      query.Columns.Add(DQSelectColumn.Field("GoodsProperty_ID"));
      query.Columns.Add(DQSelectColumn.Field("GoodsProperty_Name"));
      query.Columns.Add(DQSelectColumn.Field("GoodsPropertyCatalog_Name"));
      query.Columns.Add(DQSelectColumn.Field("SecondUnitII"));
      query.Columns.Add(DQSelectColumn.Field("SecondUnitII_MainUnitRatio"));
      query.Columns.Add(DQSelectColumn.Field("SecondUnitII_SecondUnitRatio"));


      using (var session = Dmo.NewSession())
      {
        using (var reader = session.ExecuteReader(query))
        {
          while (reader.Read())
          {
            var dto = new GoodsInfoDto();
            dto.Goods_ID = (long)reader[0];
            dto.Goods_Name = (string)reader[1];
            dto.Goods_MainUnit = (string)reader[2];
            dto.Goods_SecondUnit = (string)reader[3];
            dto.Goods_UnitConvertDirection = (NamedValue<主辅转换方向>?)reader[4];
            dto.Goods_MainUnitRatio = (Money<decimal>?)reader[5];
            dto.Goods_SecondUnitRatio = (Money<decimal>?)reader[6];
            dto.Goods_Code = (string)reader[7];
            if (dto.Goods_MainUnitRatio == null)
            {
              dto.Goods_MainUnitRatio = 1;
            }
            if (dto.Goods_SecondUnitRatio == null)
            {
              dto.Goods_SecondUnitRatio = 1;
            }
            dto.GoodsProperty_ID = (long?) reader[8];
            dto.GoodsProperty_Name = (string) reader[9];
            dto.GoodsPropertyCatalog_Name = (string) reader[10];
            dto.SecondUnitII = (string) reader[11];
            dto.SecondUnitII_MainUnitRatio = Convert.ToDecimal(reader[12]);
            dto.SecondUnitII_SecondUnitRatio = Convert.ToDecimal(reader[13]);

            list.Add(dto);
          }
        }
      }
      return list;

    }


    //根据部门取当天的生产计划存货
    [Rpc]
    public static List<GoodsInfoDto> GetByDepartPlan(long? departId)
    {
      if (departId == null || departId == 0)
      {
        throw new Exception("员工档案上没有配置部门");
      }
      var list=new List<GoodsInfoDto>();
      var bill=new JoinAlias(typeof(ProductPlan));
      var detail=new JoinAlias(typeof(ProductPlan_OutputDetail));
      var query=new DQueryDom(bill);
      query.From.AddJoin(JoinType.Inner, new DQDmoSource(detail),DQCondition.EQ(bill,"ID",detail, "ProductPlan_ID") );

      query.Where.Conditions.Add(DQCondition.GreaterThanOrEqual(bill, "Date", DateTime.Today));
      query.Where.Conditions.Add(DQCondition.LessThan(bill,"Date", DateTime.Today.AddDays(1)));
      query.Where.Conditions.Add(DQCondition.EQ(bill,"BillState",单据状态.已审核));
      query.Where.Conditions.Add(B3ButcheryUtil.部门或上级部门条件(departId??0, bill));

      query.Columns.Add(DQSelectColumn.Field("Goods_ID", detail));
      query.Columns.Add(DQSelectColumn.Field("Goods_Name", detail));
      query.Columns.Add(DQSelectColumn.Field("Goods_MainUnit", detail));
      query.Columns.Add(DQSelectColumn.Field("Goods_SecondUnit", detail));
      query.Columns.Add(DQSelectColumn.Field("Goods_UnitConvertDirection", detail));
      query.Columns.Add(DQSelectColumn.Field("Goods_MainUnitRatio", detail));
      query.Columns.Add(DQSelectColumn.Field("Goods_SecondUnitRatio", detail));
      query.Columns.Add(DQSelectColumn.Field("Goods_Code", detail));

      query.Columns.Add(DQSelectColumn.Field("Goods_SecondUnitII", detail));
      query.Columns.Add(DQSelectColumn.Field("Goods_SecondUnitII_MainUnitRatio", detail));
      query.Columns.Add(DQSelectColumn.Field("Goods_SecondUnitII_SecondUnitRatio", detail));

      using (var session=Dmo.NewSession())
      {
        using (var reader=session.ExecuteReader(query))
        {
          while (reader.Read())
          {
            var dto=new GoodsInfoDto();
            dto.Goods_ID = (long) reader[0];
            dto.Goods_Name = (string) reader[1];
            dto.Goods_MainUnit = (string) reader[2];
            dto.Goods_SecondUnit = (string) reader[3];
            dto.Goods_UnitConvertDirection = (NamedValue<主辅转换方向>?) reader[4];
            dto.Goods_MainUnitRatio = (Money<decimal>?) reader[5];
            dto.Goods_SecondUnitRatio = (Money<decimal>?) reader[6];
            dto.Goods_Code = (string) reader[7];
            if (dto.Goods_MainUnitRatio == null)
            {
              dto.Goods_MainUnitRatio = 1;
            }
            if (dto.Goods_SecondUnitRatio == null)
            {
              dto.Goods_SecondUnitRatio = 1;
            }

            dto.SecondUnitII = (string)reader[8];
            dto.SecondUnitII_MainUnitRatio = Convert.ToDecimal(reader[9]);
            dto.SecondUnitII_SecondUnitRatio = Convert.ToDecimal(reader[10]);

            
            list.Add(dto);
          }
        }
      }
      return list;
    }

    [Rpc]
    public static GoodsInfo Get(long? id ) {
 
      if(id==null)
        return null;
      var goodsInfo = new GoodsInfo();

      var goods = WebBLUtil.GetSingleDmo<Goods>("ID", id, "SecondUnit", "UnitConvertDirection", "MainUnit", "MainUnitRatio", "SecondUnitRatio");
      goodsInfo.Goods_SecondUnit = goods.SecondUnit;
      goodsInfo.Goods_MainUnit = goods.MainUnit;
      goodsInfo.Goods_UnitConvertDirection = goods.UnitConvertDirection;
      goodsInfo.Goods_MainUnitRatio = goods.MainUnitRatio;
      goodsInfo.Goods_SecondUnitRatio = goods.SecondUnitRatio;
      return goodsInfo;
    }
 
  }
}