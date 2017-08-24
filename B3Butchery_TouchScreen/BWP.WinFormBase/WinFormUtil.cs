using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace BWP.WinFormBase
{
  public class WinFormUtil
  {
    // 申明要使用的dll和api
    [DllImport("User32.dll", EntryPoint = "FindWindow")]
    static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
    [DllImportAttribute("user32.dll", EntryPoint = "MoveWindow")]
    static extern bool MoveWindow(System.IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

    [DllImport("user32.dll")]
    static extern bool SetForegroundWindow(IntPtr hWnd);


    /// <summary>
    /// 1. 默认居中
    /// </summary>
    /// <param name="form"></param>
    /// <param name="noBorder"></param>
    public void InitForm(Form form, bool noBorder = false)
    {
      if (noBorder)
      {
        form.FormBorderStyle = FormBorderStyle.None | FormBorderStyle.FixedSingle;
        form.ControlBox = false;
      }
      form.StartPosition = FormStartPosition.CenterScreen;
    }

    /// <summary>
    /// 打开屏幕键盘
    /// </summary>
    public static void OpenVirtualKeyboard()
    {
      //打开软键盘
      try
      {
        var oskFile = Environment.SystemDirectory + "\\osk.exe";
        if (!System.IO.File.Exists(oskFile))
        {
          MessageBox.Show("软件盘可执行文件不存在！");
          return;
        }
        Process.Start(oskFile);
        // 上面的语句在打开软键盘后，系统还没用立刻把软键盘的窗口创建出来了。所以下面的代码用循环来查询窗口是否创建，只有创建了窗口
        // FindWindow才能找到窗口句柄，才可以移动窗口的位置和设置窗口的大小。这里是关键。
        IntPtr intptr = IntPtr.Zero;
        while (IntPtr.Zero == intptr)
        {
          System.Threading.Thread.Sleep(100);
          intptr = FindWindow(null, "屏幕键盘");
        }
        // 获取屏幕尺寸
        int iActulaWidth = Screen.PrimaryScreen.Bounds.Width;
        int iActulaHeight = Screen.PrimaryScreen.Bounds.Height;
        // 设置软键盘的显示位置，底部居中
        int posX = (iActulaWidth - 1000) / 2;
        int posY = (iActulaHeight - 300);
        //设定键盘显示位置
        MoveWindow(intptr, posX, posY, 1000, 300, true);
        //设置软键盘到前端显示
        SetForegroundWindow(intptr);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    /// <summary>
    /// 关闭屏幕键盘
    /// </summary>
    public static void CloseVirtualKeyboard()
    {
      try
      {
        foreach (var item in Process.GetProcessesByName("osk"))
        {
          item.Kill();
        }
      }
      catch (Exception ex)
      {
        //MessageBox.Show(ex.Message);
      }
    }
  }
}
