using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSingSoft.WebControls2;

namespace BWP.Web.Pages.B3Butchery.Bills.FrozenInStore_
{
  class FrozenInStoreList : DomainBillListPage<FrozenInStore, IFrozenInStoreBL>
  {
    protected override void AddQueryControls(VLayoutPanel vPanel) {
      vPanel.Add(CreateDefaultBillQueryControls((panel, config) =>
      {
        config.Add("AccountingUnit_ID");
        config.Add("Department_ID");
        config.Add("Employee_ID");
        config.Add("Date");
        config.Add("Store_ID");
        config.Add("OtherInStoreType_ID");
        config.Add("ProductionPlan_ID");

      }));
    }

    protected override void AddDFBrowseGridColumn(DFBrowseGrid grid, string field) {
      base.AddDFBrowseGridColumn(grid, field);
      if (field == "BillState")
      {
        AddDFBrowseGridColumn(grid, "AccountingUnit_Name");
        AddDFBrowseGridColumn(grid, "Department_Name");
        AddDFBrowseGridColumn(grid, "Employee_Name");
        AddDFBrowseGridColumn(grid, "Date");
        AddDFBrowseGridColumn(grid, "Store_Name");
        AddDFBrowseGridColumn(grid, "OtherInStoreType_Name");
        AddDFBrowseGridColumn(grid, "ProductionPlan_PlanNumber");
        AddDFBrowseGridColumn(grid, "CheckUser_Name");
        AddDFBrowseGridColumn(grid, "Remark");
      }
    }
  }
}
