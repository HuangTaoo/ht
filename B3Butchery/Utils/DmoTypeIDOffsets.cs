using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BWP.B3Butchery.Utils
{
  internal static class DmoTypeIDOffsets
  {
    public const byte ProductPlan = 1;
    public const byte ProduceOutput = 2;
    public const byte ProduceInput = 3;
		public const byte ProductInStore = 4;
		public const byte DailyProductReport = 5;
    public const byte ProductInStore_Temp = 6;
    public const byte ProductNotice = 7;
    public const byte ProductPackaging = 8;
    public const byte TemporaryStorage = 9;
    public const byte FrozenInStore = 10;
    public const byte PackingRecipients = 11;
    public const byte Picking = 12;
    public const byte ProduceFinish = 13;
  }
}
