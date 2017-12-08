using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Butchery.BL;

using BWP.B3Butchery.BO;
using TSingSoft.WebControls2;
using TSingSoft.WebPluginFramework;

namespace BWP.Web.Pages.B3Butchery.Bills.FrozenOutStore_
{


    class FrozenOutStoreList : DomainBillListPage<FrozenOutStore, IFrozenOutStoreBL>
    {
        protected override void AddQueryControls(VLayoutPanel vPanel)
        {
            vPanel.Add(CreateDefaultBillQueryControls((panel, config) =>
            {
                config.Add("AccountingUnit_ID");
                config.Add("Department_ID");
                config.Add("Employee_ID");
                config.Add("Date");


            }));
        }

        protected override void AddDFBrowseGridColumn(DFBrowseGrid grid, string field)
        {
            base.AddDFBrowseGridColumn(grid, field);
            if (field == "BillState")
            {
                AddDFBrowseGridColumn(grid, "AccountingUnit_Name");
                AddDFBrowseGridColumn(grid, "Department_Name");
                AddDFBrowseGridColumn(grid, "Employee_Name");
                AddDFBrowseGridColumn(grid, "Date");
    
                AddDFBrowseGridColumn(grid, "Remark");
            }
        }

        protected override void InitToolBar(HLayoutPanel toolbar)
        {
            base.InitToolBar(toolbar);
//            if (User.IsInRole("B3Butchery.报表.速冻入库分析"))
//            {
//                var dataAnysBtn = new TSButton() { Text = "数据分析", UseSubmitBehavior = false };
//                dataAnysBtn.OnClientClick = string.Format("OpenUrlInTopTab('{0}','速冻入库分析');return false;", WpfPageUrl.ToGlobal(AspUtil.AddTimeStampToUrl("~/B3Butchery/Reports/FrozenOutStoreReport_/FrozenOutStoreReport.aspx")));
//                toolbar.Add(dataAnysBtn);
//            }
        }
    }
}
