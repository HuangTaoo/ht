using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace B3ButcheryCE
{
    public partial class ErrorInfoDialog : Form
    {
        public ErrorInfoDialog(string info)
        {
            InitializeComponent();
            Util.SetSceen(this);
            textBox1.Text = info;
        }
    }
}