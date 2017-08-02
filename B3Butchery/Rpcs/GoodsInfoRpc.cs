using System;
using System.Collections.Generic;
using System.Linq;
using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using BWP.B3Butchery.Rpcs.RpcObject;
using BWP.B3Butchery.Utils;
using BWP.B3Frameworks;
using BWP.B3Frameworks.BO.NamedValueTemplate;
using BWP.B3Frameworks.Utils;
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

    [Rpc]
    public static List<GoodsInfoDto> GetFromProductPlan()
    {
      JoinAlias mainJoinAlias;
      var query = GetPlanDquery(out mainJoinAlias);
      return GetListByDquery(query);
    }

    [Rpc]
    public static List<GoodsInfoDto> GetFromProductPlanByDept(long departId)
    {
      if (departId == 0)
      {
        throw new Exception("员工档案上没有配置部门");
      }
   
      JoinAlias mainJoinAlias;
      var query = GetPlanDquery(out mainJoinAlias);
       query.Where.Conditions.Add(B3ButcheryUtil.部门或上级部门条件(departId, mainJoinAlias));

      return GetListByDquery(query);
    }

    //根据部门取当天的生产计划存货   为了兼容旧的接口保留了，新的接口不调用此方法
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
      var goods=new JoinAlias(typeof(Goods));
      var query=new DQueryDom(bill);
      query.From.AddJoin(JoinType.Inner, new DQDmoSource(detail),DQCondition.EQ(bill,"ID",detail, "ProductPlan_ID") );
      query.From.AddJoin(JoinType.Left, new DQDmoSource(goods),DQCondition.EQ(goods, "ID",detail, "Goods_ID") );
      
      query.Where.Conditions.Add(DQCondition.GreaterThanOrEqual(bill, "Date", DateTime.Today));
      query.Where.Conditions.Add(DQCondition.LessThan(bill,"Date", DateTime.Today.AddDays(1)));
      query.Where.Conditions.Add(DQCondition.EQ(bill,"BillState",单据状态.已审核));

//      query.Where.Conditions.Add(B3ButcheryUtil.部门或上级部门条件(departId??0, bill));
      OrganizationUtil.AddOrganizationLimit(query, typeof(ProductPlan));

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
 
      query.Columns.Add(DQSelectColumn.Field("GoodsProperty_ID", goods));
      query.Columns.Add(DQSelectColumn.Field("GoodsProperty_Name", goods));
      query.Columns.Add(DQSelectColumn.Field("GoodsPropertyCatalog_Name", goods));

      query.Where.Conditions.Add(DQCondition.EQ(bill,"Domain_ID",DomainContext.Current.ID));
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

            dto.GoodsProperty_ID = (long?)reader[11];
            dto.GoodsProperty_Name = (string)reader[12];
            dto.GoodsPropertyCatalog_Name = (string)reader[13];

            list.Add(dto);
          }
        }
      }
      return list;
    }

    static DQueryDom GetPlanDquery(out JoinAlias billAlias)
    {
      var bill = new JoinAlias(typeof(ProductPlan));
      billAlias = bill;
      var detail = new JoinAlias(typeof(ProductPlan_OutputDetail));
      var goods = new JoinAlias(typeof(Goods));
      var query = new DQueryDom(bill);
      query.From.AddJoin(JoinType.Inner, new DQDmoSource(detail), DQCondition.EQ(bill, "ID", detail, "ProductPlan_ID"));
      query.From.AddJoin(JoinType.Left, new DQDmoSource(goods), DQCondition.EQ(goods, "ID", detail, "Goods_ID"));

      query.Where.Conditions.Add(DQCondition.GreaterThanOrEqual(bill, "Date", DateTime.Today));
      query.Where.Conditions.Add(DQCondition.LessThan(bill, "Date", DateTime.Today.AddDays(1)));
      query.Where.Conditions.Add(DQCondition.EQ(bill, "BillState", 单据状态.已审核));

      OrganizationUtil.AddOrganizationLimit(query, typeof(ProductPlan));

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

      query.Columns.Add(DQSelectColumn.Field("GoodsProperty_ID", goods));
      query.Columns.Add(DQSelectColumn.Field("GoodsProperty_Name", goods));
      query.Columns.Add(DQSelectColumn.Field("GoodsPropertyCatalog_Name", goods));
      query.Columns.Add(DQSelectColumn.Field("GoodsPropertyCatalog_Sort", goods));
      query.Columns.Add(DQSelectColumn.Field("Spell", goods));

      query.OrderBy.Expressions.Add(DQOrderByExpression.Create(goods,"GoodsPropertyCatalog_Sort"));

      query.Where.Conditions.Add(DQCondition.EQ(bill, "Domain_ID", DomainContext.Current.ID));

      return query;
    }

    static List<GoodsInfoDto> GetListByDquery(DQueryDom query)
    {
      var list = new List<GoodsInfoDto>();
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

            dto.SecondUnitII = (string)reader[8];
            dto.SecondUnitII_MainUnitRatio = Convert.ToDecimal(reader[9]);
            dto.SecondUnitII_SecondUnitRatio = Convert.ToDecimal(reader[10]);

            dto.GoodsProperty_ID = (long?)reader[11];
            dto.GoodsProperty_Name = (string)reader[12];
            dto.GoodsPropertyCatalog_Name = (string)reader[13];
            dto.GoodsPropertyCatalog_Sort = (int?)reader[14];
            dto.Goods_Spell=(string)reader[15];



            list.Add(dto);
          }
        }
      }
      return list;
    }


    [Rpc]
    public static GoodsInfoDto Get(long? id ) {
 
      if(id==null)
        return null;
      var goodsInfo = new GoodsInfoDto();

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