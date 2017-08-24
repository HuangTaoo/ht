using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using B3Butchery_TouchScreen.SqliteEntityFramWork;

namespace B3Butchery_TouchScreen.SqlEntityFramWork
{
  public class AppSetting:DbBase
  {
    public AppSettintType AppSettintType { get; set; }
    public int IntValue { get; set; }
  }
  public enum AppSettintType
  {
    CurrentBiaoQian=1,
  }
}
