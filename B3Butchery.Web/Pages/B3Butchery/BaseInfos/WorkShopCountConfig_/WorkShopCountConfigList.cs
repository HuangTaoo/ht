using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSingSoft.WebControls2;

namespace BWP.Web.Pages.B3Butchery.BaseInfos.WorkShopCountConfig_
{



    class WorkShopCountConfigList : DomainBaseInfoListPage<WorkShopCountConfig, IWorkShopCountConfigBL>
    {

    protected override void AddQueryControls(VLayoutPanel vPanel)
    {


      vPanel.Add(CreateDefaultBaseInfoQueryControls((panel, config) =>
      {
        config.Add("WorkshopCategory_ID");
        config.Add("No");
      }));
    }

    protected override void AddDFBrowseGridColumn(DFBrowseGrid grid, string field)
    {

      base.AddDFBrowseGridColumn(grid, field);
      if (field == "Name")
      {
        AddDFBrowseGridColumn(grid, "WorkshopCategory_Name");
        AddDFBrowseGridColumn(grid, "No");
      }
    }

  }
}
