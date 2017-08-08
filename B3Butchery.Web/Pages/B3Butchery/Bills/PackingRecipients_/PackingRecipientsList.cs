using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using BWP.B3Butchery.Utils;
using BWP.B3Frameworks;
using BWP.Web.Layout;
using TSingSoft.WebControls2;
using TSingSoft.WebPluginFramework;

namespace BWP.Web.Pages.B3Butchery.Bills.PackingRecipients_
{
  class PackingRecipientsList : DomainBillListPage<PackingRecipients, IPackingRecipientsBL>
  {

    protected override void AddQueryControls(VLayoutPanel vPanel)
    {
      vPanel.Add(CreateDefaultBillQueryControls((panel, config) =>
      {
        config.AddAfter("AccountingUnit_ID", "ID");
        config.AddAfter("Department_ID", "AccountingUnit_ID");
        config.AddAfter("Employee_ID", "Department_ID");
        config.AddAfter("Store_ID", "Employee_ID");
        config.AddAfter("Date", "Store_ID");
//        config.AddAfter("InStoreDate", "Date");

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
        AddDFBrowseGridColumn(grid, "Store_Name");
        AddDFBrowseGridColumn(grid, "Date");
        AddDFBrowseGridColumn(grid, "Remark");
      }
    }

    protected override void InitToolBar(HLayoutPanel toolbar)
    {
      base.InitToolBar(toolbar);
      if (User.IsInRole("B3Butchery.包装领用.访问"))
      {
        var dataAnysBtn = new TSButton() { Text = "数据分析", UseSubmitBehavior = false };
        dataAnysBtn.OnClientClick = string.Format("OpenUrlInTopTab('{0}','包装领用分析');return false;", WpfPageUrl.ToGlobal(AspUtil.AddTimeStampToUrl("~/B3Butchery/Reports/PackingRecipientsAnalyse_/PackingRecipientsAnalyse.aspx")));
        toolbar.Add(dataAnysBtn);
      }
    }

  }
}
