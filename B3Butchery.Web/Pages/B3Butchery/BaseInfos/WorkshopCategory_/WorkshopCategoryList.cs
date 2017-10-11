using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using TSingSoft.WebControls2;

namespace BWP.Web.Pages.B3Butchery.BaseInfos.WorkshopCategory_
{
  class WorkshopCategoryList : DomainBaseInfoListPage<WorkshopCategory, IWorkshopCategoryBL>
  {
    protected override void AddDFBrowseGridColumn(DFBrowseGrid grid, string field)
    {
      base.AddDFBrowseGridColumn(grid, field);
      if (field == "Name")
      {
        AddDFBrowseGridColumn(grid, "IfWeight");
      }
    }
  }
}
