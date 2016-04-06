using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSingSoft.WebControls2;
using BWP.Web.Utils;
using BWP.B3Butchery.BO;
using BWP.B3Butchery.BL;
using BWP.B3Butchery.Utils;
using DataKind = BWP.B3Frameworks.B3FrameworksConsts.DataSources;

namespace BWP.Web.Pages.B3Butchery.BaseInfos.ProductLinks_
{
  class ProductLinksList : DomainBaseInfoListPage<ProductLinks, IProductLinksBL>
  {
    protected override void AddQueryControls(VLayoutPanel vPanel)
    {
      vPanel.Add(CreateDefaultBaseInfoQueryControls((panel, config) =>
      {
        panel.Add("ProductLine_ID", QueryCreator.DFChoiceBox(mDFInfo.Fields["ProductLine_ID"], B3ButcheryDataSource.生产线));
        panel.Add("ChargePerson_ID", QueryCreator.DFChoiceBox(mDFInfo.Fields["ChargePerson_ID"], DataKind.授权员工全部));
        config.AddBefore("ProductLine_ID", "Remark");
        config.AddBefore("ChargePerson_ID", "ProductLine_ID");
      }));
    }

    protected override void AddDFBrowseGridColumn(DFBrowseGrid grid, string field)
    {
      base.AddDFBrowseGridColumn(grid, field);
      if (field == "Name")
      {
        AddDFBrowseGridColumn(grid, "ProductLine_Name");
        AddDFBrowseGridColumn(grid, "ChargePerson_Name");
      }
    }
  }
}
