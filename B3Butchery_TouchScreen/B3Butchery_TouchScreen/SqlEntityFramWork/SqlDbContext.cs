using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;

using B3Butchery_TouchScreen.SqlEntityFramWork;
using B3Butchery_TouchScreen.SqliteEntityFramWork;

namespace B3Butchery_TouchScreen
{
  public  class SqlDbContext:DbContext
  {
    public SqlDbContext():base("Default")
    {
      Database.Initialize(false);
    }

    public  DbSet<BiaoQian> BiaoQians { get; set; }
    public  DbSet<GridConfig> GridConfigs { get; set; }
    public  DbSet<AppSetting> AppSettings { get; set; }
    public  DbSet<GridAddedNumber> GridAddedNumbers { get; set; }
    public  DbSet<InputRecord> InputRecords { get; set; }



    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      modelBuilder.Entity<BiaoQian>().HasMany(x => x.GridConfigs);
      modelBuilder.Entity<GridConfig>().HasMany(x => x.InputRecords);
      modelBuilder.Entity<GridConfig>().HasMany(x => x.GridAddedNumbers);
      base.OnModelCreating(modelBuilder);
    }
  }
}
