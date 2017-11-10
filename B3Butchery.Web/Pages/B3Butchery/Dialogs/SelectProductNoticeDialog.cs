using System;
using BWP.B3Butchery.BO;
using BWP.B3Frameworks;
using BWP.Web.Layout;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using Forks.Utils;
using TSingSoft.WebControls2;

namespace BWP.Web.Pages.B3Butchery.Dialogs {
  class SelectProductNoticeDialog : DmoMultiSelectDialog<ProductNotice, ProduceFinish_Detail> {
    private DFCheckBox _hideFinishedBill;

    protected override void CreateQuery(VLayoutPanel vPanel) {
      var layoutManager = new LayoutManager("", mDFInfo, mQueryContainer);

      var config = new AutoLayoutConfig { Cols = 8, DefaultLabelWidth = 4 };

      config.Add("ID");
      config.Add("CreateUser_Name");
      config.Add("CheckUser_Name");
      config.Add("AccountingUnit_ID");
      config.Add("Employee_ID");
      config.Add("Customer_ID");
      config.Add("Date");
      config.Add("ProductionUnit_ID");
      layoutManager.Config = config;

      var section = mPageLayoutManager.AddSection(B3FrameworksConsts.PageLayouts.QueryConditions, B3FrameworksConsts.PageLayouts.QueryConditions_DisplayName);
      section.ApplyLayout(layoutManager, config, mPageLayoutManager, mDFInfo as DFInfo);
      vPanel.Add(layoutManager.CreateLayout());
      _hideFinishedBill = new DFCheckBox();
      _hideFinishedBill.Text = "隐藏已转完工存货";
      _hideFinishedBill.Checked = true;
      vPanel.Add(_hideFinishedBill);
      base.CreateQuery(vPanel);
    }

    protected override void CreateQueryGridColumns(DFBrowseGrid grid) {
      grid.Columns.Add(new DFBrowseGridColumn("AccountingUnit_Name"));
      grid.Columns.Add(new DFBrowseGridColumn("Customer_Name"));
      grid.Columns.Add(new DFBrowseGridColumn("Goods_Name"));
      grid.Columns.Add(new DFBrowseGridColumn("Goods_MainUnit"));
      grid.Columns.Add(new DFBrowseGridColumn("Goods_SecondUnit"));
      grid.Columns.Add(new DFBrowseGridColumn("Number"));
      grid.Columns.Add(new DFBrowseGridColumn("SecondNumber"));

    }

    protected override DQueryDom GetQueryDom() {
      var dom = base.GetQueryDom();
      var detail = JoinAlias.Create("detail");
      var alias = dom.From.RootSource.Alias;

      dom.Columns.Add(DQSelectColumn.Field("ID", alias));
      dom.Columns.Add(DQSelectColumn.Create(DQExpression.Field(detail, "ID"), "DetailID"));
      dom.Columns.Add(DQSelectColumn.Field("AccountingUnit_Name"));
      dom.Columns.Add(DQSelectColumn.Field("Customer_Name"));
      dom.Columns.Add(DQSelectColumn.Field("Goods_Name", detail));
      dom.Columns.Add(DQSelectColumn.Field("Goods_MainUnit", detail));
      dom.Columns.Add(DQSelectColumn.Field("Goods_SecondUnit", detail));
      dom.Columns.Add(DQSelectColumn.Field("Goods_ID", detail));
      dom.Columns.Add(DQSelectColumn.Field("Number", detail));
      dom.Columns.Add(DQSelectColumn.Field("SecondNumber", detail));
      dom.Columns.Add(DQSelectColumn.Field("DoneNumber", detail));
      dom.EAddCheckedCondition(alias);
      dom.Where.Conditions.Add(DQCondition.EQ(alias, "Domain_ID", DomainContext.Current.ID));
      if (_hideFinishedBill.Checked) {
        dom.Where.Conditions.Add(DQCondition.GreaterThan(DQExpression.Field(detail, "Number"), DQExpression.IfNull(DQExpression.Field(detail, "DoneNumber"), DQExpression.Value(0))));
      }
      return dom;
    }


    protected override void SetResultFromDFDataRow(ProduceFinish_Detail dmo, DFDataRow row) {
      dmo.Goods_ID = (long)row["Goods_ID"];
      dmo.ProductNotice_ID = (long)row["ID"];
      dmo.ProductNotice_Detail_ID = (long)row["DetailID"];
      dmo.Number = (Money<Decimal>?)row["Number"] - ((Money<Decimal>?)row["DoneNumber"] ?? 0);
      dmo.SecondNumber = (Money<Decimal>?)row["SecondNumber"];

    }

  }
}
