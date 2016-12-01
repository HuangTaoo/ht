using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using BWP.B3Frameworks.Utils;
using BWP.B3UnitedInfos;
using BWP.B3UnitedInfos.BL;
using BWP.B3UnitedInfos.BO;
using BWP.Web.CustomPageLayout;
using BWP.Web.Layout;
using BWP.Web.Utils;
using BWP.Web.WebControls;
using Forks.EnterpriseServices.DataForm;
using Forks.Utils.Collections;
using TSingSoft.WebControls2;
using TSingSoft.WebPluginFramework;

namespace BWP.Web.Pages.B3Butchery.Bills.ProductNotice_ {
  class ProductNoticeEdit : DepartmentWorkFlowBillEditPage<ProductNotice, IProductNoticeBL> {
    private DFEditGrid _detailGrid;
    protected override void BuildBody(Control control) {
      base.BuildBody(control);
      AddDetails(control.EAdd(new TitlePanel("单据明细", "单据明细")));
    }

    protected override void BuildBasePropertiesEditor(TitlePanel titlePanel, PageLayoutSection pageLayoutSection) {
      var layoutManager = new LayoutManager("", mDFInfo, mDFContainer);
      layoutManager.Add("CustomerAddress", new DFTextBox(mDFInfo.Fields["CustomerAddress"]) );
      var config = new AutoLayoutConfig();
      layoutManager.Config = config;
      config.Add("AccountingUnit_ID");      
      config.Add("Date");
      config.Add("Department_ID");
      config.Add("Customer_ID");
      config.Add("Employee_ID"); 
      config.Add("ProductionUnit_ID");
        config.Add("CustomerAddress");
      config.Add("Remark");

      pageLayoutSection.SetRequired("AccountingUnit_ID" );
      pageLayoutSection.ApplyLayout(layoutManager, config, mPageLayoutManager, mDFInfo);

      titlePanel.Controls.Add(layoutManager.CreateLayout());
    }

    private void AddDetails(TitlePanel titlePanel) {
      var vPanel = titlePanel.EAdd(new VLayoutPanel());
      if (CanSave) {
        AddToolsPanel(vPanel);
      }

      var editor = new DFCollectionEditor<ProductNotice_Detail>(() => Dmo.Details);
      editor.AllowDeletionFunc = () => CanSave;

      editor.CanDeleteFunc = detail => true;
      editor.IsEditableFunc = (field, detail) => CanSave;

      _detailGrid = new DFEditGrid(editor);
      _detailGrid.DFGridSetEnabled = false;
      _detailGrid.Width = Unit.Percentage(100);

      _detailGrid.ShowLineNo = true;
      _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Code"));
      _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Name"));
      _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Spec"));
       
      _detailGrid.Columns.EAdd(new DFEditGridColumn<DFTextBox>("Number")).SumMode = SumMode.Sum; 
      _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_MainUnit"));
      _detailGrid.Columns.EAdd(new DFEditGridColumn<DFTextBox>("SecondNumber")).SumMode = SumMode.Sum;
      _detailGrid.Columns.EAdd(new DFEditGridColumn<DFValueLabel>("Goods_SecondUnit"));
      _detailGrid.Columns.EAdd(new DFEditGridColumn<DFTextBox>("ProduceRequest")).InitEditControl += (sender, e) => { e.Control.Width = 150;};
      _detailGrid.Columns.EAdd(new DFEditGridColumn<DFDateInput>("ProduceDate"));
      _detailGrid.Columns.EAdd(new DFEditGridColumn<DFDateInput>("DeliveryDate"));
      _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("DmoTypeID"));
        _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("DmoID"));
      _detailGrid.Columns.Add(new DFEditGridColumn("Remark"));
      _detailGrid.ValueColumns.Add("Goods_ID");
      _detailGrid.ValueColumns.Add("Goods_UnitConvertDirection");
      _detailGrid.ValueColumns.Add("Goods_MainUnitRatio");
      _detailGrid.ValueColumns.Add("Goods_SecondUnitRatio");
      mDFContainer.AddNonDFControl(_detailGrid, "$detailGrid");


      var section = mPageLayoutManager.AddSection("DetaiColumns", "明细列");
      titlePanel.SetPageLayoutSetting(mPageLayoutManager, section.Name);
      section.SetRequired("Number" );
      section.ApplyLayout(_detailGrid, mPageLayoutManager, DFInfo.Get(typeof(ProductNotice_Detail)));

      vPanel.Add(_detailGrid);

      var scriptManager = new GoodsEditGridScriptManager(_detailGrid, "Number" );
      titlePanel.Controls.Add(scriptManager);

    }

    public override void AppToUI() {
      base.AppToUI();
      _detailGrid.DataBind();
    }

    public override void GetFromUI() {
      base.GetFromUI();
      _detailGrid.GetFromUI();
    }

    private void AddToolsPanel(VLayoutPanel vPanel)
    {
      var toobar = new HLayoutPanel();
      vPanel.Add(toobar, new VLayoutOption(HorizontalAlign.Left));

      toobar.Add(new SimpleLabel("选择存货"));
      var goodsSelect = new ChoiceBox(B3UnitedInfosConsts.DataSources.存货) {
        Width = Unit.Pixel(120),
        AutoPostBack = true,
        EnableMultiSelection = true,
        EnableInputArgument = true
      };
      toobar.Add(goodsSelect);
      goodsSelect.SelectedValueChanged += (sender, e) => {
        _detailGrid.GetFromUI();
        foreach (var sGoodsID in goodsSelect.GetValues()) {
          var goods = GoodsBL.Instance.Load(Convert.ToInt64(sGoodsID));
          var detail = new ProductNotice_Detail();
          detail.Goods_ID = goods.ID;
          DmoUtil.RefreshDependency(detail, "Goods_ID");
          Dmo.Details.Add(detail);
        }
        goodsSelect.DisplayValue = string.Empty;
        _detailGrid.DataBind();
      };

      var quickSelctButton = new DialogButton { Url = "~/B3UnitedInfos/Dialogs/QucicklySelectGoodsDetailsDialog.aspx", Text = "快速选择" };
      quickSelctButton.Click += delegate {
        ReceiveSelectedGoodsDetailDialog();
      };
      toobar.Add(quickSelctButton);


      var dialogButton = new DialogButton { Url = "~/B3UnitedInfos/Dialogs/SelectGoodsDetailDialog.aspx", Text = "查询存货" };
      toobar.Add(dialogButton);

      dialogButton.Click += delegate {
        ReceiveSelectedGoodsDetailDialog();

      };
        toobar.Add(new TSButton("载入预报")).Click += delegate
        {
            GetFromUI();
            Dmo.Details.Clear();
            mBL.LoadPredictDetail(Dmo);
            _detailGrid.DataBind();
            //AspUtil.Alert(this, "载入预报成功");
        };
    }

    private void ReceiveSelectedGoodsDetailDialog() {
      var selectedList = DialogUtil.GetCachedObj<SelectedGoodsDetail>(this);

      selectedList.Select(item => new ProductNotice_Detail  {
        Goods_ID = item.Goods_ID,
        Number = item.Number,
        SecondNumber = item.SecondNumber
      }).ToList() 
        .EEnumerate(detail => DmoUtil.RefreshDependency(detail, "Goods_ID"))
        .EAddToCollection(Dmo.Details);
      _detailGrid.DataBind();
    }

  }
}
