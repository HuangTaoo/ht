using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using BWP.B3Frameworks.BO.NamedValueTemplate;
using BWP.Web.Layout;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using Forks.EnterpriseServices.SqlDoms;
using Forks.Utils;
using TSingSoft.WebPluginFramework;
using TSingSoft.WebControls2;
using BWP.Web.Utils;
using BWP.B3Butchery.Utils;
using BWP.B3UnitedInfos.BO;

namespace BWP.Web.Pages.B3Butchery.Bills.DailyProductReport_
{
	class DailyProductReportEdit : DepartmentWorkFlowBillEditPage<DailyProductReport, IDailyProductReportBL>
	{
		protected override void BuildBody(Control container)
		{
			var layoutManager = new LayoutManager("main", mDFInfo, mDFContainer);
			var planNumberBox = layoutManager.Add("PlanNumber_ID", InputCreator.DFChoiceBox(B3ButcheryDataSource.计划号, "PlanNumber_Name"));
			planNumberBox.OnBeforeDrop = "this.argument2=__DFContainer.getControl('Date').value";
			var config = new AutoLayoutConfig();
			config.Add("Date");
			config.Add("AccountingUnit_ID");
			config.Add("Department_ID");
			config.Add("PlanNumber_ID");
			config.Add("Remark");
			layoutManager.Config = config;
			container.Controls.Add(layoutManager.CreateLayout());

			var vPanel = container.EAdd(new VLayoutPanel());
			if (CanSave)
			{
				vPanel.Add(new TSButton("载入明细", delegate
				{
					LoadDetail();
				}), new VLayoutOption(HorizontalAlign.Left));
			}
			CreateInputDetailPanel(vPanel);
			CreateOutputDetailPanel(vPanel);
		}

		DFEditGrid inputGrid, outputGrid;
		private void CreateInputDetailPanel(VLayoutPanel vPanel)
		{
			vPanel.Add(new LiteralControl("<h2>投入明细：</h2>"), new VLayoutOption(HorizontalAlign.Left));
			var detailEditor = new DFCollectionEditor<DailyProductReport_InputDetail>(() => Dmo.InputDetails);
			detailEditor.AllowDeletionFunc = () => CanSave;
			detailEditor.CanDeleteFunc = (detail) => CanSave;
			detailEditor.IsEditableFunc = (field, detail) => CanSave;
			inputGrid = vPanel.Add(new DFEditGrid(detailEditor) { Width = Unit.Percentage(100) });
			inputGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Name"));
			inputGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Code"));
			inputGrid.Columns.Add(new DFEditGridColumn<DFTextBox>("Number"));
			inputGrid.Columns.Add(new DFEditGridColumn<DFTextBox>("Weight"));
			inputGrid.Columns.Add(new DFEditGridColumn<DFTextBox>("Remark"));
		}

		private void CreateOutputDetailPanel(VLayoutPanel vPanel)
		{
			vPanel.Add(new LiteralControl("<h2>产出明细：</h2>"), new VLayoutOption(HorizontalAlign.Left));
			var detailEditor = new DFCollectionEditor<DailyProductReport_OutputDetail>(() => Dmo.OutputDetails);
			detailEditor.AllowDeletionFunc = () => CanSave;
			detailEditor.CanDeleteFunc = (detail) => CanSave;
			detailEditor.IsEditableFunc = (field, detail) => CanSave;
			outputGrid = vPanel.Add(new DFEditGrid(detailEditor) { Width = Unit.Percentage(100) });
			outputGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Name"));
			outputGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Code"));
			outputGrid.Columns.Add(new DFEditGridColumn<DFTextBox>("Number"));
			outputGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("OutputPrecent"));
			outputGrid.Columns.Add(new DFEditGridColumn<DFTextBox>("Price"));
			outputGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Money"));
			outputGrid.Columns.Add(new DFEditGridColumn<DFTextBox>("Remark"));
		}

		private void LoadDetail()
		{
			GetFromUI();
			LoadInputDetail();
			LoadOutputDetail();
		}

		void LoadInputDetail()
		{
			Dmo.InputDetails.Clear();
			var main = new JoinAlias(typeof(ProduceInput));
			var detail = new JoinAlias(typeof(ProduceInput_Detail));
			var productLinksDetail = new JoinAlias(typeof(ProductLinks_InputDetail));
			var query = new DQueryDom(detail);
			query.From.AddJoin(JoinType.Left, new DQDmoSource(main), DQCondition.EQ(main, "ID", detail, "ProduceInput_ID"));
			query.From.AddJoin(JoinType.Left, new DQDmoSource(productLinksDetail), DQCondition.And(DQCondition.EQ(main, "ProductLinks_ID", productLinksDetail, "ProductLinks_ID"), DQCondition.EQ(detail, "Goods_ID", productLinksDetail, "Goods_ID")));
			query.Columns.Add(DQSelectColumn.Field("Goods_ID"));
			query.Columns.Add(DQSelectColumn.Field("Goods_Name"));
			query.Columns.Add(DQSelectColumn.Field("Goods_Code"));
			query.Columns.Add(DQSelectColumn.Sum("Number"));
			query.Columns.Add(DQSelectColumn.Sum("SecondNumber"));
			query.GroupBy.Expressions.Add(DQExpression.Field("Goods_ID"));
			query.GroupBy.Expressions.Add(DQExpression.Field("Goods_Name"));
			query.GroupBy.Expressions.Add(DQExpression.Field("Goods_Code"));
			query.Where.Conditions.Add(DQCondition.And(DQCondition.EQ(main, "Time", Dmo.Date), DQCondition.EQ(main, "AccountingUnit_ID", Dmo.AccountingUnit_ID), DQCondition.EQ(main, "Department_ID", Dmo.Department_ID), DQCondition.EQ(main, "PlanNumber_ID", Dmo.PlanNumber_ID), DQCondition.EQ(main, "BillState", 单据状态.已审核), DQCondition.EQ(productLinksDetail, "LivingBodyMark", true)));
			using (var session = Forks.EnterpriseServices.DomainObjects2.Dmo.NewSession())
			{
				using (var reader = session.ExecuteReader(query))
				{
					while (reader.Read())
					{
						var d = new DailyProductReport_InputDetail
						{
							Goods_ID = (long?)reader[0],
							Goods_Name = (string)reader[1],
							Goods_Code = (string)reader[2],
							Weight = ((Money<decimal>?)reader[3]).EToDecimal(),
							Number = ((Money<decimal>?)reader[4]).EToDecimal()
						};
						Dmo.InputDetails.Add(d);
					}
				}
			}
			inputGrid.DataBind();
		}

		void LoadOutputDetail()
		{
			Dmo.OutputDetails.Clear();
			var main = new JoinAlias(typeof(ProductInStore));
			var detail = new JoinAlias(typeof(ProductInStore_Detail));
			var referencePirceAlias = new JoinAlias(typeof(GoodsReferencePrice));
			var query = new DQueryDom(detail);
			query.From.AddJoin(JoinType.Left, new DQDmoSource(main), DQCondition.EQ(main, "ID", detail, "ProductInStore_ID"));
			query.From.AddJoin(JoinType.Left, new DQDmoSource(referencePirceAlias), DQCondition.EQ(detail, "Goods_ID", referencePirceAlias, "Goods_ID"));
			query.Columns.Add(DQSelectColumn.Field("Goods_ID"));
			query.Columns.Add(DQSelectColumn.Field("Goods_Name"));
			query.Columns.Add(DQSelectColumn.Field("Goods_Code"));
			query.Columns.Add(DQSelectColumn.Sum("Number"));
			query.Columns.Add(DQSelectColumn.Field("ReferencePrice", referencePirceAlias));
			query.GroupBy.Expressions.Add(DQExpression.Field("Goods_ID"));
			query.GroupBy.Expressions.Add(DQExpression.Field("Goods_Name"));
			query.GroupBy.Expressions.Add(DQExpression.Field("Goods_Code"));
			query.GroupBy.Expressions.Add(DQExpression.Field(referencePirceAlias, "ReferencePrice"));
			query.Where.Conditions.Add(DQCondition.And(DQCondition.EQ(main, "AccountingUnit_ID", Dmo.AccountingUnit_ID), DQCondition.EQ(main, "Department_ID", Dmo.Department_ID), DQCondition.EQ(detail, "ProductPlan_ID", Dmo.PlanNumber_ID), DQCondition.EQ(main, "BillState", 单据状态.已审核)));
			using (var session = Forks.EnterpriseServices.DomainObjects2.Dmo.NewSession())
			{
				using (var reader = session.ExecuteReader(query))
				{
					var inputSum = Dmo.InputDetails.Sum(x => x.Weight ?? 0);
					while (reader.Read())
					{
						var d = new DailyProductReport_OutputDetail
						{
							Goods_ID = (long?)reader[0],
							Goods_Name = (string)reader[1],
							Goods_Code = (string)reader[2],
							Number = ((Money<decimal>?)reader[3]).EToDecimal(),
							Price = ((decimal?)reader[4] ?? 0)
						};
						d.Money = d.Price * d.Number;
						if (inputSum != 0)
							d.OutputPrecent = d.Number / inputSum;
						Dmo.OutputDetails.Add(d);
					}
				}
			}
			outputGrid.DataBind();
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (!IsPostBack)
				DataBind();
		}

		public override void GetFromUI()
		{
			base.GetFromUI();
			inputGrid.GetFromUI();
			outputGrid.GetFromUI();
		}

		protected override void InitNewDmo(DailyProductReport dmo)
		{
			base.InitNewDmo(dmo);
			dmo.Date = DateTime.Today;
		}
	}
}
