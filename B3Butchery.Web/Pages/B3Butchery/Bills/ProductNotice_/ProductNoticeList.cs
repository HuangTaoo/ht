using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using BWP.B3Frameworks;
using BWP.Web.Layout;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using TSingSoft.WebControls2;
using TSingSoft.WebPluginFramework;

namespace BWP.Web.Pages.B3Butchery.Bills.ProductNotice_ {
  class ProductNoticeList : DomainBillListPage<ProductNotice, IProductNoticeBL> {

    protected override void AddDFBrowseGridColumn(DFBrowseGrid grid, string field) {
      base.AddDFBrowseGridColumn(grid, field);
      if (field == "BillState") {
        AddDFBrowseGridColumn(grid, "Date");
        AddDFBrowseGridColumn(grid, "AccountingUnit_Name"); 
        AddDFBrowseGridColumn(grid, "ProductionUnit_Name"); 
        AddDFBrowseGridColumn(grid, "Customer_Name"); 
        AddDFBrowseGridColumn(grid, "Employee_Name"); 
      }
    }

    public override bool EnableBatchPrint {
      get {
        return true;
      }
    }

    public override bool EnableBatchCheck {
      get { return true; }
    }

    protected override DQueryDom GetQueryDom()
      {
          var query= base.GetQueryDom();
          if (!BLContext.User.IsInRole("B3Butchery.生产通知单.管理"))
          {
              query.Where.Conditions.Add(DQCondition.EQ("CreateUser_ID", BLContext.User.ID));
          }
          return query;
      }

      public override bool EnableTheming { get; set; }

      protected override void AddQueryControls(VLayoutPanel vPanel) {
      var layoutManager = new LayoutManager("", mDFInfo, mQueryContainer);

      var config = new AutoLayoutConfig();
      config.Add("ID");
      config.Add("CreateUser_Name");
      config.Add("CheckUser_Name");
      config.Add("AccountingUnit_ID"); 
      config.Add("Employee_ID");
      config.Add("Customer_ID");
      config.Add("Date");
      config.Add("BillState");
      config.Add("ProductionUnit_ID");
      config.Add("IsLocked");
      AddTagQueryInput(layoutManager, config);

      var section = mPageLayoutManager.AddSection(B3FrameworksConsts.PageLayouts.QueryConditions, B3FrameworksConsts.PageLayouts.QueryConditions_DisplayName);
      section.SetRequired("BillState");
      section.ApplyLayout(layoutManager, config, mPageLayoutManager, mDFInfo);

      layoutManager.Config = config;

      vPanel.Add(layoutManager.CreateLayout());

    }

    protected override void InitToolBar(HLayoutPanel toolbar)
    {
        base.InitToolBar(toolbar);
        if (User.IsInRole("B3Butchery.报表.生产通知分析"))
        {
            var dataAnysBtn = new TSButton() { Text = "数据分析", UseSubmitBehavior = false };
            dataAnysBtn.OnClientClick = string.Format("OpenUrlInTopTab('{0}','生产通知分析');return false;", WpfPageUrl.ToGlobal(AspUtil.AddTimeStampToUrl("~/B3Butchery/Reports/ProductNoticeReport_/ProductNoticeReport.aspx")));
            toolbar.Add(dataAnysBtn);
        }
    }
  }
}
