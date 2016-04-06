using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using TSingSoft.WebControls2;
using TSingSoft.WebPluginFramework;

namespace BWP.Web.Pages.B3Butchery.Bills.DailyProductReport_
{
	class DailyProductReportList : DomainBillListPage<DailyProductReport, IDailyProductReportBL>
	{
		protected override void AddQueryControls(VLayoutPanel vPanel)
		{
			vPanel.Add(CreateDefaultBillQueryControls((panel, config) =>
			{
				config.AddAfter("Date", "ID");
				config.AddAfter("AccountingUnit_ID", "Date");
				config.AddAfter("Department_ID", "AccountingUnit_ID");
				config.AddAfter("PlanNumber_ID", "Department_ID");
			}));
		}

		protected override void AddDFBrowseGridColumn(DFBrowseGrid grid, string field)
		{
			base.AddDFBrowseGridColumn(grid, field);
			if (field == "BillState")
			{
				AddDFBrowseGridColumn(grid, "Date");
				AddDFBrowseGridColumn(grid, "AccountingUnit_Name");
				AddDFBrowseGridColumn(grid, "Department_Name");
				AddDFBrowseGridColumn(grid, "PlanNumber_Name");
			}
		}

		protected override void InitToolBar(HLayoutPanel toolbar)
		{
			base.InitToolBar(toolbar);
			if (User.IsInRole("B3Butchery.报表.生产日报数据分析"))
			{
				var dataAnysBtn = new TSButton() { Text = "数据分析", UseSubmitBehavior = false };
				dataAnysBtn.OnClientClick = string.Format("OpenUrlInTopTab('{0}','生产日报数据分析');return false;", WpfPageUrl.ToGlobal(AspUtil.AddTimeStampToUrl("~/B3Butchery/Reports/DailyProductReportAnys_/DailyProductReportAnys.aspx")));
				toolbar.Add(dataAnysBtn);
			}
		}
	}
}
