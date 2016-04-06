using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Butchery.BO;
using BWP.B3Frameworks.BL;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.BusinessInterfaces;

namespace BWP.B3Butchery.BL
{
	[BusinessInterface(typeof(DailyProductReportBL))]
	[LogicName("生产日报")]
	public interface IDailyProductReportBL : IDepartmentWorkFlowBillBL<DailyProductReport>
	{ }

	public class DailyProductReportBL : DepartmentWorkFlowBillBL<DailyProductReport>, IDailyProductReportBL
	{
		protected override void beforeSave(DailyProductReport dmo)
		{
			var inputSum = dmo.InputDetails.Sum(x => x.Weight ?? 0);
			foreach (var detail in dmo.OutputDetails)
			{
				detail.Money = detail.Price * detail.Number;
				if (inputSum != 0)
					detail.OutputPrecent = detail.Number / inputSum;
			}
			base.beforeSave(dmo);
		}
	}
}
