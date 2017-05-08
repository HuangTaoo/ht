using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace B3HRCE.Rpc_.ClientPersonalPiece_
{
    public class ClientPersonalPieceDetail
    {
        public long? Employee_ID { get; set; }

        public long? PieceItem_ID { get; set; }

        public long? Job_ID { get; set; }

        public string Employee_Name { get; set; }

        public string PieceItem_Name { get; set; }

        public string Job_Name { get; set; }

        public decimal? Number { get; set; }
    }
}
