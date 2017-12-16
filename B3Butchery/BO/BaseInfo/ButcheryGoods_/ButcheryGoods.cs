using BWP.B3Butchery.Utils;
using BWP.B3Frameworks;
using BWP.B3UnitedInfos.BO;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BWP.B3Butchery.BO
{

    [DFClass, Serializable]
    [LogicName("存货")]
    [MapToTable("B3UnitedInfos_Goods")]
    [DmoTypeID(B3FrameworksConsts.DmoTypeIDBases.B3Butchery, DmoTypeIDOffsets.ButcheryGoods)]
    public class ButcheryGoods:Goods
    {
        [Join("GoodsProperty_ID", "ID")]
        [LogicName("属性分类")]
        [ReferenceTo(typeof(GoodsProperty), "GoodsPropertyCatalog_ID")]
        public long? GoodsPropertyCatalog_ID { get; set; }


        [LogicName("标准盘数")]
        public Money<decimal>? StandPlateNumber { get; set; }

        [LogicName("是否半成品")]
        [DbColumn(DefaultValue = true)]
        public bool IsSemiGoods { get; set; }

        [LogicName("生产班组")]
        [DFDataKind(B3ButcheryDataSource.生产班组)]
        [DFExtProperty(B3FrameworksConsts.DFExtProperties.DisplayField, "ProductShift_Name")]
        [DFExtProperty(B3FrameworksConsts.DFExtProperties.QueryDataKind, B3ButcheryDataSource.生产班组)]
        public long? ProductShift_ID { get; set; }

        [Join("ProductShift_ID", "ID")]
        [LogicName("生产班组")]
        [ReferenceTo(typeof(ProductShift), "Name")]
        public string ProductShift_Name { get; set; }
    }
}
