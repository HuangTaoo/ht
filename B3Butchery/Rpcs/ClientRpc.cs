using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using BWP.B3Frameworks.Utils;
using Forks.EnterpriseServices.BusinessInterfaces;
using Forks.EnterpriseServices.JsonRpc;
using TSingSoft.WebPluginFramework;

namespace BWP.B3Butchery.Rpcs {
  [Rpc]
  public static class ClientRpc {

    [Rpc(RpcFlags.SkipAuth)]
    public static string GetPdaVersion() {
      return "20170419";
    }

    [Rpc]
    public static long InsertProductInStore(ProductInStore dmo) {

      using (var context = new TransactionContext()) { 
        var bl = BIFactory.Create<IProductInStoreBL>(context);
        bl.InitNewDmo(dmo);
        dmo.InStoreDate = BLContext.Today;
        foreach (var detail in dmo.Details)
        {
          DmoUtil.RefreshDependency(detail,"Goods_ID");
        }
        bl.Insert(dmo);
        context.Commit();
        return dmo.ID; 
      }
    }
  }
}
