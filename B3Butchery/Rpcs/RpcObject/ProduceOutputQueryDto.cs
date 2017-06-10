using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forks.EnterpriseServices.JsonRpc;
using TSingSoft.WebControls2;

namespace BWP.B3Butchery.Rpcs.RpcObject
{
  [RpcObject]
  public class ProduceOutputQueryDto
  {
    public  long AccountingUnit_ID { get; set; }
    public  long Department_ID { get; set; }
    public  long Store_ID { get; set; }
  }
}
