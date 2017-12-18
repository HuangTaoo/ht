using BWP.B3Frameworks;
using BWP.B3Frameworks.Utils;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using TSingSoft.WebControls2;
using TSingSoft.WebPluginFramework;
using TSingSoft.WebPluginFramework.Controls;
using TSingSoft.WebPluginFramework.Security;
using BWP.B3Frameworks.BO;
using BWP.B3Butchery.BO;
using BWP.B3ProduceUnitedInfos.BO;
using Forks.EnterpriseServices.SqlDoms;
using BWP.B3Frameworks.BO.NamedValueTemplate;
using System.Web.UI.WebControls;
using Forks.Utils.Collections;

namespace BWP.Web.Pages.B3Butchery.Reports.FrozenInStoreDiffReport_
{
    //归零差异表




    class ZeroDiffReport : AppBasePage
    {
        protected override void InitForm(HtmlForm form)
        {
            if (!User.IsInRole("B3Butchery.报表.归零差异表"))
                throw new AppSecurityException();
            form.Controls.Add(new PageTitle("归零差异表"));
            var vPanel = form.EAdd(new VLayoutPanel());
            CreateQueryPanel(vPanel);

            CreateDetailPanel(vPanel);
        }

  

        DFDateInput dateInput;
        private void CreateQueryPanel(VLayoutPanel vPanel)
        {
            var tablePanel = vPanel.Add(new TableLayoutPanel(5, 5), new VLayoutOption(System.Web.UI.WebControls.HorizontalAlign.Justify));
            var row = 0;
            const int labelWidth = 4;
            tablePanel.Add(0, 1, row, row + 1, new SimpleLabel("日期", labelWidth));
            dateInput = tablePanel.Add(1, 2, row, ++row, new DFDateInput() { Date = DateTime.Today.AddDays(-1)});
   
            tablePanel.Add(0, 1, row, row + 1, new SimpleLabel("会计单位"));
            tablePanel.Add(1, 2, row, ++row, CreateAccPart());

            tablePanel.Add(0, 1, row, row + 1, new SimpleLabel("生产单位"));
            tablePanel.Add(1, 2, row, ++row, CreateProducUnitPart());

            tablePanel.Add(0, 1, row, row + 1, new SimpleLabel("存货分类"));
            tablePanel.Add(1, 2, row, ++row, CreatGoodsCategoryPart());

        }





        #region 添加搜索列

        FilterTree accFilterTree;
        FilterTreeContainer mTreeContainer = new FilterTreeContainer();
        //班组
        private Control CreateAccPart()
        {
            accFilterTree = new FilterTree();
            accFilterTree.HorizontalRepeatColumns = 10;
            this.Load += delegate
            {
                if (!IsPostBack)
                {
                    var rootNode = accFilterTree.DataSource;
                    var shiftInfo = GetAccInfo();
                    foreach (var item in shiftInfo)
                    {
                        accFilterTree.DataSource.Childs.Add(new FilterTreeNode(item.Item2, item.Item1.ToString()));
                    }
                }
            };
            mTreeContainer.Add("acc", accFilterTree);
            accFilterTree.FilterAction = (query, node) =>
            {
                if (!string.IsNullOrEmpty(node.Value))
                {
                    var shiftID = long.Parse(node.Value);
                    query.Where.Conditions.Add(DQCondition.EQ(main, "AccountingUnit_ID", shiftID));
                }
            };
            return accFilterTree;


        }
        FilterTree proudceUnitFilterTree;

        //生产单位
        private Control CreateProducUnitPart()
        {
            proudceUnitFilterTree = new FilterTree();
            proudceUnitFilterTree.HorizontalRepeatColumns = 10;
            this.Load += delegate
            {
                if (!IsPostBack)
                {
                    var rootNode = proudceUnitFilterTree.DataSource;
                    var packModeInfo = GetProduceUnitInfo();
                    foreach (var item in packModeInfo)
                    {
                        proudceUnitFilterTree.DataSource.Childs.Add(new FilterTreeNode(item.Item2, item.Item1.ToString()));
                    }
                }
            };
            mTreeContainer.Add("produceUnit", proudceUnitFilterTree);
            proudceUnitFilterTree.FilterAction = (query, node) =>
            {
                if (!string.IsNullOrEmpty(node.Value))
                {
                    var packMode = long.Parse(node.Value);
                    query.Where.Conditions.Add(DQCondition.EQ(main, "ProductionUnit_ID", packMode));
                }
            };
            return proudceUnitFilterTree;
        }




        FilterTree goodsCategoryFilterTree;

        //存货类别
        private Control CreatGoodsCategoryPart()
        {
            goodsCategoryFilterTree = new FilterTree();
            goodsCategoryFilterTree.HorizontalRepeatColumns = 10;
            this.Load += delegate
            {
                if (!IsPostBack)
                {
                    var rootNode = goodsCategoryFilterTree.DataSource;
                    var packModeInfo = GetGoodsCategoryInfo();
                    foreach (var item in packModeInfo)
                    {
                        goodsCategoryFilterTree.DataSource.Childs.Add(new FilterTreeNode(item.Item2, item.Item1.ToString()));
                    }
                }
            };
            mTreeContainer.Add("goodsCategory", goodsCategoryFilterTree);
            goodsCategoryFilterTree.FilterAction = (query, node) =>
            {
                if (!string.IsNullOrEmpty(node.Value))
                {
                    var packMode = long.Parse(node.Value);
                    query.Where.Conditions.Add(DQCondition.EQ(main, "GoodsCategory_ID", packMode));
                }
            };


            mTreeContainer.Select += delegate
            {
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
            return goodsCategoryFilterTree;
        }



        private List<Tuple<long, string>> GetAccInfo()
        {
            return GetBaseInfo<AccountingUnit>();
        }
        private List<Tuple<long, string>> GetProduceUnitInfo()
        {
            return GetBaseInfo<ProductionUnit>();
        }
        private List<Tuple<long, string>> GetGoodsCategoryInfo()
        {
            return GetBaseInfo<GoodsCategory>();
        }

        private List<Tuple<long, string>> GetBaseInfo<T>()
        {
            var query = new DQueryDom(new JoinAlias(typeof(T)));

            query.Columns.Add(DQSelectColumn.Field("ID"));
            query.Columns.Add(DQSelectColumn.Field("Name"));

            query.Where.Conditions.Add(DQCondition.EQ("Stopped", false));
            query.Where.Conditions.Add(DQCondition.EQ("Domain_ID", DomainContext.Current.ID));
            OrganizationUtil.AddOrganizationLimit(query, typeof(T));
            return query.EExecuteList<long, string>();
        }
        #endregion



        #region 查询结果
        DFBrowseGrid detailGrid;
        List<int> detailSumIndex = new List<int>();
        List<int> detailGroupSumIndex = new List<int>();
        private void CreateDetailPanel(VLayoutPanel vPanel)
        {

            detailGrid = vPanel.Add(new DFBrowseGrid(new DFDataTableEditor()) { Width = Unit.Percentage(100) });
            detailGrid.EnableRowsGroup = true;
            detailGrid.Columns.Add(new DFBrowseGridColumn("会计单位"));
            detailGrid.Columns.Add(new DFBrowseGridColumn("部门"));
            detailGrid.Columns.Add(new DFBrowseGridColumn("产出品名"));
            detailGrid.Columns.Add(new DFBrowseGridColumn("产出数量"));
            detailGrid.Columns.Add(new DFBrowseGridColumn("产出辅数量"));
            detailGrid.Columns.Add(new DFBrowseGridColumn("速冻出库品名"));
            detailGrid.Columns.Add(new DFBrowseGridColumn("速冻出库数量"));
            detailGrid.Columns.Add(new DFBrowseGridColumn("速冻出库辅数量"));
            detailGrid.Columns.Add(new DFBrowseGridColumn("速冻库差异数量"));
            detailGrid.Columns.Add(new DFBrowseGridColumn("速冻库差异辅数量II"));
            detailGrid.Columns.Add(new DFBrowseGridColumn("包装品名"));
            detailGrid.Columns.EAdd(new DFBrowseGridColumn("包装数量")).SumMode = SumMode.Sum;
            detailGrid.Columns.EAdd(new DFBrowseGridColumn("包装辅数量")).SumMode = SumMode.Sum;
            detailGrid.Columns.Add(new DFBrowseGridColumn("对比值"));
        }

        #endregion


        #region 查询


        private IEnumerable<ListItem> GetAllItems()
        {
            var list = new List<Tuple<string, string>>();
            list.Add(new Tuple<string, string>("AccountingUnit_Name", "会计单位"));
            list.Add(new Tuple<string, string>("Department_Name", "部门"));
            list.Add(new Tuple<string, string>("_pr_Goods_Name", "产出品名"));
            list.Add(new Tuple<string, string>("_pr_Number", "产出数量"));
            list.Add(new Tuple<string, string>("_pr_SecondNumber2", "产出辅数量"));
            list.Add(new Tuple<string, string>("_fr_Goods_Name", "速冻出库品名"));
            list.Add(new Tuple<string, string>("AllNumber", "速冻出库数量"));
            list.Add(new Tuple<string, string>("AllSecondNumber2", "速冻出库辅数量"));
            list.Add(new Tuple<string, string>("速冻库差异数量", "速冻库差异数量"));
            list.Add(new Tuple<string, string>("速冻库差异辅数量II", "速冻库差异辅数量II"));
            list.Add(new Tuple<string, string>("成品Name", "包装品名"));
            list.Add(new Tuple<string, string>("_pa_Number", "包装数量"));
            list.Add(new Tuple<string, string>("_pa_SecondNumber2", "包装辅数量"));
            list.Add(new Tuple<string, string>("对比值", "对比值"));
            return list.Select(x => new ListItem(x.Item2, x.Item1));
        }



        private JoinAlias main = null;
        protected  DQueryDom GetQueryDom()
        {

            var items = GetAllItems();
            main = new JoinAlias(typeof(ProductOutTemp));
            var query = new DQueryDom(main);
            ProductOutTemp.Register(query, dateInput.Date);
            var frozen = new JoinAlias("tempFrozen", typeof(FrozenOutTemp));

            var frozenNum = new JoinAlias("tempFrozenAllNumber", typeof(FrozeTemp));
            FrozenOutTemp.Register(query, dateInput.Value);
            FrozenOutTemp.AddJoin(query, frozen);

            FrozeTemp.Register(query, dateInput.Value);
            FrozeTemp.AddJoin(query, frozenNum);

            var 速冻出库重量Exp = DQExpression.Field(frozenNum, "AllNumber");
            var 产出重量Exp = DQExpression.Field(main, "Number");
            var 速冻出库袋数Exp = DQExpression.Field(frozenNum, "AllSecondNumber2");
            var 产出袋数Exp = DQExpression.Field(main, "SecondNumber2");
            foreach (ListItem item in items)
            {
                var field = item.Value;
                if (field.StartsWith("_"))
                {
                    field = field.Substring(4);
                }

                switch (item.Text)
                {
                    case "会计单位":
                    case "部门":
                    case "经办人":
                    case "产出品名":
                    case "速冻出库品名":
                    case "产出数量":
                    case "产出辅数量":
                        query.Columns.Add(DQSelectColumn.Create(DQExpression.Field(main, field), item.Text));
                        query.GroupBy.Expressions.Add(DQExpression.Field(main, field));
                        break;
                    case "速冻出库数量":
                        query.Columns.Add(DQSelectColumn.Create(速冻出库重量Exp, item.Text));
                        query.GroupBy.Expressions.Add(速冻出库重量Exp);
                        break;
                    case "速冻出库辅数量":
                        query.Columns.Add(DQSelectColumn.Create(速冻出库袋数Exp, item.Text));
                        query.GroupBy.Expressions.Add(速冻出库袋数Exp);
                        break;
                    case "包装品名":
                        query.Columns.Add(DQSelectColumn.Create(DQExpression.Field(frozen, field), item.Text));
                        query.GroupBy.Expressions.Add(DQExpression.Field(frozen, field));
                        break;
                    case "包装数量":
                    case "包装辅数量":
                        query.Columns.Add(DQSelectColumn.Create(DQExpression.Sum(DQExpression.Field(frozen, field)), item.Text));
                        detailSumIndex.Add(query.Columns.Count - 1);
                        break;
                    case "速冻库差异数量":
                
                        query.Columns.Add(DQSelectColumn.Create(DQExpression.Subtract(产出重量Exp, 速冻出库重量Exp), item.Text));
                        //query.GroupBy.Expressions.Add(DQExpression.Subtract(产出重量Exp, 速冻出库重量Exp));
                        break;
                    case "速冻库差异辅数量II":
                 
                        query.Columns.Add(DQSelectColumn.Create(DQExpression.Subtract(产出袋数Exp, 速冻出库袋数Exp), item.Text));
                        //query.GroupBy.Expressions.Add(DQExpression.Subtract(产出袋数Exp, 速冻出库袋数Exp));
                        break;
                        case "对比值":

                        var 对比值Exp = DQExpression.Divide(速冻出库重量Exp, DQExpression.NullIfZero(产出重量Exp));

                        query.Columns.Add(DQSelectColumn.Create(对比值Exp, item.Text));
                        break;
                }


            }



            return query;
        }


        class ProductOutTemp
        {
            public long? AccountingUnit_ID { get; set; }
            public long? ProductionUnit_ID { get; set; }
            public string AccountingUnit_Name { get; set; }
            public string Department_Name { get; set; }
            public DateTime? Time { get; set; }


            public long? Goods_ID { get; set; }

            public string Goods_Name { get; set; }
            public long? GoodsCategory_ID { get; set; }
            

            public decimal? Number { get; set; }
            public decimal? SecondNumber2 { get; set; }
            //        产出辅数量


            private static DQueryDom GetDom(DateTime? date)
            {
                var main = new JoinAlias("__produMain", typeof(ProduceOutput));

                var detail = new JoinAlias("__prodDetail", typeof(ProduceOutput_Detail));
                var dom = new DQueryDom(main);
                dom.From.AddJoin(JoinType.Left, new DQDmoSource(detail), DQCondition.EQ(detail, "ProduceOutput_ID", main, "ID"));

                dom.Columns.Add(DQSelectColumn.Field("AccountingUnit_ID", main));
                dom.Columns.Add(DQSelectColumn.Field("ProductionUnit_ID", main));
                dom.Columns.Add(DQSelectColumn.Field("AccountingUnit_Name", main));
                dom.Columns.Add(DQSelectColumn.Field("Department_Name", main));
                var exp = DQExpression.Snippet(" (CONVERT(varchar(10), [__produMain].[Time], 23))");
                dom.Columns.Add(DQSelectColumn.Create(exp, "Time"));
                dom.Columns.Add(DQSelectColumn.Field("Goods_ID", detail));
   
                
                dom.Columns.Add(DQSelectColumn.Field("Goods_Name", detail));
                dom.Columns.Add(DQSelectColumn.Field("GoodsCategory_ID", detail));
                dom.Columns.Add(DQSelectColumn.Sum(detail, "Number"));
                dom.Columns.Add(DQSelectColumn.Sum(detail, "SecondNumber2"));


                dom.GroupBy.Expressions.Add(exp);
                dom.GroupBy.Expressions.Add(DQExpression.Field(main, "AccountingUnit_ID"));
                dom.GroupBy.Expressions.Add(DQExpression.Field(main, "ProductionUnit_ID"));
                dom.GroupBy.Expressions.Add(DQExpression.Field(main, "AccountingUnit_Name"));
                dom.GroupBy.Expressions.Add(DQExpression.Field(main, "Department_Name"));
                dom.GroupBy.Expressions.Add(DQExpression.Field(detail, "Goods_ID"));
                dom.GroupBy.Expressions.Add(DQExpression.Field(detail, "Goods_Name"));
                dom.GroupBy.Expressions.Add(DQExpression.Field(detail, "GoodsCategory_ID"));

                if (date != null)
                {
                    var c2 = DQCondition.LessThanOrEqual(main, "Time", date.Value.AddDays(1));
                    var c1 = DQCondition.GreaterThanOrEqual(main, "Time", date.Value);
                    dom.Where.Conditions.Add(DQCondition.And(c1, c2));
                }

                dom.Where.Conditions.Add(DQCondition.EQ(main, "BillState", 单据状态.已审核));
                return dom;
            }


            public static void Register(DQueryDom mainDom, DateTime? date)
            {
                mainDom.RegisterQueryTable(typeof(ProductOutTemp), new[] { "AccountingUnit_ID", "ProductionUnit_ID",  "AccountingUnit_Name", "Department_Name", "Time", "Goods_ID", "Goods_Name", "GoodsCategory_ID" ,"Number", "SecondNumber2" }, GetDom(date));
            }

        }

        class FrozeTemp
        {
            public long? Goods_ID { get; set; }
            public decimal? AllNumber { get; set; }
            public decimal? AllSecondNumber2 { get; set; }
            public static void Register(DQueryDom mainDom, DateTime? date)
            {
                mainDom.RegisterQueryTable(typeof(FrozeTemp), new[] { "Goods_ID", "AllNumber", "AllSecondNumber2" }, GetAllNumDom(date));
            }

            public static void AddJoin(DQueryDom mainDom, JoinAlias selfAlias)
            {
                var root = mainDom.From.RootSource.Alias;
                mainDom.From.AddJoin(JoinType.Left, new DQDmoSource(selfAlias), DQCondition.And(
                    DQCondition.EQ(selfAlias, "Goods_ID", root, "Goods_ID")));
            }
            private static DQueryDom GetAllNumDom(DateTime? date)
            {
                var main = new JoinAlias("__frezenMainte", typeof(FrozenOutStore));

                var detail = new JoinAlias("__frezenMainte_De", typeof(FrozenOutStore_Detail));
                var dom = new DQueryDom(main);
                dom.From.AddJoin(JoinType.Left, new DQDmoSource(detail), DQCondition.EQ(detail, "FrozenOutStore_ID", main, "ID"));


                dom.Columns.Add(DQSelectColumn.Field("Goods_ID", detail));
                dom.Columns.Add(DQSelectColumn.Create(DQExpression.Sum(DQExpression.Field(detail, "Number")), "AllNumber"));
                dom.Columns.Add(DQSelectColumn.Create(DQExpression.Sum(DQExpression.Field(detail, "SecondNumber2")), "AllSecondNumber2"));

                if (date != null)
                {
                    var c2 = DQCondition.LessThanOrEqual(main, "Date", date.Value.AddDays(2));
                    var c1 = DQCondition.GreaterThanOrEqual(main, "Date", date.Value.AddDays(1));
                    dom.Where.Conditions.Add(DQCondition.And(c1, c2));
                }
                dom.GroupBy.Expressions.Add(DQExpression.Field(detail, "Goods_ID"));
                dom.Where.Conditions.Add(DQCondition.InEQ(main, "BillState", 1));
                return dom;
            }
        }
        class FrozenOutTemp
        {

            public DateTime? Date { get; set; }
            public long? Goods_ID { get; set; }
            public long? 成品ID { get; set; }
            public string 成品Name { get; set; }
            public decimal? Number { get; set; }
            public decimal? SecondNumber2 { get; set; }



            private static DQueryDom GetDom(DateTime? date)
            {
                var main = new JoinAlias("__frezenMain", typeof(FrozenOutStore));

                var detail = new JoinAlias(typeof(FrozenOutStore_Detail));
                var dom = new DQueryDom(main);
                dom.From.AddJoin(JoinType.Left, new DQDmoSource(detail), DQCondition.EQ(detail, "FrozenOutStore_ID", main, "ID"));

                var exp = DQExpression.Snippet(" (CONVERT(varchar(10), [__frezenMain].[Date], 23))");
                dom.Columns.Add(DQSelectColumn.Create(exp, "Date"));
                dom.Columns.Add(DQSelectColumn.Field("Goods_ID", detail));
                dom.Columns.Add(DQSelectColumn.Create(DQExpression.Field(detail, "Goods2_ID"), "成品ID"));
                dom.Columns.Add(DQSelectColumn.Create(DQExpression.Field(detail, "Goods2_Name"), "成品Name"));

                dom.Columns.Add(DQSelectColumn.Field("Number", detail));
                dom.Columns.Add(DQSelectColumn.Field("SecondNumber2", detail));

                if (date != null)
                {
                    var c2 = DQCondition.LessThanOrEqual(main, "Date", date.Value.AddDays(2));
                    var c1 = DQCondition.GreaterThanOrEqual(main, "Date", date.Value.AddDays(1));
                    dom.Where.Conditions.Add(DQCondition.And(c1, c2));
                }
                dom.Where.Conditions.Add(DQCondition.InEQ(main, "BillState", 1));
                return dom;
            }



            public static void Register(DQueryDom mainDom, DateTime? date)
            {
                mainDom.RegisterQueryTable(typeof(FrozenOutTemp), new[] { "Date", "Goods_ID", "成品ID", "成品Name", "Number", "SecondNumber2" }, GetDom(date));
            }

            public static void AddJoin(DQueryDom mainDom, JoinAlias selfAlias)
            {

                var root = mainDom.From.RootSource.Alias;
                var datediff =
                    DQExpression.DateDiff(DQExpression.Field(selfAlias, "Date"), DQExpression.Field(root, "Time"));
                mainDom.From.AddJoin(JoinType.Left, new DQDmoSource(selfAlias), DQCondition.And(
                  DQCondition.EQ(DQExpression.Value(-1), datediff),
                  DQCondition.EQ(selfAlias, "Goods_ID", root, "Goods_ID")));
            }







        }



        //暂时先不用
        static void AddDateCondition(JoinAlias main, DQueryDom query, string dtFile, DateInput sd, DateInput ed)
        {
            if (sd.Value.HasValue)
                query.Where.Conditions.Add(DQCondition.GreaterThanOrEqual(main, dtFile, sd.Value.Value));
            if (ed.Value.HasValue)
                query.Where.Conditions.Add(DQCondition.LessThanOrEqual(main, dtFile, ed.Value.Value));
        }


        #endregion




    }
}
