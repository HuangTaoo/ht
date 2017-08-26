using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using B3Butchery_TouchScreen.SqlEntityFramWork;
using B3HuaDu_TouchScreen.Config;
using BWP.WinFormBase;
using Forks.JsonRpc.Client;

namespace B3Butchery_TouchScreen
{
  static class Program
  {
    /// <summary>
    /// 应用程序的主入口点。
    /// </summary>
    [STAThread]
    static void Main()
    {
      try
      {
        //处理未捕获的异常   
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        //处理UI线程异常   
        Application.ThreadException += Application_ThreadException;
        //处理非UI线程异常   
        AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

        var aProcessName = Process.GetCurrentProcess().ProcessName;
        if ((Process.GetProcessesByName(aProcessName)).GetUpperBound(0) > 0)
        {
          MessageBox.Show(@"系统已经在运行中，如果要重新启动，请从进程中关闭...", @"系统警告", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        else
        {
          Application.EnableVisualStyles();
          Application.SetCompatibleTextRenderingDefault(false);

          RpcFacade.Init(AppFactory.AppConfig.ServerUrl, "B3Butchery_TouchScreen");
          Application.Run(new  LoginForm());
        }
      }
      catch (Exception e)
      {
        LogUtil.WriteError(e);
        MessageBox.Show("错误：" + e.Message);
      }

    }

    ///<summary>
    ///  在发生未处理异常时处理的方法
    ///</summary>
    ///<param name="sender"> </param>
    ///<param name="e"> </param>
    private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
    {
      var ex = e.Exception;
      var err = String.Empty;
      if (ex != null)
      {
        LogUtil.WriteError(ex);
        err = ex.Message;
      }
      MessageBox.Show("错误：" + err);
    }

    private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
      var err = String.Empty;
      var ex = e.ExceptionObject as Exception;
      if (ex != null)
      {
        LogUtil.WriteError(ex);
        err = ex.Message;
      }
      MessageBox.Show("错误：" + err);
    }

  }
}
