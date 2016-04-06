using System;
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
using BWP.Web.Pages.B3Butchery.Dialogs;

namespace BWP.Web.Pages.B3Butchery.Bills.ProductInStore_Temp_
{
  public class ProductInStore_TempEdit : DomainBaseInfoEditPage<ProductInStore_Temp, IProductInStore_TempBL>
  {
    protected override void BuildBody(Control container)
    {
      var mainInfo = container.EAdd(new TitlePanel("基本信息"));
      var layoutManager = new LayoutManager("main", mDFInfo, mDFContainer);    
      var config = new AutoLayoutConfig();
      config.Add("Name");
      config.Add("AccountingUnit_ID");
      config.Add("Department_ID");
      config.Add("Employee_ID");
      config.Add("Store_ID");
      config.Add("InStoreType_ID");
      config.Add("InStoreDate");
      config.Add("CheckEmployee_ID");
      config.Add("CheckDate");
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
              var d = new ProductInStore_Temp_Detail() { Goods_ID = long.Parse(item)};
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
            Dmo.Details.Add(detail);
          }
          detailGrid.DataBind();
        };
      }
      var detailGridEditor = new DFCollectionEditor<ProductInStore_Temp_Detail>(() => Dmo.Details);
      detailGridEditor.AllowDeletionFunc = () => CanSave;
      detailGridEditor.CanDeleteFunc = (detail) => CanSave;
      detailGridEditor.IsEditableFunc = (field, detail) => CanSave;
      detailGrid = titlePanel.EAdd(new DFEditGrid(detailGridEditor) { Width = Unit.Percentage(100) });     
      detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Name"));
      detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Code"));
      detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Spec"));     
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
    }

    protected override void InitNewDmo(ProductInStore_Temp dmo)
    {
      base.InitNewDmo(dmo);
      var profile = DomainUserProfileUtil.Load<B3ButcheryUserProfile>();
      dmo.InStoreDate = DateTime.Today.AddDays(profile.ProductInStoreDaysBrake ?? 0);
    }

  }
}
