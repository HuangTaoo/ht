using BWP.B3Butchery.BO;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using Forks.EnterpriseServices.JsonRpc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using TSingSoft.WebPluginFramework;

namespace BWP.B3Butchery.Rpcs.ClientServiceRpc
{
    [Rpc]
    class ClientGoodsSetRpc
    {

        static JavaScriptSerializer serializer = new JavaScriptSerializer();

        [Rpc(RpcFlags.SkipAuth)]
        public static string GetList()
        {
            var query = new DmoQuery(typeof(ClientGoodsSet));
            query.Where.Conditions.Add(DQCondition.EQ("Stopped", false));

            var list = query.EExecuteList().Cast<ClientGoodsSet>().ToList();
            var json = JsonConvert.SerializeObject(list);
            return json;
        }
    }
}
