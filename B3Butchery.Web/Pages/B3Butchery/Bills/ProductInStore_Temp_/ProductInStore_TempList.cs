using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Butchery.BO;
using BWP.B3Butchery.BL;
using TSingSoft.WebControls2;

namespace BWP.Web.Pages.B3Butchery.Bills.ProductInStore_Temp_
{
  public class ProductInStore_TempList : DomainBaseInfoListPage<ProductInStore_Temp, IProductInStore_TempBL>
  {
    protected override void AddQueryControls(VLayoutPanel vPanel)
    {
      vPanel.Add(CreateDefaultBaseInfoQueryControls((panel, config) =>
      {
        config.AddAfter("AccountingUnit_ID", "ID");
        config.AddAfter("Department_ID", "AccountingUnit_ID");
        config.AddAfter("Employee_ID", "Department_ID");
        config.AddAfter("Store_ID", "Employee_ID");
        config.AddAfter("InStoreType_ID", "Store_ID");
        config.AddAfter("InStoreDate", "InStoreType_ID");
        config.AddAfter("CheckEmployee_ID", "InStoreDate");
        config.AddAfter("CheckDate", "CheckEmployee_ID");     
      }));
    }

    protected override void AddDFBrowseGridColumn(DFBrowseGrid grid, string field)
    {
      base.AddDFBrowseGridColumn(grid, field);
      if (field == "Name")
      {
        AddDFBrowseGridColumn(grid, "AccountingUnit_Name");
        AddDFBrowseGridColumn(grid, "Department_Name");
        AddDFBrowseGridColumn(grid, "Employee_Name");
        AddDFBrowseGridColumn(grid, "Store_Name");
        AddDFBrowseGridColumn(grid, "InStoreType_Name");
        AddDFBrowseGridColumn(grid, "InStoreDate");
        AddDFBrowseGridColumn(grid, "CheckEmployee_Name");
        AddDFBrowseGridColumn(grid, "CheckDate");
      }
    }
  }
}
