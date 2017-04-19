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
    public partial class MaterialStatisticsForm : Form
    {
        public MaterialStatisticsForm()
        {
            InitializeComponent();
        }

        private void MaterialStatisticsForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var f=new MaterialStatisticsInputNumberForm();
            if (f.ShowDialog() == DialogResult.OK)
            {
                var number = f.Number;

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}