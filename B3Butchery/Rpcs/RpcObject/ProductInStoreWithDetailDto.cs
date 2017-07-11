using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forks.EnterpriseServices.JsonRpc;
using Forks.Utils;

namespace BWP.B3Butchery.Rpcs.RpcObject
{
  [RpcObject]
  public class ProductInStoreWithDetailDto
  {
    public long ID { get; set; }
    public DateTime? Date { get; set; }
    public string Goods_Name { get; set; }
    public Money<decimal>? Number{ get; set; }
    public Money<decimal>? SecondNumber { get; set; }

  }
}
