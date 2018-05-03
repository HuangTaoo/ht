using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using BWP.B3Frameworks;
using BWP.B3Frameworks.Utils;
using Forks.EnterpriseServices.BusinessInterfaces;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.JsonRpc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TSingSoft.WebPluginFramework;

namespace BWP.B3Butchery.Rpcs
{
  [Rpc]
  public static class PackagingTransferRpc
  {
    [Rpc(RpcFlags.SkipAuth)]
    public static long InertAndCheck(string json)
    {
      PackagingTransfer jsonDom = JsonConvert.DeserializeObject<PackagingTransfer>(json);
      long returnid;
      using (new SpecialDomainUserBLScope(jsonDom.CreateUser_Name))
      {
        //判断是否有权限
        if (!BLContext.User.IsInRole("B3Butchery.包装调拨单.新建"))
        {
          throw new Exception("没有新建权限");
        }

        using (var session = Dmo.NewSession())
        {
          var bl = BIFactory.Create<IPackagingTransferBL>(session);
          //          bl.InitNewDmo(jsonDom);
          var profile = DomainUserProfileUtil.Load<B3ButcheryUserProfile>();
          if (profile.AccountingUnit_ID == null)
          {
            throw new Exception("板块个性设置没有设置会计单位");
          }

          jsonDom.AccountingUnit_ID = profile.AccountingUnit_ID;
          jsonDom.Domain_ID = DomainContext.Current.ID;
          if (jsonDom.Date == null)
          {
            jsonDom.Date=DateTime.Today;
          }

          //插入单据
          bl.Insert(jsonDom);
          //审核
          bl.Check(jsonDom);
          returnid = jsonDom.ID;
          session.Commit();
        }
      }
      return returnid;
    }
  }
}
