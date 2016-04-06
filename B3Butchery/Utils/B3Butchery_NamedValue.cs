using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forks.Utils;

namespace BWP.B3Butchery.Utils
{
	public sealed class 采集方式
	{
		public static readonly NamedValue<采集方式> 投入 = new NamedValue<采集方式>(0);
		public static readonly NamedValue<采集方式> 产出 = new NamedValue<采集方式>(1);
	}
}
