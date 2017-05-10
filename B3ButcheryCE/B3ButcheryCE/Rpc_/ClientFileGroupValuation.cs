using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace B3ButcheryCE.Rpc_
{
    public class ClientFileGroupValuation
    {
        public long ID { get; set; }

        public string Name { get; set; }

        public long Department_ID { get; set; }

        public string Department_Name { get; set; }

        public List<ClientFileGroupValuation_FileGroupDetail> FileGroupDetails = new List<ClientFileGroupValuation_FileGroupDetail>();

        public List<ClientFileGroupValuation_PieceItemDetail> PieceItemDetails = new List<ClientFileGroupValuation_PieceItemDetail>();

    }
}
