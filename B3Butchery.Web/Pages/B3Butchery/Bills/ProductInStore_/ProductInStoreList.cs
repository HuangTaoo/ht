using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using TSingSoft.WebControls2;
using TSingSoft.WebPluginFramework;
using BWP.Web.Utils;
using BWP.B3Butchery.Utils;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.SqlDoms;
using System.Web.UI.WebControls;
using BWP.B3Frameworks.BO;
using BWP.Web.Layout;

namespace BWP.Web.Pages.B3Butchery.Bills.ProductInStore_
{
	public class ProductInStoreList : DomainBillListPage<ProductInStore, IProductInStoreBL>
	{
     ChoiceBox planNumberBox;
		protected override void AddQueryControls(VLayoutPanel vPanel)
		{
			vPanel.Add(CreateDefaultBillQueryControls((panel, config) =>
			{
				panel.Add("ProductPlan_ID", new SimpleLabel("生产计划号"), planNumberBox = new ChoiceBox(B3ButcheryDataSource.计划号) { EnableMultiSelection = true, EnableInputArgument = true, Width = Unit.Pixel(160), EnableTopItem = true });
				config.AddAfter("AccountingUnit_ID", "ID");
				config.AddAfter("Department_ID", "AccountingUnit_ID");
				config.AddAfter("Employee_ID", "Department_ID");
				config.AddAfter("Store_ID", "Employee_ID");
				config.AddAfter("InStoreType_ID", "Store_ID");
				config.AddAfter("InStoreDate", "InStoreType_ID");
				config.AddAfter("CheckEmployee_ID", "InStoreDate");
				config.AddAfter("CheckDate", "CheckEmployee_ID");
        config.AddAfter("ProductPlan_ID", "CheckDate");
        AddQuery(config);
			}));
		}

    protected virtual void AddQuery(AutoLayoutConfig config)
    {

    }

		protected override void AddDFBrowseGridColumn(DFBrowseGrid grid, string field)
		{
			base.AddDFBrowseGridColumn(grid, field);
			if (field == "BillState")
			{
				AddDFBrowseGridColumn(grid, "AccountingUnit_Name");
				AddDFBrowseGridColumn(grid, "Department_Name");
				AddDFBrowseGridColumn(grid, "Employee_Name");
				AddDFBrowseGridColumn(grid, "Store_Name");
				AddDFBrowseGridColumn(grid, "InStoreType_Name");
				AddDFBrowseGridColumn(grid, "InStoreDate");
				AddDFBrowseGridColumn(grid, "CheckEmployee_Name");
				AddDFBrowseGridColumn(grid, "CheckDate");
        AddDFBrowseGridColumn(grid, "CheckUser_Name");
        AddDFBrowseGridColumn(grid, "Remark");
      }
		}

    protected override DQueryDom GetQueryDom()
    {
      var query = base.GetQueryDom();
      if (!planNumberBox.IsEmpty)
      {
        var qType = typeof(ProductInStoreTemp);
        var detail = new JoinAlias(qType);
        query.RegisterQueryTable(qType, new string[] { "MainID" }, ProductInStoreTemp.GetQueryDom(planNumberBox.Value));
        query.From.AddJoin(JoinType.Left, new DQDmoSource(detail), DQCondition.EQ(query.From.RootSource.Alias, "ID", detail, "MainID"));
        query.Where.Conditions.Add(DQCondition.IsNotNull(DQExpression.Field(detail, "MainID")));
      }
      return query;
    }

		protected override void InitToolBar(HLayoutPanel toolbar)
		{
			base.InitToolBar(toolbar);
			if (User.IsInRole("B3Butchery.报表.成品入库分析"))
			{
				var dataAnysBtn = new TSButton() { Text = "数据分析", UseSubmitBehavior = false };
				dataAnysBtn.OnClientClick = string.Format("OpenUrlInTopTab('{0}','成品入库分析');return false;", WpfPageUrl.ToGlobal(AspUtil.AddTimeStampToUrl("~/B3Butchery/Reports/ProductInStoreReport_/ProductInStoreReport.aspx")));
				toolbar.Add(dataAnysBtn);
			}
		}

        public override bool EnableBatchCheck
        {
            get
            {
                return CheckDefaultRole("批量审核", true);
            }
        }

    public override bool EnableBatchPrint
    {
      get
      {
        return CheckDefaultRole("打印", true);
      }
    }

  }

  class ProductInStoreTemp
  {
    public long MainID { get; set; }

    public static DQueryDom GetQueryDom(string planNumber)
    {
      var query = new DQueryDom(new JoinAlias(typeof(ProductInStore_Detail)));
			query.Columns.Add(DQSelectColumn.Field("ProductInStore_ID", "MainID"));
      if (!string.IsNullOrEmpty(planNumber))
      {
        var expression = new List<IDQExpression>();
        foreach (var item in planNumber.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries))
          expression.Add(DQExpression.Value(long.Parse(item)));
        if (expression.Count > 0)
          query.Where.Conditions.Add(DQCondition.InList(DQExpression.Field("ProductPlan_ID"), expression.ToArray()));
      }
      query.Distinct = true;
      return query;
    }
  }
}