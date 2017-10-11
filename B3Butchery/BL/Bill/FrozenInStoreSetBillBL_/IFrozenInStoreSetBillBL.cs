using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Butchery.BO;
using BWP.B3Frameworks.BL;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.BusinessInterfaces;

namespace BWP.B3Butchery.BL
{
  [BusinessInterface(typeof(FrozenInStoreSetBillBL))]
  [LogicName("速冻入库配置单")]
  public interface IFrozenInStoreSetBillBL : IDepartmentWorkFlowBillBL<FrozenInStoreSetBill>
  {
  }
  public class FrozenInStoreSetBillBL : DepartmentWorkFlowBillBL<FrozenInStoreSetBill>, IFrozenInStoreSetBillBL
  {

  }
}
