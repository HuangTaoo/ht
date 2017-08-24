using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
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
  public partial class FrozenInStorePieceForm : Form
  {

    private BiaoQian mBiaoQian;

    public FrozenInStorePieceForm()
    {
      InitializeComponent();
    }

    private void FrozenInStorePieceForm_Load(object sender, EventArgs e)
    {
      LoadBiaoQians();

  
//      flpGrid.Controls.Add(ControlUtil.CreateGridPanel(null));
//      flpGrid.Controls.Add(ControlUtil.CreateGridPanel(null));
//      flpGrid.Controls.Add(ControlUtil.CreateGridPanel(null));
//      flpGrid.Controls.Add(ControlUtil.CreateGridPanel(null));
//      flpGrid.Controls.Add(ControlUtil.CreateGridPanel(null));
    }

    void CreateOperateAreaControl(int biaoQianId)
    {
      flpGrid.Controls.Clear();
      using (var db=new SqlDbContext())
      {
        var gridConfigs = db.GridConfigs.Include(x=>x.InputRecords).OrderBy(x=>x.Id).Where(x=>x.BiaoQianId== biaoQianId).Skip(0).Take(10).ToList();
        foreach (GridConfig gridConfig in gridConfigs)
        {
          flpGrid.Controls.Add(ControlUtil.CreateGridPanel(gridConfig));
        }
      }
      
    }


    private void LoadBiaoQians()
    {
      flpBiaoQian.Controls.Clear();
      using (var db=new SqlDbContext())
      {
        var bqList = db.BiaoQians.ToList().OrderBy(x => x.Id).Where(x => !string.IsNullOrWhiteSpace(x.Name));

        foreach (var config in bqList)
        {
          var btn = new Button();
          btn.Height = 48;
          btn.Text = config.Name;
          btn.Name = config.Name;
          btn.Tag = config;
          btn.Click += BtnBiaoQian_Click;
          flpBiaoQian.Controls.Add(btn);
        }
        LoadCurrentCurrentBiaoQian(db);

        db.SaveChanges();
      }

   
    }

    private void LoadCurrentCurrentBiaoQian(SqlDbContext db)
    {
      if (mBiaoQian == null)
      {
        var set = db.AppSettings.FirstOrDefault(x => x.AppSettintType == AppSettintType.CurrentBiaoQian);
        if (set != null)
        {
          mBiaoQian = db.BiaoQians.FirstOrDefault(x=>x.Id==set.IntValue);
        }

        if (mBiaoQian == null)
        {
          mBiaoQian = new BiaoQian();
          mBiaoQian.Id = 1;
          var biaoQianNameConfig = db.BiaoQians.FirstOrDefault(x => x.Id == 1);
          if (biaoQianNameConfig != null)
          {
            mBiaoQian.Name = biaoQianNameConfig.Name;
          }
        }
      }
      //设置选中效果
      if (flpBiaoQian.Controls.Count > 0)
      {
        flpBiaoQian.Controls[mBiaoQian.Id - 1].BackColor = Color.Aquamarine;
        flpBiaoQian.Controls[mBiaoQian.Id - 1].Select();
        flpBiaoQian.Controls[mBiaoQian.Id - 1].Focus();
      }

      //保存当前选中标签到数据库
      UpdateCurrentSelectBiaoQian(db);
      CreateOperateAreaControl(mBiaoQian.Id);
    }

    void UpdateCurrentSelectBiaoQian(SqlDbContext db)
    {
      var newSet = db.AppSettings.FirstOrDefault(x => x.AppSettintType == AppSettintType.CurrentBiaoQian);
      if (newSet != null)
      {
        db.AppSettings.Attach(newSet);
        newSet.IntValue = mBiaoQian.Id;
      }
      else
      {
        newSet = new AppSetting();
        newSet.AppSettintType = AppSettintType.CurrentBiaoQian;
        newSet.IntValue = mBiaoQian.Id;
        db.AppSettings.Add(newSet);
      }
    }

    //点击标签
    private void BtnBiaoQian_Click(object sender, EventArgs e)
    {
      var btn = sender as Button;
      foreach (Control control in flpBiaoQian.Controls)
      {
        if (control is Button)
        {
          control.BackColor = SystemColors.Control;
        }
      }

      btn.BackColor = Color.Aquamarine;
      mBiaoQian = btn.Tag as BiaoQian;

      using (var db=new SqlDbContext())
      {
        UpdateCurrentSelectBiaoQian(db);

        CreateOperateAreaControl(mBiaoQian.Id);

        db.SaveChanges();
      }
    }

    private void button6_Click(object sender, EventArgs e)
    {
      var f=new SetBiaoQianForm();
      if (f.ShowDialog() == DialogResult.OK)
      {
        LoadBiaoQians();
      }
    }
  }
}
