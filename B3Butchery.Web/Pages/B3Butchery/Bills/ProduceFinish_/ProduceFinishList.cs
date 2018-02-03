using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using BWP.Web.Utils;
using TSingSoft.WebControls2;
using TSingSoft.WebPluginFramework;
using DataKind = BWP.B3Frameworks.B3FrameworksConsts.DataSources;
namespace BWP.Web.Pages.B3Butchery.Bills.ProduceFinish_ {
  internal class ProduceFinishList : DomainBillListPage<ProduceFinish, IProduceFinishBL> {
    protected override void AddQueryControls(VLayoutPanel vPanel) {
      vPanel.Add(CreateDefaultBillQueryControls((panel, config) => {
        panel.Add("Date",
                  QueryCreator.DateRange(mDFInfo.Fields["Date"],
                                         mQueryContainer, "MinDate",
                                         "MaxDate"));
        panel.Add("AccountingUnit_ID",
                  QueryCreator.DFChoiceBox(
                    mDFInfo.Fields["AccountingUnit_ID"], DataKind.授权会计单位全部));
        panel.Add("Department_ID",
                  QueryCreator.DFChoiceBox(mDFInfo.Fields["Department_ID"],
                                           DataKind.授权部门全部));
        panel.Add("Employee_ID",
                  QueryCreator.DFChoiceBox(mDFInfo.Fields["Employee_ID"],
                                           DataKind.授权员工全部));

        config.AddAfter("Date", "ID");
        config.AddAfter("AccountingUnit_ID", "Date");
        config.AddAfter("Department_ID", "AccountingUnit_ID");
        config.AddAfter("Employee_ID", "Department_ID");
        config.Add("ProductionUnit_ID");
      }));
    }

    protected override void AddDFBrowseGridColumn(DFBrowseGrid grid, string field) {
      base.AddDFBrowseGridColumn(grid, field);
      if (field == "BillState") {
        AddDFBrowseGridColumn(grid, "Date");
        AddDFBrowseGridColumn(grid, "AccountingUnit_Name");
        AddDFBrowseGridColumn(grid, "Department_Name");
        AddDFBrowseGridColumn(grid, "Employee_Name");

        AddDFBrowseGridColumn(grid, "ProductionUnit_Name");
      }
    }

    protected override void InitToolBar(HLayoutPanel toolbar)
    {
      base.InitToolBar(toolbar);
      if (User.IsInRole("B3Butchery.报表.生产完工分析")) {
        var dataAnysBtn = new TSButton {
          Text = "数据分析",
          UseSubmitBehavior = false,
          OnClientClick = string.Format("OpenUrlInTopTab('{0}','生产完工分析');return false;", WpfPageUrl.ToGlobal(AspUtil.AddTimeStampToUrl("~/B3Butchery/Reports/ProduceFinishReport_/ProduceFinishReport.aspx")))
        };
        toolbar.Add(dataAnysBtn);
      }
    }

  }
}