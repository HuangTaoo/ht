using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using TSingSoft.WebControls2;

namespace BWP.Web.Pages.B3Butchery.Bills.PackagingTransfer_
{
  class PackagingTransferList : DomainBillListPage<PackagingTransfer, IPackagingTransferBL>
  {
    protected override void AddQueryControls(VLayoutPanel vPanel)
    {
      vPanel.Add(CreateDefaultBillQueryControls((panel, config) =>
      {
        config.Add("AccountingUnit_ID");
        config.Add("Date");
        config.Add("OutDepartment_ID");
        config.Add("InDepartment_ID");
      }));
    }

    protected override void AddDFBrowseGridColumn(DFBrowseGrid grid, string field)
    {
      base.AddDFBrowseGridColumn(grid, field);
      if (field == "BillState")
      {
        AddDFBrowseGridColumn(grid, "AccountingUnit_Name");
        AddDFBrowseGridColumn(grid, "Date");
        AddDFBrowseGridColumn(grid, "InDepartment_Name");
        AddDFBrowseGridColumn(grid, "OutDepartment_Name");
        AddDFBrowseGridColumn(grid, "Remark");
      }
    }

  }
}
