using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using BWP.B3Frameworks;
using BWP.B3UnitedInfos.BO;
using Forks.EnterpriseServices.BusinessInterfaces;
using Forks.EnterpriseServices.DomainObjects2;
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

    [Rpc(RpcFlags.SkipAuth)]
    public static long CreateOutPut(ProduceOutput output)
    {
      using (new SpecialDomainUserBLScope(output.CreateUser_Name))
      {
        using (var session=Dmo.NewSession())
        {
          var bl = BIFactory.Create<IProduceOutputBL>(session);

          output.Domain_ID = DomainContext.Current.ID;

          foreach (var detail in output.Details)
          {
            detail.Goods_ID = GetGoodsIdByName(session, detail.Goods_Name)??0;
          }

          bl.Insert(output);
          return output.ID;
        }
     
      }
    }

    static long? GetGoodsIdByName(IDmoSession session, string name)
    {
      var query=new DQueryDom(new JoinAlias(typeof(Goods)));
      query.Where.Conditions.Add(DQCondition.EQ("Name",name));
      query.Columns.Add(DQSelectColumn.Field("ID"));
      return query.EExecuteScalar<long?>(session);
    }
  }
}
