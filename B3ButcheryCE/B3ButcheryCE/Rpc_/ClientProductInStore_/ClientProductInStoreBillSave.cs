using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace B3ButcheryCE.Rpc_.ClientProductInStore_
{
    public class ClientProductInStoreBillSave : ClientBase
    {
        public long AccountingUnit_ID { get; set; }

        public long Department_ID { get; set; }

        public long Domain_ID { get; set; }

        public long User_ID { get; set; }

        public long? InStoreType_ID { get; set; }

        public string InStoreType_Name { get; set; }

        public DateTime CreateTime { get; set; }

        public string DeviceId { get; set; }

        public List<ClientProductInStoreDetail> Details = new List<ClientProductInStoreDetail>();

    }
}
