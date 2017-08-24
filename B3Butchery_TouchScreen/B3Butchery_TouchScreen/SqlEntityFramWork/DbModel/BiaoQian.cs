using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using B3Butchery_TouchScreen.SqlEntityFramWork;

namespace B3Butchery_TouchScreen.SqliteEntityFramWork
{
  public class BiaoQian
  {
    [Key]
    [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]//不自动增长
    public int Id { get; set; }
    public string Name { get; set; }

    public virtual List<GridConfig> GridConfigs { get; set; }
  }
}
