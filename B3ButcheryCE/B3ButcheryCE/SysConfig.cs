using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using BWP.Compact;

namespace B3ButcheryCE
{
    public class SysConfig : BaseConfig<SysConfig>
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string ServerUrl { get; set; }

        public long? AccountingUnit_ID { get; set; }

        public string AccountingUnit_Name { get; set; }


        public long? Department_ID { get; set; }

        public string Department_Name { get; set; }

        public int? Department_Depth { get; set; }

        public UsageMode? UsageModes { get; set; }

        public bool OpenNumberDialog { get; set; }

        public long Domain_ID { get; set; }

        public long User_ID { get; set; }

    }
}
