using System.Web.UI.WebControls;
using BWP.B3Butchery.BO;
using BWP.B3Butchery.Utils;
using BWP.B3Frameworks;
using BWP.B3Frameworks.BO;
using BWP.B3Frameworks.Utils;
using BWP.B3UnitedInfos;
using BWP.Web.Layout;
using BWP.Web.Utils;
using BWP.Web.WebControls;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using TSingSoft.WebControls2;
using BWP.B3Frameworks.BO.NamedValueTemplate;

namespace BWP.Web.Pages.B3Butchery.Reports.FrozenInStoreReport_
{
  public class FrozenInStoreReport : DFBrowseGridReportPage
  {
    protected override string AccessRoleName
    {
      get { return "B3Butchery.报表.速冻入库分析"; }
    }

    protected override string Caption
    {
      get { return "速冻入库分析"; }
    }

    readonly DFInfo _mainInfo = DFInfo.Get(typeof(FrozenInStore));
    readonly DFInfo _detailInfo = DFInfo.Get(typeof(FrozenInStore_Detail));
    protected override void AddQueryControls(VLayoutPanel vPanel)
    {
      var customPanel = new LayoutManager("Main", _mainInfo, mQueryContainer);

      customPanel.Add("ProductionPlan_ID", new SimpleLabel("计划号"), QueryCreator.DFChoiceBoxEnableMultiSelection(_mainInfo.Fields["ProductionPlan_ID"], mQueryContainer, "ProductionPlan_ID", B3ButcheryDataSource.计划号));
      customPanel["ProductionPlan_ID"].NotAutoAddToContainer = true;
      customPanel.Add("AccountingUnit_ID", QueryCreator.DFChoiceBoxEnableMultiSelection(_mainInfo.Fields["AccountingUnit_ID"], mQueryContainer, "AccountingUnit_ID", B3FrameworksConsts.DataSources.授权会计单位全部));
      customPanel["AccountingUnit_ID"].NotAutoAddToContainer = true;
      customPanel.Add("Department_ID", QueryCreator.DFChoiceBoxEnableMultiSelection(_mainInfo.Fields["Department_ID"], mQueryContainer, "Department_ID", B3FrameworksConsts.DataSources.授权部门全部));
      customPanel["Department_ID"].NotAutoAddToContainer = true;
      customPanel.Add("Employee_ID", QueryCreator.DFChoiceBoxEnableMultiSelection(_mainInfo.Fields["Employee_ID"], mQueryContainer, "Employee_ID", B3FrameworksConsts.DataSources.授权员工全部));
      customPanel["Employee_ID"].NotAutoAddToContainer = true;
      customPanel.Add("Store_ID", new SimpleLabel("速冻库"), QueryCreator.DFChoiceBoxEnableMultiSelection(_mainInfo.Fields["Store_ID"], mQueryContainer, "Store_ID", B3ButcheryDataSource.速冻库));
      customPanel["Store_ID"].NotAutoAddToContainer = true;
      customPanel.Add("OtherInStoreType_ID", QueryCreator.DFChoiceBoxEnableMultiSelection(_mainInfo.Fields["OtherInStoreType_ID"], mQueryContainer, "OtherInStoreType_ID", B3ButcheryDataSource.屠宰分割入库类型));
      customPanel["OtherInStoreType_ID"].NotAutoAddToContainer = true;
      customPanel.Add("Remark", QueryCreator.DFTextBox(_mainInfo.Fields["Remark"]));
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
      _checkbox.Items.Add(new ListItem("入库日期", "Date"));
      _checkbox.Items.Add(new ListItem("计划号", "ProductionPlan_PlanNumber"));
      _checkbox.Items.Add(new ListItem("会计单位", "AccountingUnit_Name"));
      _checkbox.Items.Add(new ListItem("部门", "Department_Name"));
      _checkbox.Items.Add(new ListItem("经办人", "Employee_Name"));
      _checkbox.Items.Add(new ListItem("速冻库", "Store_Name"));
      _checkbox.Items.Add(new ListItem("入库类型", "OtherInStoreType_Name"));
      _checkbox.Items.Add(new ListItem("摘要", "Remark"));
      _checkbox.Items.Add(new ListItem("存货名称", "Goods_Name"));
      _checkbox.Items.Add(new ListItem("存货编码", "Goods_Code"));
      _checkbox.Items.Add(new ListItem("规格", "Goods_Spec"));
      _checkbox.Items.Add(new ListItem("主数量", "Number"));
      _checkbox.Items.Add(new ListItem("主单位", "Goods_MainUnit"));
      _checkbox.Items.Add(new ListItem("生产数量", "SecondNumber2"));
      _checkbox.Items.Add(new ListItem("生产单位", "Goods_SecondUnit2"));
      _checkbox.Items.Add(new ListItem("创建人", "CreateUser_Name"));
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
      hPanel.Add(QueryCreator.DateRange(_mainInfo.Fields["Date"], mQueryContainer, "MinDate", "MaxDate"));
    }

    protected override DQueryDom GetQueryDom()
    {
      var main = JoinAlias.Create("bill");
      var detail = JoinAlias.Create("detail");
      var query = base.GetQueryDom();
      query.Where.Conditions.Add(DQCondition.EQ(main, "BillState", 单据状态.已审核));
      OrganizationUtil.AddOrganizationLimit<Department>(query, "Department_ID");
      foreach (ListItem field in _checkbox.Items)
      {
        if (field.Selected)
        {
          switch (field.Text)
          {
            case "入库日期":
            case "计划号":
            case "会计单位":
            case "部门":
            case "经办人":
            case "速冻库":
            case "入库类型":
            case "创建人":
            case "摘要":
              query.Columns.Add(DQSelectColumn.Create(DQExpression.Field(main, field.Value), field.Text));
              query.GroupBy.Expressions.Add(DQExpression.Field(main, field.Value));
              break;
            case "存货名称":
            case "存货编码":
            case "规格":
            case "主单位":
            case "生产单位":
            case "备注":
              query.Columns.Add(DQSelectColumn.Create(DQExpression.Field(detail, field.Value), field.Text));
              query.GroupBy.Expressions.Add(DQExpression.Field(detail, field.Value));
              break;
            case "主数量":
            case "生产数量":
              query.Columns.Add(DQSelectColumn.Create(DQExpression.Sum(DQExpression.Field(detail, field.Value)), field.Text));
              SumColumnIndexs.Add(query.Columns.Count - 1);
              break;
          }
        }
      }
      return query;
    }
  }
}
