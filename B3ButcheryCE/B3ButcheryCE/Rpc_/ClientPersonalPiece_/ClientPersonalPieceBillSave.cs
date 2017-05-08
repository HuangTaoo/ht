using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace B3HRCE.Rpc_.ClientPersonalPiece_
{
    public class ClientPersonalPieceBillSave
    {
        public long AccountingUnit_ID { get; set; }

        public long Department_ID { get; set; }

        public long Domain_ID { get; set; }

        public long User_ID { get; set; }

        public DateTime CreateTime { get; set; }

        public List<ClientPersonalPieceDetail> Details = new List<ClientPersonalPieceDetail>();

    }
}
