using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

using B3Butchery_TouchScreen.SqliteEntityFramWork;

namespace B3Butchery_TouchScreen.SqlEntityFramWork
{
  public class InputRecord:DbBase
  {

    public int GirdConfigId { get; set; }

    [ForeignKey("GirdConfigId")]
    public virtual GridConfig GridConfig { get; set; }

    public int Number { get; set; }
    public decimal Weight { get; set; }

    public override string ToString()
    {
      return Number.ToString();
    }
  }
}
