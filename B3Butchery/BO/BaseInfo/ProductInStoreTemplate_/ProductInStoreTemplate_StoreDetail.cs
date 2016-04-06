using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Frameworks.BO;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;

namespace BWP.B3Butchery.BO
{
	[Serializable, DFClass, LogicName("成品入库模板_仓库明细")]
	public class ProductInStoreTemplate_StoreDetail : Base
	{
		public long ProductInStoreTemplate_ID { get; set; }

		[LogicName("仓库")]
		public long Store_ID { get; set; }

		[ReferenceTo(typeof(Store), "Name")]
		[Join("Store_ID", "ID")]
		[LogicName("仓库名称")]
		public string Store_Name { get; set; }

		[ReferenceTo(typeof(Store), "Code")]
		[Join("Store_ID", "ID")]
		[LogicName("仓库编码")]
		public string Store_Code { get; set; }

		[LogicName("备注")]
		public string Remark { get; set; }
	}

	[Serializable]
	public class ProductInStoreTemplate_StoreDetailCollection : DmoCollection<ProductInStoreTemplate_StoreDetail>
	{ }
}
