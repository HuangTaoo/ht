using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataKind = BWP.B3Frameworks.B3FrameworksConsts.DataSources;
using BWP.B3Butchery.BO;
using BWP.B3Butchery.BL;
using TSingSoft.WebControls2;
using BWP.Web.Utils;

namespace BWP.Web.Pages.B3Butchery.Bills.ProductPlan_
{
  class ProductPlanList : DomainBillListPage<ProductPlan, IProductPlanBL>
  {
    protected override void AddQueryControls(VLayoutPanel vPanel)
    {
      vPanel.Add(CreateDefaultBillQueryControls((panel, config) =>
      {
        panel.Add("Date", QueryCreator.DateRange(mDFInfo.Fields["Date"], mQueryContainer, "MinDate", "MaxDate"));
        panel.Add("AccountingUnit_ID", QueryCreator.DFChoiceBox(mDFInfo.Fields["AccountingUnit_ID"], DataKind.授权会计单位全部));
        panel.Add("Department_ID", QueryCreator.DFChoiceBox(mDFInfo.Fields["Department_ID"], DataKind.授权部门全部));
        panel.Add("Employee_ID", QueryCreator.DFChoiceBox(mDFInfo.Fields["Employee_ID"], DataKind.授权员工全部));

        config.AddAfter("Date", "ID");
        config.AddAfter("AccountingUnit_ID", "Date");
        config.AddAfter("Department_ID", "AccountingUnit_ID");
        config.AddAfter("Employee_ID", "Department_ID");
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
        AddDFBrowseGridColumn(grid, "Date");
      }
    }
  }
}
