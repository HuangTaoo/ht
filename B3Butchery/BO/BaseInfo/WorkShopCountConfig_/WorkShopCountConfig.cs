using BWP.B3Butchery.Utils;
using BWP.B3Frameworks.BO;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSingSoft.WebPluginFramework;

namespace BWP.B3Butchery.BO
{

    [DFClass]
    [LogicName("车间计数配置单")]
    [Serializable]
    public class WorkShopCountConfig : DomainBaseInfo
    {
        private DateTime? _date = BLContext.Today;
        [LogicName("日期")]
        [DFNotEmpty]
        public DateTime? Date
        {
            get { return _date; }
            set { _date = value; }
        }




    [LogicName("客户端显示序号")]
    [DbColumn(AllowNull = false, Unique = true, DefaultValue =0)]
    public int No { get; set; }





    [DFDataKind(B3ButcheryDataSource.车间品类)]
        [DFExtProperty("DisplayField", "WorkshopCategory_Name")]
        [DFExtProperty(B3ButcheryDataSource.车间品类, B3ButcheryDataSource.车间品类)]
        [DFPrompt("车间品类")]
        [DFNotEmpty]
        public long? WorkshopCategory_ID { get; set; }


        [LogicName("车间品类")]
        [ReferenceTo(typeof(WorkshopCategory), "Name")]
        [Join("WorkshopCategory_ID", "ID")]
        public string WorkshopCategory_Name { get; set; }


        private readonly WorkShopCountConfig_DetailCollection _details = new WorkShopCountConfig_DetailCollection();

        [OneToMany(typeof(WorkShopCountConfig_Detail), "ID")]
        [Join("ID", "WorkShopCountConfig_ID")]
        public WorkShopCountConfig_DetailCollection Details
        {
            get { return _details; }
        }
    }
}
