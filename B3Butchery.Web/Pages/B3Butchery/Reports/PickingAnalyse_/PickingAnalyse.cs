using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Butchery.BO;
using BWP.B3Butchery.Utils;
using BWP.B3Frameworks;
using BWP.B3Frameworks.BO.NamedValueTemplate;
using BWP.B3UnitedInfos;
using BWP.Web.Layout;
using BWP.Web.Utils;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using TSingSoft.WebControls2;

namespace BWP.Web.Pages.B3Butchery.Reports.PickingAnalyse_
{
  class PickingAnalyse : DFBrowseGridReportPage<Picking>
  {
    protected override string Caption
    {
      get { return "领料单分析"; }
    }

    protected override string QueryOptionsTabName
    {
      get
      {
        return "显示字段";
      }
    }

    protected override string AccessRoleName
    {
      get { return "B3Butchery.领料单.访问"; }
    }
    ReportDisplayOptionHelper mDisplayHelper = new ReportDisplayOptionHelper();

    protected override void AddQueryControls(VLayoutPanel vPanel)
    {
      var layout = new LayoutManager("Main", mDFInfo, mQueryContainer);

      layout.Add("ID", mQueryContainer.Add(new DFTextBox(mDFInfo.Fields["ID"]), "ID"));
      layout["ID"].NotAutoAddToContainer = true;

      layout.Add("Date", new SimpleLabel("日期"), QueryCreator.DateRange(mDFInfo.Fields["Date"], mQueryContainer, "MinDate", "MaxDate"));
      layout["Date"].NotAutoAddToContainer = true;


      layout.Add("AccountingUnit_ID", new SimpleLabel("会计单位"),
        QueryCreator.DFChoiceBoxEnableMultiSelection(mDFInfo.Fields["AccountingUnit_ID"], mQueryContainer, "AccountingUnit_ID", B3FrameworksConsts.DataSources.授权会计单位全部));
      layout["AccountingUnit_ID"].NotAutoAddToContainer = true;

      layout.Add("Department_ID", new SimpleLabel("部门"),
        QueryCreator.DFChoiceBoxEnableMultiSelection(mDFInfo.Fields["Department_ID"], mQueryContainer, "Department_ID", B3FrameworksConsts.DataSources.部门全部));
      layout["Department_ID"].NotAutoAddToContainer = true;


      layout.Add("Employee_ID", new SimpleLabel("经办人"),
  QueryCreator.DFChoiceBoxEnableMultiSelection(mDFInfo.Fields["Employee_ID"], mQueryContainer, "Employee_ID", B3FrameworksConsts.DataSources.员工全部));
      layout["Employee_ID"].NotAutoAddToContainer = true;

      layout.Add("Store_ID", new SimpleLabel("仓库"),
        QueryCreator.DFChoiceBoxEnableMultiSelection(mDFInfo.Fields["Store_ID"], mQueryContainer, "Store_ID", B3FrameworksConsts.DataSources.可用仓库全部));
      layout["Store_ID"].NotAutoAddToContainer = true;

      layout.Add("ProductLine_ID", new SimpleLabel("生产线"),
        QueryCreator.DFChoiceBoxEnableMultiSelection(mDFInfo.Fields["ProductLine_ID"], mQueryContainer, "ProductLine_ID", B3ButcheryDataSource.生产线));
      layout["ProductLine_ID"].NotAutoAddToContainer = true;

      layout.Add("Goods_ID", new SimpleLabel("存货"),
        QueryCreator.DFChoiceBoxEnableMultiSelection(mDFInfo.Fields["ID"], mQueryContainer, "Goods_ID", B3UnitedInfosConsts.DataSources.存货));
      layout["Goods_ID"].NotAutoAddToContainer = true;

      var state = mQueryContainer.Add(B3ButcheryCustomInputCreator.一般单据状态(mDFInfo.Fields["BillState"], true, false, true, true), "BillState");
      ((ChoiceBox)state).Value = 单据状态.已审核.Value.ToString() + "|";
      state.DisplayValue = "已审核;";
      state.EnableInputArgument = true;
      layout.Add("BillState", state);
      layout["BillState"].NotAutoAddToContainer = true;


      var config = new AutoLayoutConfig { Cols = 2 };
      config.Add("ID");
      config.Add("AccountingUnit_ID");
      config.Add("Date");
      config.Add("Department_ID");
      config.Add("Employee_ID");
      config.Add("Store_ID");
      config.Add("ProductLine_ID");
      config.Add("Goods_ID");
      config.Add("BillState");
      layout.Config = config;

      vPanel.Add(layout.CreateLayout());
    }

    protected override void AddQueryOptions(VLayoutPanel vPanel)
    {
      mDisplayHelper.AddOptionItem("单号", "bill", "ID", false);
      mDisplayHelper.AddOptionItem("会计单位", "bill", "AccountingUnit_Name", false);
      mDisplayHelper.AddOptionItem("日期", "bill", "Date", false);
      mDisplayHelper.AddOptionItem("部门", "bill", "Department_Name", false);
      mDisplayHelper.AddOptionItem("经办人", "bill", "Employee_Name", false);
      mDisplayHelper.AddOptionItem("仓库", "bill", "Store_Name", false);
      mDisplayHelper.AddOptionItem("生产线", "bill", "ProductLine_Name", false);
      mDisplayHelper.AddOptionItem("摘要", "bill", "Remark", false);

      mDisplayHelper.AddOptionItem("存货编码", "detail", "Goods_Code", false);
      mDisplayHelper.AddOptionItem("存货规格", "detail", "Goods_Spec", false);
      mDisplayHelper.AddOptionItem("存货名称", "detail", "Goods_Name", false);
      //      mDisplayHelper.AddOptionItem("批号", "detail", "GoodsBatch_Name", false);
      mDisplayHelper.AddOptionItem("辅数量", "detail", "SecondNumber", false, true);
      mDisplayHelper.AddOptionItem("辅单位", "detail", "Goods_SecondUnit", false);
      mDisplayHelper.AddOptionItem("主数量", "detail", "Number", false, true);
      mDisplayHelper.AddOptionItem("主单位", "detail", "Goods_MainUnit", false);
      mDisplayHelper.AddOptionItem("备注", "detail", "Remark", false);


      AddQueryOption("选项", mDisplayHelper.GetAllDisplayNames(), mDisplayHelper.GetDefaultSelelectedDisplayNames());


      base.AddQueryOptions(vPanel);
    }

    protected override DQueryDom GetQueryDom()
    {
      var dom = base.GetQueryDom();

      mDisplayHelper.AddAlias("bill", JoinAlias.Create("bill"));
      mDisplayHelper.AddAlias("detail", JoinAlias.Create("detail"));

      mDisplayHelper.AddSelectColumns(dom, (name) => OptionIsSelected("选项", name), SumColumnIndexs);
      dom.Where.Conditions.Add(DQCondition.EQ("Domain_ID", DomainContext.Current.ID));
      return dom;
    }

  }
}
