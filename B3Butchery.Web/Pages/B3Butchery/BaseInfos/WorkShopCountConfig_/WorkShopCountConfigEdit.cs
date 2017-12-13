using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.Web.CustomPageLayout;
using BWP.Web.WebControls;
using System.Web.UI;
using TSingSoft.WebControls2;
using BWP.Web.Layout;
using System.Web.UI.WebControls;
using Forks.EnterpriseServices.DataForm;
using BWP.B3Frameworks.Utils;
using BWP.B3UnitedInfos;
using Forks.Utils.Collections;

namespace BWP.Web.Pages.B3Butchery.BaseInfos.WorkShopCountConfig_
{



    class WorkShopCountConfigEdit : DomainBaseInfoEditPage<WorkShopCountConfig, IWorkShopCountConfigBL>
    {






        private DFEditGrid _detailGrid;
        protected override void BuildBody(Control control)
        {
            base.BuildBody(control);
            AddDetails(control.EAdd(new TitlePanel("存货明细", "存货明细")));
        }
        protected override void BuildBasePropertiesEditor(TitlePanel titlePanel, PageLayoutSection pageLayoutSection)
        {
            var layoutManager = new LayoutManager("", mDFInfo, mDFContainer);
            var config = new AutoLayoutConfig();
            layoutManager.Config = config;


            config.Add("Name");
            config.Add("Date");
            config.Add("WorkshopCategory_ID");
            config.Add("Remark");

            pageLayoutSection.ApplyLayout(layoutManager, config, mPageLayoutManager, mDFInfo);

            titlePanel.Controls.Add(layoutManager.CreateLayout());
        }

        private void AddDetails(TitlePanel titlePanel)
        {
            var vPanel = titlePanel.EAdd(new VLayoutPanel());
            if (CanSave)
            {
                var hPanel = vPanel.Add(new HLayoutPanel(), new VLayoutOption(HorizontalAlign.Left));
                hPanel.Add(new SimpleLabel("选择存货"));
                var selectGoods = hPanel.Add(new ChoiceBox(B3UnitedInfosConsts.DataSources.存货) { Width = Unit.Pixel(130), EnableMultiSelection = true, EnableInputArgument = true, AutoPostBack = true });
                selectGoods.SelectedValueChanged += delegate
                {
                    _detailGrid.GetFromUI();
                    if (!selectGoods.IsEmpty)
                    {
                        foreach (var item in selectGoods.GetValues())
                        {
                            if (Dmo.Details.Any( x=> x.Goods_ID == long.Parse(item)))
                            {
                                continue;
                            }
               
                            var detail = new WorkShopCountConfig_Detail { Goods_ID = long.Parse(item) };
                            DmoUtil.RefreshDependency(detail, "Goods_ID");
                            Dmo.Details.Add(detail);
                        }
                    }
                    selectGoods.Clear();
                    _detailGrid.DataBind();
                };

                //var addGoodsbt = hPanel.Add(new DialogButton
                //{
                //    Text = "选择计数名称",
                //});
                //addGoodsbt.Url = "/B3Butchery/Dialogs/SelectGoodsDialog.aspx";
                //addGoodsbt.Click += delegate
                //{
                //    _detailGrid.GetFromUI();
                //    var details = DialogUtil.GetCachedObj<TemGoodsDetail>(this);
                //    foreach (var temGoodsDetail in details)
                //    {
                //        var detail = new WorkShopCountConfig_Detail { Goods_ID = temGoodsDetail.Goods_ID };
                //        DmoUtil.RefreshDependency(detail, "Goods_ID");





                //        Dmo.Details.Add(detail);
                //    }
                //    _detailGrid.DataBind();
                //};
            }

            var editor = new DFCollectionEditor<WorkShopCountConfig_Detail>(() => Dmo.Details)
            {
                AllowDeletionFunc = () => CanSave,
                CanDeleteFunc = detail => true,
                IsEditableFunc = (field, detail) => CanSave
            };

            _detailGrid = new DFEditGrid(editor) { DFGridSetEnabled = false, Width = Unit.Percentage(100) };
            _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Name"));
            _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_Code"));
            _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("GoodsProperty_Name"));
            _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_MainUnit"));
            _detailGrid.Columns.EAdd(new DFEditGridColumn<DFValueLabel>("Goods_MainUnitRatio")).HeaderText = "主辅换算主单位比例";
            _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_SecondUnit"));
            _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_SecondUnitII_MainUnitRatio"));
            
            _detailGrid.Columns.Add(new DFEditGridColumn<DFValueLabel>("Goods_SecondUnitII"));
            _detailGrid.Columns.Add(new DFEditGridColumn<DFTextBox>("DefaultNumber1"));
     

            _detailGrid.ValueColumns.Add("Goods_ID");
            var section = mPageLayoutManager.AddSection("DetaiColumns", "明细列");
            titlePanel.SetPageLayoutSetting(mPageLayoutManager, section.Name);
            section.ApplyLayout(_detailGrid, mPageLayoutManager, DFInfo.Get(typeof(WorkShopCountConfig_Detail)));

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
