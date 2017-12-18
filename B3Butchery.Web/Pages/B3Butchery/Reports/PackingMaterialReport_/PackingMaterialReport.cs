using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using TSingSoft.WebControls2;
using TSingSoft.WebPluginFramework.Controls;
using TSingSoft.WebPluginFramework.Security;

namespace BWP.Web.Pages.B3Butchery.Reports.PackingMaterialReport_
{
    class PackingMaterialReport : AppBasePage
    {
        protected override void InitForm(HtmlForm form)
        {
            if (!User.IsInRole("B3Butchery.报表.班组包材领用测算表"))
                throw new AppSecurityException();
            form.Controls.Add(new PageTitle("在职员工考勤表"));
            var vPanel = form.EAdd(new VLayoutPanel());
            CreateQueryPanel(vPanel);
        }


        DFDateTimeInput dateInput;
        DFDateTimeInput enddateInput;
        private void CreateQueryPanel(VLayoutPanel vPanel)
        {
            var tablePanel = vPanel.Add(new TableLayoutPanel(5, 5), new VLayoutOption(System.Web.UI.WebControls.HorizontalAlign.Justify));
            var row = 0;
            const int labelWidth = 4;
            tablePanel.Add(0,1,row, row + 1,new SimpleLabel("日期",labelWidth));
            dateInput = tablePanel.Add(1, 2, row, ++row, new DFDateTimeInput() { Date = DateTime.Today });
            tablePanel.Add(1, 2, row, ++ row, new LiteralControl("~"));
            enddateInput = tablePanel.Add(1, 2, row, ++ row, new DFDateTimeInput() { Date = DateTime.Today.AddDays(1).AddSeconds(-1)});
            tablePanel.Add(0,1,row, row + 1,new SimpleLabel("班组"));
            tablePanel.Add(1, 2,row, ++ row, CreateShiftPart());

            tablePanel.Add(0, 1, row, row + 1, new SimpleLabel("装箱模式"));
            tablePanel.Add(1, 2, row, ++row, CreatePackModePart());

        }


        FilterTree shiftFilterTree;
        FilterTreeContainer mTreeContainer = new FilterTreeContainer();
        //班组
        private Control CreateShiftPart()
        {
            shiftFilterTree = new FilterTree();
            shiftFilterTree.HorizontalRepeatColumns = 10;
            this.Load += delegate
            {
                if (!IsPostBack)
                {
                    var rootNode = shiftFilterTree.DataSource;

                }
            };
            return shiftFilterTree;


        }

        //班组
        private Control CreatePackModePart()
        {
            throw new NotImplementedException();
        }
    }
}
