using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using BWP.B3Butchery.BO;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using Forks.EnterpriseServices.JsonRpc;
using Newtonsoft.Json;
using TSingSoft.WebPluginFramework;

namespace BWP.B3Butchery.Rpcs.ClientServiceRpc
{
  [Rpc]
  public static class CSBillRpc
  {
    static JavaScriptSerializer serializer = new JavaScriptSerializer();
    [Rpc(RpcFlags.SkipAuth)]
    public static string GetAllFrozenInStoreSetBillList(int billstate)
    {
      var dmoquery = new DmoQuery(typeof(FrozenInStoreSetBill));
      dmoquery.Where.Conditions.Add(DQCondition.EQ("BillState",billstate));
      var list= dmoquery.EExecuteList().Cast<FrozenInStoreSetBill>().ToList();

      var jsonStr = JsonConvert.SerializeObject(list);
      //var jsonStr = serializer.Serialize(list);
      return jsonStr;
    }
  }
}
