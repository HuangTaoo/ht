using BWP.B3Frameworks.BO;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DataForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BWP.B3Butchery.BO
{
  [DFClass, Serializable, LogicName("速冻库")]
 public class FrozenStore: DomainBaseInfo
  {
    [LogicName("速冻库编码")]
    public string Code { get; set; }
  }
}
