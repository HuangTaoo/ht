using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using TSingSoft.WebControls2;
using BWP.Web.Utils;
using DataKind = BWP.B3Frameworks.B3FrameworksConsts.DataSources;
using TSingSoft.WebPluginFramework;
using BWP.B3Butchery.Utils;

namespace BWP.Web.Pages.B3Butchery.Bills.ProduceOutput_
{
  class ProduceOutputList : DomainBillListPage<ProduceOutput, IProduceOutputBL>
  {

    protected override void AddQueryControls(VLayoutPanel vPanel)
    {
      vPanel.Add(CreateDefaultBillQueryControls((panel, config) =>
      {
        panel.Add("Time", QueryCreator.DateRange(mDFInfo.Fields["Time"], mQueryContainer, "MinTime", "MaxTime"));
        panel.Add("AccountingUnit_ID", QueryCreator.DFChoiceBox(mDFInfo.Fields["AccountingUnit_ID"], DataKind.授权会计单位全部));
        panel.Add("Department_ID", QueryCreator.DFChoiceBox(mDFInfo.Fields["Department_ID"], DataKind.授权部门全部));
        panel.Add("Employee_ID", QueryCreator.DFChoiceBox(mDFInfo.Fields["Employee_ID"], DataKind.授权员工全部));
        panel.Add("PlanNumber_ID", new SimpleLabel("计划号"), QueryCreator.DFChoiceBox(mDFInfo.Fields["PlanNumber_ID"], B3ButcheryDataSource.计划号));
        config.AddAfter("Time", "ID");
        config.AddAfter("AccountingUnit_ID", "Time");
        config.AddAfter("Department_ID", "AccountingUnit_ID");
        config.AddAfter("Employee_ID", "Department_ID");
        config.AddAfter("PlanNumber_ID", "Employee_ID");
      }));
    }

    protected override void AddDFBrowseGridColumn(DFBrowseGrid grid, string field)
    {
      base.AddDFBrowseGridColumn(grid, field);
      if (field == "BillState")
      {
        AddDFBrowseGridColumn(grid, "AccountingUnit_Name");
        AddDFBrowseGridColumn(grid, "Department_Name");
        AddDFBrowseGridColumn(grid, "Employee_Name");
        AddDFBrowseGridColumn(grid, "Time");
      }
    }
    protected override void InitToolBar(HLayoutPanel toolbar)
    {
      base.InitToolBar(toolbar);
      if (User.IsInRole("B3Butchery.报表.产出单分析"))
      {
        var dataAnysBtn = new TSButton
        {
          Text = "数据分析",
          UseSubmitBehavior = false,
          OnClientClick = string.Format("OpenUrlInTopTab('{0}','产出单分析');return false;", WpfPageUrl.ToGlobal(AspUtil.AddTimeStampToUrl("~/B3Butchery/Reports/ProduceOutputReport_/ProduceOutputReport.aspx")))
        };
        toolbar.Add(dataAnysBtn);
      }
    }

    public override bool EnableBatchCheck
    {
      get
      {
        return true;
      }
    }
  }
}
