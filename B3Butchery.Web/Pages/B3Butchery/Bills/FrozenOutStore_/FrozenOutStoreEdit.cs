using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using BWP.B3Butchery.Utils;
using BWP.B3Frameworks.Utils;
using BWP.B3UnitedInfos.BO;
using BWP.Web.CustomPageLayout;
using BWP.Web.Layout;
using BWP.Web.Pages.B3Butchery.Dialogs;
using BWP.Web.Utils;
using BWP.Web.WebControls;
using Forks.EnterpriseServices.DataForm;
using System.Web.UI;
using System.Web.UI.WebControls;
using BWP.Web.Pages.B3Butchery.WebControls;
using TSingSoft.WebControls2;
using Forks.Utils.Collections;

namespace BWP.Web.Pages.B3Butchery.Bills.FrozenOutStore_
{
  class FrozenOutStoreEdit : DepartmentWorkFlowBillEditPage<FrozenOutStore, IFrozenOutStoreBL>
  {
    private DFEditGrid _detailGrid;
    protected override void BuildBody(Control control)
    {
      base.BuildBody(control);
      AddDetails(control.EAdd(new TitlePanel("入库清单", "入库清单")));
    }
    protected override void BuildBasePropertiesEditor(TitlePanel titlePanel, PageLayoutSection pageLayoutSection)
    {
      var layoutManager = new LayoutManager("", mDFInfo, mDFContainer);

      layoutManager.Add("WorkBill_ID", new B3ButcheryDFIDLink("B3Butchery/Bills/WorkShopPackBill_/WorkShopPackBillEdit.aspx?ID=", mDFInfo.Fields["WorkBill_ID"], mDFInfo.Fields["WorkBill_ID"]));
      var config = new AutoLayoutConfig();
      layoutManager.Config = config;
      config.Add("AccountingUnit_ID");
      config.Add("Department_ID");
      config.Add("Employee_ID");
      config.Add("Date");
        config.Add("WorkBill_ID");
      config.Add("Remark");

      pageLayoutSection.SetRequired("AccountingUnit_ID");
      pageLayoutSection.ApplyLayout(layoutManager, config, mPageLayoutManager, mDFInfo);

      titlePanel.Controls.Add(layoutManager.CreateLayout());
    }

    private void AddDetails(TitlePanel titlePanel)
    {
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
              var d = new FrozenOutStore_Detail { Goods_ID = long.Parse(item) };
              var goods = WebBLUtil.GetSingleDmo<Goods>("ID", long.Parse(item));
              d.Goods_MainUnit = goods.MainUnit;
              d.Goods_Name = goods.Name;
              d.Goods_Code = goods.Code;
              d.Goods_UnitConvertDirection = goods.UnitConvertDirection;

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
            var detail = new FrozenOutStore_Detail { Goods_ID = temGoodsDetail.Goods_ID };
            //DmoUtil.RefreshDependency(detail, "Goods_ID");
            var goods = WebBLUtil.GetSingleDmo<Goods>("ID", temGoodsDetail.Goods_ID);
            detail.Goods_MainUnit = goods.MainUnit;
            detail.Goods_Name = goods.Name;
            detail.Goods_Code = goods.Code;
            detail.Goods_UnitConvertDirection = goods.UnitConvertDirection;
            Dmo.Details.Add(detail);
          }
          _detailGrid.DataBind();
        };
      }

      var editor = new DFCollectionEditor<FrozenOutStore_Detail>(() => Dmo.Details)
      {
        AllowDeletionFunc = () => CanSave,
        CanDeleteFunc = detail => true,
        IsEditableFunc = (field, detail) => CanSave
      };

      _detailGrid = new DFEditGrid(editor) { DFGridSetEnabled = false, Width = Unit.Percentage(100) };
        var productPlanCol = new DFEditGridColumn<DFChoiceBox>("ProductPlan_ID");
        productPlanCol.InitEditControl += delegate(object sender, InitEditControlEventArgs<DFChoiceBox> e)
        {
            e.Control.DataKind = B3ButcheryDataSource.计划号;
            e.Control.DFDisplayField = "ProductPlan_Name";
            e.Control.EnableInputArgument = true;
            e.Control.EnableTopItem = true;
            e.Control.Width = Unit.Pixel(120);
        };

        _detailGrid.Columns.EAdd(new DFEditGridColumn<DFValueLabel>("Goods_Name")).HeaderText = "半成品名称";
      _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Code"));
        _detailGrid.Columns.EAdd(new DFEditGridColumn<DFValueLabel>("Goods2_Name")).HeaderText = "成品名称";
      _detailGrid.Columns.EAdd(new DFEditGridColumn<DFTextBox>("Number")).HeaderText = "主数量";
      _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods2_MainUnit"));

      _detailGrid.Columns.Add(new DFEditGridColumn<DFTextBox>("Remark"));

   

      new Main_Second2_ConvertRatioRowManager(_detailGrid, "Number", "SecondNumber2");
      mDFContainer.AddNonDFControl(_detailGrid, "$DetailGrid");

      var section = mPageLayoutManager.AddSection("DetaiColumns", "明细列");
      titlePanel.SetPageLayoutSetting(mPageLayoutManager, section.Name);

      section.ApplyLayout(_detailGrid, mPageLayoutManager, DFInfo.Get(typeof(FrozenOutStore_Detail)));

      vPanel.Add(_detailGrid);

    }

    public override void AppToUI()
    {
      base.AppToUI();
      _detailGrid.DataBind();
    }

    public override void GetFromUI()
    {
      base.GetFromUI();
      _detailGrid.GetFromUI();
    }

  }
}
