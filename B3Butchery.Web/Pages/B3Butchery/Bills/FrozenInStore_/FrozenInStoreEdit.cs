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
using TSingSoft.WebControls2;
using Forks.Utils.Collections;

namespace BWP.Web.Pages.B3Butchery.Bills.FrozenInStore_
{
  class FrozenInStoreEdit : DepartmentWorkFlowBillEditPage<FrozenInStore, IFrozenInStoreBL>
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
              var d = new FrozenInStore_Detail { Goods_ID = long.Parse(item) };
              var goods = WebBLUtil.GetSingleDmo<Goods>("ID", long.Parse(item));
              d.Goods_MainUnit = goods.MainUnit;
              d.Goods_Name = goods.Name;
              d.Goods_Code = goods.Code;
              d.Goods_UnitConvertDirection = goods.UnitConvertDirection;
              d.Goods_SecondUnit2 = goods.SecondUnitII;
              d.Goods_SecondUnitII_MainUnitRatio = goods.SecondUnitII_MainUnitRatio;
              d.Goods_SecondUnitII_SecondUnitRatio = goods.SecondUnitII_SecondUnitRatio;
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
            var detail = new FrozenInStore_Detail { Goods_ID = temGoodsDetail.Goods_ID };
            //DmoUtil.RefreshDependency(detail, "Goods_ID");
            var goods = WebBLUtil.GetSingleDmo<Goods>("ID", temGoodsDetail.Goods_ID);
            detail.Goods_MainUnit = goods.MainUnit;
            detail.Goods_Name = goods.Name;
            detail.Goods_Code = goods.Code;
            detail.Goods_UnitConvertDirection = goods.UnitConvertDirection;
            detail.Goods_SecondUnitII_MainUnitRatio = goods.SecondUnitII_MainUnitRatio;
            detail.Goods_SecondUnitII_SecondUnitRatio = goods.SecondUnitII_SecondUnitRatio;
            Dmo.Details.Add(detail);
          }
          _detailGrid.DataBind();
        };
      }

      var editor = new DFCollectionEditor<FrozenInStore_Detail>(() => Dmo.Details)
      {
        AllowDeletionFunc = () => CanSave,
        CanDeleteFunc = detail => true,
        IsEditableFunc = (field, detail) => CanSave
      };

      _detailGrid = new DFEditGrid(editor) { DFGridSetEnabled = false, Width = Unit.Percentage(100) };

      _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Code"));
      _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Name"));
      _detailGrid.Columns.EAdd(new DFEditGridColumn<DFTextBox>("Number")).HeaderText = "主数量";
      _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_MainUnit"));

      var secondNumber2Input = new DFEditGridColumn<DFTextBox>("SecondNumber2");
      _detailGrid.Columns.EAdd(secondNumber2Input).HeaderText = "生产数量";

      _detailGrid.Columns.EAdd(new DFEditGridColumn<DFValueLabel>("Goods_SecondUnit2")).HeaderText = "生产单位(辅单位II)";
      _detailGrid.Columns.Add(new DFEditGridColumn<DFTextBox>("Remark"));

      _detailGrid.ValueColumns.Add("Goods_ID");
      _detailGrid.ValueColumns.Add("Goods_UnitConvertDirection");
      _detailGrid.ValueColumns.Add("Goods_SecondUnitII_MainUnitRatio");
      _detailGrid.ValueColumns.Add("Goods_SecondUnitII_SecondUnitRatio");

      new Main_Second2_ConvertRatioRowManager(_detailGrid, "Number", "SecondNumber2");
      mDFContainer.AddNonDFControl(_detailGrid, "$DetailGrid");

      var section = mPageLayoutManager.AddSection("DetaiColumns", "明细列");
      titlePanel.SetPageLayoutSetting(mPageLayoutManager, section.Name);

      section.ApplyLayout(_detailGrid, mPageLayoutManager, DFInfo.Get(typeof(FrozenInStore_Detail)));

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
