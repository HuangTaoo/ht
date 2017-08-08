using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace B3ButcheryCE.ProductInStoreConfirm_
{
    public partial class ProductInStoreConfirmInputNumberForm : Form
    {
        public decimal Number;
        public ProductInStoreConfirmInputNumberForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Number = decimal.Parse(textBox1.Text);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());                
            }           
        }
    }
}