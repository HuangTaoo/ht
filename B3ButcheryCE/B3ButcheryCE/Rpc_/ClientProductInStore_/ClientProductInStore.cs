using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace B3HRCE.Rpc_.ClientProductInStore_
{
    public class ClientProductInStore
    {
        public long ID { get; set; }

        public string Name { get; set; }

        public long Department_ID { get; set; }

        public string Department_Name { get; set; }

        public long? InStoreType_ID { get; set; }

        public string InStoreType_Name { get; set; }

        public List<ClientProductInStore_StoreDetail> StoreDetails = new List<ClientProductInStore_StoreDetail>();

        public List<ClientProductInStore_GoodsDetail> GoodsDetails = new List<ClientProductInStore_GoodsDetail>();
    }
}
