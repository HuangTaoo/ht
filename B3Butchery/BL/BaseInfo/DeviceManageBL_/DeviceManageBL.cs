using BWP.B3Butchery.BO;
using BWP.B3Frameworks.BL;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.BusinessInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BWP.B3Butchery.BL
{
  [BusinessInterface(typeof(DeviceManageBL))]
  [LogicName("设备管理")]

  public interface IDeviceManageBL : IDomainBaseInfoBL<DeviceManage>
  {

  }
  public class DeviceManageBL : DomainBaseInfoBL<DeviceManage>, IDeviceManageBL
  {

  }
}
