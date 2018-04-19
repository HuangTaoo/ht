using BWP.B3Frameworks.BO;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DataForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BWP.B3Butchery.BO
{

  [LogicName("设备管理")]
  [Serializable, DFClass]
  public class DeviceManage : DomainBaseInfo, IWithCodeBaseInfo
  {

    [LogicName("IP地址")]
    public string IP { get; set; }
    [LogicName("编码")]
    public string Code { get; set; }
  }
}
