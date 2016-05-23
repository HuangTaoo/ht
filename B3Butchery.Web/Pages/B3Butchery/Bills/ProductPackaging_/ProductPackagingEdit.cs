using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using BWP.B3Butchery.Utils;
using BWP.B3Frameworks.Utils;
using BWP.Web.Layout;
using BWP.Web.WebControls;
using Forks.EnterpriseServices.DataForm;
using TSingSoft.WebControls2;

namespace BWP.Web.Pages.B3Butchery.Bills.ProductPackaging_
{
  public class ProductPackagingEdit : DomainBillEditPage<ProductPackaging, IProductPackagingBL>
  {
    protected override void BuildBody(Control parent)
    {
      var titlePanel = parent.EAdd(new TitlePanel("基本属性", "基本属性"));
      var layoutManager = new LayoutManager("main", mDFInfo, mDFContainer);
      var config = new AutoLayoutConfig();
      config.Add("AccountingUnit_ID");
      config.Add("Department_ID");
      config.Add("Remark");

      layoutManager.Config = config;
      var section = mPageLayoutManager.AddSection("BaseProperties", "基本属性");
      section.ApplyLayout(layoutManager, config, mPageLayoutManager, mDFInfo);
      titlePanel.Controls.Add(layoutManager.CreateLayout());
      titlePanel.SetPageLayoutSetting(mPageLayoutManager, section.Name);
      CreateDetailPanel(parent.EAdd(new TitlePanel("成品明细")));
    }

    private DFEditGrid _detailGrid;
    private void CreateDetailPanel(TitlePanel titlePanel)
    {
      var vPanel = new VLayoutPanel();
      titlePanel.Controls.Add(vPanel);
      var hPanel = vPanel.Add(new HLayoutPanel(), new VLayoutOption(HorizontalAlign.Left));
      if (CanSave)
      {
        hPanel.Add(new SimpleLabel("成品明细"));
        var selectGoods = hPanel.Add(new ChoiceBox(B3ButcheryDataSource.存货带编号)
        {
          Width = Unit.Pixel(130),
          EnableMultiSelection = true,
          EnableInputArgument = true,
          AutoPostBack = true
        });
        selectGoods.SelectedValueChanged += delegate
        {
          _detailGrid.GetFromUI();
          if (!selectGoods.IsEmpty)
          {
            foreach (var item in selectGoods.GetValues())
            {
              var d = new ProductPackaging_Detail() { Goods_ID = long.Parse(item) };
              DmoUtil.RefreshDependency(d, "Goods_ID");
              Dmo.Details.Add(d);
            }
          }
          selectGoods.Clear();
          _detailGrid.DataBind();
        };
      }
      var detailEditor = new DFCollectionEditor<ProductPackaging_Detail>(() => Dmo.Details);
      detailEditor.AllowDeletionFunc = () => CanSave;
      detailEditor.CanDeleteFunc = (detail) => CanSave;
      detailEditor.IsEditableFunc = (field, detail) => CanSave;
      detailEditor.CanSelectFunc = (detail) => CanSave;

      _detailGrid = vPanel.Add(new DFEditGrid(detailEditor) { Width = Unit.Percentage(100) });
      _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Name"));
      _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Code"));
      _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("NeiGoods_Name"));
      _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("NeiGoods_Code"));
      _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("WaiGoods_Name"));
      _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("WaiGoods_Code"));
      _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Remark"));
      var section = mPageLayoutManager.AddSection("DetaiColumns", "明细列");
      section.ApplyLayout(_detailGrid, mPageLayoutManager, DFInfo.Get(typeof(ProductPackaging_Detail)));
      titlePanel.SetPageLayoutSetting(mPageLayoutManager, section.Name);
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
      _detailGrid.GetFromUI();
    }
  }
}
