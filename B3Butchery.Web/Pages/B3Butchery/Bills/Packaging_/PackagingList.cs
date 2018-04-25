using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSingSoft.WebControls2;

namespace BWP.Web.Pages.B3Butchery.Bills.Packaging_
{
  class PackagingList : DomainBillListPage<Packaging, IPackagingBL>
  {
    protected override void AddQueryControls(VLayoutPanel vPanel)
    {
      vPanel.Add(CreateDefaultBillQueryControls((panel, config) =>
      {
        config.AddAfter("Name", "ID");

        config.AddAfter("Packing_Attr", "Name");
      }));
    }
    protected override void AddDFBrowseGridColumn(DFBrowseGrid grid, string field)
    {
      base.AddDFBrowseGridColumn(grid, field);
      if (field == "ID")
      {
        AddDFBrowseGridColumn(grid, "Name");
        AddDFBrowseGridColumn(grid, "Employee_Name");

        AddDFBrowseGridColumn(grid, "Packing_Attr");
      }
    }
  }
}
