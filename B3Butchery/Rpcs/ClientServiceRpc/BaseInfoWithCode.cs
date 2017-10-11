using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Frameworks.BO;
using Forks.EnterpriseServices.JsonRpc;

namespace BWP.B3Butchery.Rpcs.ClientServiceRpc
{
  [RpcObject]
  public class BaseInfoWithCode:BaseInfo
  {
    public string Code { get; set; }
  }
}
