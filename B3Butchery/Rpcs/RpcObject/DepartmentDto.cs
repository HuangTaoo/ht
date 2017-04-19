using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forks.EnterpriseServices.JsonRpc;

namespace BWP.B3Butchery.Rpcs.RpcObject
{
  [RpcObject]
  public class DepartmentDto:BaseInfoDto
  {
    public int? Department_Depth { get; set; }
  }
}
