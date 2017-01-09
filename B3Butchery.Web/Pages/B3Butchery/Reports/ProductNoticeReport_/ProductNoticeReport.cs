using System;
using System.Linq;
using System.Web.UI.WebControls;
using BWP.B3Butchery.BO;
using BWP.B3Frameworks;
using BWP.B3Frameworks.BO;
using BWP.B3Frameworks.Utils;
using BWP.B3ProduceUnitedInfos;
using BWP.B3UnitedInfos;
using BWP.B3UnitedInfos.BO;
using BWP.Web.Layout;
using BWP.Web.Utils;
using BWP.Web.WebControls;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using Forks.EnterpriseServices.SqlDoms;
using TSingSoft.WebControls2;
using System.Collections.Generic;

namespace BWP.Web.Pages.B3Butchery.Reports.ProductNoticeReport_
{
    class ProductNoticeReport : DFBrowseGridReportPage
    {
        readonly DFInfo _mainInfo = DFInfo.Get(typeof(ProductNotice));
        readonly DFInfo _detailInfo = DFInfo.Get(typeof(ProductNotice_Detail));
        readonly Dictionary<string, DFInfo> _fileInfo = new Dictionary<string, DFInfo>();
        readonly Dictionary<string, DFInfo> _fileInfo1 = new Dictionary<string, DFInfo>();
        //DFTextBox 摘要 = new DFTextBox();
        //DFTextBox 备注 = new DFTextBox();
        private DFTextBox 摘要, 备注;
        protected override string AccessRoleName
        {
            get { return "B3Butchery.报表.生产通知分析"; }
        }

        protected override string Caption
        {
            get { return "生产通知分析"; }
        }

        CheckBoxListWithReverseSelect _checkbox;
        protected override void InitQueryPanel(QueryPanel queryPanel)
        {
            
            base.InitQueryPanel(queryPanel);
            var panel = queryPanel.CreateTab("显示字段");
            _checkbox = new CheckBoxListWithReverseSelect { RepeatColumns = 6, RepeatDirection = RepeatDirection.Horizontal };
            _checkbox.Items.Add(new ListItem("单号", "ID"));
            _checkbox.Items.Add(new ListItem("会计单位", "AccountingUnit_Name"));
            _checkbox.Items.Add(new ListItem("部门", "Department_Name"));
            _checkbox.Items.Add(new ListItem("业务员", "Employee_Name"));
            _checkbox.Items.Add(new ListItem("单据状态", "BillState"));
            _checkbox.Items.Add(new ListItem("日期", "Date"));
            _checkbox.Items.Add(new ListItem("生产单位", "ProductionUnit_Name"));
            _checkbox.Items.Add(new ListItem("客户", "Customer_Name"));
            _checkbox.Items.Add(new ListItem("加工要求", "ProduceRequest"));
            _checkbox.Items.Add(new ListItem("生产日期", "ProduceDate"));
            _checkbox.Items.Add(new ListItem("交货日期", "DeliveryDate"));
            _checkbox.Items.Add(new ListItem("源单据号", "DmoID"));
            _checkbox.Items.Add(new ListItem("存货名称", "Goods_Name"));
            _checkbox.Items.Add(new ListItem("主数量", "Number"));
            _checkbox.Items.Add(new ListItem("主单位", "Goods_MainUnit"));
            _checkbox.Items.Add(new ListItem("辅数量", "SecondNumber"));
            _checkbox.Items.Add(new ListItem("辅单位", "Goods_SecondUnit"));
            _checkbox.Items.Add(new ListItem("摘要", "摘要"));
            _checkbox.Items.Add(new ListItem("备注", "Remark"));
        
            panel.EAdd(_checkbox);
            var hPanel = new HLayoutPanel();
            CreateDataRangePanel(hPanel);
            queryPanel.ConditonPanel.EAdd(hPanel);
            mQueryControls.Add("显示字段", _checkbox);
            mQueryControls.EnableHoldLastControlNames.Add("显示字段");
        }

        void CreateDataRangePanel(HLayoutPanel hPanel)
        {
            hPanel.Add(new SimpleLabel(_mainInfo.Fields["Date"].Prompt));
            hPanel.Add(QueryCreator.TimeRange(_mainInfo.Fields["Date"], mQueryContainer, "MinDate", "MaxDate"));
        }

        protected override void AddQueryControls(VLayoutPanel vPanel)
        {
            var customPanel = new LayoutManager("Main", _mainInfo, mQueryContainer);
            customPanel.Add("ID", QueryCreator.DFTextBox(_mainInfo.Fields["ID"]));
            customPanel.Add("BillState", QueryCreator.一般单据状态(_mainInfo.Fields["BillState"]));
            customPanel.Add("AccountingUnit_ID", QueryCreator.DFChoiceBoxEnableMultiSelection(_mainInfo.Fields["AccountingUnit_ID"], mQueryContainer, "AccountingUnit_ID", B3FrameworksConsts.DataSources.授权会计单位全部));
            customPanel["AccountingUnit_ID"].NotAutoAddToContainer = true;
            customPanel.Add("Department_ID", QueryCreator.DFChoiceBoxEnableMultiSelection(_mainInfo.Fields["Department_ID"], mQueryContainer, "Department_ID", B3FrameworksConsts.DataSources.授权部门全部));
            customPanel["Department_ID"].NotAutoAddToContainer = true;

            customPanel.Add("Customer_ID", QueryCreator.DFChoiceBoxEnableMultiSelection(_mainInfo.Fields["Customer_ID"], mQueryContainer, "Customer_ID", "B3Sale_客户全部"));
            customPanel["Customer_ID"].NotAutoAddToContainer = true;
            customPanel.Add("ProductionUnit_ID", QueryCreator.DFChoiceBoxEnableMultiSelection(_mainInfo.Fields["ProductionUnit_ID"], mQueryContainer, "ProductionUnit_ID", B3ProduceUnitedInfosDataSources.生产单位全部));
            customPanel["ProductionUnit_ID"].NotAutoAddToContainer = true;

            customPanel.Add("Employee_ID", QueryCreator.DFChoiceBoxEnableMultiSelection(_mainInfo.Fields["Employee_ID"], mQueryContainer, "Employee_ID", B3FrameworksConsts.DataSources.授权员工全部));
            customPanel["Employee_ID"].NotAutoAddToContainer = true;

            customPanel.Add("Goods_ID", new SimpleLabel("存货"), QueryCreator.DFChoiceBoxEnableMultiSelection(_detailInfo.Fields["Goods_ID"], mQueryContainer, "Goods_ID", B3UnitedInfosConsts.DataSources.存货));
            customPanel["Goods_ID"].NotAutoAddToContainer = true;


            摘要 = new DFTextBox(_mainInfo.Fields["Remark"]);
            备注=new DFTextBox(_detailInfo.Fields["Remark"]);
            customPanel.Add("摘要", new SimpleLabel("摘要"), 摘要);
            customPanel.Add("备注", new SimpleLabel("备注"), 备注);
      
            customPanel.CreateDefaultConfig(2).Expand = false;
            vPanel.Add(customPanel.CreateLayout());
        }

        string[] mainFields = new string[] { "ID", "AccountingUnit_Name", "Department_Name", "Customer_Name", "ProductionUnit_Name", "Employee_Name", "Date","BillState" };
        string[] sumFileds = new string[] { "Number", "SecondNumber" };
        string[] detailFields = new string[] { "ProduceRequest", "ProduceDate", "DeliveryDate", "DmoID" };
        string[] goodsFields = new string[] { "Name", "MainUnit", "SecondUnit" };
        protected override DQueryDom GetQueryDom()
        {
            var query = base.GetQueryDom();
            OrganizationUtil.AddOrganizationLimit<Department>(query, "Department_ID");
            var detail = JoinAlias.Create("detail");
            var goodsAlias = new JoinAlias(typeof(Goods));
            var alias = query.From.RootSource.Alias;
            query.From.AddJoin(JoinType.Left, new DQDmoSource(goodsAlias), DQCondition.EQ(detail, "Goods_ID", goodsAlias, "ID"));
            foreach (ListItem field in _checkbox.Items)
            {
                if (field.Selected)
                {
                    if (sumFileds.Contains(field.Value))
                    {
                        query.Columns.Add(DQSelectColumn.Sum(detail, field.Value));
                        SumColumnIndexs.Add(query.Columns.Count - 1);
                    }
                    else if (goodsFields.Contains(field.Value))
                    {
                        query.Columns.Add(DQSelectColumn.Field(field.Value, goodsAlias));
                        query.GroupBy.Expressions.Add(DQExpression.Field(goodsAlias, field.Value));
                    }
                        
                    else if (mainFields.Contains(field.Value))
                    {
                        query.Columns.Add(DQSelectColumn.Field(field.Value));
                        query.GroupBy.Expressions.Add(DQExpression.Field(field.Value));
                    }
                    else if (detailFields.Contains(field.Value))
                    {
                        query.Columns.Add(DQSelectColumn.Field(field.Value,detail));
                        query.GroupBy.Expressions.Add(DQExpression.Field(detail,field.Value));
                    }
                    else if (field.Value == "摘要")
                    {
                        query.Columns.Add(DQSelectColumn.Create(DQExpression.Field(alias, "Remark"), "摘要"));
                        query.GroupBy.Expressions.Add(DQExpression.Field(alias, "Remark"));
                    }
                   
                    else
                    {
                        query.Columns.Add(DQSelectColumn.Field(field.Value, detail));
                        query.GroupBy.Expressions.Add(DQExpression.Field(detail, field.Value));
                    }

                  

                }
            }
            query.Where.Conditions.Add(DQCondition.EQ("Domain_ID", DomainContext.Current.ID));
            if (!摘要.IsEmpty)
            {
                query.Where.Conditions.Add(DQCondition.And(DQCondition.Like(alias,"Remark",摘要.Text)));
            }

            if (!备注.IsEmpty)
            {
                query.Where.Conditions.Add(DQCondition.And(DQCondition.Like(detail, "Remark", 备注.Text)));
            }
         
            if (query.Columns.Count == 0)
                throw new Exception("至少选择一条显示列");
            return query;
        }
    }
}
