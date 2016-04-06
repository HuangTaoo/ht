using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSingSoft.WebControls2;
using System.Web.UI.WebControls;

namespace BWP.B3Butchery.Utils
{
  public class Main_Second_ConvertRatioRowManager
  {
    DFEditGridColumn<DFTextBox> numberInput, secondNumberInput;
    string numberField, secondNumberField;
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
