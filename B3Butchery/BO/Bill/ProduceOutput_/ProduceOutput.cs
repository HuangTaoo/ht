using System;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices;
using BWP.B3Frameworks.BO;
using Forks.EnterpriseServices.DomainObjects2;
using BWP.B3Frameworks;
using BWP.B3Butchery.Utils;
using TSingSoft.WebControls2;

namespace BWP.B3Butchery.BO
{
	[Serializable, DFClass, LogicName("产出单")]
	[OrganizationLimitedDmo("Department_ID", typeof(Department))]
	[DmoTypeID(B3FrameworksConsts.DmoTypeIDBases.B3Butchery, DmoTypeIDOffsets.ProduceOutput)]

	public class ProduceOutput : DepartmentWorkFlowBill
	{
    [DFDataKind(B3ButcheryDataSource.速冻库)]
    [DFExtProperty("DisplayField", "FrozenStore_Name")]
    [DFExtProperty(B3ButcheryDataSource.速冻库, B3ButcheryDataSource.速冻库)]
    [DFPrompt("速冻库")]
    public long? FrozenStore_ID { get; set; }


    [LogicName("速冻库")]
    [ReferenceTo(typeof(FrozenStore), "Name")]
    [Join("FrozenStore_ID", "ID")]
    public string FrozenStore_Name { get; set; }

    [LogicName("时间")]
		[DFNotEmpty]
		public DateTime? Time { get; set; }

		[LogicName("是否从手持机传入")]
		[DbColumn(DefaultValue = false)]
		public bool? IsHandsetSend { get; set; }

		[LogicName("计划号")]
		public long? PlanNumber_ID { get; set; }

		[LogicName("计划号")]
		[ReferenceTo(typeof(ProductPlan), "PlanNumber")]
		[Join("PlanNumber_ID", "ID")]
		public string PlanNumber_Name { get; set; }

		[LogicName("生产环节")]
		[DFExtProperty("WebControlType", DFEditControl.ChoiceBox)]
		[DFDataKind(B3ButcheryDataSource.生产环节)]
		[DFExtProperty("DisplayField", "ProductLinks_Name")]
		public long? ProductLinks_ID { get; set; }

		[ReferenceTo(typeof(ProductLinks), "Name")]
		[Join("ProductLinks_ID", "ID")]
		[LogicName("生产环节")]
		public string ProductLinks_Name { get; set; }

		[LogicName("生产环节模板")]
		public long? ProductLinkTemplate_ID { get; set; }

    [ReferenceTo(typeof(ProductLinkTemplate), "Name")]
    [Join("ProductLinkTemplate_ID", "ID")]
    public string ProductLinkTemplate_Name { get; set; }

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

		private ProduceOutput_DetailCollection mDetails = new ProduceOutput_DetailCollection();
		[OneToMany(typeof(ProduceOutput_Detail), "ID")]
		[Join("ID", "ProduceOutput_ID")]
		public ProduceOutput_DetailCollection Details
		{
			get { return mDetails; }
			set { mDetails = value; }
		}
	}
}
