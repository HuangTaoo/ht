using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Butchery.Utils;
using BWP.B3Frameworks;
using BWP.B3Frameworks.BO;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.Utils;
using TSingSoft.WebControls2;

namespace BWP.B3Butchery.BO
{
	[DFClass, Serializable, LogicName("生产环节模板")]
	[DFCPrompt("模板名称", Property = "Name")]
	public class ProductLinkTemplate : DomainBaseInfo
	{
		[LogicName("会计单位")]
		[DFExtProperty("WebControlType", DFEditControl.ChoiceBox)]
		[DFDataKind(B3FrameworksConsts.DataSources.授权会计单位)]
		[DFExtProperty("DisplayField", "AccountingUnit_Name")]
		[DFExtProperty(B3FrameworksConsts.DFExtProperties.QueryDataKind, B3FrameworksConsts.DataSources.授权会计单位全部)]
		[DFNotEmpty]
		public long? AccountingUnit_ID { get; set; }

		[ReferenceTo(typeof(AccountingUnit), "Name")]
		[Join("AccountingUnit_ID", "ID")]
		[LogicName("会计单位")]
		public string AccountingUnit_Name { get; set; }


		[DFDataKind(B3FrameworksConsts.DataSources.授权部门)]
		[LogicName("部门")]
		[DFExtProperty("WebControlType", DFEditControl.ChoiceBox)]
		[DFExtProperty("DisplayField", "Department_Name")]
		[DFExtProperty(B3FrameworksConsts.DFExtProperties.QueryDataKind, B3FrameworksConsts.DataSources.授权部门全部)]
		public long? Department_ID { get; set; }

		[Join("Department_ID", "ID")]
		[LogicName("部门")]
		[ReferenceTo(typeof(Department), "Name")]
		public string Department_Name { get; set; }

		[DFDataKind(B3ButcheryDataSource.生产环节)]
		[LogicName("生产环节")]
		[DFExtProperty("WebControlType", DFEditControl.ChoiceBox)]
		[DFExtProperty("DisplayField", "ProductLinks_Name")]
		[DFExtProperty(B3FrameworksConsts.DFExtProperties.QueryDataKind, B3ButcheryDataSource.生产环节全部)]
		public long? ProductLinks_ID { get; set; }

		[Join("ProductLinks_ID", "ID")]
		[LogicName("生产环节")]
		[ReferenceTo(typeof(ProductLinks), "Name")]
		public string ProductLinks_Name { get; set; }

		[LogicName("采集方式")]
		public NamedValue<采集方式>? CollectType { get; set; }

		[ReferenceTo(typeof(Department), "Depth")]
		[Join("Department_ID", "ID")]
		[LogicName("部门深度")]
		public int? Department_Depth { get; set; }

		[NonDmoProperty]
		public string Department_TreeName { get; set; }

		[ReferenceTo(typeof(Department), "TreeDeep1ID")]
		[Join("Department_ID", "ID")]
		public long? Department_TreeDeep1ID { get; set; }

		[ReferenceTo(typeof(Department), "TreeDeep2ID")]
		[Join("Department_ID", "ID")]
		public long? Department_TreeDeep2ID { get; set; }

		[ReferenceTo(typeof(Department), "TreeDeep3ID")]
		[Join("Department_ID", "ID")]
		public long? Department_TreeDeep3ID { get; set; }

		[ReferenceTo(typeof(Department), "TreeDeep4ID")]
		[Join("Department_ID", "ID")]
		public long? Department_TreeDeep4ID { get; set; }

		[ReferenceTo(typeof(Department), "TreeDeep5ID")]
		[Join("Department_ID", "ID")]
		public long? Department_TreeDeep5ID { get; set; }

		[ReferenceTo(typeof(Department), "TreeDeep6ID")]
		[Join("Department_ID", "ID")]
		public long? Department_TreeDeep6ID { get; set; }

		[ReferenceTo(typeof(Department), "TreeDeep7ID")]
		[Join("Department_ID", "ID")]
		public long? Department_TreeDeep7ID { get; set; }

		[ReferenceTo(typeof(Department), "TreeDeep8ID")]
		[Join("Department_ID", "ID")]
		public long? Department_TreeDeep8ID { get; set; }

		private ProductLinkTemplate_DetailCollection mDetail = new ProductLinkTemplate_DetailCollection();
		[OneToMany(typeof(ProductLinkTemplate_Detail), "ID")]
		[Join("ID", "ProductLinkTemplate_ID")]
		public ProductLinkTemplate_DetailCollection Details
		{
			get { return mDetail; }
			set { mDetail = value; }
		}
	}
}
