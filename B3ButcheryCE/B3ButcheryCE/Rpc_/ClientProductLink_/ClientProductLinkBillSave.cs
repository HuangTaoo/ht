using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace B3HRCE.Rpc_.ClientProductLink_
{
    public class ClientProductLinkBillSave : ClientBase
    {
        public long AccountingUnit_ID { get; set; }

        public long Department_ID { get; set; }

        public long Domain_ID { get; set; }

        public long User_ID { get; set; }

        public DateTime CreateTime { get; set; }

        public string CollectType { get; set; }

        public long? ProductLinks_ID { get; set; }

        public List<ClientProductLinkDetail> Details = new List<ClientProductLinkDetail>();
    }
}
