using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forks.EnterpriseServices.JsonRpc;

namespace BWP.B3Butchery.Rpcs.RpcObject
{
  [RpcObject]
  public class EmpInfoTable
  {
    public long User_ID { get; set; }
    public string User_Name { get; set; }
    public long Domain_ID { get; set; }
    public long Employee_ID { get; set; }
    public string Employee_Name { get; set; }
    public long? AccountingUnit_ID { get; set; }
    public string AccountingUnit_Name { get; set; }
    public long? Department_ID { get; set; }
    public string Department_Name { get; set; }

    public long? ProductionUnit_ID { get; set; }
    public string ProductionUnit_Name { get; set; }

    public string Role { get; set; }
  }
}
