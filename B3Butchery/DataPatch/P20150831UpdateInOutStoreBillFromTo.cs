using BWP.B3Butchery.BO;
using BWP.B3UnitedInfos.DataPatchs;
using Forks.EnterpriseServices.BusinessInterfaces;
using TSingSoft.WebPluginFramework.Install;

namespace BWP.B3Butchery.DataPatch {
  [DataPatch]
  public class P20150831UpdateInOutStoreBillFromTo : IDataPatch {

    public void Execute(TransactionContext context) {
      UpdateInOutStoreHelper.UpdateFromTo(typeof(ProductInStore), "Department_Name", "Store_Name", context);
    }
  }
}
