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
using TSingSoft.WebControls2;

namespace BWP.B3Butchery.BO
{
	[DFClass, Serializable, LogicName("成品入库模板")]
	[DFCPrompt("模板名称", Property = "Name")]
	public class ProductInStoreTemplate : DomainBaseInfo
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

		[LogicName("入库类型")]
		[DFExtProperty("WebControlType", DFEditControl.ChoiceBox)]
		[DFDataKind(B3ButcheryDataSource.屠宰分割入库类型)]
		[DFExtProperty("DisplayField", "InStoreType_Name")]
		[DFExtProperty(B3FrameworksConsts.DFExtProperties.QueryDataKind, B3ButcheryDataSource.屠宰分割入库类型全部)]
		public long? InStoreType_ID { get; set; }

		[ReferenceTo(typeof(InStoreType), "Name")]
		[Join("InStoreType_ID", "ID")]
		[LogicName("入库类型")]
		public string InStoreType_Name { get; set; }

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

		[LogicName("经办人")]
		[DFDataKind(B3FrameworksConsts.DataSources.授权员工)]
		[DFExtProperty("WebControlType", DFEditControl.ChoiceBox)]
		[DFExtProperty("DisplayField", "Employee_Name")]
		[DFExtProperty(B3FrameworksConsts.DFExtProperties.QueryDataKind, B3FrameworksConsts.DataSources.授权员工全部)]
		public long? Employee_ID { get; set; }

		[ReferenceTo(typeof(Employee), "Name")]
		[Join("Employee_ID", "ID")]
		[LogicName("经办人")]
		public string Employee_Name { get; set; }

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

		private ProductInStoreTemplate_StoreDetailCollection mStoreDetail = new ProductInStoreTemplate_StoreDetailCollection();
		[OneToMany(typeof(ProductInStoreTemplate_StoreDetail),"ID")]
		[Join("ID", "ProductInStoreTemplate_ID")]
		public ProductInStoreTemplate_StoreDetailCollection StoreDetails
		{
			get { return mStoreDetail; }
			set { mStoreDetail = value; }
		}

		private ProductInStoreTemplate_GoodsDetailCollection mGoodsDetail = new ProductInStoreTemplate_GoodsDetailCollection();
		[OneToMany(typeof(ProductInStoreTemplate_GoodsDetail),"ID")]
		[Join("ID", "ProductInStoreTemplate_ID")]
		public ProductInStoreTemplate_GoodsDetailCollection GoodsDetails
		{
			get { return mGoodsDetail; }
			set { mGoodsDetail = value; }
		}
	}
}
