using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using Forks.EnterpriseServices.DataForm;
using Forks.Utils;
using TSingSoft.WebControls2;

namespace BWP.Web.Pages.B3Butchery.WebControls
{



    public class B3ButcheryDFIDLink : DFControl, IDFFieldEditor
    {

        public B3ButcheryDFIDLink(IDFField textField, IDFField linkField)
        {
            PCheck.IsNotNullA(linkField, "linkField");

            TextField = textField;
            DFField = linkField;
        }

        public B3ButcheryDFIDLink(string url, IDFField textField, IDFField linkField)
        {
            PCheck.IsNotNullA(url, "url");
            PCheck.IsNotNullA(textField, "textField");
            PCheck.IsNotNullA(linkField, "linkField");
            Url = url;
            DFField = linkField;
            TextField = textField;
        }

        public B3ButcheryDFIDLink(string url, string text, IDFField linkField)
        {
            PCheck.IsNotNullA(url, "url");
            PCheck.IsNotNullA(text, "text");
            PCheck.IsNotNullA(linkField, "linkField");
            Text = text;
            DFField = linkField;
            Url = url;
        }

        IDFField mTextField;
        public IDFField TextField
        {
            get { return mTextField; }
            set { mTextField = value; }
        }

        public string Url
        {
            set { ViewState["Url"] = value; }
            get { return (string)ViewState["Url"]; }
        }

        string mText;
        public string Text
        {
            get { return mText; }
            set { mText = value; }
        }

        string LinkValue
        {
            get { return (string)ViewState["LinkValue"]; }
            set { ViewState["LinkValue"] = value; }
        }

        string TextValue
        {
            get { return (string)ViewState["TextValue"]; }
            set { ViewState["TextValue"] = value; }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (LinkValue != null)
                writer.AddAttribute(HtmlTextWriterAttribute.Onclick, "javascript:OpenUrlInTopTab('" + AspUtil.AddTimeStampToUrl(ResolveClientUrl(Url) + HttpUtility.HtmlEncode(LinkValue)) + "')");
            writer.AddAttribute(HtmlTextWriterAttribute.Href, "#");
            writer.RenderBeginTag(HtmlTextWriterTag.A);
            writer.Write("No." + (TextValue ?? Text));
            writer.RenderEndTag();
        }

        public void ApplyToUI(object dfObject)
        {
            ApplyValueToUI(DFField.GetValue(dfObject));
            if (TextField != null)
                TextValue = TextField.ToDisplayString(dfObject);
        }

        public void GetFromUI(object dfObject) { }
        public void ValidInput() { }

        void ApplyValueToUI(object value)
        {
            if (value == null)
                LinkValue = null;
            else
                LinkValue = value.ToString();
        }

        void IDFFieldEditor.ApplyValueToUI(object value)
        {
            this.ApplyValueToUI(value);
        }

        object IDFFieldEditor.GetValueFromUI()
        {
            throw new NotSupportedException("DFLink不支持输入");
        }

        bool IDFFieldEditor.Readonly
        {
            get { return true; }
            set { }
        }
    }
}
