using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using BWP.B3Butchery.BO;
using BWP.B3Butchery.BO.Views;
using BWP.B3Butchery.Utils;
using BWP.B3Frameworks;
using BWP.B3Frameworks.BO;
using BWP.B3Frameworks.BO.NamedValueTemplate;
using BWP.B3Frameworks.Utils;
using BWP.B3UnitedInfos.BO;
using BWP.Web.Layout;
using BWP.Web.Utils;
using BWP.Web.WebControls;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using Forks.EnterpriseServices.SqlDoms;
using TSingSoft.WebControls2;
using DataKind = BWP.B3Frameworks.B3FrameworksConsts.DataSources;

namespace BWP.Web.Pages.B3Butchery.Reports.DailyProductReportAnys_
{
	class DailyProductReportAnys : DFBrowseGridReportPage
	{
		DFInfo mainInfo = DFInfo.Get(typeof(DailyProductReportView));
		protected override string AccessRoleName
		{
			get { return "B3Butchery.报表.生产日报数据分析"; }
		}

		protected override string Caption
		{
			get { return "生产日报数据分析"; }
		}

		CheckBoxListWithReverseSelect checkbox;
		protected override void InitQueryPanel(QueryPanel queryPanel)
		{
			base.InitQueryPanel(queryPanel);
			var panel = queryPanel.CreateTab("显示字段");
			checkbox = new CheckBoxListWithReverseSelect() { RepeatColumns = 6, RepeatDirection = RepeatDirection.Horizontal };
			checkbox.Items.Add(new ListItem("存货名称", "Goods_Name"));
			checkbox.Items.Add(new ListItem("存货编码", "Goods_Code"));
			checkbox.Items.Add(new ListItem("重量", "Weight"));
			checkbox.Items.Add(new ListItem("数量", "Number"));
			checkbox.Items.Add(new ListItem("出成率", "OutputPrecent"));
			checkbox.Items.Add(new ListItem("单价", "Price"));
			checkbox.Items.Add(new ListItem("金额", "金额"));
			panel.EAdd(checkbox);
			queryPanel.ConditonPanel.EAdd(CreateDataRangePanel());
		}

		DateInput sd, ed;
		Control CreateDataRangePanel()
		{
			var hPanel = new HLayoutPanel();
			hPanel.Add(new SimpleLabel("日期"));
			sd = hPanel.Add(new DateInput());
			hPanel.Add(new LiteralControl("→"));
			ed = hPanel.Add(new DateInput());
			return hPanel;
		}

		protected override void AddQueryControls(VLayoutPanel vPanel)
		{
			var customPanel = new LayoutManager("Main", mainInfo, mQueryContainer);

			customPanel.Add("PlanNumber_ID", QueryCreator.DFChoiceBoxEnableMultiSelection(mainInfo.Fields["PlanNumber_ID"], mQueryContainer, "PlanNumber_ID", B3ButcheryDataSource.计划号));
			customPanel["PlanNumber_ID"].NotAutoAddToContainer = true;

			customPanel.Add("AccountingUnit_ID", QueryCreator.DFChoiceBoxEnableMultiSelection(mainInfo.Fields["AccountingUnit_ID"], mQueryContainer, "AccountingUnit_ID", DataKind.授权会计单位全部));
			customPanel["AccountingUnit_ID"].NotAutoAddToContainer = true;
			customPanel.CreateDefaultConfig(2).Expand = false;
			vPanel.Add(customPanel.CreateLayout());
		}

		protected override DQueryDom GetQueryDom()
		{
			var query = base.GetQueryDom();
			OrganizationUtil.AddOrganizationLimit<Department>(query, "Department_ID");
			foreach (ListItem field in checkbox.Items)
			{
				if (field.Selected)
				{
					if (field.Text == "金额")
						query.Columns.Add(DQSelectColumn.Create(DQExpression.Multiply(DQExpression.Field("Number"), DQExpression.Field("Price")), field.Text));
					else
						query.Columns.Add(DQSelectColumn.Field(field.Value));
				}
			}

			if (sd.Value.HasValue)
				query.Where.Conditions.Add(DQCondition.GreaterThanOrEqual("Date", sd.Value.Value));
			if (ed.Value.HasValue)
				query.Where.Conditions.Add(DQCondition.LessThanOrEqual("Date", ed.Value.Value));
			query.Where.Conditions.Add(DQCondition.EQ("Domain_ID", DomainContext.Current.ID));
			if (query.Columns.Count == 0)
				throw new Exception("至少选择一条显示列");
			return query;
		}
	}
}
