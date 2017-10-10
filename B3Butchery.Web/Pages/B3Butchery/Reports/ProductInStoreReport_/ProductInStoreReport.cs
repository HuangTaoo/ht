using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using BWP.B3Butchery.BO;
using BWP.B3Butchery.Utils;
using BWP.B3Frameworks;
using BWP.B3Frameworks.BO;
using BWP.B3Frameworks.BO.NamedValueTemplate;
using BWP.B3Frameworks.Utils;
using BWP.B3UnitedInfos.BO;
using BWP.Web.Layout;
using BWP.Web.Utils;
using BWP.Web.WebControls;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using Forks.EnterpriseServices.SqlDoms;
using TSingSoft.WebControls2;
using DataKind = BWP.B3Frameworks.B3FrameworksConsts.DataSources;
using TSingSoft.WebControls2.QBELinks;

namespace BWP.Web.Pages.B3Butchery.Reports.ProductInStoreReport_
{
	class ProductInStoreReport : DFBrowseGridReportPage
	{
		DFInfo mainInfo = DFInfo.Get(typeof(ProductInStore));
		DFInfo detailInfo = DFInfo.Get(typeof(ProductInStore_Detail));
    readonly static short mDmoTypeID = TypeUtil.GetNullableDmoTypeID<ProductInStore>().Value;
		protected override string AccessRoleName
		{
			get { return "B3Butchery.报表.成品入库分析"; }
		}

		protected override string Caption
		{
			get { return "成品入库分析"; }
		}

		CheckBoxListWithReverseSelect checkbox;
    DFTextBox depth;
		protected override void InitQueryPanel(QueryPanel queryPanel)
		{
			base.InitQueryPanel(queryPanel);
      var panel = queryPanel.CreateTab("显示字段");
      _showTypeList = new CheckBoxList {
        RepeatColumns = 6,
        RepeatDirection = RepeatDirection.Horizontal
      };
      _showTypeList.Items.Add(new ListItem("合并单元格") {
        Selected = true
      });

      panel.EAdd(new HLayoutPanel() { new SimpleLabel("显示格式"), _showTypeList });
      mQueryControls.Add("显示格式", _showTypeList);
			checkbox = new CheckBoxListWithReverseSelect() { RepeatColumns = 6, RepeatDirection = RepeatDirection.Horizontal };
      checkbox.Items.Add(new ListItem("单号", "ID"));
      checkbox.Items.Add(new ListItem("会计单位", "AccountingUnit_Name"));
			checkbox.Items.Add(new ListItem("部门", "Department_Name"));
			checkbox.Items.Add(new ListItem("仓库", "Store_Name"));
			checkbox.Items.Add(new ListItem("入库类型", "InStoreType_Name"));
			checkbox.Items.Add(new ListItem("入库时间", "InStoreDate"));
			checkbox.Items.Add(new ListItem("验收日期", "CheckDate"));
			checkbox.Items.Add(new ListItem("生产日期", "ProductionDate"));
			checkbox.Items.Add(new ListItem("生产计划号", "ProductPlan_Name"));
      checkbox.Items.Add(new ListItem("摘要", "Remark"));
      checkbox.Items.Add(new ListItem("存货名称", "Name"));
      checkbox.Items.Add(new ListItem("打印简称", "PrintShortName"));
      checkbox.Items.Add(new ListItem("存货属性", "GoodsProperty_Name"));
			checkbox.Items.Add(new ListItem("存货编码", "Code"));
			checkbox.Items.Add(new ListItem("规格", "Spec"));
      checkbox.Items.Add(new ListItem("产地", "Origin"));
      checkbox.Items.Add(new ListItem("存货品牌", "Brand"));
      checkbox.Items.Add(new ListItem("存货属性", "存货属性"));
      checkbox.Items.Add(new ListItem("属性分类", "属性分类"));
      checkbox.Items.Add(new ListItem("主数量", "Number"));
			checkbox.Items.Add(new ListItem("主单位", "MainUnit"));
			checkbox.Items.Add(new ListItem("辅数量", "SecondNumber"));
			checkbox.Items.Add(new ListItem("辅单位", "SecondUnit"));
			checkbox.Items.Add(new ListItem("单价", "Price"));
			checkbox.Items.Add(new ListItem("金额", "Money"));
      checkbox.Items.Add(new ListItem("创建人", "CreateUser_Name"));
      checkbox.Items.Add(new ListItem("备注", "DRemark"));
      checkbox.Items.Add(new ListItem("标签", "Names"));
      checkbox.Items.Add(new ListItem("单据状态", "BillState"));
      checkbox.Items.Add(new ListItem("每日"));
      checkbox.Items.Add(new ListItem("每月"));
      checkbox.Items.Add(new ListItem("每年"));

      checkbox.Items.Add(new ListItem("货位", "CargoSpace_Name"));


      panel.EAdd(checkbox);
      panel.EAddLiteral("<BR/>");
      panel.EAddLiteral("<span style='color:red'>属性分类等级:</span>");
      depth = panel.EAdd(new DFTextBox());
      depth.Width = Unit.Pixel(20);
      var hPanel = new HLayoutPanel();
      CreateDataRangePanel(hPanel);
      queryPanel.ConditonPanel.EAdd(hPanel);
      mQueryControls.Add("显示字段", checkbox);
      mQueryControls.EnableHoldLastControlNames.Add("显示字段");
    }

		void CreateDataRangePanel(HLayoutPanel hPanel)
		{
      hPanel.Add(new SimpleLabel(mainInfo.Fields["InStoreDate"].Prompt));
      hPanel.Add(QueryCreator.TimeRange(mainInfo.Fields["InStoreDate"], mQueryContainer, "MinInStoreDate", "MaxInStoreDate"));
		}

        DFTextBox goodsOrigin, goodsName;
        DFChoiceBox _storeInput,cargoSpaceName;
		protected override void AddQueryControls(VLayoutPanel vPanel)
		{
			var customPanel = new LayoutManager("Main", mainInfo, mQueryContainer);
      customPanel.Add("ID",QueryCreator.DFTextBox(mainInfo.Fields["ID"]));
			customPanel.Add("AccountingUnit_ID", QueryCreator.DFChoiceBoxEnableMultiSelection(mainInfo.Fields["AccountingUnit_ID"], mQueryContainer, "AccountingUnit_ID", DataKind.授权会计单位全部));
			customPanel["AccountingUnit_ID"].NotAutoAddToContainer = true;
			customPanel.Add("Department_ID", QueryCreator.DFChoiceBoxEnableMultiSelection(mainInfo.Fields["Department_ID"], mQueryContainer, "Department_ID", DataKind.授权部门全部));
			customPanel["Department_ID"].NotAutoAddToContainer = true;
			customPanel.Add("Store_ID",_storeInput= QueryCreator.DFChoiceBoxEnableMultiSelection(mainInfo.Fields["Store_ID"], mQueryContainer, "Store_ID", DataKind.授权仓库全部), false);
			customPanel["Store_ID"].NotAutoAddToContainer = true;
			customPanel.Add("InStoreType_ID", QueryCreator.DFChoiceBoxEnableMultiSelection(mainInfo.Fields["InStoreType_ID"], mQueryContainer, "InStoreType_ID", B3ButcheryDataSource.屠宰分割入库类型全部));
			customPanel["InStoreType_ID"].NotAutoAddToContainer = true;
			customPanel.Add("CheckDate", QueryCreator.DateRange(mainInfo.Fields["CheckDate"], mQueryContainer, "MinCheckDate", "MaxCheckDate"));

			customPanel.Add("InStoreDate", new SimpleLabel("生产日期"), QueryCreator.DateRange(detailInfo.Fields["ProductionDate"], mQueryContainer, "MinProductionDate", "MaxProductionDate"));
			customPanel.Add("ProductPlan_ID", new SimpleLabel("生产计划号"), QueryCreator.DFChoiceBoxEnableMultiSelection(detailInfo.Fields["ProductPlan_ID"], mQueryContainer, "ProductPlan_ID", B3ButcheryDataSource.计划号));
			customPanel["ProductPlan_ID"].NotAutoAddToContainer = true;
      customPanel.Add("Remark", QueryCreator.DFTextBox(mainInfo.Fields["Remark"]));
      DFChoiceBox goodsInput;
      customPanel.Add("Goods_ID", new SimpleLabel("存货名称"), goodsInput = QueryCreator.DFChoiceBoxEnableMultiSelection(detailInfo.Fields["Goods_ID"], mQueryContainer, "Goods_ID", B3ButcheryDataSource.存货带编号全部));
      goodsInput.PlaceHolder = "名称 编号 简拼";
      customPanel["Goods_ID"].NotAutoAddToContainer = true;
      customPanel.Add("Origin", new SimpleLabel("存货产地"), goodsOrigin = QueryCreator.DFTextBox(detailInfo.Fields["Goods_Code"]));
      customPanel.Add("Goods_Name", new SimpleLabel("存货名称"), goodsName = QueryCreator.DFTextBox(detailInfo.Fields["Goods_Name"]));
      customPanel.Add("Goods_Brand", new SimpleLabel("存货品牌"), QueryCreator.DFChoiceBox(detailInfo.Fields["Goods_Name"], B3ButcheryDataSource.存货品牌));
      customPanel.Add("GoodsProperty_ID",new SimpleLabel("存货属性"),QueryCreator.DFChoiceBox(detailInfo.Fields["ID"],B3UnitedInfos.B3UnitedInfosConsts.DataSources.存货属性全部));
      customPanel.Add("PropertyCatalog_ID", new SimpleLabel("属性分类"), QueryCreator.DFChoiceBox(detailInfo.Fields["ID"], B3UnitedInfos.B3UnitedInfosConsts.DataSources.存货属性分类全部));
      customPanel.Add("DRemark", new SimpleLabel("备注"), QueryCreator.DFTextBox(detailInfo.Fields["Remark"]));
      customPanel.Add("CargoSpace_ID", new SimpleLabel("货位"), cargoSpaceName = QueryCreator.DFChoiceBoxEnableMultiSelection(detailInfo.Fields["CargoSpace_ID"], mQueryContainer, "CargoSpace_ID", B3ButcheryDataSource.货位), false);
      customPanel["CargoSpace_ID"].NotAutoAddToContainer = true;
      

      customPanel.Add("BillState", QueryCreator.一般单据状态(mainInfo.Fields["BillState"]));    
     
      var config = customPanel.CreateDefaultConfig(4);
      config.Expand = false;
      TagWebUtil.AddTagQueryInput(mDmoTypeID, customPanel, config, mQueryContainer);
			vPanel.Add(customPanel.CreateLayout());
		}

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            cargoSpaceName.OnBeforeDrop = "var ref = window.document.getElementById('" + _storeInput.ClientID + "');if(typeof(ref.value)!='undefined'){this.codeArgument = ref.value;}else if(ref.behind){this.codeArgument = ref.behind.get_value();};";

        }

    string[] mainFields = new string[] { "ID", "AccountingUnit_Name", "Department_Name", "Store_Name", "InStoreType_Name", "InStoreDate", "CheckDate", "CreateUser_Name", "Remark", "BillState" };
		string[] sumFileds = new string[] { "Number", "SecondNumber", "Money" };
    string[] goodsFields = new string[] { "Name", "GoodsProperty_Name", "Code", "Spec", "MainUnit", "SecondUnit", "Origin", "Brand", "PrintShortName" };
	  private CheckBoxList _showTypeList;

      protected override DQueryDom GetQueryDom()
      {
          mBrowseGrid.EnableRowsGroup = _showTypeList.Items.FindByText("合并单元格").Selected;
          var query = base.GetQueryDom();
          OrganizationUtil.AddOrganizationLimit<Department>(query, "Department_ID");
          OrganizationUtil.AddOrganizationLimit<Store>(query, "Store_ID");
          var bill = query.From.RootSource.Alias;
          var detail = JoinAlias.Create("detail");
          var goodsAlias = new JoinAlias(typeof(Goods));
          var goodsProperty = new JoinAlias(typeof(GoodsProperty));
          var propertyCatalog = new JoinAlias(typeof(GoodsPropertyCatalog));
          query.From.AddJoin(JoinType.Left, new DQDmoSource(goodsAlias), DQCondition.EQ(detail, "Goods_ID", goodsAlias, "ID"));
          query.From.AddJoin(JoinType.Left, new DQDmoSource(goodsProperty), DQCondition.EQ(goodsAlias, "GoodsProperty_ID", goodsProperty, "ID"));
          query.From.AddJoin(JoinType.Left, new DQDmoSource(propertyCatalog), DQCondition.EQ(goodsProperty, "GoodsPropertyCatalog_ID", propertyCatalog, "ID"));

          foreach (ListItem field in checkbox.Items)
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
                  else if (field.Text == "每日")
                  {
                      var snippetDay = DQExpression.Snippet<DateTime?>("(Convert(nvarchar(10),[bill].[InStoreDate], 23))");
                      query.Columns.Add(DQSelectColumn.Create(snippetDay, "每日"));
                      query.GroupBy.Expressions.Add(snippetDay);
                  }
                  else if (field.Text == "每月")
                  {
                      var snippetMonth = DQExpression.Snippet<string>("Left(Convert(nvarchar(10),[bill].[InStoreDate], 23),7)");
                      query.Columns.Add(DQSelectColumn.Create(snippetMonth, "每月"));
                      query.GroupBy.Expressions.Add(snippetMonth);
                  }
                  else if (field.Text == "每年")
                  {
                      var snippetMonth = DQExpression.Snippet<string>("Left(Convert(nvarchar(10),[bill].[InStoreDate], 23),4)");
                      query.Columns.Add(DQSelectColumn.Create(snippetMonth, "每月"));
                      query.GroupBy.Expressions.Add(snippetMonth);
                  }
                  else if (field.Text == "标签")
                  {
                      var tarName = new JoinAlias(typeof(Dmo_TagNames));
                      query.From.AddJoin(JoinType.Left, new DQDmoSource(tarName), DQCondition.And(DQCondition.EQ(tarName, "DmoID", query.From.RootSource.Alias, "ID"), DQCondition.EQ(tarName, "DmoTypeID", mDmoTypeID)));
                      query.Columns.Add(DQSelectColumn.Create(DQExpression.Field(tarName, "Names"), "标签"));
                      query.GroupBy.Expressions.Add(DQExpression.Field(tarName, "Names"));
                  }
                  else if (field.Text == "存货属性")
                  {
                      query.Columns.Add(DQSelectColumn.Create(DQExpression.Field(goodsProperty, "Name"), field.Text));
                      query.GroupBy.Expressions.Add(DQExpression.Field(goodsProperty, "Name"));
                  }
                  else if (field.Text == "属性分类")
                  {
                      query.Columns.Add(DQSelectColumn.Create(DQExpression.Field(propertyCatalog, "Name"), field.Text));
                      query.GroupBy.Expressions.Add(DQExpression.Field(propertyCatalog, "Name"));
                      var v = 0;
                      if (!depth.IsEmpty && int.TryParse(depth.Text, out v))
                      {
                          if (v < 0)
                              v = 0;
                          if (v > 8)
                              v = 8;
                          for (var i = 1; i <= v; i++)
                          {
                              var p = new JoinAlias("_p" + i, typeof(GoodsPropertyCatalog));
                              query.From.AddJoin(JoinType.Left, new DQDmoSource(p), DQCondition.EQ(p, "ID", propertyCatalog, string.Format("TreeDeep{0}ID", i)));
                              query.Columns.Add(DQSelectColumn.Create(DQExpression.Field(p, "Name"), i + "级分类"));
                              query.GroupBy.Expressions.Add(DQExpression.Field(p, "Name"));
                          }
                      }
                  }
                  else if (field.Text == "货位")
                  {
                      query.Columns.Add(DQSelectColumn.Create(DQExpression.Field(detail, "CargoSpace_Name"), field.Text));
                      query.GroupBy.Expressions.Add(DQExpression.Field(detail, "CargoSpace_Name"));
                  }
                  else
                  {
                      var s = field.Value;
                      if (field.Value == "DRemark")
                      {
                          s = "Remark";
                          query.Columns.Add(DQSelectColumn.Create(DQExpression.Field(detail, s), "备注"));
                      }
                      else
                          query.Columns.Add(DQSelectColumn.Field(s, detail));
                      query.GroupBy.Expressions.Add(DQExpression.Field(detail, s));
                  }
              }
          }
          if (!string.IsNullOrEmpty(goodsOrigin.Text))
              query.Where.Conditions.Add(DQCondition.Like(goodsAlias, "Origin", goodsOrigin.Text));
          if (!goodsName.IsEmpty)
              query.Where.Conditions.Add(DQCondition.Like(goodsAlias, "Name", goodsName.Text));
          query.Where.Conditions.Add(DQCondition.And(DQCondition.EQ("Domain_ID", DomainContext.Current.ID)));
          var brand = mQueryContainer.GetControl<DFChoiceBox>("Goods_Brand");
          if (!brand.IsEmpty)
              query.Where.Conditions.Add(DQCondition.EQ(goodsAlias, "Brand", brand.Value));
          var gProperty = mQueryContainer.GetControl<DFChoiceBox>("GoodsProperty_ID");
          if (!gProperty.IsEmpty)
              query.Where.Conditions.Add(DQCondition.EQ(goodsProperty, "ID", gProperty.Value));
          TreeUtil.AddTreeCondition<GoodsPropertyCatalog>(query, mQueryContainer, "PropertyCatalog_ID", propertyCatalog);
          TagWebUtil.AddTagQueryCondition(mDmoTypeID, query, bill, mQueryContainer);
          if (query.Columns.Count == 0)
              throw new Exception("至少选择一条显示列");
          return query;
      }
	}
}
