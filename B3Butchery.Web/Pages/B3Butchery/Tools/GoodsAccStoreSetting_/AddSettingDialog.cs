using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BWP.B3Butchery.BO;
using BWP.B3Frameworks;
using BWP.B3Frameworks.Utils;
using BWP.B3UnitedInfos;
using BWP.B3UnitedInfos.BO;
using BWP.Web.Layout;
using BWP.Web.Utils;
using Forks.EnterpriseServices.BusinessInterfaces;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using Forks.EnterpriseServices.SqlDoms;
using TSingSoft.WebControls2;
using TSingSoft.WebPluginFramework.Pages;

namespace BWP.Web.Pages.B3Butchery.Tools.GoodsAccStoreSetting_
{
  public class AddSettingDialog : ServerPage
  {
    readonly QueryContainer _mQueryContainer;

    public AddSettingDialog()
    {
      var type = GetType();
      _mQueryContainer = QueryContainer.FromResource(type.FullName + ".xml", type.Assembly);
    }

    protected override void InitForm(HtmlForm form)
    {
      var vPanel = form.EAdd(new VLayoutPanel());
      AddQueryControl(vPanel);
      AddQueryResult(vPanel);
    }

    DFChoiceBox _goodsPropertyCatalogBox;
    DFCheckBox _checkBox;
    private void AddQueryControl(VLayoutPanel vPanel)
    {
      var mDFInfo = DFInfo.Get(typeof(Goods));
      var manager = new LayoutManager("", mDFInfo, _mQueryContainer);
      manager.Add("ID", new SimpleLabel("存货ID"), QueryCreator.DFTextBox(mDFInfo.Fields["ID"]));

      manager.Add("Name", QueryCreator.DFTextBox(mDFInfo.Fields["Name"]));

      manager.Add("Code", QueryCreator.DFTextBox(mDFInfo.Fields["Code"]));

      manager.Add("GoodsPropertyCatalog_ID", new SimpleLabel("存货属性分类"), _goodsPropertyCatalogBox = QueryCreator.DFChoiceBox(mDFInfo.Fields["ID"], B3UnitedInfosConsts.DataSources.存货属性分类));

      manager.Add("GoodsProperty_ID", new SimpleLabel("存货属性"), QueryCreator.DFChoiceBox(mDFInfo.Fields["ID"], B3UnitedInfosConsts.DataSources.存货属性全部));
      _checkBox = new DFCheckBox { Text = "隐藏已设置的存货", Checked = true };
      manager.Add("Stopped", _checkBox, false, true);
      var config = new AutoLayoutConfig
      {
        Cols = 8,
        DefaultLabelWidth = 4,
      };
      config.Add("ID");
      config.Add("Name");
      config.Add("Code");
      config.Add("GoodsPropertyCatalog_ID");
      config.Add("GoodsProperty_ID");
      config.Add("Stopped");
      manager.Config = config;
      vPanel.Add(manager.CreateLayout());
      var hPanel = vPanel.Add(new HLayoutPanel());
      hPanel.Add(new TSButton("开始查询", delegate { StartQuery(); }));
      hPanel.Add(new RedirectTSButton("清除条件"));
    }

    void StartQuery()
    {
      _mGrid.Query = GetQueryDom();
      _mGrid.CurrentPageIndex = 0;
      _mGrid.DataBind();
    }

    DFBrowseGrid _mGrid;
    private void AddQueryResult(VLayoutPanel vPanel)
    {
      _mGrid = vPanel.Add(new DFBrowseGrid(new DFDataTableEditor()) { Width = Unit.Percentage(100), MultiSelectionEnabled = true });
      _mGrid.Columns.Add(new DFBrowseGridAutoColumn());

      AddUpdatePanel(vPanel);
    }

    ChoiceBox _accountingUnit, _store;
    private void AddUpdatePanel(VLayoutPanel vPanel)
    {
      var hPanel = vPanel.Add(new HLayoutPanel());
      hPanel.Add(new SimpleLabel("会计单位"));
      _accountingUnit = hPanel.Add(new ChoiceBox(B3FrameworksConsts.DataSources.授权会计单位) { Width = Unit.Pixel(130), EnableInputArgument = true });
      hPanel.Add(new SimpleLabel("仓库"));
      _store = hPanel.Add(new ChoiceBox(B3FrameworksConsts.DataSources.授权仓库) { Width = Unit.Pixel(130), EnableInputArgument = true });
      hPanel.Add(new TSButton("指定存货仓库", delegate { UpdateOrInsert(); }));
    }

    private void UpdateOrInsert()
    {
      if (String.IsNullOrEmpty(_accountingUnit.Value))
        throw new ApplicationException("请选择会计单位!");
      if (String.IsNullOrEmpty(_store.Value))
        throw new ApplicationException("请选择仓库!");
      if (_mGrid.CurrentData == null)
        throw new ApplicationException("请选择存货！");

      foreach (DFDataRow row in _mGrid.GetSelectedItems())
      {
        var accountingUnitID = Convert.ToInt64(_accountingUnit.Value);
        var goodsID = (long?)row["ID"];
        if (ISExitSameRecord(accountingUnitID, goodsID))
          throw new ApplicationException("同一个存货，同一个会计单位，不能有多个仓库！");
      }

      foreach (DFDataRow row in _mGrid.GetSelectedItems())
      {
        var goodsStore = new Goods_Accounting_Store
        {
          AccountingUnit_ID = Convert.ToInt64(_accountingUnit.Value),
          Store_ID = Convert.ToInt64(_store.Value),
          Goods_ID = (long) row["ID"]
        };
        using (var context = new TransactionContext())
        {
          context.Session.Insert(goodsStore);
          context.Commit();
        }
      }
      DialogUtil.SetCachedObj(this, 1);
    }

    private bool ISExitSameRecord(long? accountingUnitID, long? goodsID)
    {
      var query = new DQueryDom(new JoinAlias(typeof(Goods_Accounting_Store)));
      query.Columns.Add(DQSelectColumn.Count());
      query.Where.Conditions.Add(DQCondition.EQ("AccountingUnit_ID", accountingUnitID));
      query.Where.Conditions.Add(DQCondition.EQ("Goods_ID", goodsID));

      using (var session = Dmo.NewSession())
      {
        return Convert.ToInt32(session.ExecuteScalar(query)) != 0;
      }
    }

    private DQueryDom GetQueryDom()
    {
      var query = _mQueryContainer.Build();
      var root = query.From.RootSource.Alias;
      var pptAlias = JoinAlias.Create("gppt");
      var propertyCatalog = new JoinAlias(typeof(GoodsPropertyCatalog));
      var alreadySet = new JoinAlias(typeof(Goods_Accounting_Store));
      query.From.AddJoin(JoinType.Left, new DQDmoSource(propertyCatalog), DQCondition.EQ(pptAlias, "GoodsPropertyCatalog_ID", propertyCatalog, "ID"));
      query.From.AddJoin(JoinType.Left, new DQDmoSource(alreadySet), DQCondition.EQ(root, "ID", alreadySet, "Goods_ID"));
      query.Columns.Add(DQSelectColumn.Field("ID", "存货ID"));
      query.Columns.Add(DQSelectColumn.Field("Code"));
      query.Columns.Add(DQSelectColumn.Field("Name"));
      query.Columns.Add(DQSelectColumn.Field("Spec"));
      query.Columns.Add(DQSelectColumn.Field("MainUnit"));
      query.Columns.Add(DQSelectColumn.Field("SecondUnit"));
      query.Columns.Add(DQSelectColumn.Create(DQExpression.Field(pptAlias, "Name"), "存货属性"));
      query.Columns.Add(DQSelectColumn.Create(DQExpression.Field(propertyCatalog, "Name"), "属性分类"));
      query.Columns.Add(DQSelectColumn.Field("AccountingUnit_Name", alreadySet, "已配会计单位"));
      if (!_goodsPropertyCatalogBox.IsEmpty)
        TreeUtil.AddTreeCondition<GoodsPropertyCatalog>(query, long.Parse(_goodsPropertyCatalogBox.Value), propertyCatalog);
      if (_checkBox.Checked)
        query.Where.Conditions.Add(DQCondition.IsNull(DQExpression.Field(alreadySet, "Goods_ID")));
      return query;
    }
  }
}
