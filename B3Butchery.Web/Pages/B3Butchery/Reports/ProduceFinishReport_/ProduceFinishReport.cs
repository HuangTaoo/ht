using BWP.B3Frameworks;
using BWP.B3Frameworks.BO.NamedValueTemplate;
using BWP.Web.Layout;
using BWP.Web.Utils;
using BWP.Web.WebControls;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using Forks.EnterpriseServices.SqlDoms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSingSoft.WebControls2;
using BWP.B3Butchery.BO;
using BWP.B3ProduceUnitedInfos;
using BWP.B3UnitedInfos;
using BWP.B3UnitedInfos.BO;

namespace BWP.Web.Pages.B3Butchery.Reports.ProduceFinishReport_
{
  class ProduceFinishReport : DFBrowseGridReportPage<ProduceFinish>
  {
    protected override string Caption
    {
      get { return "生产完工分析"; }
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
      get { return "B3Butchery.报表.生产完工分析"; }
    }



    readonly DFInfo _detailInfo = DFInfo.Get(typeof(ProduceFinish_Detail));

   // CheckBoxListWithReverseSelect _checkbox;
    readonly Dictionary<string, DFInfo> _fileInfo = new Dictionary<string, DFInfo>();
    readonly List<string> _sumCol = new List<string>();

    protected override void AddQueryControls(VLayoutPanel vPanel)
    {
      var layout = new LayoutManager("Main", mDFInfo, mQueryContainer);

      layout.Add("ID", mQueryContainer.Add(new DFTextBox(mDFInfo.Fields["ID"]), "ID"));
      layout["ID"].NotAutoAddToContainer = true;
      layout.Add("AccountingUnit_ID", new SimpleLabel("会计单位"), QueryCreator.DFChoiceBoxEnableMultiSelection(mDFInfo.Fields["AccountingUnit_ID"], mQueryContainer, "AccountingUnit_ID", B3FrameworksConsts.DataSources.授权会计单位全部));
      layout["AccountingUnit_ID"].NotAutoAddToContainer = true;
      layout.Add("Department_ID", new SimpleLabel("部门"), QueryCreator.DFChoiceBoxEnableMultiSelection(mDFInfo.Fields["Department_ID"], mQueryContainer, "Department_ID", "授权部门"));
      layout["Department_ID"].NotAutoAddToContainer = true;
      layout.Add("Employee_ID", new SimpleLabel("经办人"), QueryCreator.DFChoiceBoxEnableMultiSelection(mDFInfo.Fields["Employee_ID"], mQueryContainer, "Employee_ID", "授权员工"));
      layout["Employee_ID"].NotAutoAddToContainer = true;
       layout.Add("ProductionUnit_ID", new SimpleLabel("生产单位"), QueryCreator.DFChoiceBoxEnableMultiSelection(mDFInfo.Fields["ProductionUnit_ID"], mQueryContainer, "ProductionUnit_ID", B3ProduceUnitedInfosDataSources.生产单位全部));
      layout["ProductionUnit_ID"].NotAutoAddToContainer = true;
      layout.Add("Goods_ID", new SimpleLabel("存货"), QueryCreator.DFChoiceBoxEnableMultiSelection(mDFInfo.Fields["ID"], mQueryContainer, "Goods_ID", B3UnitedInfosConsts.DataSources.存货));
      layout["Goods_ID"].NotAutoAddToContainer = true;
      layout.Add("GoodsProperty_ID", new SimpleLabel("存货属性"), QueryCreator.DFChoiceBoxEnableMultiSelection(mDFInfo.Fields["ID"], mQueryContainer, "GoodsProperty_ID", B3UnitedInfosConsts.DataSources.存货属性全部));
      layout["GoodsProperty_ID"].NotAutoAddToContainer = true;
      layout.Add("BrandItem_ID", new SimpleLabel("品牌项"), QueryCreator.DFChoiceBoxEnableMultiSelection(mDFInfo.Fields["ID"], mQueryContainer, "BrandItem_ID", B3UnitedInfosConsts.DataSources.品牌项));
      layout["BrandItem_ID"].NotAutoAddToContainer = true;
      layout.Add("ProductLine_ID", new SimpleLabel("产品线"), QueryCreator.DFChoiceBoxEnableMultiSelection(mDFInfo.Fields["ID"], mQueryContainer, "ProductLine_ID", B3UnitedInfosConsts.DataSources.产品线全部));
      layout["ProductLine_ID"].NotAutoAddToContainer = true;
      layout.Add("Date", new SimpleLabel("生产日期"), QueryCreator.TimeRange(mDFInfo.Fields["Date"], mQueryContainer, "MinDate", "MaxDate"));
      layout["Date"].NotAutoAddToContainer = true;
      var state = mQueryContainer.Add(B3ButcheryCustomInputCreator.一般单据状态(mDFInfo.Fields["BillState"], true, false, true, true), "BillState");
      ((ChoiceBox)state).Value = 单据状态.已审核.Value.ToString() + "|";
      state.DisplayValue = "已审核;";
      state.EnableInputArgument = true;
      layout.Add("BillState", state);
      layout["BillState"].NotAutoAddToContainer = true;
      var config = new AutoLayoutConfig { Cols = 2 };
      config.Add("ID");
      config.Add("AccountingUnit_ID");
      config.Add("Department_ID");
      config.Add("Employee_ID");
      config.Add("ProductionUnit_ID");
      config.Add("Goods_ID");
      config.Add("GoodsProperty_ID");
      config.Add("BrandItem_ID");
      config.Add("ProductLine_ID");
      config.Add("Date");
      config.Add("BillState");

      layout.Config = config;

      vPanel.Add(layout.CreateLayout());
    }

    ReportDisplayOptionHelper mDisplayHelper = new ReportDisplayOptionHelper();
    protected override void AddQueryOptions(VLayoutPanel vPanel)
    {
      mDisplayHelper.AddOptionItem("单号", "bill", "ID", false);
      mDisplayHelper.AddOptionItem("日期", "bill", "Date", false);
      mDisplayHelper.AddOptionItem("生产单位", "bill", "ProductionUnit_Name", false);
      mDisplayHelper.AddOptionItem("会计单位", "bill", "AccountingUnit_Name", false);
      mDisplayHelper.AddOptionItem("部门", "bill", "Department_Name", false);
      mDisplayHelper.AddOptionItem("经办人", "bill", "Employee_Name", false);
      mDisplayHelper.AddOptionItem("客户", "bill", "Customer_Name", false);
      mDisplayHelper.AddOptionItem("存货属性", "detail", "GoodsProperty_Name", false);
      mDisplayHelper.AddOptionItem("品牌项", "detail", "BrandItem_Name", false);
      mDisplayHelper.AddOptionItem("产品线", "goods", "ProductLine_Name", false);
      mDisplayHelper.AddOptionItem("存货编号", "detail", "Goods_Code", false);
      mDisplayHelper.AddOptionItem("存货名称", "detail", "Goods_Name", false);
      mDisplayHelper.AddOptionItem("规格", "detail", "Goods_Spec", false);
      mDisplayHelper.AddOptionItem("主单位", "detail", "Goods_MainUnit", false);
      mDisplayHelper.AddOptionItem("主数量", "detail", "Number", true,true);
      mDisplayHelper.AddOptionItem("辅单位", "detail", "Goods_SecondUnit", false);
      mDisplayHelper.AddOptionItem("辅数量", "detail", "SecondNumber", true,true);
      mDisplayHelper.AddOptionItem("计划数量", "noticDetail", "Number", true, true);
      mDisplayHelper.AddOptionItem("未完成数量", () => {
        return DQExpression.Subtract(DQExpression.Field(JoinAlias.Create("noticDetail"), "Number"), DQExpression.Field(JoinAlias.Create("noticDetail"), "DoneNumber"));
      }, true, true);
      AddQueryOption("选项", mDisplayHelper.GetAllDisplayNames(), mDisplayHelper.GetDefaultSelelectedDisplayNames());
      base.AddQueryOptions(vPanel);
    }

    protected override DQueryDom GetQueryDom()
    {
      var dom = base.GetQueryDom();
      var bill = dom.From.RootSource.Alias;
      mDisplayHelper.AddAlias("bill", JoinAlias.Create("bill"));
      var detail = new JoinAlias("detail", typeof(ProduceFinish_Detail));
      var noticDetail = new JoinAlias("noticDetail", typeof(ProductNotice_Detail));
      var goods = new JoinAlias("goods", typeof(Goods));
      mDisplayHelper.AddAlias("detail", JoinAlias.Create("detail"));
      mDisplayHelper.AddAlias("noticDetail", JoinAlias.Create("noticDetail"));
      mDisplayHelper.AddAlias("goods", JoinAlias.Create("goods"));
      dom.From.AddJoin(JoinType.Left, new DQDmoSource(detail), DQCondition.EQ(bill, "ID", detail, "ProduceFinish_ID"));
      dom.From.AddJoin(JoinType.Left, new DQDmoSource(noticDetail), DQCondition.EQ(detail, "ProductNotice_Detail_ID", noticDetail, "ID"));
      dom.From.AddJoin(JoinType.Left, new DQDmoSource(goods), DQCondition.EQ(detail, "Goods_ID", goods, "ID"));
      var goodsChb = mQueryContainer.GetControl<DFChoiceBox>("Goods_ID");
      var proChb = mQueryContainer.GetControl<DFChoiceBox>("GoodsProperty_ID");
      var brandChb = mQueryContainer.GetControl<DFChoiceBox>("BrandItem_ID");
      var lineChb = mQueryContainer.GetControl<DFChoiceBox>("ProductLine_ID");
      if (!goodsChb.IsEmpty) {
        dom.Where.Conditions.Add(DQCondition.InList(DQExpression.Field(detail, "Goods_ID"), goodsChb.GetValues().Select(x => DQExpression.Value(x)).ToArray()));
      }
      if (!proChb.IsEmpty) {
        dom.Where.Conditions.Add(DQCondition.InList(DQExpression.Field(detail, "GoodsProperty_ID"), proChb.GetValues().Select(x => DQExpression.Value(x)).ToArray()));
      }
      if (!brandChb.IsEmpty) {
        dom.Where.Conditions.Add(DQCondition.InList(DQExpression.Field(detail, "BrandItem_ID"), brandChb.GetValues().Select(x => DQExpression.Value(x)).ToArray()));
      }
      if (!lineChb.IsEmpty) {
        dom.Where.Conditions.Add(DQCondition.InList(DQExpression.Field(goods, "ProductLine_ID"), lineChb.GetValues().Select(x => DQExpression.Value(x)).ToArray()));
      }
      mDisplayHelper.AddSelectColumns(dom, (name) => OptionIsSelected("选项", name), SumColumnIndexs);
      return dom;
    }
  }
}
