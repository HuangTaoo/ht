using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using B3Butchery_TouchScreen.SqlEntityFramWork;
using B3Butchery_TouchScreen.SqliteEntityFramWork;
using B3Butchery_TouchScreen.Utils;
using B3HuaDu_TouchScreen.Config;
using BWP.WinFormBase;
using Forks.JsonRpc.Client;
using Forks.JsonRpc.Client.Data;


namespace B3Butchery_TouchScreen
{
  public partial class FrozenInStorePieceForm : Form
  {
    private LoadingForm loadingForm;
    private Thread loadThread; //加载线程

    private BiaoQian mBiaoQian;

    public FrozenInStorePieceForm()
    {
      InitializeComponent();
      loadingForm = new LoadingForm();

      loadThread = new Thread((ThreadStart) delegate
      {
        Application.Run(loadingForm);
      });
      loadThread.IsBackground = true;
      loadThread.Start();

    }
    private void FrozenInStorePieceForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      OpenLoginForm();
    }
    void OpenLoginForm()
    {
      foreach (Form form in Application.OpenForms)
      {
        if (form is LoginForm)
        {
          form.Show();
          return;
        }
      }
    }


    private void FrozenInStorePieceForm_Load(object sender, EventArgs e)
    {
      LoadBiaoQians();

      loadingForm.Invoke(new Action(() => loadingForm.Close()));
      if (loadThread.IsAlive)
      {
        loadThread.Abort();
      }
//      LoadingUtil.Hide();
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

    private void btnCommit_Click(object sender, EventArgs e)
    {
      var sb=new StringBuilder();
      var seccesGridConfigList = getSuccessGridConfigs();

      foreach (IGrouping<DateTime?, GridConfig> grouping in seccesGridConfigList.GroupBy(x=>x.ProductDate))
      {
        try
        {
          var date = grouping.Key;
          if (date == null)
          {
            date = DateTime.Today;
          }

          var dmo = new RpcObject("/MainSystem/B3Butchery/BO/FrozenInStore");
          dmo.Set("Date", date);
          foreach (GridConfig gridConfig in grouping)
          {
            var number = gridConfig.InputRecords.Sum(x => x.Number);
            var weight = gridConfig.InputRecords.Sum(x => x.Weight);
            //有数量并且没有提交
            if ((number > 0 || weight > 0) && !gridConfig.IsCommited)
            {
              var detail = new RpcObject("/MainSystem/B3Butchery/BO/FrozenInStore_Detail");
              detail.Set("Goods_ID", gridConfig.Goods_ID);
              detail.Set("SecondNumber2", Convert.ToDecimal(number));//生产数量
              detail.Set("Number", weight);//主数量
              dmo.Get<ManyList>("Details").Add(detail);
            }
          }
          RpcFacade.Call<long>("/MainSystem/B3Butchery/Rpcs/FrozenInStoreRpc/ButcherTouchScreenInsert", dmo);
        }
        catch (Exception exception)
        {
         sb.AppendLine(exception.Message);
        }
      }
      //设置确定按钮是否能点击
      SetBtnOkEnable(seccesGridConfigList);
      SetIsCommitedTrue(seccesGridConfigList);
      MessageBox.Show("提交成功");
      if (sb.ToString().Length > 0)
      {
        MessageBox.Show(sb.ToString(),"未生产速冻入库错误信息");
      }
    }

    private void SetIsCommitedTrue(List<GridConfig> seccesGridConfigList)
    {
      using (var db=new SqlDbContext())
      {
        foreach (GridConfig config in seccesGridConfigList)
        {
          var dbConfig = db.GridConfigs.Find(config.Id);
          db.GridConfigs.Attach(dbConfig);
          dbConfig.IsCommited = true;
        }
        db.SaveChanges();
      }
    }

    List<GridConfig> getSuccessGridConfigs()
    {
      var succesGridConfigList = new List<GridConfig>();
      using (var db = new SqlDbContext())
      {
        //遍历所有标签
        var bqList = db.BiaoQians.OrderBy(x => x.Id).Skip(0).Take(10).ToList();
        foreach (BiaoQian qian in bqList)
        {
          var gridConfigs = db.GridConfigs.Include(x => x.InputRecords).Where(x => x.BiaoQianId == qian.Id && x.Goods_ID.HasValue && x.Goods_ID.Value > 0 && !x.IsCommited).ToList();
          foreach (GridConfig gridConfig in gridConfigs)
          {
              var number = gridConfig.InputRecords.Sum(x => x.Number);
              var weight = gridConfig.InputRecords.Sum(x => x.Weight);
              //有数量并且没有提交
              if (number > 0 || weight > 0)
              {
              succesGridConfigList.Add(gridConfig);
              }
          }
        }
      }
      return succesGridConfigList;
    }

    private void SetBtnOkEnable(List<GridConfig> seccesGridConfigList)
    {
        foreach (Control control in flpGrid.Controls)
        {
          var gridConfig = control.Tag as GridConfig;
          //如果已经创建成功
          if (seccesGridConfigList.Any(x => x.Id == gridConfig.Id))
          {
            gridConfig.IsCommited = true;
            control.Controls["btnOk"].Enabled = true;
            control.Controls["btnOk"].BackColor = Color.DodgerBlue;
          }
      }
    }
  }
}
