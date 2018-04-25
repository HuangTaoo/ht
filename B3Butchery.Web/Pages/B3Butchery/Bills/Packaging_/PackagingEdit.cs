using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using BWP.B3Frameworks.Utils;
using BWP.B3UnitedInfos;
using BWP.Web.CustomPageLayout;
using BWP.Web.Layout;
using BWP.Web.WebControls;
using Forks.EnterpriseServices.DataForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using TSingSoft.WebControls2;

namespace BWP.Web.Pages.B3Butchery.Bills.Packaging_
{
  public class PackagingEdit : DomainBillEditPage<Packaging, IPackagingBL>
  {
    private DFEditGrid detailGrid;

    protected override void BuildBasePropertiesEditor(TitlePanel titlePanel, PageLayoutSection section)
    {
      var layoutManager = new LayoutManager("main", mDFInfo, mDFContainer);
      var config = new AutoLayoutConfig();
      config.Add("Name");
      config.Add("Packing_Attr");
      layoutManager.Config = config;
      section.ApplyLayout(layoutManager, config, mPageLayoutManager, mDFInfo);
      titlePanel.Controls.Add(layoutManager.CreateLayout());
    }

    protected override void BuildBody(Control control)
    {
      base.BuildBody(control);
      AddDetails(control.EAdd(new TitlePanel("单据明细", "单据明细")));
    }


    private void AddDetails(TitlePanel titlePanel)
    {
      var vPanel = titlePanel.EAdd(new VLayoutPanel());
      var toobar = new HLayoutPanel();
      vPanel.Add(toobar, new VLayoutOption(HorizontalAlign.Left));

      if (CanSave)
      {
        AddToolsPanel(toobar);
      }

      var editor = new DFCollectionEditor<Packaging_Detail>(() => Dmo.Details);
      editor.AllowDeletionFunc = () => CanSave;

      editor.CanDeleteFunc = detail => true;
      editor.IsEditableFunc = (field, detail) => CanSave;

      detailGrid = new DFEditGrid(editor);
      detailGrid.DFGridSetEnabled = false;
      detailGrid.Width = Unit.Percentage(100);

      detailGrid.ShowLineNo = true;
      detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Code"));
      detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Name"));
      detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Spec"));
      detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("GoodsProperty_Name"));

      var section = mPageLayoutManager.AddSection("DetaiColumns", "明细列");
      titlePanel.SetPageLayoutSetting(mPageLayoutManager, section.Name);

      section.ApplyLayout(detailGrid, mPageLayoutManager, DFInfo.Get(typeof(Packaging_Detail)));

      vPanel.Add(detailGrid);

    }


    private void AddToolsPanel(HLayoutPanel toobar)
    {
      toobar.Add(new SimpleLabel("选择存货"));
      var goodsSelect = new ChoiceBox(B3UnitedInfosConsts.DataSources.存货)
      {
        Width = Unit.Pixel(120),
        AutoPostBack = true,
        EnableMultiSelection = true,
        EnableInputArgument = true
      };
      toobar.Add(goodsSelect);
      goodsSelect.SelectedValueChanged += (sender, e) =>
      {
        detailGrid.GetFromUI();
        foreach (var sGoodsID in goodsSelect.GetValues())
        {

          var detail = new Packaging_Detail();
          detail.Goods_ID = Convert.ToInt64(sGoodsID);
          DmoUtil.RefreshDependency(detail, "Goods_ID");
          Dmo.Details.Add(detail);
        }
        goodsSelect.DisplayValue = string.Empty;
        detailGrid.DataBind();
      };

    }


    public override void GetFromUI()
    {
      base.GetFromUI();
      detailGrid.GetFromUI();
    }

    public override void AppToUI()
    {
      base.AppToUI();
      detailGrid.DataBind();
    }
  }
}
