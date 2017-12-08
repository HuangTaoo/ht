using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        protected override void AddQueryControls(VLayoutPanel vPanel)
        {
            var customPanel = new LayoutManager("Main", _mainInfo, mQueryContainer);

            customPanel.Add("AccountingUnit_ID", QueryCreator.DFChoiceBoxEnableMultiSelection(_mainInfo.Fields["AccountingUnit_ID"], mQueryContainer, "AccountingUnit_ID", B3FrameworksConsts.DataSources.授权会计单位全部));
            customPanel["AccountingUnit_ID"].NotAutoAddToContainer = true;
            customPanel.Add("Department_ID", QueryCreator.DFChoiceBoxEnableMultiSelection(_mainInfo.Fields["Department_ID"], mQueryContainer, "Department_ID", B3FrameworksConsts.DataSources.授权部门全部));
            customPanel["Department_ID"].NotAutoAddToContainer = true;
           

            customPanel.Add("Goods_ID", new SimpleLabel("存货"), QueryCreator.DFChoiceBoxEnableMultiSelection(_detailInfo.Fields["Goods_ID"], mQueryContainer, "Goods_ID", B3UnitedInfosConsts.DataSources.存货));
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

            _checkbox.Items.Add(new ListItem("速冻出库品名", "Goods_Name"));
            _checkbox.Items.Add(new ListItem("速冻出库数量", "Number"));
            _checkbox.Items.Add(new ListItem("包装品名", "成品Name"));
            _checkbox.Items.Add(new ListItem("包装数量", "Number"));
   

            _checkbox.Items.Add(new ListItem("产出单差异", "产出单差异"));
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
            dateInput.Value = DateTime.Today;

            hPanel.Add(dateInput);
//            hPanel.Add(QueryCreator.DateRange(_mainInfo.Fields["Date"], mQueryContainer, "MinDate", "MaxDate"));
        }





        protected override DQueryDom GetQueryDom()
        {
            var main = JoinAlias.Create("bill");
            var detail = JoinAlias.Create("detail");
            var query = base.GetQueryDom();
            query.Where.Conditions.Add(DQCondition.EQ(main, "BillState", 单据状态.已审核));
            OrganizationUtil.AddOrganizationLimit<Department>(query, "Department_ID");

            var frozen = new JoinAlias("tempFrozen", typeof(FrozenOutTemp));
            FrozenOutTemp.Register(query, dateInput.Value);
            FrozenOutTemp.AddJoin(query,frozen, detail);



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
//
//
//            _checkbox.Items.Add(new ListItem("产出单差异", "产出单差异"));
            foreach (ListItem field in _checkbox.Items)
            {
                if (field.Selected)
                {
                    switch (field.Text)
                    {

                        case "会计单位":
                        case "部门":
                        case "经办人":
                            query.Columns.Add(DQSelectColumn.Create(DQExpression.Field(main, field.Value), field.Text));
                            query.GroupBy.Expressions.Add(DQExpression.Field(main, field.Value));
                            break;
                        case "产出单品名":
                        case "速冻出库品名":
                            query.Columns.Add(DQSelectColumn.Create(DQExpression.Field(detail, field.Value), field.Text));
                            query.GroupBy.Expressions.Add(DQExpression.Field(detail, field.Value));
                            break;
                        case "产出数量":
                            query.Columns.Add(DQSelectColumn.Create(DQExpression.Sum(DQExpression.Field(detail, field.Value)), field.Text));
                            SumColumnIndexs.Add(query.Columns.Count - 1);
                            break;
                            
                        case "速冻出库数量":
                            query.Columns.Add(DQSelectColumn.Create(DQExpression.Sum(DQExpression.Field(frozen, field.Value)), field.Text));
                            SumColumnIndexs.Add(query.Columns.Count - 1);
                            break;
                        case "包装品名":
                            query.Columns.Add(DQSelectColumn.Create(DQExpression.Field(frozen, field.Value), field.Text));
                            query.GroupBy.Expressions.Add(DQExpression.Field(frozen, field.Value));
                            break;

                        case "包装数量":
                            query.Columns.Add(DQSelectColumn.Create(DQExpression.Sum(DQExpression.Field(frozen, field.Value)), field.Text));
                            SumColumnIndexs.Add(query.Columns.Count - 1);
                            break;
                        case  "产出单差异":


                            break;
                    }
                }
            }
            if (dateInput.Value != null)
            {
                query.Where.Conditions.Add(DQCondition.EQ(main, "Time", dateInput.Value));
            }
            return query;
        }
    }


    class FrozenOutTemp
    {

        public DateTime? Date { get; set; }
        public long? Goods_ID { get; set; }
        public long? 成品ID { get; set; }
        public string 成品Name { get; set; }
        public decimal? Number { get; set; }



        private static DQueryDom GetDom(DateTime? date)
        {
            var main = new JoinAlias("__frezenMain",typeof(FrozenOutStore));

            var detail = new JoinAlias(typeof(FrozenOutStore_Detail));
            var dom = new DQueryDom(main);
            dom.From.AddJoin(JoinType.Left, new DQDmoSource(detail), DQCondition.EQ(detail, "FrozenOutStore_ID", main, "ID"));

            var exp = DQExpression.Snippet(" (CONVERT(varchar(10), [__frezenMain].[Date], 23))");
            dom.Columns.Add(DQSelectColumn.Create(exp, "Date"));
            dom.Columns.Add(DQSelectColumn.Create(DQExpression.Field(detail, "Goods2_ID"), "Goods_ID"));
            dom.Columns.Add(DQSelectColumn.Create(DQExpression.Field(detail, "Goods_ID"), "成品ID"));
            dom.Columns.Add(DQSelectColumn.Create(DQExpression.Field(detail, "Goods_Name"), "成品Name"));
         
            dom.Columns.Add(DQSelectColumn.Field("Number", detail));
            if (date != null)
            {
                dom.Where.Conditions.Add(DQCondition.EQ(exp, DQExpression.Value<DateTime>(date.Value.AddDays(1))));
            }
            return dom;
        }

        public static void Register(DQueryDom mainDom, DateTime? date)
        {
            mainDom.RegisterQueryTable(typeof(FrozenOutTemp), new[] { "Date", "Goods_ID", "成品ID", "成品Name", "Number" }, GetDom(date));
        }

        public static void AddJoin(DQueryDom mainDom, JoinAlias selfAlias, JoinAlias detailAlias)
        {

            var root = mainDom.From.RootSource.Alias;
            var datediff =
                DQExpression.DateDiff(DQExpression.Field(selfAlias, "Date"), DQExpression.Field(root, "Time"));
            mainDom.From.AddJoin(JoinType.Left, new DQDmoSource(selfAlias), DQCondition.And(
              DQCondition.EQ(DQExpression.Value(1), datediff),
              DQCondition.EQ(selfAlias, "Goods_ID", detailAlias, "Goods_ID")));
        }
    }


}
