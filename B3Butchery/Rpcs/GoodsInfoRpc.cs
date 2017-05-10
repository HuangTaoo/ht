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
    public static List<GoodsInfoDto> GetAllGoods()
    {
      var list = new List<GoodsInfoDto>();
      var query=new DQueryDom(new JoinAlias(typeof(Goods)));
      query.Where.Conditions.Add(DQCondition.EQ("Stopped",false));
      //query.Where.Conditions.Add(DQCondition.EQ("Domain_ID", DomainContext.Current.ID));

      query.Columns.Add(DQSelectColumn.Field("ID"));
      query.Columns.Add(DQSelectColumn.Field("Name"));
      query.Columns.Add(DQSelectColumn.Field("MainUnit"));
      query.Columns.Add(DQSelectColumn.Field("SecondUnit"));
      query.Columns.Add(DQSelectColumn.Field("UnitConvertDirection"));
      query.Columns.Add(DQSelectColumn.Field("MainUnitRatio"));
      query.Columns.Add(DQSelectColumn.Field("SecondUnitRatio"));
      query.Columns.Add(DQSelectColumn.Field("Code"));

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
            list.Add(dto);
          }
        }
      }
      return list;

    }

    [Rpc]
    public static List<GoodsInfoDto> GetByDepartPlan(long? departId)
    {
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