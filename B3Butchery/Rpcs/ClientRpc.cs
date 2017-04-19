using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forks.EnterpriseServices.JsonRpc;

namespace BWP.B3Butchery.Rpcs
{
  [Rpc]
  public static class ClientRpc
  {

    [Rpc(RpcFlags.SkipAuth)]
    public static string GetPdaVersion()
    {
      return "20170419";
    }
  }
}
