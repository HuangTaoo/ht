using BWP.B3Butchery.BO;
using BWP.B3Butchery.Utils;
using BWP.B3Frameworks;
using BWP.B3Frameworks.Utils;
using BWP.Web.Utils;
using BWP.Web.WebControls;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using Forks.EnterpriseServices.SqlDoms;
using Forks.Utils;
using Forks.Utils.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TSingSoft.WebControls2;
using TSingSoft.WebControls2.DFGrids;
using TSingSoft.WebPluginFramework;
using TSingSoft.WebPluginFramework.Controls;
using TSingSoft.WebPluginFramework.Exports;
using TSingSoft.WebPluginFramework.Security;

namespace BWP.Web.Pages.B3Butchery.Reports.PackingMaterialReport_
{
    class PackingMaterialReport : AppBasePage
    {
        protected override void InitForm(HtmlForm form)
        {
            if (!User.IsInRole("B3Butchery.报表.班组包材领用测算表"))
                throw new AppSecurityException();
            form.Controls.Add(new PageTitle("班组包材领用测算表"));
    
            CreateQueryPanel(form.EAdd(new TitlePanel("查询条件", "查询条件")));

            CreateDetailPanel(form.EAdd(new TitlePanel("查询结果", "查询结果")));
        }

        private void CreateDetailPanel(TitlePanel vPanel)
        {
            detailGrid = vPanel.EAdd(new DFBrowseGrid(new DFDataTableEditor()) { Width = Unit.Percentage(100) });
            detailGrid.Columns.EAdd(new DFBrowseGridColumn("Goods_Name"));
            detailGrid.Columns.Add(new DFBrowseGridColumn("PlanNumber_Name"));
            detailGrid.Columns.Add(new DFBrowseGridColumn("计数规格"));
            detailGrid.Columns.EAdd(new DFBrowseGridColumn("盘数")).SumMode = SumMode.Sum;
            detailGrid.Columns.EAdd(new DFBrowseGridColumn("袋数")).SumMode = SumMode.Sum;
            detailGrid.Columns.EAdd(new DFBrowseGridColumn("重量")).SumMode = SumMode.Sum;
            detailGrid.Columns.Add(new DFBrowseGridColumn("包装模式"));

      var section = mPageLayoutManager.AddSection("DetailColumns", "布局");
      section.ApplyLayout(detailGrid, mPageLayoutManager, DFInfo.Get(typeof(ProduceOutput_Detail)));
      vPanel.SetPageLayoutSetting(mPageLayoutManager, section.Name);


      InitToolBar(vPanel);
    }


    #region 创建ToolBar 导出和打印

    void InitToolBar(TitlePanel panel)
    {
      var toolbar = new HLayoutPanel();
      toolbar.CssClass += "LeftPaddingWrapper";
      panel.EAdd(toolbar);
      var exporter = new Exporter();
      toolbar.Add(new TSButton("导出到Excel", delegate {
        var lastQuery = detailGrid.LastQuery;
        if (lastQuery == null)
          throw new Exception("请先进行查询");
        var dom = new LoadArguments((DQueryDom)lastQuery.DQuery.Clone());
        foreach (var colIndex in lastQuery.SumColumns)
          dom.SumColumns.Add(colIndex);
        foreach (var colIndex in lastQuery.GroupSumColumns)
          dom.GroupSumColumns.Add(colIndex);
        dom.DQuery.Range = SelectRange.All;
        string fileName = "班组包材领用测算表.xlsx";
        exporter.Export(new QueryResultExcelExporter(fileName, GetQueryResult(dom)));
      }));
      toolbar.Add(exporter);

      var printBtn = new TSButton("打印");
      printBtn.OnClientClick = "preventEventDefault(event);if(window.__printUrl){Print(window.__printUrl);}else{alert('先搜索出结果才能打印');}";
      toolbar.Add(printBtn);
    }


    private QueryResult GetQueryResult(LoadArguments arg)
    {
      var data = new DFDataTableEditor().Load(arg);
      return new QueryResult(data.TotalCount, data.Data.Rows, data.Data.Columns, arg.SumColumns.Any() ? data.Data.SumRow : null);
    }


    #endregion



        DFDateTimeInput dateInput;
        DFDateTimeInput enddateInput;
        private void CreateQueryPanel(TitlePanel titlePanel)
        {
          var vPanel = titlePanel.EAdd(new VLayoutPanel());
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
                    var shiftInfo = GetShiftInfo();
                    foreach (var item in shiftInfo)
                    {
                        shiftFilterTree.DataSource.Childs.Add(new FilterTreeNode(item.Item2, item.Item1.ToString()));
                    }
                }
            };
            mTreeContainer.Add("shift", shiftFilterTree);
            shiftFilterTree.FilterAction = (query, node) =>
            {
                if (!string.IsNullOrEmpty(node.Value))
                {
                    var shiftID = long.Parse(node.Value);
                    query.Where.Conditions.Add(DQCondition.EQ("Goods_ProductShift_ID", shiftID));
                }
            };
            return shiftFilterTree;


        }

        private List<Tuple<long, string>> GetShiftInfo()
        {
            var query = new DQueryDom(new JoinAlias(typeof(ProductShift)));

            query.Columns.Add(DQSelectColumn.Field("ID"));
            query.Columns.Add(DQSelectColumn.Field("Name"));

            query.Where.Conditions.Add(DQCondition.EQ("Stopped", false));
            query.Where.Conditions.Add(DQCondition.EQ("Domain_ID", DomainContext.Current.ID));
            OrganizationUtil.AddOrganizationLimit(query, typeof(ProductShift));
           return query.EExecuteList<long, string>();
       
        }




        FilterTree packModeFilterTree;

        //包装模式
        private Control CreatePackModePart()
        {
            packModeFilterTree = new FilterTree();
            packModeFilterTree.HorizontalRepeatColumns = 10;
            this.Load += delegate
            {
                if (!IsPostBack)
                {
                    var rootNode = packModeFilterTree.DataSource;
                    var packModeInfo = GetPackModeInfo();
                    foreach (var item in packModeInfo)
                    {
                        packModeFilterTree.DataSource.Childs.Add(new FilterTreeNode(item.Value, item.Key.ToString()));
                    }
                }
            };
            mTreeContainer.Add("packMode", packModeFilterTree);
            packModeFilterTree.FilterAction = (query, node) =>
            {
                if (!string.IsNullOrEmpty(node.Value))
                {
                    var packMode = long.Parse(node.Value);
                    query.Where.Conditions.Add(DQCondition.EQ("Goods_PackageModel", packMode));
                }
            };


            mTreeContainer.Select += delegate
            {

              var shift = "";
              var packModel = "";
              var beginDate = "";
              var endDate = "";
              if (shiftFilterTree != null)
              {
                var shiftNode = shiftFilterTree.GetSelecteItem();
                 shift = shiftNode.Value;

              }
              if (packModeFilterTree != null)
              {
                var packNode = packModeFilterTree.GetSelecteItem();
                 packModel = packNode.Value;
              }
              if (dateInput.Value != null)
              {
                beginDate = dateInput.Value.ToString();
              }
              if (enddateInput.Value != null)
              {
                endDate = enddateInput.Value.ToString();
              }
              var printUrl = string.Format("PackingMaterialReportPrint.aspx?Shift={0}&PackMode={1}&BeginDate={2}&EndDate={3}", shift, packModel, beginDate, endDate);
              Page.ClientScript.RegisterStartupScript(this.GetType(), "__printUrl", string.Format("window.__printUrl='{0}';", printUrl), true);


              var query = GetQueryDom();
                mTreeContainer.AddConditions(query);

                var args = new LoadArguments(query);
                foreach (var item in detailSumIndex)
                    args.SumColumns.Add(item);
                foreach (var item in detailGroupSumIndex)
                    args.GroupSumColumns.Add(item);
                detailGrid.LoadArguments = args;
                detailGrid.DataBind();
            };
            return packModeFilterTree;
        }


        DFBrowseGrid  detailGrid;
        List<int> detailSumIndex = new List<int>();
        List<int> detailGroupSumIndex = new List<int>();
        private DQueryDom GetQueryDom()
        {

            var detail = new JoinAlias("__detail", typeof(ProduceOutput_Detail));
            var main = new JoinAlias("__main", typeof(ProduceOutput));
            var goods = new JoinAlias("__goods", typeof(ButcheryGoods));

            var query = new DQueryDom(detail);
            query.From.AddJoin(JoinType.Left, new DQDmoSource(main), DQCondition.EQ(main, "ID", detail, "ProduceOutput_ID"));
            query.From.AddJoin(JoinType.Left, new DQDmoSource(goods), DQCondition.EQ(goods, "ID", detail, "Goods_ID"));
            query.Columns.Add(DQSelectColumn.Field("Goods_Name"));
            query.Columns.Add(DQSelectColumn.Field("PlanNumber_Name", detail));
   
            query.Columns.Add(DQSelectColumn.Create(DQExpression.Field(detail, "Remark"), "计数规格"));
            query.Columns.Add(DQSelectColumn.Create(DQExpression.Sum(DQExpression.Field(detail, "SecondNumber")), "盘数"));
            detailSumIndex.Add(query.Columns.Count - 1);
            query.Columns.Add(DQSelectColumn.Create(DQExpression.Sum(DQExpression.Field(detail, "SecondNumber2")), "袋数"));
            detailSumIndex.Add(query.Columns.Count - 1);
            query.Columns.Add(DQSelectColumn.Create(DQExpression.Sum(DQExpression.Field(detail, "Number")), "重量"));
            detailSumIndex.Add(query.Columns.Count - 1);
            query.Columns.Add(DQSelectColumn.Create(DQExpression.Field(goods, "PackageModel"), "包装模式"));
            //query.Columns.Add(DQSelectColumn.Create(DQExpression.Field(goods, "ProductShift_ID"), "生产班组ID"));

            query.GroupBy.Expressions.Add(DQExpression.Field(detail,"Goods_Name"));
            query.GroupBy.Expressions.Add(DQExpression.Field(detail, "PlanNumber_Name"));
            query.GroupBy.Expressions.Add(DQExpression.Field(detail, "Remark"));
            query.GroupBy.Expressions.Add(DQExpression.Field(goods, "PackageModel"));
            //query.GroupBy.Expressions.Add(DQExpression.Field(goods, "ProductShift_ID"));
            if (dateInput.Value != null)
            {
                query.Where.Conditions.Add(DQCondition.GreaterThanOrEqual(main, "Time", dateInput.Value.Value));
            }
            if (enddateInput.Value != null)
            {
                query.Where.Conditions.Add(DQCondition.LessThanOrEqual(main, "Time", enddateInput.Value.Value));
            }
            return query;




        }

        private IDictionary<short, string> GetPackModeInfo()
        {
          return  NamedValue<包装模式>.GetDictionary();
        }

    }
}
