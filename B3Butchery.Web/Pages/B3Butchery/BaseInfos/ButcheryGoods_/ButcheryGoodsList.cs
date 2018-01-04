using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using BWP.B3Frameworks.Utils;
using BWP.B3UnitedInfos.BO;
using BWP.Web.WebControls;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using Forks.EnterpriseServices.SqlDoms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using TSingSoft.WebControls2;
using TSingSoft.WebPluginFramework.Controls;
using TSingSoft.WebPluginFramework.Exports;

namespace BWP.Web.Pages.B3Butchery.BaseInfos.ButcheryGoods_
{


    class ButcheryGoodsList : BaseInfoListPage<ButcheryGoods, IButcheryGoodsBL>
    {
        protected override DQueryDom GetQueryDom()
        {
            var dom = base.GetQueryDom();
            var prop = new JoinAlias(typeof(GoodsProperty));
            dom.From.AddJoin(JoinType.Inner, new DQDmoSource(prop), DQCondition.EQ(prop, "ID", dom.From.RootSource.Alias, "GoodsProperty_ID"));
 
            DomainUtil.AddDomainPermissionLimit(dom, typeof(GoodsProperty), prop);
            return dom;
        }

        protected override void AddQueryControls(VLayoutPanel vPanel)
        {
            vPanel.Add(CreateDefaultBaseInfoQueryControls((layoutManager, config) =>
            {
                config.AddAfter("GoodsProperty_ID", "ID");

     

            }));
        }

        protected override void AddDFBrowseGridColumn(DFBrowseGrid grid, string field)
        {
            base.AddDFBrowseGridColumn(grid, field);
            if (field == "Name")
            {
                AddDFBrowseGridColumn(grid, "Brand");
                AddDFBrowseGridColumn(grid, "ProductLine_ID");
  

            }
        }

        protected override void AddGrid(Control vbox)
        {
            base.AddGrid(vbox);
            AddResultControls(vbox);
        }

        private CheckBoxListWithReverseSelect _list;

        readonly DmoInfo _dmoInfo = DmoInfo.Get(typeof(ButcheryGoods));
        void AddResultControls(Control vPanel)
        {
            vPanel.Controls.Add(new LiteralControl("选择要导出到Excel的字段"));
            _list = new CheckBoxListWithReverseSelect() { RepeatColumns = 6 };
            _list.RepeatDirection = RepeatDirection.Horizontal;
            var fields = new[] { "ID",  "Name",  "PrintShortName","Code", "Spec", "GoodsProperty_Name",
      "Feature", "Origin", "Brand","ProductLine_Name", "TaxRate", "MainUnit", "SecondUnit", "MainUnitRatio", "SecondUnitRatio", "UnitConvertDirection", "Barcode" , "OuterCode" , "SecondUnitII" , "SecondUnitII_MainUnitRatio" , "SecondUnitII_SecondUnitRatio", "Remark","属性分类"};

            foreach (string field in fields)
            {
                IDmoFieldInfo dmoFieldInfo;

                if (field == "属性分类")
                {
                    var item = new ListItem();
                    item.Text = field;
                    item.Value = "属性分类";
                    item.Selected = true;
                    _list.Items.Add(item);
                    continue;
                }
                if (!_dmoInfo.Fields.TryGetValue(field, out dmoFieldInfo))
                    continue;
                var dfField = mDFInfo.Fields.FirstOrDefault(x => field == x.Name);
                AddCheckBoxField(dfField, _list);
            }
            vPanel.Controls.Add(_list);
            HLayoutPanel hPanel = new HLayoutPanel();
            vPanel.Controls.Add(hPanel);
            var button = new LinkButton();
            button.Text = "导出到Excel";
            hPanel.Add(button);
            var exporter = new Exporter();
            hPanel.Add(exporter);
            button.Click += delegate
            {
                var dom = GetQueryDom();
                dom.OrderBy.Expressions.Clear();
                dom.Columns.Clear();
                var alias = dom.From.RootSource.Alias;
                foreach (ListItem item in _list.Items)
                {
                    if (item.Selected)
                    {
                        if (item.Text == "属性分类")
                        {
                            var goodsPropertyAlias = new JoinAlias("gpy", typeof(GoodsProperty));
                            dom.From.AddJoin(JoinType.Left, new DQDmoSource(goodsPropertyAlias), DQCondition.EQ(dom.From.RootSource.Alias, "GoodsProperty_ID", goodsPropertyAlias, "ID"));

                            dom.Columns.Add(DQSelectColumn.Field("GoodsPropertyCatalog_Name", goodsPropertyAlias, "属性分类"));
                        }
                        else
                            dom.Columns.Add(DQSelectColumn.Field(item.Value, item.Text));
                    }
                }
                dom.OrderBy.Expressions.Add(DQOrderByExpression.Create(alias, "ID", true));
                exporter.Export(new DQueryExcelExporter(LogicName + ".xls", new LoadArguments(dom)));
            };
        }

        static void AddCheckBoxField(IDFFieldCore fieldInfo, ListControl list)
        {
            if (fieldInfo == null)
                return;
            var item = new ListItem();
            item.Text = fieldInfo.Prompt;
            item.Value = fieldInfo.Name;
            item.Selected = true;
            list.Items.Add(item);
        }

    }
}
