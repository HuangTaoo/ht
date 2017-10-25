using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using TSingSoft.WebControls2;

namespace BWP.Web.Pages.B3Butchery.BaseInfos.CalculateGoods_
{
  class CalculateGoodsList : DomainBaseInfoListPage<CalculateGoods, ICalculateGoodsBL>
  {
    protected override void AddQueryControls(VLayoutPanel vPanel)
    {
      vPanel.Add(CreateDefaultBaseInfoQueryControls((panel, config) =>
      {
        config.Add("Code");
        config.Add("CalculateCatalog_ID");
        config.Add("Goods_ID");

      }));
    }

    protected override void AddDFBrowseGridColumn(DFBrowseGrid grid, string field)
    {
      base.AddDFBrowseGridColumn(grid, field);
      if (field == "ID")
      {
        AddDFBrowseGridColumn(grid, "Code");
        AddDFBrowseGridColumn(grid, "CalculateCatalog_Name");
        AddDFBrowseGridColumn(grid, "MainUnit");
        AddDFBrowseGridColumn(grid, "SecondUnit");
        AddDFBrowseGridColumn(grid, "DefaultNumber1");
        AddDFBrowseGridColumn(grid, "Goods_Name");
      }
    }
  }
}