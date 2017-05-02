using BWP.B3Butchery.BO;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using Forks.EnterpriseServices.JsonRpc;
using Forks.EnterpriseServices.SqlDoms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSingSoft.WebPluginFramework;

namespace BWP.B3Butchery.Rpcs
{
  class FrozenInStoreRpc
  {
    /// <summary>
    /// 根据速冻入库单号获取存货名称和数量
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    [Rpc]
    public static List<RpcEasyProductInStore_Detail> GetRpcEasyFrozenInStoreDetailById(long ID) {

      var ris = new JoinAlias(typeof(FrozenInStore));
      var risDetail = new JoinAlias(typeof(FrozenInStore_Detail));
      DQueryDom query = new DQueryDom(ris);
      query.From.AddJoin(JoinType.Left, new DQDmoSource(risDetail), DQCondition.EQ(ris, "ID", risDetail, "FrozenInStore_ID"));
      query.Columns.Add(DQSelectColumn.Field("Goods_Name", risDetail));
      query.Columns.Add(DQSelectColumn.Field("Number", risDetail));
      query.Where.Conditions.Add(DQCondition.EQ("ID", ID));
      return query.EExecuteList<string, object>().Select(x => new RpcEasyProductInStore_Detail(x.Item1, x.Item2)).ToList();
    }
    
  }
}
