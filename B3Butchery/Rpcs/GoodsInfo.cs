using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Frameworks.BO.NamedValueTemplate;
using Forks.EnterpriseServices.JsonRpc;
using Forks.Utils;

namespace BWP.B3Butchery.Rpcs {
  [RpcObject]
  public class GoodsInfo {
    public string Goods_MainUnit { get; set; }

    public string Goods_SecondUnit { get; set; }
     
    public NamedValue<主辅转换方向>? Goods_UnitConvertDirection { get; set; }
 
    public Money<decimal>? Goods_MainUnitRatio { get; set; }
 
    public Money<decimal>? Goods_SecondUnitRatio { get; set; }
  }
}
