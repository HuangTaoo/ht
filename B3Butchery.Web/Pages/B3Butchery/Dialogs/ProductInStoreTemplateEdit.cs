using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Butchery.BO;
using BWP.B3Butchery.BL;
using System.Web.UI;
using BWP.Web.WebControls;
using BWP.Web.Layout;
using BWP.Web.Utils;
using BWP.B3Butchery.Utils;
using TSingSoft.WebControls2;
using System.Web.UI.WebControls;
using BWP.B3Frameworks.Utils;
using Forks.EnterpriseServices.DataForm;
using Forks.Utils.Collections;
using BWP.B3UnitedInfos;
using TSingSoft.WebPluginFramework;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using BWP.B3Frameworks;
using Forks.EnterpriseServices.SqlDoms;
using Forks.EnterpriseServices.BusinessInterfaces;

namespace BWP.Web.Pages.B3Butchery.Dialogs
{
  public class ProductInStoreTemplateEdit : DepartmentWorkFlowBillEditPage<ProductInStore_Temp, IProductInStore_TempBL>
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
      layoutManager.Config = config;
      var section = mPageLayoutManager.AddSection("BaseProperties", "基本属性");
      section.ApplyLayout(layoutManager, config, mPageLayoutManager, mDFInfo);
      mainInfo.Controls.Add(layoutManager.CreateLayout());
      mainInfo.SetPageLayoutSetting(mPageLayoutManager, section.Name);
      CreateDetailPanel(container.EAdd(new TitlePanel("明细信息")));
     
    }

    DFEditGrid detailGrid;
    private void CreateDetailPanel(TitlePanel titlePanel)
    {
      var hPanel = titlePanel.EAdd(new HLayoutPanel());
      hPanel.Add(new LiteralControl("<h2>入库清单：</h2>"));
      if (CanSave)
      {
        hPanel.Add(new SimpleLabel("选择存货"));
        var selectGoods = hPanel.Add(new ChoiceBox(B3ButcheryDataSource.存货带编号) { Width = Unit.Pixel(130), EnableMultiSelection = true, EnableInputArgument = true, AutoPostBack = true });
        selectGoods.SelectedValueChanged += delegate
        {
          detailGrid.GetFromUI();
          if (!selectGoods.IsEmpty)
          {
            foreach (var item in selectGoods.GetValues())
            {
              var d = new ProductInStore_Temp_Detail() { Goods_ID = long.Parse(item), ProductionDate = DateTime.Today, Price = 0 };
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
            var detail = new ProductInStore_Temp_Detail();
            detail.Goods_ID = temGoodsDetail.Goods_ID;
            DmoUtil.RefreshDependency(detail, "Goods_ID");
            detail.TaxRate = detail.Goods_TaxRate;
            detail.Number = temGoodsDetail.Number;
            detail.SecondNumber = temGoodsDetail.SecondNumber;
            Dmo.Details.Add(detail);
          }
          detailGrid.DataBind();
        };

        var updateButton = hPanel.Add(new TSButton("更新生产计划号", delegate
        {
          GetFromUI();
          foreach (var item in Dmo.Details)
          {
            item.ProductPlan_ID = Dmo.ProductPlan_ID;
            item.ProductPlan_Name = Dmo.ProductPlan_Name;
          }
          detailGrid.DataBind();
        }));
      }


      var detailGridEditor = new DFCollectionEditor<ProductInStore_Temp_Detail>(() => Dmo.Details);
      detailGridEditor.AllowDeletionFunc = () => CanSave;
      detailGridEditor.CanDeleteFunc = (detail) => CanSave;
      detailGridEditor.IsEditableFunc = (field, detail) => CanSave;
      detailGrid = titlePanel.EAdd(new DFEditGrid(detailGridEditor) { Width = Unit.Percentage(100) });
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
[id,['ProductionDate','QualityDays']],
function(result,dfContainer){
  dfContainer.setValue('ProductionDate',getDateTime(result.ProductionDate));
  dfContainer.setValue('QualityDays',result.QualityDays );
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
      detailGrid.Columns.EAdd(new DFEditGridColumn<DFTextBox>("Number")).SumMode = SumMode.Sum; ;
      detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_MainUnit"));
      detailGrid.Columns.EAdd(new DFEditGridColumn<DFTextBox>("SecondNumber")).SumMode = SumMode.Sum; ;
      detailGrid.Columns.Add(new DFEditGridColumn<DFTextBox>("Price"));
      detailGrid.Columns.EAdd(new DFEditGridColumn<DFTextBox>("Money")).SumMode = SumMode.Sum; ;
      detailGrid.Columns.Add(new DFEditGridColumn<DFTextBox>("Remark"));
      detailGrid.ValueColumns.Add("Goods_ID");
      detailGrid.ValueColumns.Add("Goods_UnitConvertDirection");
      detailGrid.ValueColumns.Add("Goods_MainUnitRatio");
      detailGrid.ValueColumns.Add("Goods_SecondUnitRatio");
      new MainToSecondConvertRowManger(detailGrid);
      var section = mPageLayoutManager.AddSection("DetailColumns", "明细列");
      section.ApplyLayout(detailGrid, mPageLayoutManager, DFInfo.Get(typeof(ProductInStore_Temp_Detail)));
      titlePanel.SetPageLayoutSetting(mPageLayoutManager, section.Name);
      var hPanel2 = new HLayoutPanel() { Align = HorizontalAlign.Left };
      titlePanel.Controls.Add(detailGrid);
    
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

    protected override void InitNewDmo(ProductInStore_Temp dmo)
    {
      base.InitNewDmo(dmo);
      var profile = DomainUserProfileUtil.Load<B3ButcheryUserProfile>();
      dmo.InStoreDate = DateTime.Today.AddDays(profile.ProductInStoreDaysBrake ?? 0);
    }

  }
}
