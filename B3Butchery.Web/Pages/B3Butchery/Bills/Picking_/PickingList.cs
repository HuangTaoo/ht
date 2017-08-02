using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using BWP.Web.Layout;
using TSingSoft.WebControls2;

namespace BWP.Web.Pages.B3Butchery.Bills.Picking_
{
  public class PickingList : DomainBillListPage<Picking, IPickingBL>
  {
    protected override void AddQueryControls(VLayoutPanel vPanel)
    {
      vPanel.Add(CreateDefaultBillQueryControls((panel, config) =>
      {
        config.Add("AccountingUnit_ID");
        config.Add("Department_ID");
        config.Add("Employee_ID");
        config.Add("Date");
        config.Add("Store_ID");
        config.Add("ProductLine_ID");
        AddCustomerQueryControls(config);
      }));
    }

    protected virtual void AddCustomerQueryControls(AutoLayoutConfig config)
    {
      
    }

    protected override void AddDFBrowseGridColumn(DFBrowseGrid grid, string field)
    {
      base.AddDFBrowseGridColumn(grid, field);
      if (field == "BillState")
      {
        AddDFBrowseGridColumn(grid, "AccountingUnit_Name");
        AddDFBrowseGridColumn(grid, "Department_Name");
        AddDFBrowseGridColumn(grid, "Employee_Name");
        AddDFBrowseGridColumn(grid, "Date");
        AddDFBrowseGridColumn(grid, "ProductLine_Name");
        AddDFBrowseGridColumn(grid, "Store_Name");
        AddCustomerDFBrowseGridColumn(grid);
        AddDFBrowseGridColumn(grid, "Remark");
      }
    }

    protected virtual void AddCustomerDFBrowseGridColumn(DFBrowseGrid grid)
    {
      
    }
  }
}
