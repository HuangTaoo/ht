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

  public sealed class 生产类型
  {
    public static readonly NamedValue<生产类型> 日计划 = new NamedValue<生产类型>(0);
    public static readonly NamedValue<生产类型> 周计划 = new NamedValue<生产类型>(1);
  }

  public sealed class 暂存类型
  {
    public static readonly NamedValue<暂存类型> 零箱 = new NamedValue<暂存类型>(0);
    public static readonly NamedValue<暂存类型> 未冻好 = new NamedValue<暂存类型>(1);
    public static readonly NamedValue<暂存类型> 返车间 = new NamedValue<暂存类型>(2);
  }

    public sealed class 包装模式
    {
        public static readonly NamedValue<包装模式> 箱装 = new NamedValue<包装模式>(0);
        public static readonly NamedValue<包装模式> 袋装 = new NamedValue<包装模式>(1);

    }
}
