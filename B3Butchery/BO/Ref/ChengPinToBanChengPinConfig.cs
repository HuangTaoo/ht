using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3UnitedInfos.BO;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using TSingSoft.WebPluginFramework;

namespace BWP.B3Butchery.BO
{
	[DFClass, Serializable, BOClass]
	[KeyField("Goods_ID", KeyGenType.assigned)]
	public class ChengPinToBanChengPinConfig
	{
		[LogicName("成品")]
		public long Goods_ID { get; set; }

		[LogicName("半成品")]
		public long? Goods2_ID { get; set; }

		[ReferenceTo(typeof(Goods), "Name")]
		[Join("Goods2_ID", "ID")]
		[LogicName("半成品名称")]
		public string Goods2_Name { get; set; }

		[ReferenceTo(typeof(Goods), "Code")]
		[Join("Goods2_ID", "ID")]
		[LogicName("半成品编码")]
		public string Goods2_Code { get; set; }

		[ReferenceTo(typeof(Goods), "Spec")]
		[Join("Goods2_ID", "ID")]
		[LogicName("半成品规格")]
		public string Goods2_Spec { get; set; }

		[LogicName("备注")]
		public string Remark { get; set; }
	}
}
