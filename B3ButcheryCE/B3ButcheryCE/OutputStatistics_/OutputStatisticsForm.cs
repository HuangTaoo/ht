using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using B3ButcheryCE.ProductInStore_;

namespace B3HRCE.OutputStatistics_
{
    public partial class OutputStatisticsForm : Form
    {
        public OutputStatisticsForm()
        {
            InitializeComponent();
            Util.SetSceen(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new MaterialStatisticsForm().ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new FrozenInStore_.FrozenInStoreForm().ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new ProductInStoreForm().ShowDialog();
        }
    }
}