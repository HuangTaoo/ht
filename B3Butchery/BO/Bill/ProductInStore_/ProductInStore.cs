using System;
using System.Collections.Generic;
using BWP.B3Butchery.Utils;
using BWP.B3Frameworks;
using BWP.B3Frameworks.BO;
using BWP.B3ProduceUnitedInfos;
using BWP.B3ProduceUnitedInfos.BO;
using BWP.B3UnitedInfos;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using TSingSoft.WebControls2;
using BWP.B3Frameworks.Attributes;
using Forks.Utils;
using BWP.B3Frameworks.BO.MoneyTemplate;

namespace BWP.B3Butchery.BO
{
	[DFClass, Serializable, LogicName("成品入库")]
	[OrganizationLimitedDmo("Department_ID", typeof(Department))]
	[DmoTypeID(B3FrameworksConsts.DmoTypeIDBases.B3Butchery, DmoTypeIDOffsets.ProductInStore)]
	[UnitedInOutStoreBill]
  [BusinessCaseSlaveBO]
  [BusinessCaseMasterBO]
  [EditUrl("~/B3Butchery/Bills/ProductInStore_/ProductInStoreEdit.aspx")]
  [ListUrl("~/B3Butchery/Bills/ProductInStore_/ProductInStoreList.aspx")]
  [BillTimeField("InStoreDate")]
  public class ProductInStore : DepartmentWorkFlowBill
	{
		[LogicName("仓库")]
		[DFExtProperty("WebControlType", DFEditControl.ChoiceBox)]
		[DFDataKind(B3FrameworksConsts.DataSources.授权仓库)]
		[DFExtProperty(B3FrameworksConsts.DFExtProperties.QueryDataKind, B3FrameworksConsts.DataSources.授权仓库全部)]
		[DFExtProperty("DisplayField", "Store_Name")]
		public long? Store_ID { get; set; }

		[ReferenceTo(typeof(Store), "Name")]
		[Join("Store_ID", "ID")]
		[LogicName("仓库")]
		public string Store_Name { get; set; }

		[LogicName("入库类型")]
		[DFExtProperty("WebControlType", DFEditControl.ChoiceBox)]
		[DFDataKind(B3ButcheryDataSource.屠宰分割入库类型)]
		[DFExtProperty(B3FrameworksConsts.DFExtProperties.QueryDataKind, B3ButcheryDataSource.屠宰分割入库类型全部)]
		[DFExtProperty("DisplayField", "InStoreType_Name")]
		public long? InStoreType_ID { get; set; }

		[ReferenceTo(typeof(InStoreType), "Name")]
		[Join("InStoreType_ID", "ID")]
		[LogicName("入库类型")]
		public string InStoreType_Name { get; set; }

		[LogicName("入库时间")]
    [DFExtProperty("WebControlType", DFEditControl.DateTimeInput)]
    public DateTime? InStoreDate { get; set; }

		[LogicName("验收人")]
		[DFExtProperty("WebControlType", DFEditControl.ChoiceBox)]
		[DFDataKind(B3FrameworksConsts.DataSources.授权员工)]
		[DFExtProperty(B3FrameworksConsts.DFExtProperties.QueryDataKind, B3FrameworksConsts.DataSources.授权员工全部)]
		[DFExtProperty("DisplayField", "CheckEmployee_Name")]
		public long? CheckEmployee_ID { get; set; }

		[ReferenceTo(typeof(Employee), "Name")]
		[Join("CheckEmployee_ID", "ID")]
		[LogicName("验收人")]
		public string CheckEmployee_Name { get; set; }

		[LogicName("验收日期")]
		public DateTime? CheckDate { get; set; }

		[LogicName("是否从手持机传入")]
		[DbColumn(DefaultValue = false)]
		public bool? IsHandsetSend { get; set; }

    [LogicName("生产计划号")]
    public long? ProductPlan_ID { get; set; }

    [ReferenceTo(typeof(ProductPlan), "PlanNumber")]
    [Join("ProductPlan_ID", "ID")]
    [LogicName("生产计划号")]
    public string ProductPlan_Name { get; set; }

        [LogicName("手持机生成ID")]
        public string DeviceId { get; set; }//记录从手持机传入单据的ID，防止传入2次

		[LogicName("成品入库模板")]
		public long? ProductInStoreTemplate_ID { get; set; }

    [LogicName("成品入库模板")]
    [ReferenceTo(typeof(ProductInStoreTemplate), "Name")]
    [Join("ProductInStoreTemplate_ID", "ID")]
    public string ProductInStoreTemplate_Name { get; set; }

    #region 客户预留字段

    [LogicName("生产单位")]
    [DFDataKind(B3ProduceUnitedInfosDataSources.生产单位全部)]
    [DFExtProperty(B3FrameworksConsts.DFExtProperties.DisplayField, "ProductionUnit_Name")]
    public long? ProductionUnit_ID { get; set; }

    [LogicName("生产单位")]
    [ReferenceTo(typeof(ProductionUnit), "Name")]
    [Join("ProductionUnit_ID", "ID")]
    public string ProductionUnit_Name { get; set; }

    #endregion

    [LogicName("金额")]
    public Money<金额>? Money { get; set; }

    [LogicName("大写金额")]
    [NonDmoProperty]
    public string UpperMoney
    {
      get
      {
        return Money.HasValue ? Money.Value.ToString("C") : "";
      }
    }

    [LogicName("客户端生成标识字段")]
    public string Client { get; set; }

		[ReferenceTo(typeof(Department), "TreeDeep1ID")]
		[Join("Department_ID", "ID")]
		public long? Department_TreeDeep1ID { get; set; }

		[ReferenceTo(typeof(Department), "TreeDeep2ID")]
		[Join("Department_ID", "ID")]
		public long? Department_TreeDeep2ID { get; set; }

		[ReferenceTo(typeof(Department), "TreeDeep3ID")]
		[Join("Department_ID", "ID")]
		public long? Department_TreeDeep3ID { get; set; }

		[ReferenceTo(typeof(Department), "TreeDeep4ID")]
		[Join("Department_ID", "ID")]
		public long? Department_TreeDeep4ID { get; set; }

		[ReferenceTo(typeof(Department), "TreeDeep5ID")]
		[Join("Department_ID", "ID")]
		public long? Department_TreeDeep5ID { get; set; }

		[ReferenceTo(typeof(Department), "TreeDeep6ID")]
		[Join("Department_ID", "ID")]
		public long? Department_TreeDeep6ID { get; set; }

		[ReferenceTo(typeof(Department), "TreeDeep7ID")]
		[Join("Department_ID", "ID")]
		public long? Department_TreeDeep7ID { get; set; }

		[ReferenceTo(typeof(Department), "TreeDeep8ID")]
		[Join("Department_ID", "ID")]
		public long? Department_TreeDeep8ID { get; set; }

		private ProductInStore_DetailCollection mDetails = new ProductInStore_DetailCollection();
		[OneToMany(typeof(ProductInStore_Detail), "ID")]
		[Join("ID", "ProductInStore_ID")]
		public ProductInStore_DetailCollection Details
		{
			get { return mDetails; }
			set { mDetails = value; }
		}

    //仙坛用到，明细生产日期必须一致，放到表头为了导U8界面做查询用
    [LogicName("生产日期")]
    public  DateTime? ProductionDate { get; set; }


    [NonDmoProperty]
    public List<string> K3Bills { get; set; }
	}
}
