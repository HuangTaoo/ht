using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace B3ButcheryCE.Rpc_
{
    public class ClientFileGroupValuationBillSave
    {
        public long AccountingUnit_ID { get; set; }

        public long Department_ID { get; set; }

        public decimal? Number { get; set; }

        public long FileGroup_ID { get; set; }

        public string FileGroup_Name { get; set; }

        public long PieceItem_ID { get; set; }

        public string PieceItem_Name { get; set; }

        public long Domain_ID { get; set; }

        public long User_ID { get; set; }

        public bool IsHandsetSend { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
