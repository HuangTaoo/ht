using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using TSingSoft.WebControls2;

namespace BWP.Web.Pages.B3Butchery.Bills.FrozenInStoreSetBill_
{
  class FrozenInStoreSetBillList : DomainBillListPage<FrozenInStoreSetBill, IFrozenInStoreSetBillBL>
  {
    protected override void AddQueryControls(VLayoutPanel vPanel)
    {
      vPanel.Add(CreateDefaultBillQueryControls((panel, config) =>
      {
        config.Add("AccountingUnit_ID");
        config.Add("Department_ID");
        config.Add("Name");
        config.Add("Date");
        config.Add("WorkshopCategory_ID");
      }));
    }

    protected override void AddDFBrowseGridColumn(DFBrowseGrid grid, string field)
    {
      base.AddDFBrowseGridColumn(grid, field);
      if (field == "BillState")
      {
        AddDFBrowseGridColumn(grid, "AccountingUnit_Name");
        AddDFBrowseGridColumn(grid, "Department_Name");
        AddDFBrowseGridColumn(grid, "Name");
        AddDFBrowseGridColumn(grid, "Date");
        AddDFBrowseGridColumn(grid, "WorkshopCategory_Name");
        AddDFBrowseGridColumn(grid, "CheckUser_Name");
        AddDFBrowseGridColumn(grid, "Remark");
      }
    }
  }
}
