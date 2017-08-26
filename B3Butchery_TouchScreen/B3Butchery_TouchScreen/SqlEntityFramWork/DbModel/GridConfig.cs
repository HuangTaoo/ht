using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using B3Butchery_TouchScreen.SqliteEntityFramWork;

namespace B3Butchery_TouchScreen.SqlEntityFramWork
{
  public class GridConfig:DbBase
  {
    public GridConfig()
    {
      InputRecords=new List<InputRecord>();
      GridAddedNumbers=new List<GridAddedNumber>();
    }

    public int BiaoQianId { get; set; }

    [ForeignKey("BiaoQianId")]
    public virtual BiaoQian BiaoQian { get; set; }

    public string Name { get; set; }
    public string GuiGe { get; set; }

    public long? Goods_ID { get; set; }
    public string Goods_Name { get; set; }

    public DateTime? ProductDate { get; set; }//生产日期

    [DefaultValue(false)]
    public bool IsCommited { get; set; }

    public virtual List<InputRecord> InputRecords { get; set; }
    public virtual List<GridAddedNumber> GridAddedNumbers { get; set; }


  }
}
