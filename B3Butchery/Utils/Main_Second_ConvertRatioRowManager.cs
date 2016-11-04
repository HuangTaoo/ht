using System.Text;
using TSingSoft.WebControls2;

namespace BWP.B3Butchery.Utils
{
  public class Main_Second_ConvertRatioRowManager
  {
    DFEditGridColumn<DFTextBox> numberInput, secondNumberInput, secondNumber2Input;
    string numberField, secondNumberField, secondNumber2Field;
    public Main_Second_ConvertRatioRowManager(DFEditGrid grid, string mNumberfield, string sNumberfield, string sNumber2field)
    {
      numberField = mNumberfield;
      secondNumberField = sNumberfield;
      secondNumber2Field = sNumber2field;

      foreach (DFGridColumn column in grid.Columns)
      {
        if (column is DFEditGridColumn<DFTextBox>)
        {
          var c = (DFEditGridColumn<DFTextBox>)column;
          if (c.Name == mNumberfield)
            numberInput = c;
          else if (c.Name == sNumberfield)
            secondNumberInput = c;
          else if (c.Name == sNumber2field)
            secondNumber2Input = c;
        }
        SetClientScript1();
      }
    }

    private void SetClientScript1()
    {
      SetNumberChanged1();
      SetSecondNumberChanged1();
    }

    string setSecondNumber1
    {
      get
      {
        return "dfContainer.setValue('" + secondNumber2Field + "', dfContainer.getValue('" + numberField + "') * " + ratioLeft + "/" + ratioRight + ");";
      }
    }

    private void SetNumberChanged1()
    {
      if (numberInput == null)
        return;
      var builder = new StringBuilder();
      builder.Append(@"if({convertDirection}=='双向转换'||{convertDirection}=='由主至辅'){ if({ratioLeft}>0)
				{setSecondNumber}{setSecondNumber1}}"
        .Replace("{convertDirection}", convertDirection)
        .Replace("{ratioLeft}", ratioLeft)
        .Replace("{setSecondNumber}", setSecondNumber)
        .Replace("{setSecondNumber1}", setSecondNumber1));
      numberInput.InitEditControl += delegate(object sender, InitEditControlEventArgs<DFTextBox> e)
      {
        e.Control.Attributes["onchange"] = builder.ToString();
      };
    }
    string setSecondNumberToMainNumber1
    {
      get
      {
        return "dfContainer.setValue('" + numberField + "', dfContainer.getValue('" + secondNumber2Field + "')*" + ratioRight + " / " + ratioLeft + ");";
      }
    }

    private void SetSecondNumberChanged1()
    {
      if (secondNumber2Input == null)
        return;

      var builder = new StringBuilder();

      builder.Append(@"if({convertDirection}=='双向转换'||{convertDirection}=='由辅至主'){ if({ratioRight}>0)
				{setMainNumber}}"
        .Replace("{convertDirection}", convertDirection)
        .Replace("{ratioRight}", ratioRight)
        .Replace("{setMainNumber}", setSecondNumberToMainNumber1));

      secondNumber2Input.InitEditControl += delegate(object sender, InitEditControlEventArgs<DFTextBox> e)
      {
        e.Control.Attributes["onchange"] = builder.ToString();
      };
    }









    public Main_Second_ConvertRatioRowManager(DFEditGrid grid, string mNumberfield, string sNumberfield)
    {
      numberField = mNumberfield;
      secondNumberField = sNumberfield;

      foreach (DFGridColumn column in grid.Columns)
      {
        if (column is DFEditGridColumn<DFTextBox>)
        {
          DFEditGridColumn<DFTextBox> c = (DFEditGridColumn<DFTextBox>)column;
          if (c.Name == mNumberfield)
            numberInput = c;
          else if (c.Name == sNumberfield)
            secondNumberInput = c;
        }
        SetClientScript();
      }
    }

    string setSecondNumberToMainNumber
    {
      get
      {
        return "dfContainer.setValue('" + numberField + "', dfContainer.getValue('" + secondNumberField + "')*" + ratioRight + " / " + ratioLeft + ");";
      }
    }

    string setSecondNumber
    {
      get
      {
        return "dfContainer.setValue('" + secondNumberField + "', dfContainer.getValue('" + numberField + "') * " + ratioLeft + "/" + ratioRight + ");";
      }
    }

    string convertDirection
    {
      get
      {
        return "dfContainer.getValue('Goods_UnitConvertDirection')";
      }
    }

    string ratioLeft
    {
      get
      {
        return "dfContainer.getValue('Goods_SecondUnitRatio')";
      }
    }

    string ratioRight
    {
      get
      {
        return "dfContainer.getValue('Goods_MainUnitRatio')";
      }
    }

    private void SetClientScript()
    {
      SetNumberChanged();
      SetSecondNumberChanged();
    }

    private void SetSecondNumberChanged()
    {
      if (secondNumberInput == null)
        return;

      StringBuilder builder = new StringBuilder();

      if (numberInput != null)
      {
        builder.Append(@"if({convertDirection}=='双向转换'||{convertDirection}=='由辅至主'){ if({ratioRight}>0)
				{setMainNumber}}"
          .Replace("{convertDirection}", convertDirection)
          .Replace("{ratioRight}", ratioRight)
          .Replace("{setMainNumber}", setSecondNumberToMainNumber));
      }

      secondNumberInput.InitEditControl += delegate(object sender, InitEditControlEventArgs<DFTextBox> e)
      {
        e.Control.Attributes["onchange"] = builder.ToString();
      };
    }
    //0双、1到辅、2到主
    private void SetNumberChanged()
    {
      if (numberInput == null)
        return;

      StringBuilder builder = new StringBuilder();

      if (secondNumberInput != null)
      {
        builder.Append(@"if({convertDirection}=='双向转换'||{convertDirection}=='由主至辅'){ if({ratioLeft}>0)
				{setSecondNumber}}"
          .Replace("{convertDirection}", convertDirection)
          .Replace("{ratioLeft}", ratioLeft)
          .Replace("{setSecondNumber}", setSecondNumber));
      }

      numberInput.InitEditControl += delegate(object sender, InitEditControlEventArgs<DFTextBox> e)
      {
        e.Control.Attributes["onchange"] = builder.ToString();
      };
    }
  }
}
