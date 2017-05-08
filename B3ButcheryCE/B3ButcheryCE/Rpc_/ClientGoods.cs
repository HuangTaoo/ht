using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Forks.JsonRpc.Client.Data;
using System.Xml.Serialization;

namespace B3HRCE.Rpc_
{
    public class ClientGoods
    {
        public long Goods_ID { get; set; }

        public string Goods_Name { get; set; }

        public string Goods_BarCode { get; set; }

        public string Goods_MainUnit { get; set; }

        public string Goods_SecondUnit { get; set; }

        public GoodsUnitConvertDirection Goods_UnitConvertDirection { get; set; }

        public decimal? Goods_MainUnitRatio { get; set; }

        public decimal? Goods_SecondUnitRatio { get; set; }

        public decimal? Goods_Number { get; set; }

        public decimal? Goods_SecondNumber { get; set; }


        //[XmlIgnore]
        //public bool ListViewChecked { get; set; }

    }

    public enum GoodsUnitConvertDirection {

        双向转换=0,
        由主至辅=1,
        由辅至主=2
    }
}


