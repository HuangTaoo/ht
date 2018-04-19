using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSingSoft.WebControls2;

namespace BWP.Web.Pages.B3Butchery.BaseInfos.DeviceManage_
{
  class DeviceManageList : DomainBaseInfoListPage<DeviceManage, IDeviceManageBL>
  {

    //显示查询条件个数
    //protected override void AddQueryControls(VLayoutPanel vPanel)
    //{
    //  vPanel.Add(CreateDefaultBaseInfoQueryControls((panel, config) =>
    //  {
    //    config.Add("Code");
    //    config.Add("CalculateCatalog_ID");
    //    config.Add("Goods_ID");

    //  }));
    //}

    //查询结果显示字段列
    protected override void AddDFBrowseGridColumn(DFBrowseGrid grid, string field)
    {
      base.AddDFBrowseGridColumn(grid, field);
      if (field == "Name")
      {
        AddDFBrowseGridColumn(grid, "IP");
      }
    }
  }
}
