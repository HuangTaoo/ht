using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BWP.WinFormBase;

namespace BWP.WinFormControl
{
  public  class DFTextBox:TextBox
  {
    public DFTextBox()
    {
      this.Font=new Font("宋体",20);
    }

    protected override void OnClick(EventArgs e)
    {
      WinFormUtil.OpenVirtualKeyboard();
      base.OnClick(e);

      var form = GetParentFormm(this);
      form.Activate();
      this.Focus();

    }

    Form GetParentFormm(Control control)
    {
      if (control.Parent != null)
      {
        if (control.Parent is Form)
        {
          return control.Parent as Form; ;
        }
        else
        {
          return GetParentFormm(control.Parent);
        }
      }
      return control as Form;

    }
  }
}
