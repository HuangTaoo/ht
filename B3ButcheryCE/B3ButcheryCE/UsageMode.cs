using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace B3HRCE
{
    [Flags]
    public enum UsageMode
    {
        empty = 0,
        案组计件新增 = 1,
        个人计件新增 = 2,
        成品入库新增 = 4,
        生产环节新增 = 8,
    }
}
