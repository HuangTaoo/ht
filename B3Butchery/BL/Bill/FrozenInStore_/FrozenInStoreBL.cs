using BWP.B3Butchery.BO;
using BWP.B3Frameworks.BL;
using BWP.B3UnitedInfos;
using BWP.B3UnitedInfos.BO;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.BusinessInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSingSoft.WebPluginFramework;
namespace BWP.B3Butchery.BL
{
  [BusinessInterface(typeof(FrozenInStoreBL))]
  [LogicName("速冻入库")]
  public interface IFrozenInStoreBL : IDepartmentWorkFlowBillBL<FrozenInStore>
  {
  }
  public class FrozenInStoreBL : DepartmentWorkFlowBillBL<FrozenInStore>, IFrozenInStoreBL
  {
   
  }
}
