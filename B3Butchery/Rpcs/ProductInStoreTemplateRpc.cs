using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Butchery.BO;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using Forks.EnterpriseServices.JsonRpc;
using Forks.EnterpriseServices.SqlDoms;
using Newtonsoft.Json;

namespace BWP.B3Butchery.Rpcs
{
  class ProductInStoreTemplateGoodsDto
  {
    public long Goods_ID { get; set; }
    public string Goods_Name { get; set; }
    public string Goods_Code { get; set; }
    public string Goods_Spec { get; set; }
  }

  [Rpc]
  public static class ProductInStoreTemplateRpc
  {
    [Rpc(RpcFlags.SkipAuth)]
    public static string GetAllGoods()
    {
      var list=new List<ProductInStoreTemplateGoodsDto>();
      var bill = new JoinAlias(typeof(ProductInStoreTemplate));
      var detail = new JoinAlias(typeof(ProductInStoreTemplate_GoodsDetail));
      var query = new DQueryDom(bill);
      query.From.AddJoin(JoinType.Right,new DQDmoSource(detail),DQCondition.EQ(bill,"ID",detail, "ProductInStoreTemplate_ID") );

      query.Distinct = true;

      query.Columns.Add(DQSelectColumn.Field("Goods_ID",detail));
      query.Columns.Add(DQSelectColumn.Field("Goods_Name", detail));
      query.Columns.Add(DQSelectColumn.Field("Goods_Code", detail));
      query.Columns.Add(DQSelectColumn.Field("Goods_Spec", detail));

      using (var session=Dmo.NewSession())
      {
        using (var reader=session.ExecuteReader(query))
        {
          while (reader.Read())
          {
            var dto=new ProductInStoreTemplateGoodsDto();
            dto.Goods_ID = (long) reader[0];
            dto.Goods_Name = (string) reader[1];
            dto.Goods_Code = (string) reader[2];
            dto.Goods_Spec = (string) reader[3];
            if (list.Any(x => x.Goods_ID == dto.Goods_ID))
            {
              continue;
            }
            list.Add(dto);
          }
        }
      }
      return JsonConvert.SerializeObject(list);
    }
  }
}
