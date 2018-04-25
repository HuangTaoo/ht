using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BWP.B3Butchery.Rpcs.ClientServiceRpc.Dtos
{
  [Serializable]
  class PackagingDto
  {
    public string Name { get; set; }
    public long? Goods_ID { get; set; }

    public string Goods_Name { get; set; }
    public string Goods_Code { get; set; }
    public string Goods_Spec { get; set; }
    public long? GoodsProperty_ID { get; set; }
    public string GoodsProperty_Name { get; set; }
    public string Packing_Attr { get; set; }

  }
}
