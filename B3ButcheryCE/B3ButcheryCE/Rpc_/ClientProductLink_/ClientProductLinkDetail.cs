using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace B3ButcheryCE.Rpc_.ClientProductLink_
{
    public class ClientProductLinkDetail
    {
        public long? ProductPlanID { get; set; }

        public string ProductNumber { get; set; }

        public long? Goods_ID { get; set; }

        public string Goods_Name { get; set; }

        public decimal? MainNumber { get; set; }

        public decimal? SecondNumber { get; set; }
    }
}
