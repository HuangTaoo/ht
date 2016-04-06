using System;
using BWP.B3UnitedInfos.BL;
using BWP.B3UnitedInfos.BO;
using BWP.Web.CustomPageLayout;
using BWP.Web.Layout;
using BWP.Web.Utils;
using BWP.Web.WebControls;
using TSingSoft.WebPluginFramework;
using TSingSoft.WebControls2;
using Forks.Utils;
using BWP.B3Frameworks.BO.MoneyTemplate;
using System.Web.UI;
using Forks.EnterpriseServices.DataForm;

namespace BWP.Web.Pages.B3Butchery.Dialogs
{
    class GoodsBatchEdit : DomainBaseInfoEditPage<GoodsBatch, IGoodsBatchBL>
    {
        Goods mGoods;

        protected override string PageLayoutUrl
        {
            get
            {
                var url = base.PageLayoutUrl;
                if (IsNewDialog)
                {
                    url += "$IsNewDialog";
                }
                return url;
            }
        }


        protected override void InitForm(System.Web.UI.HtmlControls.HtmlForm form)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["Goods_ID"]))
            {
                mGoods = WebBLUtil.GetSingleDmo<Goods>(new Tuple<string, object>("ID", long.Parse(Request.QueryString["Goods_ID"])));
            }

            base.InitForm(form);
        }


        protected override void BuildBasePropertiesEditor(TitlePanel titlePanel, PageLayoutSection pageLayoutSection)
        {
            var layoutManager = new LayoutManager("", mDFInfo, mDFContainer);
            layoutManager.Add("PlantCode", InputCreator.DFMemo("厂家标识"));
            layoutManager.Add("CustomTaxRate", new DFValueLabel(mDFInfo.Fields["CustomTaxRate"]));
            var config = new AutoLayoutConfig();
            if (IsNewDialog)
            {
                config.Add("Goods_Name");
            }
            else
            {
                config.Add("Goods_ID");
            }
            config.Add("Goods_Code");
            config.Add("Name");
            config.Add("ProductionDate");
            config.Add("InStoreDate");
            config.Add("InStorePrice");
            config.Add("PlantCode");
            config.Add("Remark");
            config.Add("CustomTaxRate");
            pageLayoutSection.SetRequired("Goods_ID", "Name");
            pageLayoutSection.ApplyLayout(layoutManager, config, mPageLayoutManager, mDFInfo);

            layoutManager.Config = config;
            titlePanel.Controls.Add(layoutManager.CreateLayout());
        }

        protected override void InitNewDmo(GoodsBatch dmo)
        {
            base.InitNewDmo(dmo);
            dmo.InStoreDate = BLContext.Today;
            if (mGoods != null)
            {
                dmo.Goods_ID = mGoods.ID;
                dmo.Goods_Name = mGoods.Name;
                dmo.Goods_Code = mGoods.Code;
            }
            if (!string.IsNullOrEmpty(Request.QueryString["TaxRate"]))
            {
                decimal rate;
                if (decimal.TryParse(Request.QueryString["TaxRate"].Replace("%", ""), out rate))
                {
                    dmo.CustomTaxRate = rate / 100;
                }
                else if (decimal.TryParse(Request.QueryString["TmpTaxRate"].Replace("%", ""), out rate))
                {
                    dmo.CustomTaxRate = rate / 100;
                }
            }
        }

        public override void GetFromUI()
        {
            var sourceReadonly = (mDFContainer.GetControl<Control>("Name") as IDFFieldEditor).Readonly;
            if (!sourceReadonly)
            {
                mDFContainer.MakeReadonly("Name");
                Dmo.Name = (string)mDFContainer.GetControl<DFTextBox>("Name").Text.Trim();
            }
            base.GetFromUI();
        }
    }
}
