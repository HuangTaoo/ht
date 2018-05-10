using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using BWP.B3Frameworks;
using BWP.B3Frameworks.Utils;
using BWP.B3UnitedInfos.BO;
using Forks.EnterpriseServices.BusinessInterfaces;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using Forks.EnterpriseServices.JsonRpc;
using Forks.EnterpriseServices.SqlDoms;
using Forks.Utils;
using Newtonsoft.Json;
using TSingSoft.WebPluginFramework;

namespace BWP.B3Butchery.Rpcs
{
    //[RpcObject]
    public class GetChengPin
    {
        public long Goods_ID { get; set; }
        public string Goods_Name { get; set; }
        //Goods2为成品   Goods为半成品
        public long Goods2_ID { get; set; }
        public string Goods2_Name { get; set; }

        //主单位
        public decimal MainUnitRatio { get; set; }
        //主单位
        public decimal SecondUnitRatio { get; set; }
        //成品数量
        public Money<decimal> Number { get; set; }

        public long PlanNumber { get; set; }
    }
    [Rpc]
    public static class OuterPackingRpc
    {
        [Rpc(RpcFlags.SkipAuth)]
        public static string GetFinishProductList()
        {
            var lastday = DateTime.Today;

            var joinProduce = new JoinAlias(typeof(ProduceOutput));
            var joinDetail = new JoinAlias(typeof(ProduceOutput_Detail));
            var joinChengPin = new JoinAlias(typeof(ChengPinToBanChengPinConfig));

            var query = new DQueryDom(joinDetail);
            query.From.AddJoin(JoinType.Left, new DQDmoSource(joinProduce), DQCondition.EQ(joinProduce, "ID", joinDetail, "ProduceOutput_ID"));
            query.From.AddJoin(JoinType.Left, new DQDmoSource(joinChengPin), DQCondition.EQ(joinDetail, "Goods_ID", joinChengPin, "Goods2_ID"));

            query.Columns.Add(DQSelectColumn.Field("Goods_ID", joinDetail));
            query.Columns.Add(DQSelectColumn.Field("Goods_Name", joinDetail));
            query.Columns.Add(DQSelectColumn.Field("Goods_ID", joinChengPin));
            query.Columns.Add(DQSelectColumn.Field("Goods_Name", joinChengPin));


            query.Where.Conditions.Add(DQCondition.GreaterThanOrEqual(joinProduce, "Time", lastday.AddDays(-1)));
            query.Where.Conditions.Add(DQCondition.LessThan(joinProduce, "Time", lastday));


            var list = new List<GetChengPin>();
            using (var session = Dmo.NewSession())
            {
                using (var reader = session.ExecuteReader(query))
                {
                    while (reader.Read())
                    {
                        var dto = new GetChengPin();

                        dto.Goods_ID = Convert.ToInt64(reader[0]);
                        dto.Goods_Name = reader[1].ToString();
                        dto.Goods2_ID = Convert.ToInt64(reader[2]);
                        dto.Goods2_Name = reader[3].ToString();
                        list.Add(dto);
                    }
                }
            }

            return JsonConvert.SerializeObject(list);
        }


        [Rpc(RpcFlags.SkipAuth)]
        public static string GetPlanNumber()
        {
            var lastday = DateTime.Today;

            var joinProduce = new JoinAlias(typeof(ProduceOutput));
            var joinDetail = new JoinAlias(typeof(ProduceOutput_Detail));
            var joinChengPin = new JoinAlias(typeof(ChengPinToBanChengPinConfig));
            var joinNumber = new JoinAlias(typeof(ButcheryGoods));
            var query = new DQueryDom(joinDetail);
            query.From.AddJoin(JoinType.Left, new DQDmoSource(joinProduce), DQCondition.EQ(joinProduce, "ID", joinDetail, "ProduceOutput_ID"));
            query.From.AddJoin(JoinType.Left, new DQDmoSource(joinChengPin), DQCondition.EQ(joinDetail, "Goods_ID", joinChengPin, "Goods2_ID"));
            query.From.AddJoin(JoinType.Left, new DQDmoSource(joinNumber), DQCondition.EQ(joinChengPin, "Goods2_ID", joinNumber, "ID"));

            
            //主单位
            query.Columns.Add(DQSelectColumn.Field("MainUnitRatio", joinNumber));
            //辅单位
            query.Columns.Add(DQSelectColumn.Field("SecondUnitRatio", joinNumber));

            //成品数量   要做的操作是 成品数量/（主单位*辅单位）
            query.Columns.Add(DQSelectColumn.Field("Number", joinDetail));
            query.Where.Conditions.Add(DQCondition.GreaterThanOrEqual(joinProduce, "Time", lastday.AddDays(-1)));
            query.Where.Conditions.Add(DQCondition.LessThan(joinProduce, "Time", lastday));

            var list = new List<GetChengPin>();
            using (var session = Dmo.NewSession())
            {
                using (var reader = session.ExecuteReader(query))
                {
                    while (reader.Read())
                    {
                        var dto = new GetChengPin();

                        dto.MainUnitRatio = (decimal)reader[4];
                        dto.SecondUnitRatio = (decimal)reader[5];
                        dto.Number = (Money<decimal>)reader[6];

                        dto.PlanNumber = Convert.ToInt32(dto.Number / (dto.MainUnitRatio * dto.SecondUnitRatio));
                        list.Add(dto);
                    }
                }
            }
            return JsonConvert.SerializeObject(list);

        }


    }
}
