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
  public partial class LoadingForm : Form
  {
    public LoadingForm()
    {
      InitializeComponent();
    }

    private static readonly LoadingForm instance = new LoadingForm();
    public static LoadingForm GetInstance()
    {
      return instance;
    }

    private void LoadingForm_Load(object sender, EventArgs e)
    {

    }
  }
}
