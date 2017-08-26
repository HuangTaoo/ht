using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace B3Butchery_TouchScreen.Utils
{
  public  class LoadingUtil
  {
    private static Thread td;
    public static void Show(Action action)
    {
      var f= LoadingForm.GetInstance();
      f.Show();
      td=new Thread(new ThreadStart(action));
      td.IsBackground = true;
      td.Start();

//      Thread.Sleep(1000);
//      action();
    }
    public static void Hide()
    {
      var f = LoadingForm.GetInstance();
      f.Invoke(new Action(() => { f.Close(); }));
//      LoadingForm.GetInstance().Close();
    }

  }
}
