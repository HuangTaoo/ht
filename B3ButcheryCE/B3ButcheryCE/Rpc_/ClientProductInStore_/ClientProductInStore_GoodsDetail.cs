using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace B3ButcheryCE.Rpc_.ClientProductInStore_
{
    public class ClientProductInStore_GoodsDetail
    {
        public long Goods_ID { get; set; }

        public string Goods_Name { get; set; }

        public string Goods_Code { get; set; }

        public decimal Goods_Number { get; set; }

        public string Goods_UnitConvertDirection { get; set; }

        public decimal? Goods_MainUnitRatio { get; set; }

        public decimal? Goods_SecondUnitRatio { get; set; }
    }
}
