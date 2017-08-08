using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using BWP.B3Frameworks.BO.NamedValueTemplate;
using Forks.EnterpriseServices.DataForm;
using TSingSoft.WebControls2;

namespace BWP.Web.Utils
{
  public class CustomInputCreator
  {
    private static Unit _width = 180;
    private static readonly Unit HalfWidth;

    static CustomInputCreator()
    {
      double num = (_width.Value / 2.0) - 7.0;
      HalfWidth = Unit.Pixel(int.Parse(num.ToString()));
    }

    public static Control DFDateRange(DFContainer container, string minParam, string maxParam)
    {
      DFDateInput child = container.Add<DFDateInput>(new TSingSoft.WebControls2.DFDateInput(), minParam);
      DFDateInput input2 = container.Add<DFDateInput>(new TSingSoft.WebControls2.DFDateInput(), maxParam);
      child.Width = HalfWidth;
      input2.Width = HalfWidth;
      Panel panel = new Panel();
      panel.Controls.Add(child);
      panel.Controls.Add(new LiteralControl("→"));
      panel.Controls.Add(input2);
      return panel;
    }

    public static Control QueryDateTimeRange(IDFField fieldInfo, QueryContainer container, string minParam, string maxParam)
    {
      DateTime? beginDefault = null;
      return QueryDateTimeRange(fieldInfo, container, minParam, maxParam, beginDefault, null);
    }


    public static Control TimeRange(IDFField fieldInfo, QueryContainer container, string minParam, string maxParam, DateTime? beginDefault, DateTime? endDefault)
    {
      DFDateTimeInput minInput = container.Add(new DFDateTimeInput(fieldInfo), minParam);
      DFDateTimeInput maxInput = container.Add(new DFDateTimeInput(fieldInfo), maxParam);
      maxInput.DefaultTime = DateInputDefaultTime.maxValue;
      minInput.DefaultTime = DateInputDefaultTime.minValue;
      minInput.Style.Add("width", "73px");
      maxInput.Style.Add("width", "73px");
      if (beginDefault.HasValue)
        minInput.Date = beginDefault.Value;
      if (endDefault.HasValue)
        maxInput.Date = endDefault.Value;

      Panel panel = new Panel();
      panel.Controls.Add(minInput);
      panel.Controls.Add(new LiteralControl("→"));
      panel.Controls.Add(maxInput);
      return panel;
    }

    public static Control QueryDateTimeRange(IDFField fieldInfo, QueryContainer container, string minParam, string maxParam, DateTime? beginDefault, DateTime? endDefault)
    {
      TSingSoft.WebControls2.DFDateTimeInput child = container.Add<TSingSoft.WebControls2.DFDateTimeInput>(new TSingSoft.WebControls2.DFDateTimeInput(fieldInfo), minParam);
      TSingSoft.WebControls2.DFDateTimeInput input2 = container.Add<TSingSoft.WebControls2.DFDateTimeInput>(new TSingSoft.WebControls2.DFDateTimeInput(fieldInfo), maxParam);
      input2.DefaultTime = DateInputDefaultTime.maxValue;
      child.DefaultTime = DateInputDefaultTime.minValue;
      child.Width = HalfWidth;
      input2.Width = HalfWidth;
      if (beginDefault.HasValue)
      {
        child.Date = beginDefault.Value;
      }
      if (endDefault.HasValue)
      {
        input2.Date = endDefault.Value;
      }
      Panel panel = new Panel();
      panel.Controls.Add(child);
      panel.Controls.Add(new LiteralControl("→"));
      panel.Controls.Add(input2);
      return panel;
    }

    public static Control QueryDateRange(IDFField fieldInfo, QueryContainer container, string minParam, string maxParam)
    {
      DateTime? beginDefault = null;
      return QueryDateRange(fieldInfo, container, minParam, maxParam, beginDefault, null);
    }

    public static Control QueryDateRange(IDFField fieldInfo, QueryContainer container, string minParam, string maxParam, DateTime? beginDefault, DateTime? endDefault)
    {
      TSingSoft.WebControls2.DFDateInput child = container.Add<TSingSoft.WebControls2.DFDateInput>(new TSingSoft.WebControls2.DFDateInput(fieldInfo), minParam);
      TSingSoft.WebControls2.DFDateInput input2 = container.Add<TSingSoft.WebControls2.DFDateInput>(new TSingSoft.WebControls2.DFDateInput(fieldInfo), maxParam);
      input2.DefaultTime = DateInputDefaultTime.maxValue;
      child.DefaultTime = DateInputDefaultTime.minValue;
      child.Width = HalfWidth;
      input2.Width = HalfWidth;
      if (beginDefault.HasValue)
      {
        child.Date = beginDefault.Value;
      }
      if (endDefault.HasValue)
      {
        input2.Date = endDefault.Value;
      }
      Panel panel = new Panel();
      panel.Controls.Add(child);
      panel.Controls.Add(new LiteralControl("→"));
      panel.Controls.Add(input2);
      return panel;
    }


    public static DFNamedValueInput<单据状态> 一般单据状态(IDFField fieldInfo, bool enableTopItem, bool defaultValue, bool enableMultiSelection, bool enableMultiSelectionViewer)
    {
      var choiceBox = new DFNamedValueInput<单据状态>(fieldInfo) { EnableTopItem = enableTopItem, EnableMultiSelection = enableMultiSelection, EnableMultiSelectionViewer = enableMultiSelectionViewer, InputArgument = "一般单据", Width = Unit.Empty };
      if (defaultValue)
        choiceBox.Value = 单据状态.未审核;
      choiceBox.Width = 160;
      return choiceBox;
    }
  }
}
