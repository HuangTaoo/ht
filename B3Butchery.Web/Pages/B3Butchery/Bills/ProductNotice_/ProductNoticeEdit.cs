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
using BWP.B3UnitedInfos.Utils;
using BWP.B3Frameworks;
using BWP.B3Butchery.Utils;
using BWP.B3Butchery;

namespace BWP.Web.Pages.B3Butchery.Bills.ProductNotice_ {
  public class ProductNoticeEdit : DepartmentWorkFlowBillEditPage<ProductNotice, IProductNoticeBL> {
    private DFEditGrid _detailGrid;
    B3ButcheryConfig butcheryConfig = new B3ButcheryConfig();

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
      var toobar = new HLayoutPanel();
      vPanel.Add(toobar, new VLayoutOption(HorizontalAlign.Left));
      
      if (CanSave) {
        AddToolsPanel(toobar);
      }

      AddCopyAndPaste(toobar);
      var editor= AddGridByOrderByID();
      editor.AllowDeletionFunc = () => CanSave;

      editor.CanDeleteFunc = detail => true;
      editor.IsEditableFunc = (field, detail) => CanSave;

      _detailGrid = new DFEditGrid(editor);
      _detailGrid.DFGridSetEnabled = false;
      _detailGrid.Width = Unit.Percentage(100);
      mDFContainer.AddNonDFControl(_detailGrid, "$detailGrid");
      _detailGrid.NextRowOnEnter = true;
      _detailGrid.LastRowOnDown = "__DFContainer.getControl('$SelectGoods').behind.focus();";
      _detailGrid.ShowLineNo = true;
      _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Code"));
      _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Name"));
      AddGridColumnsByBrandItem_ID(_detailGrid);
      foreach (var ioc in TypeIOCCenter.GetIOCList<ProductNoticeEdit.BeforeDetailGridApplyLayout>(GetType()))
      {
          ioc.Invoke(_detailGrid);
      }
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
      if (GlobalFlags.get(B3ButcheryFlags.IsYongDa))
      {
        var cargoSpaceColumn = _detailGrid.Columns.EAdd(new DFEditGridColumn<DFChoiceBox>("SaleZone_ID"));
        cargoSpaceColumn.InitEditControl += (sender, e) =>
        {
          e.Control.EnableTopItem = true;
          e.Control.EnableInputArgument = true;
          e.Control.DataKind = B3ButcheryDataSource.销售地区全部;
          e.Control.DFDisplayField = "SaleZone_Name";
          e.Control.Width = Unit.Pixel(160);
        };

      }
      AddProductNoticeDetailGrid(_detailGrid);

      _detailGrid.ValueColumns.Add("Goods_ID");
      _detailGrid.ValueColumns.Add("Goods_UnitConvertDirection");
      _detailGrid.ValueColumns.Add("Goods_MainUnitRatio");
      _detailGrid.ValueColumns.Add("Goods_SecondUnitRatio");

      var section = mPageLayoutManager.AddSection("DetaiColumns", "明细列");
      titlePanel.SetPageLayoutSetting(mPageLayoutManager, section.Name);
      section.SetRequired("Number" );
      section.ApplyLayout(_detailGrid, mPageLayoutManager, DFInfo.Get(typeof(ProductNotice_Detail)));

      vPanel.Add(_detailGrid);

      var scriptManager = new GoodsEditGridScriptManager(_detailGrid, "Number" );
      titlePanel.Controls.Add(scriptManager);

    }

    protected virtual DFCollectionEditor<ProductNotice_Detail> AddGridByOrderByID()
    {
       return new DFCollectionEditor<ProductNotice_Detail>(() => Dmo.Details);
    }

    protected virtual void AddGridColumnsByBrandItem_ID(DFEditGrid _detailGrid)
    {
    }

    protected virtual void AddProductNoticeDetailGrid(DFEditGrid _detailGrid)
    {
    }

    private void AddCopyAndPaste(HLayoutPanel toobar)
    {
      toobar.Add(new TSButton("复制", delegate
      {
        GoodsDetailSummaryClipboardUtil.Copy(Dmo.Details.Select(item => (GoodsDetailSummaryBase)item).ToList());
        AspUtil.Alert(this, "复制成功！");
      }));

      if (CanSave)
      {
        toobar.Add(new TSButton("粘贴", delegate
        {
          var list = GoodsDetailSummaryClipboardUtil.Paste<ProductNotice_Detail>();
          foreach (var detail in list)
          {
            DmoUtil.RefreshDependency(detail, "Goods_ID");
            Dmo.Details.Add(detail);
          }
          _detailGrid.DataBind();
        }));
      }
    }

    public override void AppToUI() {
      base.AppToUI();
      _detailGrid.DataBind();
    }

    public override void GetFromUI() {
      base.GetFromUI();
      _detailGrid.GetFromUI();
    }
    protected ProductNotice_Detail last = null;
    protected virtual void AddToolsPanel(HLayoutPanel toobar)
    {      
      toobar.Add(new SimpleLabel("选择存货"));
      var goodsSelect = new ChoiceBox(B3UnitedInfosConsts.DataSources.存货) {
        Width = Unit.Pixel(120),
        AutoPostBack = true,
        EnableMultiSelection = true,
        EnableInputArgument = true
      };
      mDFContainer.AddNonDFControl(goodsSelect, "$SelectGoods");
      toobar.Add(goodsSelect);
      goodsSelect.SelectedValueChanged += (sender, e) => {
        _detailGrid.GetFromUI();
        last = Dmo.Details.LastOrDefault();
        foreach (var sGoodsID in goodsSelect.GetValues()) {
          var goods = GoodsBL.Instance.Load(Convert.ToInt64(sGoodsID));
          var detail = new ProductNotice_Detail();
          detail.Goods_ID = goods.ID;
          DmoUtil.RefreshDependency(detail, "Goods_ID");
          AddBrandItem(detail);
          Dmo.Details.Add(detail);
        }
        goodsSelect.DisplayValue = string.Empty;
        _detailGrid.DataBind();
        var script = B3ButcheryWebUtil.SetCursorPositionScript(butcheryConfig.ProductNoticeCursorField, "$detailGrid", Dmo.Details.Count, _detailGrid.PageSize);
        if (!string.IsNullOrEmpty(script))
          Page.ClientScript.RegisterStartupScript(GetType(), "Startup", script, true);
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

    protected virtual void AddBrandItem(ProductNotice_Detail detail) { }

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


    public interface BeforeDetailGridApplyLayout
    {
        void Invoke(DFEditGrid grid);
    }
  }
}
