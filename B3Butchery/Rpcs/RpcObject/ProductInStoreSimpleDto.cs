using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forks.EnterpriseServices.JsonRpc;

namespace BWP.B3Butchery.Rpcs.RpcObject
{
  [RpcObject]
  public class ProductInStoreSimpleDto
  {
    public long ID { get; set; }
    public DateTime InStoreDate { get; set; }
    public string Store_Name { get; set; }
  }
}
