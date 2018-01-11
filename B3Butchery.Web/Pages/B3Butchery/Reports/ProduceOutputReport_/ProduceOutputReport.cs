using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using BWP.B3Butchery.BO;
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

namespace BWP.Web.Pages.B3Butchery.Reports.ProduceOutputReport_
{
	class ProduceOutputReport : DFBrowseGridReportPage
	{
		DFInfo mainInfo = DFInfo.Get(typeof(ProduceOutput));
		DFInfo detailInfo = DFInfo.Get(typeof(ProduceOutput_Detail));
		protected override string AccessRoleName
		{
			get { return "B3Butchery.报表.产出单分析"; }
		}

		protected override string Caption
		{
			get { return "产出单分析"; }
		}

		CheckBoxListWithReverseSelect checkbox;
		protected override void InitQueryPanel(QueryPanel queryPanel)
		{
			base.InitQueryPanel(queryPanel);
			var panel = queryPanel.CreateTab("显示字段");
			checkbox = new CheckBoxListWithReverseSelect() { RepeatColumns = 6, RepeatDirection = RepeatDirection.Horizontal };
      //显示字段包括：{日期}、{计划号}、{会计单位}、{部门}、{经办人}、{生产环节}、{存货名称}、{存货编码}、{规格}、{主数量}、{主单位}、{辅数量}、{辅单位}、{备注}
      checkbox.Items.Add(new ListItem("ID", "ID"));
      checkbox.Items.Add(new ListItem("日期", "Time"));
			checkbox.Items.Add(new ListItem("计划号", "PlanNumber_Name"));
			checkbox.Items.Add(new ListItem("会计单位", "AccountingUnit_Name"));
			checkbox.Items.Add(new ListItem("部门", "Department_Name"));
			checkbox.Items.Add(new ListItem("经办人", "Employee_Name"));
			checkbox.Items.Add(new ListItem("生产环节", "ProductLinks_Name"));
			checkbox.Items.Add(new ListItem("存货名称", "Name"));
			checkbox.Items.Add(new ListItem("存货编码", "Code"));
			checkbox.Items.Add(new ListItem("规格", "Spec"));
			checkbox.Items.Add(new ListItem("主数量", "Number"));
			checkbox.Items.Add(new ListItem("主单位", "MainUnit"));
			checkbox.Items.Add(new ListItem("辅数量", "SecondNumber"));
			checkbox.Items.Add(new ListItem("辅单位", "SecondUnit"));
            checkbox.Items.Add(new ListItem("创建人", "CreateUser_Name"));
			checkbox.Items.Add(new ListItem("备注", "Remark"));

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

		DFTextBox goodsName, goodsCode;
		protected override void AddQueryControls(VLayoutPanel vPanel)
		{
			var customPanel = new LayoutManager("Main", mainInfo, mQueryContainer);
			//查询条件包括：：{日期}、{计划号}、{会计单位}、{部门}、{经办人}、{生产环节}、{存货名称}、{存货编码}，

			customPanel.Add("PlanNumber_ID", QueryCreator.DFChoiceBoxEnableMultiSelection(mainInfo.Fields["PlanNumber_ID"], mQueryContainer, "PlanNumber_ID", B3ButcheryDataSource.计划号));
			customPanel["PlanNumber_ID"].NotAutoAddToContainer = true;

			customPanel.Add("AccountingUnit_ID", QueryCreator.DFChoiceBoxEnableMultiSelection(mainInfo.Fields["AccountingUnit_ID"], mQueryContainer, "AccountingUnit_ID", DataKind.授权会计单位全部));
			customPanel["AccountingUnit_ID"].NotAutoAddToContainer = true;

			customPanel.Add("Department_ID", QueryCreator.DFChoiceBoxEnableMultiSelection(mainInfo.Fields["Department_ID"], mQueryContainer, "Department_ID", DataKind.授权部门全部));
			customPanel["Department_ID"].NotAutoAddToContainer = true;

			customPanel.Add("Employee_ID", QueryCreator.DFChoiceBoxEnableMultiSelection(mainInfo.Fields["Employee_ID"], mQueryContainer, "Employee_ID", DataKind.授权员工全部));
			customPanel["Employee_ID"].NotAutoAddToContainer = true;

			customPanel.Add("ProductLinks_ID", QueryCreator.DFChoiceBoxEnableMultiSelection(mainInfo.Fields["ProductLinks_ID"], mQueryContainer, "ProductLinks_ID", B3ButcheryDataSource.生产环节全部));
			customPanel["ProductLinks_ID"].NotAutoAddToContainer = true;

			customPanel.Add("AccountingUnit_Name", new SimpleLabel("存货名称"), goodsName = QueryCreator.DFTextBox(detailInfo.Fields["Goods_Name"]));
			customPanel.Add("Department_Name", new SimpleLabel("存货编号"), goodsCode = QueryCreator.DFTextBox(detailInfo.Fields["Goods_Code"]));
			customPanel.CreateDefaultConfig(2).Expand = false;
			vPanel.Add(customPanel.CreateLayout());
		}

		string[] sumFileds = new string[] { "Number", "SecondNumber" };
		string[] goodsFields = new string[] { "Name", "Code", "Spec", "MainUnit", "SecondUnit" };
		protected override DQueryDom GetQueryDom()
		{
			var query = base.GetQueryDom();
			OrganizationUtil.AddOrganizationLimit<Department>(query, "Department_ID");
			var detail = JoinAlias.Create("detail");
			var goodsAlias = new JoinAlias(typeof(Goods));
			query.From.AddJoin(JoinType.Left, new DQDmoSource(goodsAlias), DQCondition.EQ(detail, "Goods_ID", goodsAlias, "ID"));
			foreach (ListItem field in checkbox.Items)
			{
				if (field.Selected)
				{
					if (sumFileds.Contains(field.Value))
					{
						query.Columns.Add(DQSelectColumn.Sum(detail, field.Value));
						SumColumnIndexs.Add(query.Columns.Count - 1);
					}
					else if (goodsFields.Contains(field.Value))
					{
						query.Columns.Add(DQSelectColumn.Field(field.Value, goodsAlias));
						query.GroupBy.Expressions.Add(DQExpression.Field(goodsAlias, field.Value));
					}
					else
					{
						query.Columns.Add(DQSelectColumn.Field(field.Value));
						query.GroupBy.Expressions.Add(DQExpression.Field(field.Value));
					}
				}
			}
			if (!string.IsNullOrEmpty(goodsName.Text))
				query.Where.Conditions.Add(DQCondition.Or(DQCondition.Like(goodsAlias, "Name", goodsName.Text), DQCondition.Like(goodsAlias, "Spell", goodsName.Text)));
			if (!string.IsNullOrEmpty(goodsCode.Text))
				query.Where.Conditions.Add(DQCondition.Like(goodsAlias, "Code", goodsCode.Text));
			if (sd.Value.HasValue)
				query.Where.Conditions.Add(DQCondition.GreaterThanOrEqual("Time", sd.Value.Value));
			if (ed.Value.HasValue)
				query.Where.Conditions.Add(DQCondition.LessThanOrEqual("Time", ed.Value.Value.AddDays(1).AddSeconds(-1)));
			query.Where.Conditions.Add(DQCondition.And(DQCondition.EQ("BillState", 单据状态.已审核), DQCondition.EQ("Domain_ID", DomainContext.Current.ID)));
			if (query.Columns.Count == 0)
				throw new Exception("至少选择一条显示列");
			return query;
		}
	}
}
