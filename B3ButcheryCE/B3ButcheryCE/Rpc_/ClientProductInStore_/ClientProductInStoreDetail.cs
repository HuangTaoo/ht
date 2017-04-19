using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace B3HRCE.Rpc_.ClientProductInStore_
{
    public class ClientProductInStoreDetail
    {
        public long? ProductPlanID { get; set; }

        public string ProductNumber { get; set; }

        public long? Goods_ID { get; set; }

        public long? Store_ID { get; set; }

        public string Goods_Name { get; set; }

        public string Store_Name { get; set; }

        public decimal? MainNumber { get; set; }

        public decimal? SecondNumber { get; set; }
    }
}
