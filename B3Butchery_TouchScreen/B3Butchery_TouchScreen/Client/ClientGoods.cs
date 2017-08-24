using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B3HuaDu_TouchScreen.Client
{
  public class ClientGoods
  {
    public long Goods_ID { get; set; }
    public string Goods_Name { get; set; }
    public string Goods_Code { get; set; }

    public decimal? Ratio { get; set; }

    public override string ToString()
    {
      return Goods_Name;
    }
  }
}
