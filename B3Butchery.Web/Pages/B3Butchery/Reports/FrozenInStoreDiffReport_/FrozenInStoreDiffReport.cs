using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BWP.B3Butchery.BO;
using BWP.B3Butchery.Utils;
using BWP.B3Frameworks;
using BWP.B3Frameworks.BO;
using BWP.B3Frameworks.BO.NamedValueTemplate;
using BWP.B3Frameworks.Utils;
using BWP.B3UnitedInfos;
using BWP.Web.Layout;
using BWP.Web.Utils;
using BWP.Web.WebControls;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using Forks.EnterpriseServices.SqlDoms;
using TSingSoft.WebControls2;

namespace BWP.Web.Pages.B3Butchery.Reports.FrozenInStoreDiffReport_
{




    public class FrozenInStoreDiffReport : DFBrowseGridReportPage
    {
        protected override string AccessRoleName
        {
            get { return "B3Butchery.报表.速冻入库归零差异表"; }
        }

        protected override string Caption
        {
            get { return "速冻入库归零差异表"; }
        }

        readonly DFInfo _mainInfo = DFInfo.Get(typeof(ProduceOutput));
        readonly DFInfo _detailInfo = DFInfo.Get(typeof(ProduceOutput_Detail));

        private DFChoiceBox auuInput;
        private DFChoiceBox departInput;
        private DFChoiceBox goodInput;
        protected override void AddQueryControls(VLayoutPanel vPanel)
        {
            var customPanel = new LayoutManager("Main", _mainInfo, mQueryContainer);

           auuInput = customPanel.Add("AccountingUnit_ID", QueryCreator.DFChoiceBoxEnableMultiSelection(_mainInfo.Fields["AccountingUnit_ID"], mQueryContainer, "AccountingUnit_ID", B3FrameworksConsts.DataSources.授权会计单位全部));
            customPanel["AccountingUnit_ID"].NotAutoAddToContainer = true;
         departInput =   customPanel.Add("Department_ID", QueryCreator.DFChoiceBoxEnableMultiSelection(_mainInfo.Fields["Department_ID"], mQueryContainer, "Department_ID", B3FrameworksConsts.DataSources.授权部门全部));
            customPanel["Department_ID"].NotAutoAddToContainer = true;


            customPanel.Add("Goods_ID", new SimpleLabel("存货"), goodInput = QueryCreator.DFChoiceBoxEnableMultiSelection(_detailInfo.Fields["Goods_ID"], mQueryContainer, "Goods_ID", B3UnitedInfosConsts.DataSources.存货));
            customPanel["Goods_ID"].NotAutoAddToContainer = true;
            customPanel.CreateDefaultConfig(2).Expand = false;
            vPanel.Add(customPanel.CreateLayout());
        }

        CheckBoxListWithReverseSelect _checkbox;
        protected override void InitQueryPanel(QueryPanel queryPanel)
        {
            base.InitQueryPanel(queryPanel);
            var panel = queryPanel.CreateTab("显示字段");
            _checkbox = new CheckBoxListWithReverseSelect { RepeatColumns = 6, RepeatDirection = RepeatDirection.Horizontal };

            _checkbox.Items.Add(new ListItem("会计单位", "AccountingUnit_Name"));
            _checkbox.Items.Add(new ListItem("部门", "Department_Name"));
            _checkbox.Items.Add(new ListItem("产出单品名", "Goods_Name"));
            _checkbox.Items.Add(new ListItem("产出数量", "Number"));
            _checkbox.Items.Add(new ListItem("产出辅数量", "SecondNumber2"));
            _checkbox.Items.Add(new ListItem("速冻出库品名", "Goods_Name"));
            _checkbox.Items.Add(new ListItem("速冻出库数量", "Number"));
            _checkbox.Items.Add(new ListItem("速冻出库辅数量", "SecondNumber2"));

            _checkbox.Items.Add(new ListItem("产出单差异(重量)", "产出单差异(重量)"));
            _checkbox.Items.Add(new ListItem("产出单差异(袋数)", "产出单差异(袋数)"));
            _checkbox.Items.Add(new ListItem("包装品名", "成品Name"));
            _checkbox.Items.Add(new ListItem("包装数量", "Number"));
            _checkbox.Items.Add(new ListItem("包装辅数量", "SecondNumber2"));


            panel.EAdd(_checkbox);
            var hPanel = new HLayoutPanel();
            CreateDataRangePanel(hPanel);
            queryPanel.ConditonPanel.EAdd(hPanel);
            mQueryControls.Add("显示字段", _checkbox);
            mQueryControls.EnableHoldLastControlNames.Add("显示字段");
        }

        private DateInput dateInput;
        void CreateDataRangePanel(HLayoutPanel hPanel)
        {
            hPanel.Add(new SimpleLabel("日期"));
            dateInput = new DateInput();
            dateInput.Value = DateTime.Today.AddDays(-1);

            hPanel.Add(dateInput);
//            hPanel.Add(QueryCreator.DateRange(_mainInfo.Fields["Date"], mQueryContainer, "MinDate", "MaxDate"));
        }


        protected override void InitForm(HtmlForm form)
        {
            base.InitForm(form);
            mBrowseGrid.EnableRowsGroup = true;
        }


        protected override DQueryDom GetQueryDom()
        {
//            var main = JoinAlias.Create("bill");
//            var detail = JoinAlias.Create("detail");
//            var query = base.GetQueryDom();

            var main = new JoinAlias(typeof(ProductOutTemp));
            var query = new DQueryDom(main);
            ProductOutTemp.Register(query, dateInput.Date);


      

            var frozen = new JoinAlias("tempFrozen", typeof(FrozenOutTemp));

            var frozenNum = new JoinAlias("tempFrozenAllNumber", typeof(FrozeTemp));
            FrozenOutTemp.Register(query, dateInput.Value);
            FrozenOutTemp.AddJoin(query,frozen);

            FrozeTemp.Register(query, dateInput.Value);
            FrozeTemp.AddJoin(query, frozenNum);

            var 速冻出库重量Exp = DQExpression.Field(frozenNum, "AllNumber");
            var 速冻出库袋数Exp = DQExpression.Field(frozenNum, "AllSecondNumber2");
            foreach (ListItem field in _checkbox.Items)
            {
                if (field.Selected)
                {
                    switch (field.Text)
                    {

                        case "会计单位":
                        case "部门":
                        case "经办人":
                        case "产出单品名":
                        case "速冻出库品名":
                        case "产出数量":
                        case "产出辅数量":
                            
                            query.Columns.Add(DQSelectColumn.Create(DQExpression.Field(main, field.Value), field.Text));
                            query.GroupBy.Expressions.Add(DQExpression.Field(main, field.Value));
                            break;

                        case "速冻出库数量":
                            
                            query.Columns.Add(DQSelectColumn.Create(速冻出库重量Exp, field.Text));
                            query.GroupBy.Expressions.Add(速冻出库重量Exp);
                            break;

                        case "速冻出库辅数量":
                            query.Columns.Add(DQSelectColumn.Create(速冻出库袋数Exp, field.Text));
                            query.GroupBy.Expressions.Add(速冻出库袋数Exp);

                            break;
                        case "包装品名":
                            query.Columns.Add(DQSelectColumn.Create(DQExpression.Field(frozen, field.Value), field.Text));
                            query.GroupBy.Expressions.Add(DQExpression.Field(frozen, field.Value));
                            break;

                        case "包装数量":
                        case "包装辅数量":
                            
                            query.Columns.Add(DQSelectColumn.Create(DQExpression.Sum(DQExpression.Field(frozen, field.Value)), field.Text));
                            SumColumnIndexs.Add(query.Columns.Count - 1);
                            break;
                        case  "产出单差异(重量)":

                            var 产出重量Exp = DQExpression.Field(main, "Number");

                            query.Columns.Add(DQSelectColumn.Create(DQExpression.Subtract(产出重量Exp, 速冻出库重量Exp), field.Text));

                            query.GroupBy.Expressions.Add(DQExpression.Subtract(产出重量Exp, 速冻出库重量Exp));
                            break;
                        case "产出单差异(袋数)":

                            var 产出袋数Exp = DQExpression.Field(main, "SecondNumber2");

                            query.Columns.Add(DQSelectColumn.Create(DQExpression.Subtract(产出袋数Exp, 速冻出库袋数Exp), field.Text));

                            query.GroupBy.Expressions.Add(DQExpression.Subtract(产出袋数Exp, 速冻出库袋数Exp));
                            break;
                    }
                }
            }

            if (!string.IsNullOrEmpty(auuInput.Value))
            {

                query.Where.Conditions.Add(DQCondition.InList(DQExpression.Field(main, "AccountingUnit_Name"), auuInput.GetValues().Select(x => DQExpression.Value(x)).ToArray()));
            }

            if (!string.IsNullOrEmpty(departInput.Value))
            {

                query.Where.Conditions.Add(DQCondition.InList(DQExpression.Field(main, "Department_Name"), departInput.GetValues().Select(x => DQExpression.Value(x)).ToArray()));
            }
            if (!string.IsNullOrEmpty(goodInput.Value))
            {

                query.Where.Conditions.Add(DQCondition.InList(DQExpression.Field(main, "Goods_ID"), goodInput.GetValues().Select(x => DQExpression.Value(x)).ToArray()));
            }


            return query;
        }
    }





    //            _checkbox.Items.Add(new ListItem("会计单位", "AccountingUnit_Name"));
    //            _checkbox.Items.Add(new ListItem("部门", "Department_Name"));
    //            _checkbox.Items.Add(new ListItem("经办人", "Employee_Name"));
    //
    //
    //            _checkbox.Items.Add(new ListItem("产出单品名", "Goods_Name"));
    //            _checkbox.Items.Add(new ListItem("产出数量", "Number"));
    //
    //            _checkbox.Items.Add(new ListItem("速冻出库品名", "Goods_Name"));
    //            _checkbox.Items.Add(new ListItem("速冻出库数量", "Number"));
    //            _checkbox.Items.Add(new ListItem("包装品名", "成品ID"));
    //            _checkbox.Items.Add(new ListItem("包装数量", "成品Name"));
    class ProductOutTemp
    {

        public string AccountingUnit_Name { get; set; }
        public string Department_Name { get; set; }
        public DateTime? Time { get; set; }


        public long? Goods_ID { get; set; }

        public string Goods_Name { get; set; }

        public decimal? Number { get; set; }
        public decimal? SecondNumber2 { get; set; }
//        产出辅数量


        private static DQueryDom GetDom(DateTime? date)
        {
            var main = new JoinAlias("__produMain", typeof(ProduceOutput));

            var detail = new JoinAlias("__prodDetail", typeof(ProduceOutput_Detail));
            var dom = new DQueryDom(main);
            dom.From.AddJoin(JoinType.Left, new DQDmoSource(detail), DQCondition.EQ(detail, "ProduceOutput_ID", main, "ID"));

            dom.Columns.Add(DQSelectColumn.Field("AccountingUnit_Name", main));
            dom.Columns.Add(DQSelectColumn.Field("Department_Name", main));
            var exp = DQExpression.Snippet(" (CONVERT(varchar(10), [__produMain].[Time], 23))");
            dom.Columns.Add(DQSelectColumn.Create(exp, "Time"));
            dom.Columns.Add(DQSelectColumn.Field("Goods_ID", detail));
            dom.Columns.Add(DQSelectColumn.Field("Goods_Name", detail));
            dom.Columns.Add(DQSelectColumn.Sum(detail, "Number"));
            dom.Columns.Add(DQSelectColumn.Sum(detail, "SecondNumber2"));


            dom.GroupBy.Expressions.Add(exp);
            dom.GroupBy.Expressions.Add(DQExpression.Field(main, "AccountingUnit_Name"));
            dom.GroupBy.Expressions.Add(DQExpression.Field(main, "Department_Name"));
            dom.GroupBy.Expressions.Add(DQExpression.Field(detail, "Goods_ID"));
            dom.GroupBy.Expressions.Add(DQExpression.Field(detail, "Goods_Name"));
    
            if (date != null)
            {
                var c2 = DQCondition.LessThanOrEqual(main, "Time", date.Value.AddDays(1));
                var c1 = DQCondition.GreaterThanOrEqual(main, "Time", date.Value);
                dom.Where.Conditions.Add(DQCondition.And(c1, c2));
            }

            dom.Where.Conditions.Add(DQCondition.EQ(main, "BillState", 单据状态.已审核));
//            OrganizationUtil.AddOrganizationLimit<Department>(query, "Department_ID");
            return dom;
        }


        public static void Register(DQueryDom mainDom, DateTime? date)
        {
            mainDom.RegisterQueryTable(typeof(ProductOutTemp), new[] { "AccountingUnit_Name", "Department_Name", "Time", "Goods_ID", "Goods_Name", "Number", "SecondNumber2" }, GetDom(date));
        }

//        public static void AddJoin(DQueryDom mainDom, JoinAlias selfAlias, JoinAlias detailAlias)
//        {
//
//            var root = mainDom.From.RootSource.Alias;
//            var datediff =
//                DQExpression.DateDiff(DQExpression.Field(selfAlias, "Date"), DQExpression.Field(root, "Time"));
//            mainDom.From.AddJoin(JoinType.Left, new DQDmoSource(selfAlias), DQCondition.And(
//              DQCondition.EQ(DQExpression.Value(-1), datediff),
//              DQCondition.EQ(selfAlias, "Goods_ID", detailAlias, "Goods_ID")));
//        }
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
            var main = new JoinAlias("__frezenMain",typeof(FrozenOutStore));

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


}
