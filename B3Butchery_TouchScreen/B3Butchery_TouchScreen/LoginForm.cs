﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using B3Butchery_TouchScreen.Utils;
using B3HuaDu_TouchScreen.Config;
using BWP.WinFormBase;
using Forks.JsonRpc.Client;

namespace B3Butchery_TouchScreen
{
  public partial class LoginForm : Form
  {
    public LoginForm()
    {
      InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      var name = txtName.Text.Trim();
      var pwd = txtPwd.Text.Trim();
      RpcFacade.Login(name, pwd);
      var config = AppFactory.AppConfig;
      config.UserName = name;
      config.Password = pwd;
      XmlUtil.SerializerObjToFile(config);
      // MessageBox.Show("登录成功");
      //MessageBox.Show(AppFactory.AppContext.Department_Name);
      Hide();
      var f = new FrozenInStorePieceForm();
      f.Show();
      //      LoadingUtil.Show(f.Show);


    }

    private void LoginForm_Load(object sender, EventArgs e)
    {
      txtName.Text = AppFactory.AppConfig.UserName;
      txtPwd.Text = AppFactory.AppConfig.Password;
      //      var td=new Thread(InitEF);
      //      td.Start();
    }

    private void InitEF()
    {
      using (var db = new SqlDbContext())
      {
        var bqList = db.BiaoQians.ToList();
        button1.Invoke(new Action(() =>
        {
          button1.Enabled = true;
        }));

      }
    }

    private void button2_Click(object sender, EventArgs e)
    {
      Application.Exit();
    }
  }
}