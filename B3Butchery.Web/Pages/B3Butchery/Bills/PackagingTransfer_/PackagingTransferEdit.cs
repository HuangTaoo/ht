using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using BWP.B3Frameworks.Utils;
using BWP.B3UnitedInfos;
using BWP.Web.CustomPageLayout;
using BWP.Web.Layout;
using BWP.Web.WebControls;
using Forks.EnterpriseServices.DataForm;
using Forks.Utils.Collections;
using TSingSoft.WebControls2;

namespace BWP.Web.Pages.B3Butchery.Bills.PackagingTransfer_
{
  class PackagingTransferEdit : DomainBillEditPage<PackagingTransfer, IPackagingTransferBL>
  {
    private DFEditGrid _detailGrid;
    protected override void BuildBody(Control control)
    {
      base.BuildBody(control);
      AddDetails(control.EAdd(new TitlePanel("单据明细", "单据明细")));
    }

    protected override void BuildBasePropertiesEditor(TitlePanel titlePanel, PageLayoutSection pageLayoutSection)
    {
      var layoutManager = new LayoutManager("", mDFInfo, mDFContainer);

      var config = new AutoLayoutConfig();
      layoutManager.Config = config;
      config.Add("AccountingUnit_ID");
      config.Add("Date");
      config.Add("OutDepartment_ID");
      config.Add("InDepartment_ID");
      config.Add("Employee_ID");
      config.Add("Remark");

      pageLayoutSection.ApplyLayout(layoutManager, config, mPageLayoutManager, mDFInfo);

      titlePanel.Controls.Add(layoutManager.CreateLayout());
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

      //      AddCopyAndPaste(toobar);

      var editor = new DFCollectionEditor<PackagingTransfer_Detail>(() => Dmo.Details);
      editor.AllowDeletionFunc = () => CanSave;

      editor.CanDeleteFunc = detail => true;
      editor.IsEditableFunc = (field, detail) => CanSave;

      _detailGrid = new DFEditGrid(editor);
      _detailGrid.DFGridSetEnabled = false;
      _detailGrid.Width = Unit.Percentage(100);

      _detailGrid.ShowLineNo = true;
      _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("OutGoods_Name"));
      _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Code"));
      _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Name"));
      _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Spec"));
      _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("ProductionPlan_PlanNumber"));
      _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("GoodsPacking_Name"));
      _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("SecondNumber"));
      var section = mPageLayoutManager.AddSection("DetaiColumns", "明细列");
      titlePanel.SetPageLayoutSetting(mPageLayoutManager, section.Name);

      section.ApplyLayout(_detailGrid, mPageLayoutManager, DFInfo.Get(typeof( PackagingTransfer_Detail)));

      vPanel.Add(_detailGrid);



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
        _detailGrid.GetFromUI();
        foreach (var sGoodsID in goodsSelect.GetValues())
        {
          var goodsid = Convert.ToInt64(sGoodsID);
          if (Dmo.Details.Any(x => x.Goods_ID == goodsid))
          {
            continue;
          }
          var detail = new  PackagingTransfer_Detail();
          detail.Goods_ID = goodsid;
          DmoUtil.RefreshDependency(detail, "Goods_ID");
          Dmo.Details.Add(detail);
        }
        goodsSelect.DisplayValue = string.Empty;
        _detailGrid.DataBind();
      };

    }


    public override void GetFromUI()
    {
      base.GetFromUI();
      _detailGrid.GetFromUI();
    }

    public override void AppToUI()
    {
      base.AppToUI();
      _detailGrid.DataBind();
    }
  }
}
