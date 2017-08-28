using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KeyPad;

namespace B3Butchery_TouchScreen
{
  public partial class SetInputNumberForm : Form
  {
    public string Input1,Input2,Input3,Input4;

    private void textBox1_Click(object sender, EventArgs e)
    {
      var txt = sender as TextBox;
      var f = new Keypad();
      if (f.ShowDialog() == true)
      {
        txt.Text = f.Result;
      }
    }

    public SetInputNumberForm()
    {
      InitializeComponent();
    }

    public void Init(string input1, string input2, string input3, string input4)
    {
      textBox1.Text = input1;
      textBox2.Text = input2;
      textBox3.Text = input3;
      textBox4.Text = input4;
    }

    private void button1_Click(object sender, EventArgs e)
    {
      Input1 = textBox1.Text;
      Input2= textBox2.Text;
      Input3 = textBox3.Text;
      Input4 = textBox4.Text;
      DialogResult=DialogResult.OK;
      Close();
    }
  }
}
