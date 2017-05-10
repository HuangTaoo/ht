using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace B3ButcheryCE.Rpc_.ClientProductLink_
{
    public class ClientProductLink
    {
        public long ID { get; set; }

        public string Name { get; set; }

        public long Department_ID { get; set; }

        public string Department_Name { get; set; }

        public string CollectType { get; set; }

        public long? ProductLinks_ID { get; set; }
      
        public List<ClientProductLink_GoodsDetail> Details = new List<ClientProductLink_GoodsDetail>();
    }
}
