using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BWP.B3Frameworks.Utils;
using BWP.B3UnitedInfos;
using BWP.B3UnitedInfos.BO;
using BWP.Web.Layout;
using BWP.Web.Utils;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using Forks.EnterpriseServices.SqlDoms;
using TSingSoft.WebControls2;

namespace BWP.Web.Pages.B3Butchery.BaseInfos.ClientGoodsSet_
{
  public class SelectGoodsDialogs : AppBasePage
  {
    protected QueryContainer mQueryContainer;
    protected static DFInfo mDFInfo = DFInfo.Get(typeof(Goods));
    protected static DFInfo mPropDFInfo = DFInfo.Get(typeof(GoodsProperty));
    protected DFContainer dfContainer;
    protected DFEditGrid mGrid;
    protected ChoiceBox _goodsProperty, _goodsPropertyCatalog;
    protected override void InitForm(HtmlForm form)
    {
      var vPanel = new VLayoutPanel();
      form.Controls.Add(vPanel);
      dfContainer = new DFContainer
      {
        ID = "DFContainer"
      };
      form.Controls.Add(dfContainer);
      mQueryContainer = QueryContainer.FromResource(GetType().FullName + ".xml", GetType().Assembly);

      CreateQuery(vPanel);
      CreateQueryGrid(vPanel);
    }

    private void CreateQuery(VLayoutPanel vPanel)
    {
      var layoutManager = new LayoutManager("", mDFInfo, mQueryContainer);

      var config = new AutoLayoutConfig();
      layoutManager.Config = config;

      AddQueryControl(config, layoutManager);
      vPanel.Add(layoutManager.CreateLayout(), new VLayoutOption(HorizontalAlign.Center));
      var hPanel = new HLayoutPanel() { Align = HorizontalAlign.Center };

      var qButton = new TSButton("开始查询");
      hPanel.Add(qButton);
      qButton.Click += delegate
      {
        GetDetail();
        mGrid.DataBind();
      };
      dfContainer.AddNonDFControl(qButton, "$btnsearch");
      hPanel.Add(new RedirectTSButton("清除条件"));
      vPanel.Add(hPanel);
    }

    protected virtual void AddQueryControl(AutoLayoutConfig config, LayoutManager layoutManager)
    {
      layoutManager.Add("GoodsProperty_ID",
                        QueryCreator.DFChoiceBoxEnableMultiSelection(mDFInfo.Fields["GoodsProperty_ID"], mQueryContainer,
                                                                     "GoodsProperty_ID",
                                                                     B3UnitedInfosConsts.DataSources.存货属性全部));
      layoutManager["GoodsProperty_ID"].NotAutoAddToContainer = true;

      layoutManager.Add("GoodsPropertyCatalog_ID", new SimpleLabel("属性分类"),
                        QueryCreator.DFChoiceBoxEnableMultiSelection(mDFInfo.Fields["GoodsProperty_ID"], mQueryContainer,
                                                                     "GoodsPropertyCatalog_ID",
                                                                     B3UnitedInfosConsts.DataSources.存货属性分类));
      layoutManager["GoodsPropertyCatalog_ID"].NotAutoAddToContainer = true;

      config.Add("Name");
      config.Add("Code");
      config.Add("GoodsProperty_ID");
      config.Add("GoodsPropertyCatalog_ID");
    }


    public List<Goods> Details
    {
      get
      {
        if (ViewState["Details"] == null)
        {
          ViewState["Details"] = new List<Goods>();
        }
        return (List<Goods>)ViewState["Details"];
      }
      set { ViewState["Details"] = value; }
    }

    protected virtual void ReturnList(IList<long> result)
    {
      var detailList = new List<long>();
      foreach (var saleGoods_ID in result)
      {
        detailList.Add(saleGoods_ID);
      }
      DialogUtil.SetCachedObj(this, detailList);
    }

    protected virtual void CreateQueryGrid(VLayoutPanel panel)
    {
      var detailEditor = new DFCollectionEditor<Goods>(() => Details);
      detailEditor.IsEditableFunc = (field, detail) => true;
      detailEditor.AllowDeletionFunc = () => false;
      detailEditor.CanDeleteFunc = (detail) => false;
      detailEditor.CanSelectFunc = (detail) => true;
      mGrid = new DFEditGrid(detailEditor)
      {
        Width = Unit.Percentage(100)
      };
      mGrid.MultiSelectionEnabled = true;
      CreateQueryGridColumns(mGrid);

      panel.Add(mGrid);
      var button = new TSButton("选中");
      panel.Add(button);
      button.Click += GridMultiSelection;
    }

    public void GridMultiSelection(object sender, EventArgs e)
    {
      ReturnData(mGrid.GetSelectedItems());
    }

    public virtual void ReturnData(IList list)
    {
      if (list != null && list.Count > 0)
      {
        DialogUtil.SetCachedObj(this, list.Cast<Goods>().Select(x => x.ID).ToList());
      }
    }

    protected virtual void CreateQueryGridColumns(DFEditGrid grid)
    {
      grid.Columns.Add(new DFEditGridColumn<DFValueLabel>("ID"));
      grid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Code"));
      grid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Name"));
      grid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Spec"));
      grid.Columns.Add(new DFEditGridColumn<DFValueLabel>("MainUnit"));
      grid.Columns.Add(new DFEditGridColumn<DFValueLabel>("SecondUnit"));
      grid.Columns.Add(new DFEditGridColumn<DFValueLabel>("GoodsProperty_Name"));
      grid.Columns.Add(new DFEditGridColumn<DFValueLabel>("GoodsPropertyCatalog_Name"));
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if (!IsPostBack)
      {
        GetDetail();
        mGrid.DataBind();
      }
    }

    public virtual void GetDetail()
    {
      Details.Clear();
      var prop = JoinAlias.Create("prop");

      DQueryDom dom = mQueryContainer.Build();
//      dom.Where.Conditions.Add(DQCondition.EQ(prop, "IsButchery", true));
      var rootAlias = dom.From.RootSource.Alias;

      var goodsPropertyCatalog = new JoinAlias(typeof(GoodsPropertyCatalog));
      dom.From.AddJoin(JoinType.Left, new DQDmoSource(goodsPropertyCatalog), DQCondition.EQ(prop, "GoodsPropertyCatalog_ID", goodsPropertyCatalog, "ID"));

      dom.Columns.Add(DQSelectColumn.Field("Code", rootAlias));
      dom.Columns.Add(DQSelectColumn.Field("Name", rootAlias));
      dom.Columns.Add(DQSelectColumn.Field("Spec", rootAlias));
      dom.Columns.Add(DQSelectColumn.Field("MainUnit", rootAlias));
      dom.Columns.Add(DQSelectColumn.Field("SecondUnit", rootAlias));
      dom.Columns.Add(DQSelectColumn.Field("Name", prop));
      dom.Columns.Add(DQSelectColumn.Field("Name", goodsPropertyCatalog));
      dom.Where.Conditions.Add(DQCondition.EQ("Stopped", false));
      DomainUtil.AddDomainPermissionLimit(dom, typeof(GoodsProperty), prop);
      using (var session = Dmo.NewSession())
      {
        using (var reader = session.ExecuteReader(dom))
        {
          while (reader.Read())
          {
            var detail = new Goods();

            detail.ID = (long)reader[0];
            detail.Code = (string)reader[1];
            detail.Name = (string)reader[2];
            detail.Spec = (string)reader[3];
            detail.MainUnit = (string)reader[4];
            detail.SecondUnit = (string)reader[5];
            detail.GoodsProperty_Name = (string)reader[6];
            detail.GoodsPropertyCatalog_Name = (string)reader[7];

            Details.Add(detail);
          }
        }
      }
    }

  }
}
