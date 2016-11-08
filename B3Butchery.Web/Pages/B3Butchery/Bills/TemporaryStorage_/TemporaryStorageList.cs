using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using BWP.B3Butchery.Utils;
using BWP.B3Frameworks;
using BWP.Web.Utils;
using TSingSoft.WebControls2;

namespace BWP.Web.Pages.B3Butchery.Bills.TemporaryStorage_
{
  public class TemporaryStorageList : DomainBillListPage<TemporaryStorage, ITemporaryStorageBL>
  {
    protected override void AddQueryControls(VLayoutPanel vPanel)
    {
      vPanel.Add(CreateDefaultBillQueryControls((panel, config) =>
      {
        panel.Add("AccountingUnit_ID", QueryCreator.DFChoiceBox(mDFInfo.Fields["AccountingUnit_ID"], B3FrameworksConsts.DataSources.授权会计单位全部));
        panel.Add("Department_ID", QueryCreator.DFChoiceBox(mDFInfo.Fields["Department_ID"], B3FrameworksConsts.DataSources.授权部门全部));
        config.AddAfter("AccountingUnit_ID", "ID");
        config.AddAfter("Department_ID", "AccountingUnit_ID");
        config.AddAfter("Employee_ID", "Department_ID");
        config.AddAfter("ProductPlan_ID", "Employee_ID");
        config.AddAfter("Date", "ProductPlan_ID");
        config.AddAfter("TemporaryStorageType", "Date");
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
        AddDFBrowseGridColumn(grid, "ProductPlan_Name");
        AddDFBrowseGridColumn(grid, "Date");
        AddDFBrowseGridColumn(grid, "TemporaryStorageType");
      }
    }
  }
}
