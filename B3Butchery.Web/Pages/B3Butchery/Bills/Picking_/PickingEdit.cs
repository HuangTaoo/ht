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
using BWP.Web.Utils;
using BWP.Web.WebControls;
using Forks.EnterpriseServices.DataForm;
using Forks.Utils.Collections;
using TSingSoft.WebControls2;

namespace BWP.Web.Pages.B3Butchery.Bills.Picking_
{
  public class PickingEdit : DepartmentWorkFlowBillEditPage<Picking, IPickingBL>
  {
    protected override void BuildBasePropertiesEditor(TitlePanel titlePanel, PageLayoutSection section)
    {
      var layoutManager = new LayoutManager("main", mDFInfo, mDFContainer);
      var config = new AutoLayoutConfig();
      config.Add("AccountingUnit_ID");
      config.Add("Department_ID");
      config.Add("Employee_ID");
      config.Add("Date");
      config.Add("ProductLine_ID");
      config.Add("Store_ID");


       BuildBasePropertiesConfig(config);

      config.Add("Remark");

      layoutManager.Config = config;
      section.ApplyLayout(layoutManager, config, mPageLayoutManager, mDFInfo);
      titlePanel.Controls.Add(layoutManager.CreateLayout());
    }

    protected virtual  void BuildBasePropertiesConfig(AutoLayoutConfig config)
    {
      
    }

    protected override void BuildBody(Control form)
    {
      base.BuildBody(form);
      AddDetail(form.EAdd(new TitlePanel("单据明细", "单据明细")));
    }

    DFEditGrid detailGrid;
    private void AddDetail(TitlePanel titlePanel)
    {
      var vPanel = titlePanel.EAdd(new VLayoutPanel());

      if (CanSave)
      {
        var hPanel = new HLayoutPanel();
        vPanel.Add(hPanel, new VLayoutOption(HorizontalAlign.Left));
        hPanel.Add(new SimpleLabel("选择存货"));
        var selectGoods = new ChoiceBox(B3UnitedInfosConsts.DataSources.存货) { Width = Unit.Pixel(130), EnableInputArgument = true, AutoPostBack = true, EnableTopItem = true, EnableMultiSelection = true };
        selectGoods.SelectedValueChanged += delegate
        {
          detailGrid.GetFromUI();
          if (!selectGoods.IsEmpty)
          {
            var gids = selectGoods.GetValues().Distinct();
            foreach (var g in gids)
            {
              if (Dmo.Details.Any(x => x.Goods_ID == long.Parse(g)))
              {
                continue;
              }
              var d = new Picking_Detail() { Goods_ID = long.Parse(g) };
              DmoUtil.RefreshDependency(d, "Goods_ID");
              Dmo.Details.Add(d);
            }
          }
          selectGoods.Clear();
          detailGrid.DataBind();

        };
        hPanel.Add(selectGoods);

      }

      var editor = new DFCollectionEditor<Picking_Detail>(() => Dmo.Details);
      editor.AllowDeletionFunc = () => CanSave;
      editor.IsEditableFunc = (field, detail) => CanSave;
      editor.CanDeleteFunc = detail => CanSave;

      detailGrid = new DFEditGrid(editor) { Width = Unit.Percentage(100) };
      detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Code"));
      detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Name"));
      detailGrid.Columns.EAdd(new DFEditGridColumn<DFValueLabel>("Goods_Spec"));
      detailGrid.Columns.EAdd(new DFEditGridColumn<DFTextBox>("Number"));
      detailGrid.Columns.EAdd(new DFEditGridColumn<DFValueLabel>("Goods_MainUnit"));
      detailGrid.Columns.EAdd(new DFEditGridColumn<DFTextBox>("SecondNumber"));
      detailGrid.Columns.EAdd(new DFEditGridColumn<DFValueLabel>("Goods_SecondUnit"));
      detailGrid.Columns.EAdd(new DFEditGridColumn<DFTextBox>("Remark"));

      detailGrid.ValueColumns.Add("Goods_ID");
      detailGrid.ValueColumns.Add("Goods_UnitConvertDirection");
      detailGrid.ValueColumns.Add("Goods_MainUnitRatio");
      detailGrid.ValueColumns.Add("Goods_SecondUnitRatio");
      mDFContainer.AddNonDFControl(detailGrid, "$detailGrid");

      var section = mPageLayoutManager.AddSection("DetaiColumns", "明细列");
      section.SetRequired("Number", "SecondNumber");
      section.ApplyLayout(detailGrid, mPageLayoutManager, DFInfo.Get(typeof(Picking_Detail)));
      new NumberSecondNumberConvertRowMangerWithMoneyChanged(detailGrid);
      vPanel.Add(detailGrid);
    }

    public override void AppToUI()
    {
      base.AppToUI();
      detailGrid.DataBind();
    }

    public override void GetFromUI()
    {
      base.GetFromUI();
      detailGrid.GetFromUI();
    }

  }
}
