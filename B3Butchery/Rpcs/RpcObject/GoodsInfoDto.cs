using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forks.EnterpriseServices.JsonRpc;

namespace BWP.B3Butchery.Rpcs.RpcObject
{
  [RpcObject]
  public class GoodsInfoDto: GoodsInfo
  {
    public long Goods_ID { get; set; }
    public string Goods_Name { get; set; }
    public string Goods_Code { get; set; }

    
  }
}
