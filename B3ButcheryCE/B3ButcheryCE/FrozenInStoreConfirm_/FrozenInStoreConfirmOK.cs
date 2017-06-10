using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace B3ButcheryCE.FrozenInStoreConfirm_
{
    public partial class FrozenInStoreConfirmOK : Form
    {
        public decimal Number = 0;
        public FrozenInStoreConfirmOK()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                Number = decimal.Parse(textBox1.Text);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}