using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using BWP.Web.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace BWP.Web.Pages.B3Butchery.BaseInfos.ProductShift_
{


    class ProductShiftEdit : DomainBaseInfoEditPage<ProductShift, IProductShiftBL>
    {
        protected override void BuildBody(Control parent)
        {
            var layoutManager = new LayoutManager("", mDFInfo, mDFContainer);

            var config = new AutoLayoutConfig();
            config.Add("Name");
            config.Add("Remark");
            layoutManager.Config = config;
            parent.Controls.Add(layoutManager.CreateLayout());
        }
    }
}
