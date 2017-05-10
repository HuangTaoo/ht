using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace B3ButcheryCE.Rpc_.ClientPersonalPiece_
{
    public class ClientPersonalPiece
    {
        public long ID { get; set; }

        public string Name { get; set; }

        public long Department_ID { get; set; }

        public string Department_Name { get; set; }

        public List<ClientPersonalPiece_EmployeeDetail> EmployeeDetails = new List<ClientPersonalPiece_EmployeeDetail>();

        public List<ClientPersonalPiece_PieceItemDetail> PieceItemDetails = new List<ClientPersonalPiece_PieceItemDetail>();
    }
}
