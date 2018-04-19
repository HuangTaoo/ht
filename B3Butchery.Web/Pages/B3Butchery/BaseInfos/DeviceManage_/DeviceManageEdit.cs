using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using BWP.Web.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace BWP.Web.Pages.B3Butchery.BaseInfos.DeviceManage_
{
  class DeviceManageEdit : DomainBaseInfoEditPage<DeviceManage, IDeviceManageBL>
  {
    protected override void BuildBody(Control parent)
    {
      var layoutManager = new LayoutManager("", mDFInfo, mDFContainer);
      var config = new AutoLayoutConfig();
      config.Add("Name");
      config.Add("Code");
      config.Add("Remark");
      config.Add("IP");
      layoutManager.Config = config;
      parent.Controls.Add(layoutManager.CreateLayout());
    }
  }
}
