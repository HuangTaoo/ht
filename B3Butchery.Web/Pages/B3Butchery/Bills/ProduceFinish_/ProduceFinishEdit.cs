using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using BWP.B3Butchery;
using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using BWP.B3Butchery.Utils;
using BWP.B3Frameworks.Utils;
using BWP.B3UnitedInfos;
using BWP.Web.CustomPageLayout;
using BWP.Web.Layout;
using BWP.Web.Pages.B3Butchery.Dialogs;
using BWP.Web.Utils;
using BWP.Web.WebControls;
using Forks.EnterpriseServices.DataForm;
using TSingSoft.WebControls2;
using TSingSoft.WebPluginFramework;

namespace BWP.Web.Pages.B3Butchery.Bills.ProduceFinish_ {
  public class ProduceFinishEdit : DepartmentWorkFlowBillEditPage<ProduceFinish, IProduceFinishBL> {

    B3ButcheryConfig butcheryConfig = new B3ButcheryConfig();

    protected override void BuildBody(Control form) {
      base.BuildBody(form);
      CreateOutputDetailPanel(form.EAdd(new TitlePanel("明细", "明细")));
    }

    protected override void BuildBasePropertiesEditor(TitlePanel titlePanel, PageLayoutSection section) {
      var layoutManager = new LayoutManager("main", mDFInfo, mDFContainer);
      
      //var config2 = new B3ButcheryConfig();
      //con
      var config = new AutoLayoutConfig();
      config.Add("AccountingUnit_ID");
      config.Add("Date");
      config.Add("Department_ID");
      config.Add("Customer_ID");
      config.Add("Employee_ID");
      config.Add("ProductionUnit_ID");
      config.Add("Remark");
      layoutManager.Config = config;
      section.ApplyLayout(layoutManager, config, mPageLayoutManager, mDFInfo);
      titlePanel.Controls.Add(layoutManager.CreateLayout());

    }

   protected DFEditGrid _detailGrid;
   protected readonly bool _useBrand = GlobalFlags.get(B3UnitedInfosConsts.GlobalFlags.库存支持品牌项);
    private void CreateOutputDetailPanel(TitlePanel tPanel) {
      var hPanel = new HLayoutPanel();

      if (CanSave) {
        tPanel.Controls.Add(hPanel);

        hPanel.Add(new SimpleLabel("选择存货"));
        var selectEmp = hPanel.Add(new ChoiceBox(B3UnitedInfosConsts.DataSources.存货) { Width = Unit.Pixel(130), EnableInputArgument = true, AutoPostBack = true });
        mDFContainer.AddNonDFControl(selectEmp, "$SelectGoods");
        selectEmp.SelectedValueChanged += delegate {
          _detailGrid.GetFromUI();
          if (!selectEmp.IsEmpty) {
            var empID = long.Parse(selectEmp.Value);
            if (!Dmo.Details.Any(x => x.Goods_ID == empID)) {
              var d = new ProduceFinish_Detail() { Goods_ID = empID };
              var last = Dmo.Details.LastOrDefault();
              if (last != null) {
                d.BrandItem_ID = last.BrandItem_ID;
                d.BrandItem_Name = last.BrandItem_Name;
              }
              DmoUtil.RefreshDependency(d, "Goods_ID");
              Dmo.Details.Add(d);
            }
          }
          selectEmp.Clear();
          _detailGrid.DataBind();
          var script = B3ButcheryWebUtil.SetCursorPositionScript(butcheryConfig.ProduceFinishCursorField, "$DetailGrid", Dmo.Details.Count, _detailGrid.PageSize);
          if (!string.IsNullOrEmpty(script))
            Page.ClientScript.RegisterStartupScript(GetType(), "Startup", script, true);
        };
        var addGoodsbt = hPanel.Add(new DialogButton {
          Text = "选择存货",
        });
        addGoodsbt.Url = "/B3Butchery/Dialogs/SelectGoodsDialog.aspx";
        addGoodsbt.Click += delegate {
          _detailGrid.GetFromUI();
          var details = DialogUtil.GetCachedObj<TemGoodsDetail>(this);
          foreach (var temGoodsDetail in details) {
            var detail = new ProduceFinish_Detail { Goods_ID = temGoodsDetail.Goods_ID };
            DmoUtil.RefreshDependency(detail, "Goods_ID"); 
  
            Dmo.Details.Add(detail);
          }
          _detailGrid.DataBind();
        };

        var addbt = hPanel.Add(new DialogButton {
          Text = "选择生产通知单",
        });
        addbt.Url = "/B3Butchery/Dialogs/SelectProductNoticeDialog.aspx";
        addbt.Click += delegate {
          _detailGrid.GetFromUI();
          var details = DialogUtil.GetCachedObj<ProduceFinish_Detail>(this);
          foreach (var temGoodsDetail in details) {

            DmoUtil.RefreshDependency(temGoodsDetail, "Goods_ID");

            Dmo.Details.Add(temGoodsDetail);
          }
          _detailGrid.DataBind();
        };

        AddCustomerToolBar(hPanel);
      }
      var detailEditor = new DFCollectionEditor<ProduceFinish_Detail>(() => Dmo.Details) {
        AllowDeletionFunc = () => CanSave,
        CanDeleteFunc = detail => CanSave,
        IsEditableFunc = (field, detail) => CanSave
      };
      _detailGrid = new DFEditGrid(detailEditor) { Width = Unit.Percentage(100) };
      mDFContainer.AddNonDFControl(_detailGrid, "$DetailGrid");
      _detailGrid.NextRowOnEnter = true;
      _detailGrid.LastRowOnDown = "__DFContainer.getControl('$SelectGoods').behind.focus();";
      tPanel.Controls.Add(_detailGrid);
      if(_useBrand)
        _detailGrid.Columns.Add(new DFEditGridColumn("BrandItem_ID"));
      _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Name"));
      _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Code"));
      _detailGrid.Add(new DFEditGridColumn<DFValueLabel>("Goods_Spec"));
      _detailGrid.Add(new DFEditGridColumn<DFTextBox>("Number"));
      _detailGrid.Add(new DFEditGridColumn<DFValueLabel>("Goods_MainUnit"));
      _detailGrid.Add(new DFEditGridColumn<DFTextBox>("SecondNumber"));
      _detailGrid.Add(new DFEditGridColumn<DFValueLabel>("Goods_SecondUnit"));
      _detailGrid.Columns.Add(new DFEditGridColumn<DFTextBox>("Remark"));
      _detailGrid.ValueColumns.Add("Goods_UnitConvertDirection");
      _detailGrid.ValueColumns.Add("Goods_MainUnitRatio");
      _detailGrid.ValueColumns.Add("Goods_SecondUnitRatio");
      new Main_Second_ConvertRatioRowManager(_detailGrid, "Number", "SecondNumber");
    }

    protected virtual  void AddCustomerToolBar(HLayoutPanel hPanel)
    {
    
    }

    public override void GetFromUI() {
      base.GetFromUI();
      _detailGrid.GetFromUI();
    }

    public override void AppToUI() {
      base.AppToUI();
      _detailGrid.DataBind();

    }

  }
}
