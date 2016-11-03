using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Butchery.Utils;
using BWP.B3Frameworks.BO.NamedValueTemplate;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.JsonRpc;
using Forks.Utils;

namespace BWP.B3Butchery.Hippo.QueryObjs
{
	[RpcObject]
	public class ProduceInputQueryObj
	{
		public long? ID { get; set; }

		public NamedValue<单据状态>? BillState { get; set; }

		public DateTime? MaxTime { get; set; }

		public DateTime? MinTime { get; set; }

		public long? AccountingUnit_ID { get; set; }

		public string AccountingUnit_Name { get; set; }

		public long? Department_ID { get; set; }

		public string Department_Name { get; set; }

		public long? Employee_ID { get; set; }

		public string Employee_Name { get; set; }

		public long? PlanNumber_ID { get; set; }

		public string PlanNumber_Name { get; set; }

		public long? ProductLinks_ID { get; set; }

		public string ProductLinks_Name { get; set; }

    [LogicName("投入类型")]
    public NamedValue<投入类型>? InputType { get; set; }
	}
}
