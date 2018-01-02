using BWP.Web.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BWP.Web.Utils
{
  public class ButcheryPrintAction: PrintAction
  {
    public ButcheryPrintAction(string printPage) : this(printPage, "打印") { }


    public ButcheryPrintAction(string printPage, string text) : base(printPage, text) { }

    public override bool Enabled
    {
      get
      {
        return true; 
      }
    }
    
  }
}
