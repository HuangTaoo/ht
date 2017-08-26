using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace B3Butchery_TouchScreen
{
  public partial class SelectDateFrom : Form
  {
    public DateTime mDateTime;
    public SelectDateFrom()
    {
      InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      mDateTime = dfDatePicker1.Value;
      DialogResult=DialogResult.OK;
      Close();
    }

    private void button2_Click(object sender, EventArgs e)
    {
      Close();
    }
  }
}
