using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace B3HRCE
{
    public partial class FormProcessBar : Form
    {
        public FormProcessBar()
        {
            InitializeComponent();
        }

        public bool Increase(int nValue)
        {
            if (nValue > 0)
            {
                if (prcBar.Value + nValue < prcBar.Maximum)
                {
                    prcBar.Value += nValue;
                    label1.Text = prcBar.Value + "/100";
                    return true;
                }
                else
                {
                    prcBar.Value = prcBar.Maximum;
                    label1.Text = string.Empty;
                    this.Close();
                    return false;
                }
            }
            return false;
        }  
    }
}