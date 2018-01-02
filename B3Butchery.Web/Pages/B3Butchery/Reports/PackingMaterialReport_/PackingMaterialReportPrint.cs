using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using Forks.EnterpriseServices.SqlDoms;
using Forks.Utils;
using Forks.Utils.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using TSingSoft.WebControls2;
using TSingSoft.WebControls2.BillReports;
using TSingSoft.WebPluginFramework;
using WebUnit = System.Web.UI.WebControls.Unit;
namespace BWP.Web.Pages.B3Butchery.Reports.PackingMaterialReport_
{



  class PackingMaterialReportPrint : PrintPageBase
  {






    private Control CreateReport()
    {
      var result = new TemplateBillReport();
      var parameters = new Dictionary<string, object>();



      parameters.Add("$CurrentUserName", BLContext.User.Name);
      

      AddParameters(parameters);

      var autoAdd = new Dictionary<string, object>();
      foreach (KeyValuePair<string, object> pair in parameters)
      {
        if (pair.Value != null && pair.Value is ICollection)
        {
          string newKey = pair.Key + "_RecordCount";
          if (!parameters.ContainsKey(newKey))
            autoAdd.Add(newKey, ((ICollection)pair.Value).Count);
        }
        else if (pair.Value != null && pair.Value is LoadArguments)
        {
          string newKey = pair.Key + "_RecordCount";
          if (!parameters.ContainsKey(newKey))
          {
            var loadArguments = new LoadArguments(((LoadArguments)pair.Value).DQuery.Clone() as DQueryDom);
            loadArguments.DQuery.Range = SelectRange.Top(0);
            var adapater = new DFDataAdapter(loadArguments);
            var table = adapater.PagedFill();
            autoAdd.Add(newKey, table.TotalCount);
          }
        }
      }
      foreach (KeyValuePair<string, object> pair in autoAdd)
      {
        parameters.Add(pair.Key, pair.Value);
      }

      var printTemplateFile = Path.Combine(Wpf.Settings.ConfigFolder, GetTemplateFile());
      result.ParseTemplate(FS.OpenRead(printTemplateFile), parameters);
      return result;

    }



    private string GetTemplateFile()
    {
    
      var xmlFile = Path.Combine(Wpf.Settings.ConfigFolder, "BillReports/" + "B3Butchery" + "/" + "PackingMaterialReportPrint.xml");
      var changedFile = Path.ChangeExtension(xmlFile, "chg");
      var result = string.Empty;
      if (FS.FileExists(changedFile))
      {
        result = changedFile;
      }
      else if (FS.FileExists(xmlFile))
      {
        result = xmlFile;
      }
      else
      {
        return string.Empty;
      }
      return result.Substring(Wpf.Settings.ConfigFolder.Length + 1);
    }

    private void AddParameters(Dictionary<string, object> parameters)
    {


      parameters.Add("$班组测算打印", GetHtml());

    }

    private string GetHtml()
    {
      return "";
    }

    private bool Print
    {
      get { return Request.QueryString["Print"] == "1"; }
    }

    protected virtual bool SkipPrintBL
    {
      get
      {
        return false;
      }
    }

    protected override void InitForm(HtmlForm form)
    {
      var absoluteSet = false;
      form.Controls.Add(new LiteralControl(PagerBand.PageBreak));
      Control pageContainer = CreatePageContainer(CreateReport(), out absoluteSet);
      form.Controls.Add(pageContainer);

    }

    private int pagesCount;
    private int slipPages;
    private WebUnit pageHeight = WebUnit.Empty;

    private Control CreatePageContainer(Control control, out bool absoluteSet)
    {
      absoluteSet = false;
      foreach (Control ctrl in control.Controls)
      {
        BillReport r = ctrl as BillReport;
        if (r != null)
        {
          pagesCount += r.PagesCount;
          if (pageHeight == WebUnit.Empty)
            pageHeight = r.PageHeight;
        }
      }
      HtmlGenericControl result = new HtmlGenericControl("div");
      result.Style[HtmlTextWriterStyle.Width] = "100%";
      result.Style[HtmlTextWriterStyle.Padding] = "0cm";
      result.Style[HtmlTextWriterStyle.BorderWidth] = "0cm";
      result.Style[HtmlTextWriterStyle.Margin] = "0cm";
      if (!pageHeight.IsEmpty)
      {
        result.Style[HtmlTextWriterStyle.Top] = new WebUnit((pageHeight.Value * slipPages), pageHeight.Type).ToString();
        result.Style[HtmlTextWriterStyle.Position] = "absolute";
        absoluteSet = true;
      }
      result.Controls.Add(control);
      slipPages = pagesCount;
      return result;
    }




    public  string PrintName
    {
      get { return "班组包材领用测算表"; }
    }



  }







}
