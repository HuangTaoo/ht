using BWP.B3Butchery.BO;
using BWP.B3Frameworks.BL;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.BusinessInterfaces;

namespace BWP.B3Butchery.BL
{
  [BusinessInterface(typeof(TemporaryStorageBL))]
  [LogicName("暂存单")]
  public interface ITemporaryStorageBL : IDepartmentWorkFlowBillBL<TemporaryStorage>
  {
  }

  public class TemporaryStorageBL : DepartmentWorkFlowBillBL<TemporaryStorage>, ITemporaryStorageBL
  {
  }
}
