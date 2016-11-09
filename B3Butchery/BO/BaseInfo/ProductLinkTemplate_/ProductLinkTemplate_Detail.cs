using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Frameworks.BO;
using BWP.B3Frameworks.BO.NamedValueTemplate;
using BWP.B3UnitedInfos.BO;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.Utils;

namespace BWP.B3Butchery.BO
{
	[DFClass, Serializable]
	[LogicName("生产环节模板_明细")]
	public class ProductLinkTemplate_Detail : Base
	{
		public long ProductLinkTemplate_ID { get; set; }

		[LogicName("存货")]
		public long Goods_ID { get; set; }

		[ReferenceTo(typeof(Goods), "Name")]
		[Join("Goods_ID", "ID")]
		[LogicName("存货名称")]
		public string Goods_Name { get; set; }

		[ReferenceTo(typeof(Goods), "Code")]
		[Join("Goods_ID", "ID")]
		[LogicName("存货编码")]
		public string Goods_Code { get; set; }

    [ReferenceTo(typeof(Goods), "Spec")]
    [Join("Goods_ID", "ID")]
    [LogicName("存货编码")]
    public string Goods_Spec { get; set; }

		[LogicName("主辅转换方向")]
		[ReferenceTo(typeof(Goods), "UnitConvertDirection")]
		[Join("Goods_ID", "ID")]
		public NamedValue<主辅转换方向>? Goods_UnitConvertDirection { get; set; }

		[LogicName("主辅换算主单位比例")]
		[ReferenceTo(typeof(Goods), "MainUnitRatio")]
		[Join("Goods_ID", "ID")]
		public Money<decimal>? Goods_MainUnitRatio { get; set; }

		[LogicName("主辅换算辅单位比例")]
		[ReferenceTo(typeof(Goods), "SecondUnitRatio")]
		[Join("Goods_ID", "ID")]
		public Money<decimal>? Goods_SecondUnitRatio { get; set; }

		[LogicName("备注")]
		public string Remark { get; set; }
	}

	[Serializable]
	public class ProductLinkTemplate_DetailCollection : DmoCollection<ProductLinkTemplate_Detail>
	{ }
}
