using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Butchery.BO;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using Forks.EnterpriseServices.JsonRpc;
using Newtonsoft.Json;

namespace BWP.B3Butchery.Rpcs
{
  class GoodsPackingRelationDto
  {
    public long Goods_ID { get; set; }
    public long GoodsPacking_ID { get; set; }//存货对应的包装袋名
    public long Department_ID { get; set; }//存货对应的包装袋名

  }

  [Rpc]
  public static class PackingBagTypeRpc
  {
    [Rpc(RpcFlags.SkipAuth)]
    public static bool UpdateGoodsPackingRelation(string json, string token)
    {
      if (token != "bwpsoft")
      {
        throw new Exception("请联系开发人员");
      }

      var list = JsonConvert.DeserializeObject<List<GoodsPackingRelationDto>>(json);

      using (var session = Dmo.NewSession())
      {
        foreach (GoodsPackingRelationDto dto in list)
        {
          if (dto.GoodsPacking_ID == 0 || dto.Goods_ID == 0)
          {
            continue;
          }

          var updateDom = new DQUpdateDom(typeof(PackingBagType_Detail));
          updateDom.Where.Conditions.Add(DQCondition.EQ("Goods_ID", dto.Goods_ID));
          updateDom.Where.Conditions.Add(DQCondition.InSubQuery(DQExpression.Field("PackingBagType_ID"),GetSubQuery(dto.Department_ID)));
          updateDom.Columns.Add(new DQUpdateColumn("GoodsPacking_ID",dto.GoodsPacking_ID));

          session.ExecuteNonQuery(updateDom);
        }
        session.Commit();
      }
      return true;
    }

    private static DQueryDom GetSubQuery(long dtoDepartmentId)
    {
      var query=new DQueryDom(new JoinAlias(typeof(PackingBagType)) );
      query.Where.Conditions.Add(DQCondition.EQ("Department_ID",dtoDepartmentId));
      query.Columns.Add(DQSelectColumn.Field("ID"));
      return query;
    }
  }
}
