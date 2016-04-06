using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Butchery.Utils;
using BWP.B3Frameworks;
using BWP.B3Frameworks.Attributes;
using BWP.B3Frameworks.BO;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using TSingSoft.WebControls2;

namespace BWP.B3Butchery.BO
{
	[Serializable, DFClass]
	[LogicName("生产日报")]
	[OrganizationLimitedDmo("Department_ID", typeof(Department))]
	[DmoTypeID(B3FrameworksConsts.DmoTypeIDBases.B3Butchery, DmoTypeIDOffsets.DailyProductReport)]
	public class DailyProductReport : DepartmentWorkFlowBill
	{
		[LogicName("日期")]
		[DFNotEmpty]
		public DateTime? Date { get; set; }

		[LogicName("计划号")]
		[DFExtProperty("WebControlType", DFEditControl.ChoiceBox)]
		[DFDataKind(B3ButcheryDataSource.计划号)]
		[DFExtProperty(B3FrameworksConsts.DFExtProperties.QueryDataKind, B3ButcheryDataSource.计划号)]
		[DFNotEmpty]
		[DFExtProperty("DisplayField", "PlanNumber_Name")]
		public long? PlanNumber_ID { get; set; }

		[LogicName("计划号")]
		[ReferenceTo(typeof(ProductPlan), "PlanNumber")]
		[Join("PlanNumber_ID", "ID")]
		public string PlanNumber_Name { get; set; }

		private DailyProductReport_InputDetailCollection mInputDetails = new DailyProductReport_InputDetailCollection();
		[OneToMany(typeof(DailyProductReport_InputDetail), "ID")]
		[Join("ID", "DailyProductReport_ID")]
		public DailyProductReport_InputDetailCollection InputDetails
		{
			get { return mInputDetails; }
			set { mInputDetails = value; }
		}

		private DailyProductReport_OutputDetailCollection mOutputDetails = new DailyProductReport_OutputDetailCollection();
		[OneToMany(typeof(DailyProductReport_OutputDetail), "ID")]
		[Join("ID", "DailyProductReport_ID")]
		public DailyProductReport_OutputDetailCollection OutputDetails
		{
			get { return mOutputDetails; }
			set { mOutputDetails = value; }
		}
	}
}
