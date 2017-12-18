using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using BWP.Web.Layout;

namespace BWP.Web.Pages.B3Butchery.BaseInfos.GoodsCategory_
{
  class GoodsCategoryEdit : DomainBaseInfoEditPage<GoodsCategory, IGoodsCategoryBL>
  {
    protected override void BuildBody(Control parent)
    {
      var layoutManager = new LayoutManager("", mDFInfo, mDFContainer);

      var config = new AutoLayoutConfig();
      config.Add("Name");
      config.Add("CategoryClassification_ID");
      config.Add("Remark");
      layoutManager.Config = config;
      parent.Controls.Add(layoutManager.CreateLayout());
    }
  }
}
