using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Frameworks.BO;
using BWP.B3UnitedInfos.BO;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;

namespace BWP.B3Butchery.BO
{
	[DFClass, Serializable]
	[LogicName("生产日报_投入明细")]
	public class DailyProductReport_InputDetail : Base
	{
		public long DailyProductReport_ID { get; set; }

		[DFNotEmpty]
		[LogicName("存货")]
		public long? Goods_ID { get; set; }

		[LogicName("存货名称")]
		[ReferenceTo(typeof(Goods), "Name")]
		[Join("Goods_ID", "ID")]
		public string Goods_Name { get; set; }

		[LogicName("存货编码")]
		[ReferenceTo(typeof(Goods), "Code")]
		[Join("Goods_ID", "ID")]
		public string Goods_Code { get; set; }

		[LogicName("数量")]
		public decimal? Number { get; set; }

		[LogicName("重量")]
		public decimal? Weight { get; set; }

		[LogicName("备注")]
		public string Remark { get; set; }
	}

	[Serializable]
	public class DailyProductReport_InputDetailCollection : DmoCollection<DailyProductReport_InputDetail>
	{ }
}
