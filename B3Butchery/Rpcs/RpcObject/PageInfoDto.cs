using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forks.EnterpriseServices.JsonRpc;

namespace BWP.B3Butchery.Rpcs.RpcObject
{
  [RpcObject]
  public class PageInfoDto
  {
    public int PageSize { get; set; }
    public int PageIndex { get; set; }
  }
}
