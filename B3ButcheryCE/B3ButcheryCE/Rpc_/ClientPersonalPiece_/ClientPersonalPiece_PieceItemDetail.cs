using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace B3ButcheryCE.Rpc_.ClientPersonalPiece_
{
    public class ClientPersonalPiece_PieceItemDetail
    {
        public long PieceItem_ID { get; set; }

        public string PieceItem_Name { get; set; }

        public string PieceItem_Code { get; set; }

        public long? Job_ID { get; set; }

        public string Job_Name { get; set; }
    }
}
