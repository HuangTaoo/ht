using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using BWP.B3Butchery.Utils;
using BWP.B3Frameworks.Utils;
using BWP.Web.CustomPageLayout;
using BWP.Web.Layout;
using BWP.Web.Pages.B3Butchery.Dialogs;
using BWP.Web.Utils;
using BWP.Web.WebControls;
using Forks.EnterpriseServices.DataForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using TSingSoft.WebControls2;

namespace BWP.Web.Pages.B3Butchery.Bills.FrozenInStore_
{
  class FrozenInStoreEdit : DepartmentWorkFlowBillEditPage<FrozenInStore, IFrozenInStoreBL>
  {
    private DFEditGrid _detailGrid;
    protected override void BuildBody(Control control) {
      base.BuildBody(control);
      AddDetails(control.EAdd(new TitlePanel("入库清单", "入库清单")));
    }
    protected override void BuildBasePropertiesEditor(TitlePanel titlePanel, PageLayoutSection pageLayoutSection) {
      var layoutManager = new LayoutManager("", mDFInfo, mDFContainer);
      var config = new AutoLayoutConfig();
      layoutManager.Config = config;
      config.Add("AccountingUnit_ID");
      config.Add("Department_ID");
      config.Add("Employee_ID");
      config.Add("Date");
      config.Add("Store_ID");
      config.Add("OtherInStoreType_ID");
      config.Add("ProductionPlan_ID");
      config.Add("Remark");

      pageLayoutSection.SetRequired("AccountingUnit_ID");
      pageLayoutSection.ApplyLayout(layoutManager, config, mPageLayoutManager, mDFInfo);

      titlePanel.Controls.Add(layoutManager.CreateLayout());
    }

    private void AddDetails(TitlePanel titlePanel) {
      var vPanel = titlePanel.EAdd(new VLayoutPanel());
        if (CanSave)
        {
          var hPanel = vPanel.Add(new HLayoutPanel(), new VLayoutOption(HorizontalAlign.Left));
        hPanel.Add(new SimpleLabel("选择存货"));
        var selectGoods = hPanel.Add(new ChoiceBox(B3ButcheryDataSource.存货带编号) { Width = Unit.Pixel(130), EnableMultiSelection = true, EnableInputArgument = true, AutoPostBack = true });
        selectGoods.SelectedValueChanged += delegate
        {
          _detailGrid.GetFromUI();
          if (!selectGoods.IsEmpty)
          {
            foreach (var item in selectGoods.GetValues())
            {
              var d = new FrozenInStore_Detail() { Goods_ID = long.Parse(item) };
              DmoUtil.RefreshDependency(d, "Goods_ID");
              Dmo.Details.Add(d);
            }
          }
          selectGoods.Clear();
          _detailGrid.DataBind();
        };

        var addGoodsbt = hPanel.Add(new DialogButton
        {
          Text = "选择存货",
        });
        addGoodsbt.Url = "/B3Butchery/Dialogs/SelectGoodsDialog.aspx";
        addGoodsbt.Click += delegate
        {
          _detailGrid.GetFromUI();
          var details = DialogUtil.GetCachedObj<TemGoodsDetail>(this);
          foreach (var temGoodsDetail in details)
          {
            var detail = new FrozenInStore_Detail();
            detail.Goods_ID = temGoodsDetail.Goods_ID;
            DmoUtil.RefreshDependency(detail, "Goods_ID");         
            Dmo.Details.Add(detail);
          }
          _detailGrid.DataBind();
        };
      }

      var editor = new DFCollectionEditor<FrozenInStore_Detail>(() => Dmo.Details);
      editor.AllowDeletionFunc = () => CanSave;

      editor.CanDeleteFunc = detail => true;
      editor.IsEditableFunc = (field, detail) => CanSave;

      _detailGrid = new DFEditGrid(editor);
      _detailGrid.DFGridSetEnabled = false;
      _detailGrid.Width = Unit.Percentage(100);

      _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Code"));
      _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Name"));
      _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Spec"));
      _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_MainUnit"));
      _detailGrid.Columns.Add(new DFEditGridColumn<DFTextBox>("Number"));
      _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_SecondUnit"));
      _detailGrid.Columns.Add(new DFEditGridColumn<DFTextBox>("SecondNumber"));
      _detailGrid.Columns.Add(new DFEditGridColumn<DFTextBox>("Price"));
      _detailGrid.Columns.Add(new DFEditGridColumn<DFTextBox>("Money"));
      _detailGrid.Columns.Add(new DFEditGridColumn<DFTextBox>("Remark"));

      _detailGrid.ValueColumns.Add("Goods_ID");
      _detailGrid.ValueColumns.Add("Goods_UnitConvertDirection");
      _detailGrid.ValueColumns.Add("Goods_MainUnitRatio");
      _detailGrid.ValueColumns.Add("Goods_SecondUnitRatio");
      new NumberSecondNumberConvertRowMangerWithMoneyChanged(_detailGrid);

      mDFContainer.AddNonDFControl(_detailGrid, "$DetailGrid");

      var section = mPageLayoutManager.AddSection("DetaiColumns", "明细列");
      titlePanel.SetPageLayoutSetting(mPageLayoutManager, section.Name);

      section.ApplyLayout(_detailGrid, mPageLayoutManager, DFInfo.Get(typeof(FrozenInStore_Detail)));

      vPanel.Add(_detailGrid);

    }

    public override void AppToUI() {
      base.AppToUI();
      _detailGrid.DataBind();
    }

    public override void GetFromUI() {
      base.GetFromUI();
      _detailGrid.GetFromUI();
    }

  }
}
