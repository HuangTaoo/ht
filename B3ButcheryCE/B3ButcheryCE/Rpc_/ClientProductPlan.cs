using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace B3ButcheryCE.Rpc_
{
    public class ClientProductPlan
    {
        public long ID { get; set; }

        public string PlanNumber { get; set; }

        public DateTime SyncDate { get; set; }

        public DateTime PlanDate { get; set; }
    }
}
