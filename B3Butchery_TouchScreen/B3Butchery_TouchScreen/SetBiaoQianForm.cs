using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using B3Butchery_TouchScreen.SqlEntityFramWork;
using B3Butchery_TouchScreen.SqliteEntityFramWork;
using BWP.WinFormBase;

namespace B3Butchery_TouchScreen
{
  public partial class SetBiaoQianForm : Form
  {
    public SetBiaoQianForm()
    {
      InitializeComponent();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      using (var db=new SqlDbContext())
      {
        var bqlist = db.BiaoQians.ToList();
        var bq1 = bqlist.FirstOrDefault(x => x.Id == 1);
        if (bq1 == null)
        {
          bq1=new BiaoQian();
          bq1.Id = 1;
          bq1.Name = dfTextBox1.Text;
          db.BiaoQians.Add(bq1);
        }
        else
        {
          db.BiaoQians.Attach(bq1);
          bq1.Name = dfTextBox1.Text;
        }

        var bq2 = bqlist.FirstOrDefault(x => x.Id == 2);
        if (bq2 == null)
        {
          bq2 = new BiaoQian();
          bq2.Id = 2;
          bq2.Name = dfTextBox2.Text;
          db.BiaoQians.Add(bq2);
        }
        else
        {
          db.BiaoQians.Attach(bq2);
          bq2.Name = dfTextBox2.Text;
        }

        var bq3 = bqlist.FirstOrDefault(x => x.Id == 3);
        if (bq3 == null)
        {
          bq3 = new BiaoQian();
          bq3.Id = 3;
          bq3.Name = dfTextBox3.Text;
          db.BiaoQians.Add(bq3);
        }
        else
        {
          db.BiaoQians.Attach(bq3);
          bq3.Name = dfTextBox3.Text;
        }

        var bq4 = bqlist.FirstOrDefault(x => x.Id == 4);
        if (bq4 == null)
        {
          bq4 = new BiaoQian();
          bq4.Id = 4;
          bq4.Name = dfTextBox4.Text;
          db.BiaoQians.Add(bq4);
        }
        else
        {
          db.BiaoQians.Attach(bq4);
          bq4.Name = dfTextBox4.Text;
        }

        var bq5 = bqlist.FirstOrDefault(x => x.Id == 5);
        if (bq5 == null)
        {
          bq5 = new BiaoQian();
          bq5.Id = 5;
          bq5.Name = dfTextBox5.Text;
          db.BiaoQians.Add(bq5);
        }
        else
        {
          db.BiaoQians.Attach(bq5);
          bq5.Name = dfTextBox5.Text;
        }

        var bq6 = bqlist.FirstOrDefault(x => x.Id == 6);
        if (bq6 == null)
        {
          bq6 = new BiaoQian();
          bq6.Id = 6;
          bq6.Name = dfTextBox6.Text;
          db.BiaoQians.Add(bq6);
        }
        else
        {
          db.BiaoQians.Attach(bq6);
          bq6.Name = dfTextBox6.Text;
        }

        var bq7 = bqlist.FirstOrDefault(x => x.Id == 7);
        if (bq7 == null)
        {
          bq7 = new BiaoQian();
          bq7.Id = 7;
          bq7.Name = dfTextBox7.Text;
          db.BiaoQians.Add(bq7);
        }
        else
        {
          db.BiaoQians.Attach(bq7);
          bq7.Name = dfTextBox7.Text;
        }

        var bq8 = bqlist.FirstOrDefault(x => x.Id == 8);
        if (bq8 == null)
        {
          bq8 = new BiaoQian();
          bq8.Id = 8;
          bq8.Name = dfTextBox8.Text;
          db.BiaoQians.Add(bq8);
        }
        else
        {
          db.BiaoQians.Attach(bq8);
          bq8.Name = dfTextBox8.Text;
        }

        var bq9 = bqlist.FirstOrDefault(x => x.Id == 9);
        if (bq9 == null)
        {
          bq9 = new BiaoQian();
          bq9.Id =9;
          bq9.Name = dfTextBox9.Text;
          db.BiaoQians.Add(bq9);
        }
        else
        {
          db.BiaoQians.Attach(bq9);
          bq9.Name = dfTextBox9.Text;
        }

        var bq10 = bqlist.FirstOrDefault(x => x.Id == 10);
        if (bq10 == null)
        {
          bq10 = new BiaoQian();
          bq10.Id = 10;
          bq10.Name = dfTextBox10.Text;
          db.BiaoQians.Add(bq10);
        }
        else
        {
          db.BiaoQians.Attach(bq10);
          bq10.Name = dfTextBox10.Text;
        }
        InitOperateAreaIfNotInited(db);
        db.SaveChanges();
      }
      DialogResult=DialogResult.OK;
      Close();
    }

    void InitOperateAreaIfNotInited(SqlDbContext db)
    {
      foreach (BiaoQian biaoQian in db.BiaoQians.ToList())
      {
        var count = biaoQian.GridConfigs.Count;
        for (int i = count+1; i <= 10; i++)
        {
          var area = new GridConfig();
          area.BiaoQianId = biaoQian.Id;
          biaoQian.GridConfigs.Add(area);
        }
      }
    }

    private void SetBiaoQianForm_Load(object sender, EventArgs e)
    {
      using (var db=new SqlDbContext())
      {
        var bqList = db.BiaoQians.ToList();
        var config = bqList.FirstOrDefault(x => x.Id == 1);
        if (config != null)
        {
          dfTextBox1.Text = config.Name;
        }

        config = bqList.FirstOrDefault(x => x.Id == 2);
        if (config != null)
        {
          dfTextBox2.Text = config.Name;
        }

        config = bqList.FirstOrDefault(x => x.Id == 3);
        if (config != null)
        {
          dfTextBox3.Text = config.Name;
        }
        config = bqList.FirstOrDefault(x => x.Id == 4);
        if (config != null)
        {
          dfTextBox4.Text = config.Name;
        }

        config = bqList.FirstOrDefault(x => x.Id == 5);
        if (config != null)
        {
          dfTextBox5.Text = config.Name;
        }
        config = bqList.FirstOrDefault(x => x.Id ==6);
        if (config != null)
        {
          dfTextBox6.Text = config.Name;
        }

        config = bqList.FirstOrDefault(x => x.Id == 7);
        if (config != null)
        {
          dfTextBox7.Text = config.Name;
        }
        config = bqList.FirstOrDefault(x => x.Id == 8);
        if (config != null)
        {
          dfTextBox8.Text = config.Name;
        }

        config = bqList.FirstOrDefault(x => x.Id == 9);
        if (config != null)
        {
          dfTextBox9.Text = config.Name;
        }
        config = bqList.FirstOrDefault(x => x.Id == 10);
        if (config != null)
        {
          dfTextBox10.Text = config.Name;
        }


      }
      
     

    }
  }
}
