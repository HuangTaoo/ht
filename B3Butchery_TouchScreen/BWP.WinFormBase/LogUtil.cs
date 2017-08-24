using System;
using System.IO;
using System.Text;

namespace BWP.WinFormBase
{
  public class LogUtil
  {
    /// <summary>  
    /// 锁对象  
    /// </summary>  
    private static readonly object LockHelper = new object();


    /// <summary>  
    /// 写error级别日志  
    /// </summary>  
    /// <param name="errorMessage">异常信息</param>  
    public static void WriteError(string errorMessage)
    {
      var errorMsg = "error：" + errorMessage;
      WriteMsg(errorMsg);
    }

    /// <summary>
    /// 写error级别日志  
    /// </summary>
    /// <param name="ex">异常类</param>
    public static void WriteError(Exception ex)
    {
      string errorMsg = string.Empty;
      if (ex.InnerException != null)
      {
        errorMsg = ex.InnerException.Message;
      }
      errorMsg = "error："+errorMsg +  ex.StackTrace;
      WriteMsg(errorMsg);
    }

    /// <summary>  
    /// 写日志  
    /// </summary>  
    /// <param name="msg">日志信息</param>      
    public static void WriteInfo(string msg)
    {
      msg = "info：" + msg;
      WriteMsg(msg);
    }



    private static void WriteMsg(string msg)
    {
      lock (LockHelper)
      {
        string absolutePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Log";
        if (!Directory.Exists(absolutePath))
        {
          Directory.CreateDirectory(absolutePath);
        }
        string filePath = absolutePath + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
        File.AppendAllText(filePath, "\r\n" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n" + msg, Encoding.GetEncoding("gb2312"));
      }
    }
  }
}
