using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using BWP.B3Frameworks.Utils;
using BWP.B3UnitedInfos;
using BWP.Web.CustomPageLayout;
using BWP.Web.Layout;
using BWP.Web.Utils;
using BWP.Web.WebControls;
using Forks.EnterpriseServices.DataForm;
using TSingSoft.WebControls2;
using BWP.Web.Pages.B3Butchery.Dialogs;

namespace BWP.Web.Pages.B3Butchery.BaseInfos.ClientGoodsSet_
{
  class ClientGoodsSetEdit : DomainBaseInfoEditPage<ClientGoodsSet, IClientGoodsSetBL>
  {
    private DFEditGrid _detailGrid;
    protected override void BuildBasePropertiesEditor(TitlePanel titlePanel, PageLayoutSection pageLayoutSection)
    {
      var layoutManager = new LayoutManager("", mDFInfo, mDFContainer);
      var config = new AutoLayoutConfig();
      layoutManager.Config = config;
     
      config.Add("Name");
      config.Add("Remark");

      pageLayoutSection.ApplyLayout(layoutManager, config, mPageLayoutManager, mDFInfo);

      titlePanel.Controls.Add(layoutManager.CreateLayout());
    }
    protected override void BuildBody(Control control)
    {
      base.BuildBody(control);
      AddPayDetails(control.EAdd(new TitlePanel("存货明细", "存货明细")));
    }

    private void AddPayDetails(TitlePanel titlePanel)
    {
      var vPanel = titlePanel.EAdd(new VLayoutPanel());

      if (CanSave)
      {
        var hPanel = vPanel.Add(new HLayoutPanel(), new VLayoutOption(HorizontalAlign.Left));
        hPanel.Add(new SimpleLabel("选择存货"));
        var selectGoods = new ChoiceBox(B3UnitedInfosConsts.DataSources.存货) { Width = Unit.Pixel(130), EnableInputArgument = true, AutoPostBack = true, EnableTopItem = true, EnableMultiSelection = true };
        selectGoods.SelectedValueChanged += delegate
        {
          _detailGrid.GetFromUI();
          if (!selectGoods.IsEmpty)
          {
            var gids = selectGoods.GetValues().Distinct();
            foreach (var g in gids)
            {
              var d = new ClientGoodsSet_Detail() { Goods_ID = long.Parse(g) };
              DmoUtil.RefreshDependency(d, "Goods_ID");
              Dmo.Details.Add(d);
            }
          }
          selectGoods.Clear();
          _detailGrid.DataBind();
        };


        hPanel.Add(selectGoods);
        var addGoods = hPanel.Add(new DialogButton
        {
          Text = "选择存货",
        });
        addGoods.Url = "SelectGoodsDialogs.aspx";
        addGoods.Click += delegate
        {
          _detailGrid.GetFromUI();
          foreach (var goodsID in DialogUtil.GetCachedObj<long>(this))
          {
              if (Dmo.Details.Any(x => x.Goods_ID == goodsID))
            {
              continue;
            }
              var detail = new ClientGoodsSet_Detail() { Goods_ID =  goodsID };
            DmoUtil.RefreshDependency(detail, "Goods_ID");
            Dmo.Details.Add(detail);
          }
          _detailGrid.DataBind();
        };
      };
    

      var editor = new DFCollectionEditor<ClientGoodsSet_Detail>(() => Dmo.Details);
      editor.AllowDeletionFunc = () => CanSave;
      editor.CanDeleteFunc = detail => CanSave;
      editor.IsEditableFunc = (field, detail) => CanSave;
      _detailGrid = new DFEditGrid(editor);
      _detailGrid.DFGridSetEnabled = false;
      _detailGrid.Width = Unit.Percentage(100);
   
        _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("GoodsProperty_Name"));
      _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Code"));
      _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Name"));
      _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Spec"));


      _detailGrid.ValueColumns.Add("Goods_ID");


      var section = mPageLayoutManager.AddSection("GoodsDetaiColumns", "存货明细");
      titlePanel.SetPageLayoutSetting(mPageLayoutManager, section.Name);

      section.ApplyLayout(_detailGrid, mPageLayoutManager, DFInfo.Get(typeof(ClientGoodsSet_Detail)));

      vPanel.Add(_detailGrid);
    }
    public override void AppToUI()
    {
      base.AppToUI();
      _detailGrid.DataBind();
    }

    public override void GetFromUI()
    {
      base.GetFromUI();
      _detailGrid.GetFromUI();
    }
  }
}
