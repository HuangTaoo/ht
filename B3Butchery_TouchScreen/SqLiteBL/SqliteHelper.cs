using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace SqLiteBL
{
  public  class SqliteHelper1
  {
    public static void Exe()
    {
      using (var con=new SQLiteConnection("B3ButcherTouchScreen.db"))
      {
        con.Open();
      }
    }
  }
}
