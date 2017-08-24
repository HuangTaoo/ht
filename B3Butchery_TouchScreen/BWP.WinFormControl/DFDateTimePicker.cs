using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BWP.WinFormControl
{
  public partial class DFDateTimePicker : UserControl
  {

    //private int mDFSize;

    //public int DFSize  {
    //  get { return mDFSize; }
    //  set
    //  {
    //    mDFSize = value;
    //    dateTimePicker1.Font = new Font("宋体", mDFSize);
    //  }
    //}
    public DFDateTimePicker()
    {
      InitializeComponent();
    }

    private void btnUp_Click(object sender, EventArgs e)
    {
      dateTimePicker1.Focus();
      SendKeys.Send("{Up}");
    }

    private void btnDown_Click(object sender, EventArgs e)
    {
      dateTimePicker1.Focus();
      SendKeys.Send("{Down}");
    }
  }
}
