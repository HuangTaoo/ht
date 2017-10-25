using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using BWP.Web.Layout;

namespace BWP.Web.Pages.B3Butchery.BaseInfos.CalculateGoods_
{
  class CalculateGoodsEdit : DomainBaseInfoEditPage<CalculateGoods, ICalculateGoodsBL>
  {
    protected override void BuildBody(Control parent)
    {
      var layoutManager = new LayoutManager("", mDFInfo, mDFContainer);

      var config = new AutoLayoutConfig();
      config.Add("Name");
      config.Add("Code");
      config.Add("CalculateCatalog_ID");
      config.Add("MainUnit");
      config.Add("SecondUnit");
      config.Add("MainUnitRatio");
      config.Add("SecondUnitRatio");
      config.Add("UnitConvertDirection");
      config.Add("SecondUnitII");
      config.Add("SecondUnitII_MainUnitRatio");
      config.Add("SecondUnitII_SecondUnitRatio");

      config.Add("DefaultNumber1");
      config.Add("Goods_ID");
      config.Add("Remark");
      layoutManager.Config = config;
      parent.Controls.Add(layoutManager.CreateLayout());
    }
  }
}
