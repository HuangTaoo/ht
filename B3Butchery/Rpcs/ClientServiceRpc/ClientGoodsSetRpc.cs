
using BWP.B3Butchery.BO;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using Forks.EnterpriseServices.JsonRpc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using BWP.B3UnitedInfos.BO;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.SqlDoms;
using Forks.Utils;
using TSingSoft.WebPluginFramework;

namespace BWP.B3Butchery.Rpcs.ClientServiceRpc
{
    [Rpc]
    public static class ClientGoodsSetRpc
    {

        [Rpc(RpcFlags.SkipAuth)]
        public static string GetList()
        {
            using (var session = Dmo.NewSession())
            {
                var query = new DmoQuery(typeof(ClientGoodsSet));
                query.Where.Conditions.Add(DQCondition.EQ("Stopped", false));

                var list = session.ExecuteList(query).Cast<ClientGoodsSet>().ToList();
                var prod = FromProductNotice(session);
                list.Add(prod);
                var json = JsonConvert.SerializeObject(list);
                return json;
            }
 

        }


        private static ClientGoodsSet FromProductNotice(IDmoSession session)
        {
            var client = new ClientGoodsSet();
            client.Name = "from生产通知单";//固定值
            var main = new JoinAlias(typeof(ProductNotice));
            var detail = new JoinAlias(typeof(ProductNotice_Detail));
            var goods= new JoinAlias(typeof(ButcheryGoods));
            var query = new DQueryDom(main);
            query.From.AddJoin(JoinType.Left, new DQDmoSource(detail), DQCondition.EQ(main, "ID", detail, "ProductNotice_ID"));
            query.From.AddJoin(JoinType.Left, new DQDmoSource(goods),DQCondition.EQ(detail, "Goods_ID", goods, "ID") );
            query.Where.Conditions.Add(DQCondition.GreaterThanOrEqual(main, "BillState", 20));
            query.Where.Conditions.Add(DQCondition.GreaterThanOrEqual(detail, "DeliveryDate", DateTime.Today.AddDays(-5)));
            query.Columns.Add(DQSelectColumn.Field("Goods_ID", detail));
            query.Columns.Add(DQSelectColumn.Field("Goods_Name", detail));
            query.Columns.Add(DQSelectColumn.Field("DeliveryDate", detail));
            query.Columns.Add(DQSelectColumn.Field("GoodsProperty_ID", goods));
            query.Columns.Add(DQSelectColumn.Field("GoodsProperty_Name", goods));
            query.Columns.Add(DQSelectColumn.Field("MainUnitRatio", goods));
            query.Columns.Add(DQSelectColumn.Field("Spell", goods));
            query.Columns.Add(DQSelectColumn.Field("Spec", goods));
            query.Columns.Add(DQSelectColumn.Field("Code", goods));
            query.Columns.Add(DQSelectColumn.Field("SecondUnitII_MainUnitRatio", goods));
            query.Columns.Add(DQSelectColumn.Field("StandardSecondNumber", goods));
            
            using (var reader = session.ExecuteReader(query))
            {
                while (reader.Read())
                {
                    var clientDe = new  ClientGoodsSet_Detail();
                    clientDe.Goods_ID = (long)reader[0];
                    clientDe.Goods_Name = (string)reader[1];
                    clientDe.CompletedDate = (DateTime?)reader[2];
                    clientDe.GoodsProperty_ID = (long)reader[3];
                    clientDe.GoodsProperty_Name = (string)reader[4];
                    clientDe.Goods_MainUnitRatio = (Money<decimal>?)reader[5];
                    clientDe.Goods_Spell = (string)reader[6];
                    clientDe.Goods_Spec = (string)reader[7];
                    clientDe.Goods_Code = (string)reader[8];
                    clientDe.Goods_SecondUnitII_MainUnitRatio = (Money<decimal>?)reader[9];
                    clientDe.Goods_StandardSecondNumber = (Money<decimal>?)reader[10];
                    
                    client.Details.Add(clientDe);
                }
            }
            return client;

        }
    }


}



