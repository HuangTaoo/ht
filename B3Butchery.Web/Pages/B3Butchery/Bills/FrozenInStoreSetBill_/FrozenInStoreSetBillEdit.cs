using System.Web.UI;
using System.Web.UI.WebControls;
using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using BWP.B3Butchery.Utils;
using BWP.B3UnitedInfos.BO;
using BWP.Web.CustomPageLayout;
using BWP.Web.Layout;
using BWP.Web.Pages.B3Butchery.Dialogs;
using BWP.Web.Utils;
using BWP.Web.WebControls;
using Forks.EnterpriseServices.DataForm;
using TSingSoft.WebControls2;

namespace BWP.Web.Pages.B3Butchery.Bills.FrozenInStoreSetBill_
{
  class FrozenInStoreSetBillEdit : DepartmentWorkFlowBillEditPage<FrozenInStoreSetBill, IFrozenInStoreSetBillBL>
  {
    private DFEditGrid _detailGrid;
    protected override void BuildBody(Control control)
    {
      base.BuildBody(control);
      AddDetails(control.EAdd(new TitlePanel("存货明细", "存货明细")));
    }
    protected override void BuildBasePropertiesEditor(TitlePanel titlePanel, PageLayoutSection pageLayoutSection)
    {
      var layoutManager = new LayoutManager("", mDFInfo, mDFContainer);
      var config = new AutoLayoutConfig();
      layoutManager.Config = config;
      config.Add("AccountingUnit_ID");
      config.Add("Department_ID");
      config.Add("Name");
      config.Add("Date");
      config.Add("WorkshopCategory_ID");
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
              var detail = new FrozenInStoreSetBill_Detail { Goods_ID = long.Parse(item) };
              var goods = WebBLUtil.GetSingleDmo<Goods>("ID", long.Parse(item));
              detail.Goods_MainUnit = goods.MainUnit;
              detail.Goods_Name = goods.Name;
              detail.Goods_Code = goods.Code;
              detail.Goods_MainUnit = goods.MainUnit;
              detail.Goods_SecondUnit = goods.SecondUnit;
              detail.Goods_Spec = goods.Spec;
              detail.GoodsProperty_ID = goods.GoodsProperty_ID;
              detail.GoodsProperty_Name = goods.GoodsProperty_Name;
              detail.GoodsPropertyCatalog_Name = goods.GoodsPropertyCatalog_Name;
              Dmo.Details.Add(detail);
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
            var detail = new FrozenInStoreSetBill_Detail { Goods_ID = temGoodsDetail.Goods_ID };
            //DmoUtil.RefreshDependency(detail, "Goods_ID");
            var goods = WebBLUtil.GetSingleDmo<Goods>("ID", temGoodsDetail.Goods_ID);
            detail.Goods_MainUnit = goods.MainUnit;
            detail.Goods_Name = goods.Name;
            detail.Goods_Code = goods.Code;
            detail.Goods_MainUnit = goods.MainUnit;
            detail.Goods_SecondUnit = goods.SecondUnit;
            detail.Goods_Spec = goods.Spec;
            detail.GoodsProperty_ID = goods.GoodsProperty_ID;
            detail.GoodsProperty_Name = goods.GoodsProperty_Name;
            detail.GoodsPropertyCatalog_Name = goods.GoodsPropertyCatalog_Name;
            Dmo.Details.Add(detail);
          }
          _detailGrid.DataBind();
        };
      }

      var editor = new DFCollectionEditor<FrozenInStoreSetBill_Detail>(() => Dmo.Details)
      {
        AllowDeletionFunc = () => CanSave,
        CanDeleteFunc = detail => true,
        IsEditableFunc = (field, detail) => CanSave
      };

      _detailGrid = new DFEditGrid(editor) { DFGridSetEnabled = false, Width = Unit.Percentage(100) };

      _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("GoodsPropertyCatalog_Name"));
      _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("GoodsProperty_Name"));
      _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Code"));
      _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Name"));
      _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Spec"));
      _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_MainUnit"));
      _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_SecondUnit"));

      _detailGrid.ValueColumns.Add("Goods_ID");
    

      var section = mPageLayoutManager.AddSection("DetaiColumns", "明细列");
      titlePanel.SetPageLayoutSetting(mPageLayoutManager, section.Name);

      section.ApplyLayout(_detailGrid, mPageLayoutManager, DFInfo.Get(typeof(FrozenInStoreSetBill_Detail)));

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
