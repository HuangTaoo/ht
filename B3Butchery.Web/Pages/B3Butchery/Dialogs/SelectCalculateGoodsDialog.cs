using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Butchery.BO;
using BWP.B3Butchery.Utils;
using BWP.B3Frameworks;
using BWP.B3UnitedInfos;
using BWP.B3UnitedInfos.BO;
using BWP.Web.Layout;
using BWP.Web.Utils;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using Forks.Utils.Collections;
using TSingSoft.WebControls2;
using TSingSoft.WebPluginFramework;

namespace BWP.Web.Pages.B3Butchery.Dialogs
{
  class SelectCalculateGoodsDialog : DmoMultiSelectDialog<CalculateGoods, TemGoodsDetail>
  {
    protected override void CreateQuery(VLayoutPanel vPanel)
    {
      var layoutManager = new LayoutManager("", mDFInfo, mQueryContainer);
//      layoutManager.Add("计数分类", new SimpleLabel("计数分类"), QueryCreator.DFChoiceBox(mDFInfo.Fields["ID"], B3ButcheryDataSource.计数分类));
//      layoutManager.Add("CalculateCatalog_ID", mQueryContainer.Add(QueryCreator.DFChoiceBox(mDFInfo.Fields["CalculateCatalog_ID"], B3ButcheryDataSource.计数分类), "CalculateCatalog_ID"));
//      layoutManager["ProductLine_ID"].NotAutoAddToContainer = true;
      var config = new AutoLayoutConfig { Cols = 8, DefaultLabelWidth = 4 };
      config.Add("Name");
      config.Add("Code");
      config.Add("CalculateCatalog_ID");
//      config.Add("计数分类");

      layoutManager.Config = config;

      var section = mPageLayoutManager.AddSection(B3FrameworksConsts.PageLayouts.QueryConditions, B3FrameworksConsts.PageLayouts.QueryConditions_DisplayName);
      section.ApplyLayout(layoutManager, config, mPageLayoutManager, mDFInfo as DFInfo);
      vPanel.Add(layoutManager.CreateLayout());
      base.CreateQuery(vPanel);
    }

    protected override void CreateQueryGridColumns(DFBrowseGrid grid)
    {
      grid.Columns.Add(new DFBrowseGridColumn("Name"));
      grid.Columns.Add(new DFBrowseGridColumn("Code"));
      grid.Columns.Add(new DFBrowseGridColumn("CalculateCatalog_Name"));
      grid.Columns.Add(new DFBrowseGridColumn("MainUnit"));
      grid.Columns.Add(new DFBrowseGridColumn("SecondUnit"));

      grid.ValueColumns.Add("ID");
    }

    protected override DQueryDom GetQueryDom()
    {
      var dom = base.GetQueryDom();
      dom.Columns.Add(DQSelectColumn.Field("ID"));
      dom.Columns.Add(DQSelectColumn.Field("Name"));
      dom.Columns.Add(DQSelectColumn.Field("Code"));
      dom.Columns.Add(DQSelectColumn.Field("CalculateCatalog_Name"));
      dom.Columns.Add(DQSelectColumn.Field("MainUnit"));
      dom.Columns.Add(DQSelectColumn.Field("SecondUnit"));
      dom.Where.Conditions.Add(DQCondition.EQ("Stopped", false));
      dom.Where.Conditions.Add(DQCondition.EQ("Domain_ID",DomainContext.Current.ID));
      return dom;
    }


    protected override void SetResultFromDFDataRow(TemGoodsDetail dmo, DFDataRow row)
    {
      dmo.Goods_ID = (long)row["ID"];
    }

  }
}
