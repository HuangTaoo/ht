using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Butchery.BO;
using TSingSoft.WebControls2;
using BWP.Web.Layout;
using Forks.EnterpriseServices.DataForm;
using System.Web.UI.WebControls;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.SqlDoms;
using BWP.B3Frameworks;
using BWP.B3Frameworks.Utils;
using BWP.Web.Utils;
using System.Web.UI.HtmlControls;
using Forks.EnterpriseServices.BusinessInterfaces;

namespace BWP.Web.Pages.B3Butchery.Dialogs
{
  class ProductInStoreTempDialog : AppBasePage
  {
    protected QueryContainer mQueryContainer;
    protected static DFInfo mDFInfo = DFInfo.Get(typeof(ProductInStore_Temp));
    protected DFContainer dfContainer;
    DFBrowseGrid _grid;
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
      var layout = new LayoutManager("",mDFInfo,mQueryContainer);
      var config = new AutoLayoutConfig();
      layout.Config = config;
      config.Add("ID");
      config.Add("Name");
      config.Add("AccountingUnit_ID");
      config.Add("Department_ID");
      config.Add("Employee_ID");     
      vPanel.Add(layout.CreateLayout());

      HLayoutPanel hPanel = new HLayoutPanel() { Align = HorizontalAlign.Left };
      TSButton qButton = new TSButton("开始查询");
      hPanel.Add(qButton);
      qButton.Click += delegate
      {
        _grid.Query = GetDQueryDom();
        _grid.DataBind();
      };
      dfContainer.AddNonDFControl(qButton, "$btnsearch");
      hPanel.Add(new RedirectTSButton("清除条件"));
      vPanel.Add(hPanel);
      vPanel.Add(new HLayoutPanel());
    }

    private void CreateQueryGrid(VLayoutPanel vPanel)
    {
      _grid = new DFBrowseGrid(new DFDataTableEditor()) 
      {
        Width = Unit.Percentage(100)
      };
      _grid.MultiSelectionEnabled = true;
      CreateQueryGridColumns(_grid);
      var hPanel = new HLayoutPanel() { Align = HorizontalAlign.Left };
      vPanel.Add(_grid);
      var button = new TSButton("选中");
      hPanel.Add(button);
      vPanel.Add(hPanel);
      button.Click += GridMultiSelection;
    }
    void GridMultiSelection(object sender, EventArgs e)
    {
      var billType = DmoTypeIDAttribute.GetID(typeof(ProductInStore_Temp));
      IList<ProductInStore_Temp> selectList = new List<ProductInStore_Temp>();
      _grid.GetFromUI();
      foreach (var row in _grid.GetSelectedItems())
      {
        var detail = new ProductInStore_Temp();
        detail.AccountingUnit_ID = (long?)row["AccountingUnit_ID"];
        detail.AccountingUnit_Name = (string)row["AccountingUnit_Name"];
        detail.Department_ID = (long?)row["Department_ID"];
        detail.Department_Name = (string)row["Department_Name"];
        detail.Employee_ID = (long?)row["Employee_ID"];
        detail.Employee_Name = (string)row["Employee_Name"];
        detail.InStoreType_ID = (long?)row["InStoreType_ID"];
        detail.InStoreType_Name = (string)row["InStoreType_Name"];
        detail.Store_ID = (long?)row["Store_ID"];
        detail.Store_Name = (string)row["Store_Name"];
        detail.CheckEmployee_ID = (long?)row["CheckEmployee_ID"];
        detail.CheckEmployee_Name = (string)row["CheckEmployee_Name"];
        detail.CheckDate = (DateTime?)row["CheckDate"];
        detail.InStoreDate = (DateTime?)row["InStoreDate"];
        
        var temp = GetDetails((long)row["ID"]);
        foreach (var de in temp)
        {
          var tempdetail = new ProductInStore_Temp_Detail();
          tempdetail.Goods_ID = de.Goods_ID;
          tempdetail.Goods_Code = de.Goods_Code;
          tempdetail.Goods_Name = de.Goods_Name;
          tempdetail.Goods_Spec = de.Goods_Spec;
          tempdetail.BrandItem_ID = de.BrandItem_ID;
          tempdetail.BrandItem_Name = de.BrandItem_Name;
          detail.Details.Add(tempdetail);
        }
        //detail.Goods_ID = (long)row["Goods_ID"];
        //detail.Goods_Name = (string)row["Goods_Name"];
        //detail.Goods_Code = (string)row["Goods_Code"];
        //detail.Goods_Spec = (string)row["Goods_Spec"];
        selectList.Add(detail);
      }
      DialogUtil.SetCachedObj(this, selectList);
    }

    private DQueryDom GetDQueryDom()
    {
      DQueryDom dom = mQueryContainer.Build();
      var main = dom.From.RootSource.Alias;
      var detail = new JoinAlias(typeof(ProductInStore_Temp_Detail));
      dom.From.AddJoin(JoinType.Inner, new DQDmoSource(detail), DQCondition.EQ(main, "ID", detail, "ProductInStoreTemp_ID"));
      dom.Columns.Add(DQSelectColumn.Field("ID", main));
      dom.Columns.Add(DQSelectColumn.Field("Name",main));
      dom.Columns.Add(DQSelectColumn.Field("AccountingUnit_ID", main));
      dom.Columns.Add(DQSelectColumn.Field("AccountingUnit_Name", main));
      dom.Columns.Add(DQSelectColumn.Field("Department_ID", main));
      dom.Columns.Add(DQSelectColumn.Field("Department_Name", main));
      dom.Columns.Add(DQSelectColumn.Field("Store_ID", main));
      dom.Columns.Add(DQSelectColumn.Field("Store_Name", main));
      dom.Columns.Add(DQSelectColumn.Field("Employee_ID", main));
      dom.Columns.Add(DQSelectColumn.Field("Employee_Name", main));
      dom.Columns.Add(DQSelectColumn.Field("InStoreType_ID", main));
      dom.Columns.Add(DQSelectColumn.Field("InStoreType_Name", main));
      dom.Columns.Add(DQSelectColumn.Field("CheckEmployee_ID", main));
      dom.Columns.Add(DQSelectColumn.Field("CheckEmployee_Name", main));
      dom.Columns.Add(DQSelectColumn.Field("InStoreDate", main));
      dom.Columns.Add(DQSelectColumn.Field("CheckDate", main));
      dom.Distinct = true;
      dom.Where.Conditions.Add(DQCondition.EQ("Stopped",false));
      //dom.Columns.Add(DQSelectColumn.Field("Goods_ID", detail));
      //dom.Columns.Add(DQSelectColumn.Field("Goods_Name", detail));
      //dom.Columns.Add(DQSelectColumn.Field("Goods_Code", detail));
      //dom.Columns.Add(DQSelectColumn.Field("Goods_Spec", detail));
      //dom.Columns.Add(DQSelectColumn.Create(DQExpression.Field(detail, "ID"), "DetailID"));
      dom.Where.Conditions.Add(DQCondition.EQ(main, "Domain_ID", DomainContext.Current.ID));
      return dom;
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if (!IsPostBack)
      {
        _grid.Query = GetDQueryDom();
        _grid.DataBind();
      }
    }

    private List<ProductInStore_Temp_Detail> GetDetails(long id)
    {
      var main = new JoinAlias(typeof(ProductInStore_Temp));
      var detail = new JoinAlias(typeof(ProductInStore_Temp_Detail));
      var dom = new DQueryDom(detail);
      dom.From.AddJoin(JoinType.Left, new DQDmoSource(main), DQCondition.EQ(main, "ID", detail, "ProductInStoreTemp_ID"));

      dom.Columns.Add(DQSelectColumn.Field("Goods_ID", detail));
      dom.Columns.Add(DQSelectColumn.Field("Goods_Name", detail));
      dom.Columns.Add(DQSelectColumn.Field("Goods_Code", detail));
      dom.Columns.Add(DQSelectColumn.Field("Goods_Spec", detail));
      dom.Columns.Add(DQSelectColumn.Field("BrandItem_ID", detail));
      dom.Columns.Add(DQSelectColumn.Field("BrandItem_Name", detail));
      dom.Where.Conditions.Add(DQCondition.EQ(detail, "ProductInStoreTemp_ID", id));
      var list = new List<ProductInStore_Temp_Detail>();
      using (var context = new TransactionContext())
      {
        using (var reader = context.Session.ExecuteReader(dom))
        {
          while (reader.Read())
          {
            var tempdetail = new ProductInStore_Temp_Detail();
            tempdetail.Goods_ID = (long)reader[0];
            tempdetail.Goods_Name = (string)reader[1];
            tempdetail.Goods_Code = (string)reader[2];
            tempdetail.Goods_Spec = (string)reader[3];
            tempdetail.BrandItem_ID = (long?)reader[4];
            tempdetail.BrandItem_Name = (string)reader[5];
            list.Add(tempdetail);
          }
        }
      }
      return list;
    }

    void CreateQueryGridColumns(DFBrowseGrid grid)
    {
      grid.Columns.Add(new DFBrowseGridAutoColumn("DetailID", "AccountingUnit_ID", "Department_ID", "Employee_ID", "Store_ID", "InStoreType_ID", "CheckEmployee_ID", "InStoreDate", "CheckDate"));
    }
  }
  [Serializable]
  internal class Temp:GoodsDetail
  {
    public long? AccountingUnit_ID { get; set; }
    public string AccountingUnit_Name { get; set; }
    public long? Department_ID { get; set; }
    public string Department_Name { get; set; }
    public long? Employee_ID { get; set; }
    public string Employee_Name { get; set; }
    public long? Store_ID { get; set; }
    public string Store_Name { get; set; }
    public long? InStoreType_ID { get; set; }
    public string InStoreType_Name { get; set; }
    public long? CheckEmployee_ID { get; set; }
    public string CheckEmployee_Name { get; set; }
    public new DateTime? InStoreDate { get; set; }
    public DateTime? CheckDate { get; set; }
  }
}
