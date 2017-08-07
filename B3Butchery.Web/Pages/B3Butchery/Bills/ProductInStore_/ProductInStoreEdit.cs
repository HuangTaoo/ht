using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using BWP.B3Butchery.Utils;
using BWP.B3Frameworks.Utils;
using BWP.B3UnitedInfos;
using BWP.B3UnitedInfos.BO;
using BWP.Web.Layout;
using BWP.Web.WebControls;
using Forks.EnterpriseServices.DataForm;
using TSingSoft.WebControls2;
using Forks.Utils.Collections;
using BWP.Web.Utils;
using BWP.Web.Pages.B3Butchery.Dialogs;
using TSingSoft.WebPluginFramework;
using BWP.Web.Actions;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using BWP.B3Frameworks;
using Forks.EnterpriseServices.SqlDoms;
using BWP.B3UnitedInfos.Utils;
using BWP.B3Butchery;

namespace BWP.Web.Pages.B3Butchery.Bills.ProductInStore_
{
  public class ProductInStoreEdit : DepartmentWorkFlowBillEditPage<ProductInStore, IProductInStoreBL>
  {


    protected override void BuildBody(Control container)
    {
      var mainInfo = container.EAdd(new TitlePanel("基本信息"));
      var layoutManager = new LayoutManager("main", mDFInfo, mDFContainer);
      layoutManager.Add("ProductPlan_ID", InputCreator.DFChoiceBox(B3ButcheryDataSource.计划号, "ProductPlan_Name"));
      var config = new AutoLayoutConfig();
      config.Add("AccountingUnit_ID");
      config.Add("Department_ID");
      config.Add("Employee_ID");
      config.Add("Store_ID");
      config.Add("InStoreType_ID");
      config.Add("InStoreDate");
      config.Add("CheckEmployee_ID");
      config.Add("CheckDate");
      config.Add("ProductPlan_ID");
      AddProductionUnit(config);
      config.Add("Remark");
      layoutManager.Config = config;
      var section = mPageLayoutManager.AddSection("BaseProperties", "基本属性");
      section.ApplyLayout(layoutManager, config, mPageLayoutManager, mDFInfo);
      mainInfo.Controls.Add(layoutManager.CreateLayout());
      mainInfo.SetPageLayoutSetting(mPageLayoutManager, section.Name);
      CreateDetailPanel(container.EAdd(new TitlePanel("明细信息")));
 
    }
    protected virtual void AddProductionUnit(AutoLayoutConfig config)
    {
    }

    DFEditGrid detailGrid;
    private void CreateDetailPanel(TitlePanel titlePanel)
    {
      var hPanel = titlePanel.EAdd(new HLayoutPanel());
      hPanel.Add(new LiteralControl("<h2>入库清单：</h2>"));
      AddToolsBar(hPanel);

      hPanel.Add(new TSButton("复制", delegate
      {
        GoodsDetailSummaryClipboardUtil.Copy(Dmo.Details.Select((item) => (GoodsDetailSummaryBase)item).ToList());
        AspUtil.Alert(this, "复制成功");
      }));

      if (CanSave)
      {
        hPanel.Add(new TSButton("粘贴", delegate
        {
          var list = GoodsDetailSummaryClipboardUtil.Paste<ProductInStore_Detail>();
          foreach (var detail in list)
          {
            Dmo.Details.Add(detail);

            DmoUtil.RefreshDependency(detail, "Goods_ID");
          }
          detailGrid.DataBind();

        }));
      }

      var detailGridEditor = new DFCollectionEditor<ProductInStore_Detail>(() => Dmo.Details);
      detailGridEditor.AllowDeletionFunc = () => CanSave;
      detailGridEditor.CanDeleteFunc = (detail) => CanSave;
      detailGridEditor.IsEditableFunc = (field, detail) => {
        switch (field.Name) {
          case "GoodsBatch_ID":
            return GoodsUtil.EnableBatch(detail.Goods_ID, detail.GoodsProperty_ID) && CanSave;
          case "Money":
            return false;
          case "SecondNumber":
            var hasSecondUnit = !string.IsNullOrEmpty(detail.Goods_SecondUnit);
            return CanSave && hasSecondUnit;
        }

        return CanSave;
      };
      detailGrid = titlePanel.EAdd(new DFEditGrid(detailGridEditor) { Width = Unit.Percentage(100), ShowLineNo = true });
      detailGrid.Columns.Add(new DFEditGridColumn("ProductionDate"));
      var productPlanCol = new DFEditGridColumn<DFChoiceBox>("ProductPlan_ID");
      productPlanCol.InitEditControl += delegate(object sender, InitEditControlEventArgs<DFChoiceBox> e)
      {
        e.Control.DataKind = B3ButcheryDataSource.计划号;
        e.Control.DFDisplayField = "ProductPlan_Name";
        e.Control.EnableInputArgument = true;
        e.Control.EnableTopItem = true;
        e.Control.Width = Unit.Pixel(120);
      };
      detailGrid.Columns.Add(productPlanCol);
      detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Name"));
      detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Code"));
      var script = @"
var id =parseInt(this.front.value);
simpleRestCall('/MainSystem/B3Butchery/Rpcs/GoodsBatchRpc/Get',
[id,['ProductionDate']],
function(result,dfContainer){
  dfContainer.setValue('ProductionDate',getDateTime(result.ProductionDate));
},{context:this.dfContainer});";
      detailGrid.Columns.EAdd(new DFEditGridColumn<DFChoiceBox>("GoodsBatch_ID")).InitEditControl += (sender, e) =>
      {
        e.Control.EnableTopItem = true;
        e.Control.OnBeforeDrop = "this.codeArgument = dfContainer.getValue('Goods_ID');this.dialogArguments='Goods_ID=' + this.codeArgument+'&TaxRate='+dfContainer.getValue('TaxRate')+'&TmpTaxRate='+dfContainer.getValue('TmpTaxRate')";
        e.Control.DataKind = B3UnitedInfosConsts.DataSources.存货批次;
        e.Control.DFDisplayField = "GoodsBatch_Name";
        e.Control.DialogUrl = WpfPageUrl.ToGlobal("~/B3Butchery/Dialogs/GoodsBatchEdit.aspx?IsNewDialog=1");
        e.Control.OnClientSelected = script;
        e.Control.Width = Unit.Pixel(160);
      };
      detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Spec"));
      var cargoSpaceColumn = detailGrid.Columns.EAdd(new DFEditGridColumn<DFChoiceBox>("CargoSpace_ID"));
      cargoSpaceColumn.InitEditControl += (sender, e) =>
      {
        e.Control.EnableTopItem = true;
        e.Control.OnBeforeDrop = "this.codeArgument = __DFContainer.getValue('Store_ID');";
        e.Control.DataKind = B3FrameworksConsts.DataSources.货位;
        e.Control.DFDisplayField = "CargoSpace_Name";
        e.Control.Width = Unit.Pixel(160);
      };
      detailGrid.Columns.EAdd(new DFEditGridColumn<DFTextBox>("Number")).SumMode = SumMode.Sum;
      detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_MainUnit"));
      detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_SecondUnit"));
      detailGrid.Columns.EAdd(new DFEditGridColumn<DFTextBox>("SecondNumber")).SumMode = SumMode.Sum;
      if (CheckDefaultRole("隐藏单价"))
        detailGrid.Columns.Add(new DFEditGridColumn<DFTextBox>("Price"));
      AddColumn(detailGrid);
      if (CheckDefaultRole("隐藏单价"))
        detailGrid.Columns.EAdd(new DFEditGridColumn<DFTextBox>("Money")).SumMode = SumMode.Sum;
      detailGrid.Columns.Add(new DFEditGridColumn<DFTextBox>("Remark"));
      detailGrid.ValueColumns.Add("Goods_ID");
      detailGrid.ValueColumns.Add("Goods_UnitConvertDirection");
      detailGrid.ValueColumns.Add("Goods_MainUnitRatio");
      detailGrid.ValueColumns.Add("Goods_SecondUnitRatio");
      new MainToSecondConvertRowManger(detailGrid);
      var section = mPageLayoutManager.AddSection("DetailColumns", "明细列");
      section.ApplyLayout(detailGrid, mPageLayoutManager, DFInfo.Get(typeof(ProductInStore_Detail)));
      titlePanel.SetPageLayoutSetting(mPageLayoutManager, section.Name);
    }

    public virtual void AddColumn(DFEditGrid detailGrid)
    {

    }

    private void AddToolsBar(HLayoutPanel hPanel)
    {
      if (!CanSave)
        return;
      hPanel.Add(new SimpleLabel("选择存货"));
      var selectGoods =
        hPanel.Add(new ChoiceBox(B3ButcheryDataSource.存货带编号)
        {
          Width = Unit.Pixel(130),
          EnableMultiSelection = true,
          EnableInputArgument = true,
          AutoPostBack = true
        });
      selectGoods.SelectedValueChanged += delegate
      {
        detailGrid.GetFromUI();
        if (!selectGoods.IsEmpty)
        {
          var config = new B3ButcheryConfig();
          DateTime? productionDate = null;
          //判断当前配置 否为当天时间，是 为入库时间 默认是 否
          if (config.ProductInStoreChooseDate.Value) { productionDate = Dmo.InStoreDate; } else { productionDate = DateTime.Today; }
 
          foreach (var item in selectGoods.GetValues())
          {
            var d = new ProductInStore_Detail()
            {
              Goods_ID = long.Parse(item),
              ProductionDate = productionDate,
              Price = 0
            };
            DmoUtil.RefreshDependency(d, "Goods_ID");
            Dmo.Details.Add(d);
          }
        }
        selectGoods.Clear();
        detailGrid.DataBind();
      };

      var addGoodsbt = hPanel.Add(new DialogButton
      {
        Text = "选择存货",
      });
      addGoodsbt.Url = "/B3Butchery/Dialogs/SelectGoodsDialog.aspx";
      addGoodsbt.Click += delegate
      {
        detailGrid.GetFromUI();
        var details = DialogUtil.GetCachedObj<TemGoodsDetail>(this);
        foreach (var temGoodsDetail in details)
        {
          var detail = new ProductInStore_Detail();
          detail.Goods_ID = temGoodsDetail.Goods_ID;
          DmoUtil.RefreshDependency(detail, "Goods_ID");
          detail.TaxRate = detail.Goods_TaxRate;
          detail.Number = temGoodsDetail.Number;
          detail.SecondNumber = temGoodsDetail.SecondNumber;
          Dmo.Details.Add(detail);
        }
        detailGrid.DataBind();
      };
      hPanel.Add(new SimpleLabel("生产日期"));
      var selectDate = new DateInput();

      hPanel.Add(selectDate);
      var summary = new TSButton() { Text = "统一生产日期" };
      summary.Click += (sender, e) =>
      {
        if (!selectDate.IsEmpty)
        {
          detailGrid.GetFromUI();
          var date = selectDate.Value;
          foreach (var r in Dmo.Details)
          {
            r.ProductionDate = date;
          }
          detailGrid.DataBind();
        }

      };
      hPanel.Add(summary);
      var quickSelctButton = new DialogButton() { Url = "~/B3UnitedInfos/Dialogs/QucicklySelectGoodsDetailsDialog.aspx", Text = "快速选择" };
      quickSelctButton.Click += delegate
      {
        ReceiveSelectedGoodsDetailDialog();
      };
      hPanel.Add(quickSelctButton);

      hPanel.Add(new TSButton("更新生产计划号", delegate
      {
        GetFromUI();
        foreach (var item in Dmo.Details)
        {
          item.ProductPlan_ID = Dmo.ProductPlan_ID;
          item.ProductPlan_Name = Dmo.ProductPlan_Name;
        }
        detailGrid.DataBind();
      }));
      var loadProductInStoreTemp = hPanel.Add(new DialogButton { Text = "选择模板", });
      loadProductInStoreTemp.Url = "/B3Butchery/Dialogs/ProductInStoreTempDialog.aspx";
      loadProductInStoreTemp.Click += delegate
      {
        detailGrid.GetFromUI();
        var temp = DialogUtil.GetCachedObj<ProductInStore_Temp>(this).FirstOrDefault();

        Dmo.AccountingUnit_ID = temp.AccountingUnit_ID;
        Dmo.AccountingUnit_Name = temp.AccountingUnit_Name;
        Dmo.Department_ID = temp.Department_ID;
        Dmo.Department_Name = temp.Department_Name;
        Dmo.Employee_ID = temp.Employee_ID;
        Dmo.Employee_Name = temp.Employee_Name;
        Dmo.InStoreType_ID = temp.InStoreType_ID;
        Dmo.InStoreType_Name = temp.InStoreType_Name;
        Dmo.Store_ID = temp.Store_ID;
        Dmo.Store_Name = temp.Store_Name;
        Dmo.CheckEmployee_ID = temp.CheckEmployee_ID;
        Dmo.CheckEmployee_Name = temp.CheckEmployee_Name;
        Dmo.CheckDate = temp.CheckDate;
        if (Dmo.InStoreDate != null) { Dmo.InStoreDate = temp.InStoreDate; }

        foreach (var de in temp.Details)
        {
          var detail = new ProductInStore_Detail();
          detail.Goods_ID = de.Goods_ID;
          detail.Goods_Name = de.Goods_Name;
          detail.Goods_Spec = de.Goods_Spec;
          detail.Goods_Code = de.Goods_Code;
          DmoUtil.RefreshDependency(detail, "Goods_ID");
          Dmo.Details.Add(detail);
        }
        AppToUI();
        detailGrid.DataBind();
      };
    }

    private void ReceiveSelectedGoodsDetailDialog()
    {
      var selectedList = DialogUtil.GetCachedObj<SelectedGoodsDetail>(this);

      selectedList.Select((item) => new ProductInStore_Detail()
      {
        Goods_ID = item.Goods_ID,
        Number = item.Number,
        SecondNumber = item.SecondNumber
      })
        .ToList()
        .EEnumerate((detail) => DmoUtil.RefreshDependency(detail, "Goods_ID"))
        .EAddToCollection(Dmo.Details);
      detailGrid.DataBind();
    }


    public override void GetFromUI()
    {
      base.GetFromUI();
      detailGrid.GetFromUI();
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if (!IsPostBack)
        DataBind();
    }

    public override void AppToUI()
    {
      base.AppToUI();
      if (CanSave)
        mDFContainer.MakeReadonly("Employee_ID");
    }

    protected override void InitNewDmo(ProductInStore dmo)
    {
      base.InitNewDmo(dmo);
      var profile = DomainUserProfileUtil.Load<B3ButcheryUserProfile>();
      dmo.InStoreDate = DateTime.Now.AddDays(profile.ProductInStoreDaysBrake ?? 0);
    }

    protected override void AddActions(ButtonGroup buttonGroup)
    {
      base.AddActions(buttonGroup);
      AddModelAction(buttonGroup);
      //AutoPostBackControl.Controls.Add(buttonGroup);
    }


    private void AddModelAction(ButtonGroup buttonGroup)
    {
      buttonGroup.Actions.Add(new SimpleServerAction("模板", () => true, delegate
      { 
        string url = "~/B3Butchery/Bills/ProductInStore_Temp_/ProductInStore_TempEdit.aspx";
        AspUtil.Redirect(url); 
      }));
    }
 
  }
}
