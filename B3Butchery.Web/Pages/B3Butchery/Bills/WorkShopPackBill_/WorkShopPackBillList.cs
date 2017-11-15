using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Butchery.BL;

using BWP.B3Butchery.BO;
using TSingSoft.WebControls2;

namespace BWP.Web.Pages.B3Butchery.Bills.WorkShopPackBill_
{



    class WorkShopPackBillList : DomainBillListPage<WorkShopPackBill, IWorkShopPackBillBL>
    {
        protected override void AddQueryControls(VLayoutPanel vPanel)
        {
            vPanel.Add(CreateDefaultBillQueryControls((panel, config) =>
            {
                config.Add("AccountingUnit_ID");
                config.Add("Department_ID");
                config.Add("Date");
            }));
        }

        protected override void AddDFBrowseGridColumn(DFBrowseGrid grid, string field)
        {
            base.AddDFBrowseGridColumn(grid, field);
            if (field == "BillState")
            {
                AddDFBrowseGridColumn(grid, "AccountingUnit_Name");
                AddDFBrowseGridColumn(grid, "Department_Name");
                AddDFBrowseGridColumn(grid, "Date");
              
                AddDFBrowseGridColumn(grid, "Remark");
            }
        }
    }
}
