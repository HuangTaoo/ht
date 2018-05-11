using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using TSingSoft.WebControls2;

namespace BWP.Web.Pages.B3Butchery.Bills.PackingBagType_
{
  class PackingBagTypeList : DomainBillListPage<PackingBagType, IPackingBagTypeBL>
  {
    protected override void AddQueryControls(VLayoutPanel vPanel)
    {
      vPanel.Add(CreateDefaultBillQueryControls((panel, config) =>
      {
        config.AddAfter("Department_ID", "ID");
        config.AddAfter("Employee_ID", "Department_ID");
        config.AddAfter("Name", "Employee_ID");
        config.AddAfter("DisplayMark", "Name");
        config.AddAfter("Packing_Attr", "DisplayMark");
        config.AddAfter("Packing_Pattern", "Packing_Attr"); 
        config.AddAfter("ProductShift_Name", "Packing_Pattern");
          config.AddAfter("Abbreviation", "ProductShift_Name");


          //config.AddAfter("InStoreDate", "Date");

      }));
    }

    protected override void AddDFBrowseGridColumn(DFBrowseGrid grid, string field)
    {
      base.AddDFBrowseGridColumn(grid, field);
      if (field == "ID")
      {
        AddDFBrowseGridColumn(grid, "Name");
        AddDFBrowseGridColumn(grid, "Department_Name");
        AddDFBrowseGridColumn(grid, "Employee_Name");
        AddDFBrowseGridColumn(grid, "DisplayMark");
        AddDFBrowseGridColumn(grid, "Packing_Attr");
        AddDFBrowseGridColumn(grid, "Packing_Pattern");
        AddDFBrowseGridColumn(grid, "ProductShift_Name");
        AddDFBrowseGridColumn(grid, "Abbreviation");
      }
    }
  }
}
