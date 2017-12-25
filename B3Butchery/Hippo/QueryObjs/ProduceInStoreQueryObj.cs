using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Frameworks.BO.NamedValueTemplate;
using Forks.EnterpriseServices.JsonRpc;
using Forks.Utils;

namespace BWP.B3Butchery.Hippo.QueryObjs
{
	[RpcObject]
	public class ProductInStoreQueryObj
	{
		public long? ID { get; set; }

		public NamedValue<单据状态>? BillState { get; set; }

		public DateTime? MaxInStoreDate { get; set; }

		public DateTime? MinInStoreDate { get; set; }

		public long? AccountingUnit_ID { get; set; }

		public string AccountingUnit_Name { get; set; }

		public long? Department_ID { get; set; }

		public string Department_Name { get; set; }

		public long? Employee_ID { get; set; }

		public string Employee_Name { get; set; }

		public long? Store_ID { get; set; }

		public string Store_Name { get; set; }

		public long? InStoreType_ID { get; set; }
	  public string InStoreType_Name { get; set; }

    public long? ProductPlan_ID { get; set; }
		public string ProductPlan_Name { get; set; }



    public long? ProductInStoreTemplate_ID { get; set; }

    public string ProductInStoreTemplate_Name { get; set; }
	}
}
