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
using BWP.Web.WebControls;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using Forks.EnterpriseServices.SqlDoms;
using TSingSoft.WebControls2;
using TSingSoft.WebPluginFramework.Controls;
using TSingSoft.WebPluginFramework.Pages;
using TSingSoft.WebPluginFramework.Security;

namespace BWP.Web.Pages.B3Butchery.Tools.GoodsAccStoreSetting_
{
  public class GoodsAccStoreSetting : ServerPage
  {
    readonly QueryContainer _mQueryContainer;

    public GoodsAccStoreSetting()
    {
      var type = GetType();
      _mQueryContainer = QueryContainer.FromResource(type.FullName + ".xml", type.Assembly);
    }

    protected override void InitForm(HtmlForm form)
    {
      if (!User.IsInRole("B3FoodDeepProcess.功能.存货仓库设置"))
        throw new AppSecurityException();
      form.Controls.Add(new PageTitle("存货仓库设置"));
      var vPanel = form.EAdd(new VLayoutPanel());
      AddQueryControl(vPanel);
      AddQueryResult(vPanel);
    }

    DFChoiceBox _goodsPropertyCatalogBox;
    private void AddQueryControl(VLayoutPanel vPanel)
    {
      var mDFInfo = DFInfo.Get(typeof(Goods_Accounting_Store));
      var manager = new LayoutManager("", mDFInfo, _mQueryContainer);
      manager.Add("Goods_ID", QueryCreator.DFChoiceBox(mDFInfo.Fields["Goods_ID"], B3UnitedInfosConsts.DataSources.存货全部));

      manager.Add("Store_ID", QueryCreator.DFChoiceBox(mDFInfo.Fields["Store_ID"], B3FrameworksConsts.DataSources.授权仓库));

      manager.Add("AccountingUnit_ID", QueryCreator.DFChoiceBox(mDFInfo.Fields["AccountingUnit_ID"], B3FrameworksConsts.DataSources.授权会计单位));

      manager.Add("GoodsPropertyCatalog_ID", new SimpleLabel("存货属性分类"), _goodsPropertyCatalogBox = QueryCreator.DFChoiceBox(mDFInfo.Fields["AccountingUnit_ID"], B3UnitedInfosConsts.DataSources.存货属性分类));

      manager.Add("GoodsProperty_ID", new SimpleLabel("存货属性"), QueryCreator.DFChoiceBox(mDFInfo.Fields["AccountingUnit_ID"], B3UnitedInfosConsts.DataSources.存货属性全部));

      var config = new AutoLayoutConfig
      {
        Cols = 8,
        DefaultLabelWidth = 4,
      };
      config.Add("Goods_ID");
      config.Add("Store_ID");
      config.Add("AccountingUnit_ID");
      config.Add("GoodsPropertyCatalog_ID");
      config.Add("GoodsProperty_ID");
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
      var btn = vPanel.Add(new DialogButton { Text = "增加记录", Url = "AddSettingDialog.aspx" }, new VLayoutOption(HorizontalAlign.Left));
      btn.Click += delegate { StartQuery(); };


      _mGrid = vPanel.Add(new DFBrowseGrid(new DFDataTableEditor()) { Width = Unit.Percentage(100) });
      _mGrid.Columns.Add(new DFBrowseGridCustomExtColumn(delegate(DFDataRow row, HtmlTableCell cell, int rowIndex)
      {
        var delBtn = new LinkButton
        {
          Text = "x"
        };
        delBtn.Click += delegate
        {
          var id = (long?)_mGrid.CurrentData.Data.Rows[rowIndex]["ID"];
          var dom = new DQDeleteDom(typeof(Goods_Accounting_Store));
          dom.Where.Conditions.Add(DQExpression.EQ(DQExpression.Field("ID"), DQExpression.Value(id)));
          using (var session = Dmo.NewSession())
          {
            session.ExecuteNonQuery(dom);
            session.Commit();
          }
          StartQuery();
        };
        delBtn.OnClientClick = "return confirm('确定删除吗？')";
        cell.Controls.Add(delBtn);
      }));
      _mGrid.Columns.Add(new DFBrowseGridAutoColumn());
    }

    private DQueryDom GetQueryDom()
    {
      var query = _mQueryContainer.Build();
      var goods = JoinAlias.Create("goods");
      var pptAlias = JoinAlias.Create("gppt");
      var propertyCatalog = new JoinAlias(typeof(GoodsPropertyCatalog));
      query.From.AddJoin(JoinType.Left, new DQDmoSource(propertyCatalog), DQCondition.EQ(pptAlias, "GoodsPropertyCatalog_ID", propertyCatalog, "ID"));
      query.Columns.Add(DQSelectColumn.Field("ID", "序号"));
      query.Columns.Add(DQSelectColumn.Field("Code", goods, "存货编码"));
      query.Columns.Add(DQSelectColumn.Create(DQExpression.Field(goods, "Name"), "存货名称"));
      query.Columns.Add(DQSelectColumn.Field("Spec", goods));
      query.Columns.Add(DQSelectColumn.Field("MainUnit", goods));
      query.Columns.Add(DQSelectColumn.Field("SecondUnit", goods));
      query.Columns.Add(DQSelectColumn.Field("Name", pptAlias, "存货属性"));
      query.Columns.Add(DQSelectColumn.Create(DQExpression.Field(propertyCatalog, "Name"), "属性分类"));
      query.Columns.Add(DQSelectColumn.Field("Store_Name"));
      query.Columns.Add(DQSelectColumn.Field("AccountingUnit_Name"));
      query.Where.Conditions.Add(DQCondition.EQ("AccountingUnit_Domain_ID", DomainContext.Current.ID));
      if (!_goodsPropertyCatalogBox.IsEmpty)
        TreeUtil.AddTreeCondition<GoodsPropertyCatalog>(query, long.Parse(_goodsPropertyCatalogBox.Value), propertyCatalog);
      return query;
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if (!IsPostBack)
        StartQuery();
    }
  }
}
