using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using BWP.B3Butchery.BO.BaseInfo;
using BWP.B3Butchery.Utils;
using Forks.EnterpriseServices.BusinessInterfaces;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
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


      var query = GetQueryDom();
      parameters.Add("$Details", GetList());
      parameters.Add("$DetailsType", typeof(PackingMaterialReportBo));
      //parameters.Add("$班组测算打印", GetHtml());

    }


    private List<PackingMaterialReportBo> GetList()
    {
      var list = new List<PackingMaterialReportBo>();
      var query = GetQueryDom();
      using (var context = new TransactionContext())
      {
        using (var reader = context.Session.ExecuteReader(query))
        {

          while (reader.Read())
          {
            int i = 0;
            var item = new PackingMaterialReportBo();
            item.Goods_Name = (string)reader[i++];
            item.PlanNumber = (string)reader[i++];
            item.Goods_Spec = (string)reader[i++];
            item.SecondNumber = (Money<decimal>?)reader[i++];
            item.SecondNumber2 = (Money<decimal>?)reader[i++];
            item.Number = (Money<decimal>?)reader[i++];
            item.PackageModel = (NamedValue<包装模式>?)reader[i++];
            list.Add(item);

          }
        }



      }
      return list;
    }
    private string GetHtml()
    {
      var sb = new StringBuilder();

      sb.Append("<tr>");
      sb.Append("<td >存货名称</td>");
      sb.Append("<td > 计划号</td>");
      sb.Append("<td >计数规格</td>");
      sb.Append("<td >盘数</td>");
      sb.Append("<td >袋数</td>");
      sb.Append("<td >重量</td>");
      sb.Append("<td >包装模式</td>");
      sb.Append("</tr>");


      var query = GetQueryDom();
      using (var context = new TransactionContext())
      {
        using (var reader = context.Session.ExecuteReader(query))
        {
     
          while(reader.Read())
          {
            int i = 0;
            sb.Append("<tr>");
            sb.Append(string.Format("<td >{0}</td>", reader[i++]));
            sb.Append(string.Format("<td >{0}</td>", reader[i++]));
            sb.Append(string.Format("<td >{0}</td>", reader[i++]));
            sb.Append(string.Format("<td >{0}</td>", reader[i++]));
            sb.Append(string.Format("<td >{0}</td>", reader[i++]));
            sb.Append(string.Format("<td >{0}</td>", reader[i++]));
            sb.Append(string.Format("<td >{0}</td>", reader[i++]));
            sb.Append("</tr>");
          }
        }


      }





      return sb.ToString();
    }

    long? Shift_ID
    {
      get
      {
        var str = Request.QueryString["Shift"];
        if (string.IsNullOrEmpty(str))
        {
          return null;
        }
        return long.Parse(str);
      }
    }
    long? PackMode
    {
      get
      {
        var str = Request.QueryString["PackMode"];
        if (string.IsNullOrEmpty(str))
        {
          return null;
        }
        return long.Parse(str);
      }
    }
    DateTime? BeginDate
    {
      get
      {
        var str = Request.QueryString["BeginDate"];
        if (string.IsNullOrEmpty(str))
        {
          return null;
        }
        return DateTime.Parse(str);
      }
    }
    DateTime? EndDate
    {
      get
      {
        var str = Request.QueryString["EndDate"];
        if (string.IsNullOrEmpty(str))
        {
          return null;
        }
        return DateTime.Parse(str);
      }
    }
    private DQueryDom GetQueryDom()
    {

      var detail = new JoinAlias("__detail", typeof(ProduceOutput_Detail));
      var main = new JoinAlias("__main", typeof(ProduceOutput));
      var goods = new JoinAlias("__goods", typeof(ButcheryGoods));

      var query = new DQueryDom(detail);
      query.From.AddJoin(JoinType.Left, new DQDmoSource(main), DQCondition.EQ(main, "ID", detail, "ProduceOutput_ID"));
      query.From.AddJoin(JoinType.Left, new DQDmoSource(goods), DQCondition.EQ(goods, "ID", detail, "Goods_ID"));
      query.Columns.Add(DQSelectColumn.Field("Goods_Name"));
      query.Columns.Add(DQSelectColumn.Field("PlanNumber_Name", detail));

      query.Columns.Add(DQSelectColumn.Create(DQExpression.Field(detail, "Remark"), "计数规格"));
      query.Columns.Add(DQSelectColumn.Create(DQExpression.Sum(DQExpression.Field(detail, "SecondNumber")), "盘数"));

      query.Columns.Add(DQSelectColumn.Create(DQExpression.Sum(DQExpression.Field(detail, "SecondNumber2")), "袋数"));

      query.Columns.Add(DQSelectColumn.Create(DQExpression.Sum(DQExpression.Field(detail, "Number")), "重量"));

      query.Columns.Add(DQSelectColumn.Create(DQExpression.Field(goods, "PackageModel"), "包装模式"));

      query.GroupBy.Expressions.Add(DQExpression.Field(detail, "Goods_Name"));
      query.GroupBy.Expressions.Add(DQExpression.Field(detail, "PlanNumber_Name"));
      query.GroupBy.Expressions.Add(DQExpression.Field(detail, "Remark"));
      query.GroupBy.Expressions.Add(DQExpression.Field(goods, "PackageModel"));

      if (Shift_ID != null)
      {
        query.Where.Conditions.Add(DQCondition.EQ(detail, "Goods_ProductShift_ID", Shift_ID));
      }
      if (PackMode != null)
      {
        query.Where.Conditions.Add(DQCondition.EQ(goods, "PackageModel", PackMode));
      }
      if (BeginDate != null)
      {
        query.Where.Conditions.Add(DQCondition.GreaterThanOrEqual(main, "Time", BeginDate));
      }
      if (EndDate!= null)
      {
        query.Where.Conditions.Add(DQCondition.LessThanOrEqual(main, "Time", EndDate));
      }
      query.OrderBy.Expressions.Add(DQOrderByExpression.Create(DQExpression.Field(goods, "PackageModel"), false));
      return query;




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
