using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forks.EnterpriseServices.JsonRpc;

namespace BWP.B3Butchery.Rpcs.RpcObject
{
  [RpcObject]
  public class BaseInfoDto
  {
    public long ID { get; set; }

    public string Name { get; set; }
  }
}
