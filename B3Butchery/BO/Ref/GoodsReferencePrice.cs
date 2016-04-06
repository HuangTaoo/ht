using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using TSingSoft.WebPluginFramework;

namespace BWP.B3Butchery.BO
{
	[DFClass, Serializable, BOClass]
	[KeyField("Goods_ID", KeyGenType.assigned)]
	public class GoodsReferencePrice
	{
		[LogicName("存货")]
		public long Goods_ID { get; set; }

		[LogicName("参考单价")]
		public decimal? ReferencePrice { get; set; }

		[LogicName("备注")]
		public string Remark { get; set; }
	}
}
