using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSingSoft.WebControls2;

namespace BWP.Web.Pages.B3Butchery.BaseInfos.FrozenStore_
{
  class FrozenStoreList : DomainBaseInfoListPage<FrozenStore, IFrozenStoreBL>
  {
    protected override void AddQueryControls(VLayoutPanel vPanel) {
      vPanel.Add(CreateDefaultBaseInfoQueryControls((panel, config) =>
      {
        config.AddAfter("Code", "ID");
        
      }));
    }

    protected override void AddDFBrowseGridColumn(DFBrowseGrid grid, string field) {
      base.AddDFBrowseGridColumn(grid, field);
      if (field == "ID")
      {
        AddDFBrowseGridColumn(grid, "Code");
      }
    }
  }
}
