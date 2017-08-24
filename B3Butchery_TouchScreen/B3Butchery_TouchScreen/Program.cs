using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using B3Butchery_TouchScreen.SqlEntityFramWork;

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
      //
      using (var context=new SqlDbContext())
      {
        var list = context.BiaoQians;
//        var ss = list.FirstOrDefault().OperateAreas.Count;
      }
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new FrozenInStorePieceForm());
    }
  
  }
}
