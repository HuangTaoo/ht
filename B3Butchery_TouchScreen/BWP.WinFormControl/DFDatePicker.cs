using System;
using System.Windows.Forms;

namespace BWP.WinFormControl
{
  public partial class DFDatePicker : UserControl
  {
    public DFDatePicker()
    {
      InitializeComponent();
    }

    private void btnUp_Click(object sender, System.EventArgs e)
    {
      dateTimePicker1.Focus();
      SendKeys.Send("{Up}");
    }

    private void btnDown_Click(object sender, System.EventArgs e)
    {
      dateTimePicker1.Focus();
      SendKeys.Send("{Down}");
    }

    public DateTime Value {
      get { return dateTimePicker1.Value.Date; }
    }
  }
}
