using Forks.Utils;

namespace BWP.B3Butchery.Utils
{
  public sealed class 采集方式
  {
    public static readonly NamedValue<采集方式> 投入 = new NamedValue<采集方式>(0);
    public static readonly NamedValue<采集方式> 产出 = new NamedValue<采集方式>(1);
  }

  public sealed class 投入类型
  {
    public static readonly NamedValue<投入类型> 当日屠宰 = new NamedValue<投入类型>(0);
    public static readonly NamedValue<投入类型> 非当日屠宰 = new NamedValue<投入类型>(1);
  }
}
