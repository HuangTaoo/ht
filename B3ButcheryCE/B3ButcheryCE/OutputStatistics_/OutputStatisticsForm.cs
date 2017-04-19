using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace B3HRCE.OutputStatistics_
{
    public partial class OutputStatisticsForm : Form
    {
        public OutputStatisticsForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new MaterialStatisticsForm().ShowDialog();
        }
    }
}