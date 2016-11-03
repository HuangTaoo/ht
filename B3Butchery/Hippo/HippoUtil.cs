using BWP.B3ProduceUnitedInfos;
using Bwp.Hippo;
using BWP.B3Butchery.Utils;
using BWP.B3Frameworks;
using BWP.B3Frameworks.BO;
using BWP.B3Frameworks.BO.NamedValueTemplate;
using BWP.B3UnitedInfos;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using TSingSoft.WebPluginFramework;

namespace BWP.B3Butchery.Hippo
{
	public class HippoUtil
	{
		private const string CUSASK = "/MainSystem/B3Butchery/Hippo";
		public static void Register()
		{
			RegisterProduceInputList();
			RegisterProduceInputForm();
			RegisterProduceOutputList();
			RegisterProduceOutputForm();
			RegisterProductInStoreList();
			RegisterProductInStoreForm();
      RegisterProductNoticeForm();
		  RegisterProductNoticeList();
		}

		static void RegisterProduceInputList()
		{
			var model = new ListModel
			{
				Title = "投入单",
				ItemFormat =
            "单号: [ID]\n单据状态: [BillState]\n时间:[Time]\n计划号: [PlanNumber_Name]\n会计单位: [AccountingUnit_Name]\n部门: [Department_Name]\n经办人: [Employee_Name]\n生产环节: [ProductLinks_Name]\n投入类型: [InputType]",
				OpeningView = CUSASK + "/ProduceInputAction/ProduceInputEdit",
				OpeningViewType = "form",
				QueryFields = new[]
                                                   {
																										 new FieldDesc{ Name="ID",Title="单号"},
																										 new FieldDesc{Name="BillState",Title="单据状态"},
																										 new FieldDesc{Name="MinTime",Title="最小时间"},
																										 new FieldDesc{Name="MaxTime",Title="最大时间"},
																										  new FieldDesc{Name="PlanNumber_ID",Title="计划号",DataKind=B3ButcheryDataSource.计划号,DisplayField="PlanNumber_Name"},
																										 new FieldDesc{Name="AccountingUnit_ID",Title="会计单位",DataKind=B3FrameworksConsts.DataSources.授权会计单位全部,DisplayField="AccountingUnit_Name"},
																										 new FieldDesc{Name="Department_ID",Title="部门",DataKind=B3FrameworksConsts.DataSources.授权部门全部,DisplayField="Department_Name"},
																										 new FieldDesc{Name="Employee_ID",Title="经办人",DataKind=B3FrameworksConsts.DataSources.授权员工全部,DisplayField="Employee_Name"},
																										 new FieldDesc{Name="ProductLinks_ID",Title="生产环节",DataKind=B3ButcheryDataSource.生产环节全部,DisplayField="ProductLinks_Name"},
																										 new FieldDesc{Name="InputType",Title="投入类型"}

																									 },
				QueryRpc = CUSASK + "/Actions_/ProduceInputAction/Query",
				QueryObject = CUSASK + "/QueryObjs/ProduceInputQueryObj",
				ActionBar = "New Option"
			};
			ListModel.Register(CUSASK + "/ProduceInputList", model);
		}

		static void RegisterProduceInputForm()
		{
			var model = new FormModel
			{
				Title = "投入单",
				MainObject = "/MainSystem/B3Butchery/BO/ProduceInput",
				DisplayFields = new[] {
					new FieldDesc{Name="Time", Title = "时间"},
					new FieldDesc{Name="PlanNumber_ID", Title = "计划号", DataKind = B3ButcheryDataSource.计划号,DisplayField = "PlanNumber_Name"},
					new FieldDesc{Name="AccountingUnit_ID", Title = "会计单位", DataKind = B3FrameworksConsts.DataSources.授权会计单位,DisplayField = "AccountingUnit_Name"},
					new FieldDesc{Name="Department_ID",Title="部门",DataKind=B3FrameworksConsts.DataSources.授权部门,DisplayField="Department_Name"},
					new FieldDesc{Name="Employee_ID",Title="经办人",DataKind=B3FrameworksConsts.DataSources.授权员工,DisplayField="Employee_Name"},
					new FieldDesc{Name="ProductLinks_ID",Title="生产环节",DataKind=B3ButcheryDataSource.生产环节,DisplayField="ProductLinks_Name"},
					new FieldDesc{Name="InputType",Title="投入类型"}
							 },
				ActionRpc = CUSASK + "/Actions_/ProduceInputAction/FormActions",
				ActionBar = "Save SaveAndNew Prev Next"
			};
			//var loadDetail = new ActionDesc { Flags = "", Name = "LoadDetail", Title = "" };
			//model.Actions.Add(loadDetail);
			var detail = new CollectionModel
			{/*{存货名称}、{存货编码}、{规格}、{主数量}、{辅数量}和{备注}*/
				Title = "明细清单",
				PropertyName = "Details",
				ItemType = "/MainSystem/B3Butchery/BO/ProduceInput_Detail",
				DisplayFields = new[] {
					new FieldDesc {Name = "Goods_ID", Title = "存货名称", DisplayField = "Goods_Name", DataKind=B3UnitedInfosConsts.DataSources.存货},
					new FieldDesc {Name = "Goods_Code", Title = "存货编码"},
					new FieldDesc {Name = "Goods_Spec", Title = "规格"},
					new FieldDesc {Name = "Number", Title = "主数量"},
					new FieldDesc {Name = "SecondNumber", Title = "辅数量"},
					new FieldDesc {Name = "Remark", Title = "备注"}                 
								},
				ItemFormat = "存货名称: [Goods_Name]\n存货编码: [Goods_Code]\n规格: [Goods_Spec]\n主数量: [Number]\n辅数量: [SecondNumber]\n备注: [Remark]",
				ActionBar = "New"
			};
			model.Collections.Add(detail);

			FormModel.Register(CUSASK + "/ProduceInputAction/ProduceInputEdit", model);
		}

		static void RegisterProduceOutputList()
		{
			var model = new ListModel
			{
				Title = "产出单",
				ItemFormat =
						"单号: [ID]\n单据状态: [BillState]\n时间:[Time]\n计划号: [PlanNumber_Name]\n会计单位: [AccountingUnit_Name]\n部门: [Department_Name]\n经办人: [Employee_Name]\n生产环节: [ProductLinks_Name]",
				OpeningView = CUSASK + "/ProduceOutputAction/ProduceOutputEdit",
				OpeningViewType = "form",
				QueryFields = new[]
                                                   {
																										 new FieldDesc{ Name="ID",Title="单号"},
																										 new FieldDesc{Name="BillState",Title="单据状态"},
																										 new FieldDesc{Name="MinTime",Title="最小时间"},
																										 new FieldDesc{Name="MaxTime",Title="最大时间"},
																										  new FieldDesc{Name="PlanNumber_ID",Title="计划号",DataKind=B3ButcheryDataSource.计划号,DisplayField="PlanNumber_Name"},
																										 new FieldDesc{Name="AccountingUnit_ID",Title="会计单位",DataKind=B3FrameworksConsts.DataSources.授权会计单位全部,DisplayField="AccountingUnit_Name"},
																										 new FieldDesc{Name="Department_ID",Title="部门",DataKind=B3FrameworksConsts.DataSources.授权部门全部,DisplayField="Department_Name"},
																										 new FieldDesc{Name="Employee_ID",Title="经办人",DataKind=B3FrameworksConsts.DataSources.授权员工全部,DisplayField="Employee_Name"},
																										 new FieldDesc{Name="ProductLinks_ID",Title="生产环节",DataKind=B3ButcheryDataSource.生产环节全部,DisplayField="ProductLinks_Name"},

																									 },
				QueryRpc = CUSASK + "/Actions_/ProduceOutputAction/Query",
				QueryObject = CUSASK + "/QueryObjs/ProduceOutputQueryObj",
				ActionBar = "New Option"
			};
			ListModel.Register(CUSASK + "/ProduceOutputList", model);
		}

		static void RegisterProduceOutputForm()
		{
			var model = new FormModel
			{
				Title = "产出单",
				MainObject = "/MainSystem/B3Butchery/BO/ProduceOutput",
				DisplayFields = new[] {
					new FieldDesc{Name="Time", Title = "时间"},
					new FieldDesc{Name="PlanNumber_ID", Title = "计划号", DataKind = B3ButcheryDataSource.计划号,DisplayField = "PlanNumber_Name"},
					new FieldDesc{Name="AccountingUnit_ID", Title = "会计单位", DataKind = B3FrameworksConsts.DataSources.授权会计单位,DisplayField = "AccountingUnit_Name"},
					new FieldDesc{Name="Department_ID",Title="部门",DataKind=B3FrameworksConsts.DataSources.授权部门,DisplayField="Department_Name"},
					new FieldDesc{Name="Employee_ID",Title="经办人",DataKind=B3FrameworksConsts.DataSources.授权员工,DisplayField="Employee_Name"},
						new FieldDesc{Name="ProductLinks_ID",Title="生产环节",DataKind=B3ButcheryDataSource.生产环节,DisplayField="ProductLinks_Name"},
							new FieldDesc{Name="ProductLinkTemplate_ID",Title="生产环节模板",DataKind=B3ButcheryDataSource.生产环节模板,DisplayField="ProductLinkTemplate_ID"},
							 },
				ActionRpc = CUSASK + "/Actions_/ProduceOutputAction/FormActions",
				ActionBar = "Save SaveAndNew Prev Next LoadDetail ReferToCreate"
			};
			var loadDetail = new ActionDesc { Flags = FormModel.ActionFlags.Save, Name = "LoadDetail", Title = "加载明细" };
			var referToCreate = new ActionDesc { Flags = "", Name = "ReferToCreate", Title = "参照新建" };
			model.Actions.Add(loadDetail);
			model.Actions.Add(referToCreate);
			var detail = new CollectionModel
			{/*{存货名称}、{存货编码}、{规格}、{主数量}、{辅数量}和{备注}*/
				Title = "明细清单",
				PropertyName = "Details",
				ItemType = "/MainSystem/B3Butchery/BO/ProduceOutput_Detail",
				DisplayFields = new[] {
					new FieldDesc {Name = "Goods_ID", Title = "存货名称", DisplayField = "Goods_Name", DataKind=B3UnitedInfosConsts.DataSources.存货},
					new FieldDesc {Name = "Goods_Code", Title = "存货编码"},
					new FieldDesc {Name = "Goods_Spec", Title = "规格"},
					new FieldDesc {Name = "Number", Title = "主数量"},
					new FieldDesc {Name = "SecondNumber", Title = "辅数量"},
					new FieldDesc {Name = "Remark", Title = "备注"}                 
								},
				ItemFormat = "存货名称: [Goods_Name]\n存货编码: [Goods_Code]\n规格: [Goods_Spec]\n主数量: [Number]\n辅数量: [SecondNumber]\n备注: [Remark]",
				ActionBar = "New"
			};
			model.Collections.Add(detail);

			FormModel.Register(CUSASK + "/ProduceOutputAction/ProduceOutputEdit", model);
		}

		static void RegisterProductInStoreList()
		{
			var model = new ListModel
			{
				Title = "成品入库",
				ItemFormat =
						"单号: [ID]\n单据状态: [BillState]\n会计单位: [AccountingUnit_Name]\n部门: [Department_Name]\n经办人: [Employee_Name]\n仓库: [Store_Name]\n入库类型 [InStoreType_Name]\n入库日期 [InStoreDate]\n验收人 [CheckEmployee_Name]\n验收日期 [CheckDate]",
				OpeningView = CUSASK + "/ProductInStoreAction/ProductInStoreEdit",
				OpeningViewType = "form",
				QueryFields = new[]
                                                   {
																										 new FieldDesc{ Name="ID",Title="单号"},
																										 new FieldDesc{Name="BillState",Title="单据状态"},
																										 new FieldDesc{Name="AccountingUnit_ID",Title="会计单位",DataKind=B3FrameworksConsts.DataSources.授权会计单位全部,DisplayField="AccountingUnit_Name"},
																										 new FieldDesc{Name="Department_ID",Title="部门",DataKind=B3FrameworksConsts.DataSources.授权部门全部,DisplayField="Department_Name"},
																										 new FieldDesc{Name="Employee_ID",Title="经办人",DataKind=B3FrameworksConsts.DataSources.授权员工全部,DisplayField="Employee_Name"},
																										 new FieldDesc{Name="Store_ID",Title="仓库",DataKind=B3FrameworksConsts.DataSources.授权仓库全部,DisplayField="Store_Name"},
																										  new FieldDesc{Name="InStoreType_ID",Title="入库类型",DataKind=B3ButcheryDataSource.屠宰分割入库类型全部,DisplayField="InStoreType_Name"},
																										 new FieldDesc{Name="MinInStoreDate",Title="最小入库日期"},
																										 new FieldDesc{Name="MaxInStoreDate",Title="最大入库日期"},

																									 },
				QueryRpc = CUSASK + "/Actions_/ProductInStoreAction/Query",
				QueryObject = CUSASK + "/QueryObjs/ProductInStoreQueryObj",
				ActionBar = "New Option"
			};
			ListModel.Register(CUSASK + "/ProductInStoreList", model);
		}

		static void RegisterProductInStoreForm()
		{
			var model = new FormModel
			{
				Title = "成品入库",
				MainObject = "/MainSystem/B3Butchery/BO/ProductInStore",
				DisplayFields = new[] {
					new FieldDesc{Name="AccountingUnit_ID", Title = "会计单位", DataKind = B3FrameworksConsts.DataSources.授权会计单位,DisplayField = "AccountingUnit_Name"},
					new FieldDesc{Name="Department_ID",Title="部门",DataKind=B3FrameworksConsts.DataSources.授权部门,DisplayField="Department_Name"},
					new FieldDesc{Name="Employee_ID",Title="经办人",DataKind=B3FrameworksConsts.DataSources.授权员工,DisplayField="Employee_Name"},
					new FieldDesc{Name="Store_ID", Title = "仓库", DataKind = B3FrameworksConsts.DataSources.授权仓库,DisplayField = "Store_Name"},
						new FieldDesc{Name="InStoreType_ID", Title = "入库类型", DataKind = B3ButcheryDataSource.屠宰分割入库类型,DisplayField = "InStoreType_Name"},
							new FieldDesc{Name="InStoreDate", Title = "入库日期",Widget="datetime"},
						new FieldDesc{Name="CheckEmployee_ID",Title="验收人",DataKind=B3FrameworksConsts.DataSources.授权员工,DisplayField="CheckEmployee_Name"},
					new FieldDesc{Name="CheckDate", Title = "验收日期"},
						new FieldDesc{Name="ProductInStoreTemplate_ID", Title = "成品入库模板",DisplayField="ProductInStoreTemplate_ID", DataKind = B3ButcheryDataSource.成品入库模板},

							 },
				ActionRpc = CUSASK + "/Actions_/ProductInStoreAction/FormActions",
				ActionBar = "Save SaveAndNew Prev Next LoadDetail ReferToCreate"
			};
			var loadDetail = new ActionDesc { Flags = FormModel.ActionFlags.Save, Name = "LoadDetail", Title = "加载明细" };
			var referToCreate = new ActionDesc { Flags = "", Name = "ReferToCreate", Title = "参照新建" };
			model.Actions.Add(loadDetail);
			model.Actions.Add(referToCreate);
			var detail = new CollectionModel
			{/*：{生产日期}、{生产计划号}、{存货名称}、{存货编码}、{规格}、{主数量}、{辅数量}和{备注}；*/
				Title = "明细清单",
				PropertyName = "Details",
				ItemType = "/MainSystem/B3Butchery/BO/ProductInStore_Detail",
				DisplayFields = new[] {
					new FieldDesc {Name = "ProductionDate", Title = "生产日期"},
					new FieldDesc {Name = "ProductPlan_ID", Title = "生产计划号", DisplayField = "ProductPlan_Name", DataKind=B3ButcheryDataSource.计划号},
					new FieldDesc {Name = "Goods_ID", Title = "存货名称", DisplayField = "Goods_Name", DataKind=B3UnitedInfosConsts.DataSources.存货},
					new FieldDesc {Name = "Goods_Code", Title = "存货编码"},
					new FieldDesc {Name = "Goods_Spec", Title = "规格"},
					new FieldDesc {Name = "Number", Title = "主数量"},
					new FieldDesc {Name = "SecondNumber", Title = "辅数量"},
					new FieldDesc {Name = "Remark", Title = "备注"}                      
								},
				ItemFormat = "存货名称: [Goods_Name]\n存货编码: [Goods_Code]\n规格: [Goods_Spec]\n主数量: [Number]\n辅数量: [SecondNumber]\n备注: [Remark]",
				ActionBar = "New"
			};
			model.Collections.Add(detail);

			FormModel.Register(CUSASK + "/ProductInStoreAction/ProductInStoreEdit", model);
		}

        static void RegisterProductNoticeList()
        {
            var model = new ListModel
            {
                Title = "生产通知单",
                ItemFormat =
                        "单号: [ID] 单据状态: [BillState]\n会计单位: [AccountingUnit_Name]\n部门: [Department_Name]\n生产单位: [ProductionUnit_Name]\n客户 [Customer_Name]\n日期 [Date]\n业务员: [Employee_Name]",
                OpeningView = CUSASK + "/ProductNoticeAction/ProductNoticeEdit",
                OpeningViewType = "form",
                QueryFields = new[]
                                                   {
																										 new FieldDesc{ Name="ID",Title="单号"},
																										 new FieldDesc{Name="BillState",Title="单据状态"},
																										 new FieldDesc{Name="AccountingUnit_ID",Title="会计单位",DataKind=B3FrameworksConsts.DataSources.授权会计单位全部,DisplayField="AccountingUnit_Name"},
																										 new FieldDesc{Name="Department_ID",Title="部门",DataKind=B3FrameworksConsts.DataSources.授权部门全部,DisplayField="Department_Name"},
																										 new FieldDesc{Name="Employee_ID",Title="业务员",DataKind=B3FrameworksConsts.DataSources.授权员工全部,DisplayField="Employee_Name"},
																										 new FieldDesc{Name="ProductionUnit_ID",Title="生产单位",DataKind=B3ProduceUnitedInfosDataSources.生产单位全部,DisplayField="ProductionUnit_Name"},
																										  new FieldDesc{Name="Customer_ID",Title="客户",DataKind="B3Sale_客户",DisplayField="Customer_Name"},
																										 new FieldDesc{Name="MinTime",Title="最小日期"},
																										 new FieldDesc{Name="MaxTime",Title="最大日期"},
																									 },
                QueryRpc = CUSASK + "/Actions_/ProductNoticeAction/Query",
                QueryObject = CUSASK + "/QueryObjs/ProductNoticeQueryObj",
                ActionBar = "New Option"
            };
            ListModel.Register(CUSASK + "/ProductNoticeList", model);
        }

        static void RegisterProductNoticeForm()
        {
            var model = new FormModel
            {
                Title = "生产通知单",
                MainObject = "/MainSystem/B3Butchery/BO/ProductNotice",
                DisplayFields = new[] {
					new FieldDesc{Name="AccountingUnit_ID", Title = "会计单位", DataKind = B3FrameworksConsts.DataSources.授权会计单位,DisplayField = "AccountingUnit_Name"},
					new FieldDesc{Name="Department_ID",Title="部门",DataKind=B3FrameworksConsts.DataSources.授权部门,DisplayField="Department_Name"},
					new FieldDesc{Name="Employee_ID",Title="业务员",DataKind=B3FrameworksConsts.DataSources.授权员工,DisplayField="Employee_Name"},
					new FieldDesc{Name="ProductionUnit_ID", Title = "生产单位", DataKind = B3ProduceUnitedInfosDataSources.生产单位全部,DisplayField = "ProductionUnit_Name"},
						new FieldDesc{Name="Customer_ID", Title = "客户", DataKind = "B3Sale_客户",DisplayField = "Customer_Name"},
							new FieldDesc{Name="CustomerAddress", Title = "地址"},
					new FieldDesc{Name="Date", Title = "日期"},
							 },
                ActionRpc = CUSASK + "/Actions_/ProductNoticeAction/FormActions",
                ActionBar = "Save Prev Next LoadDetail Predict ReferToCreate"
            };
            var loadDetail = new ActionDesc { Flags = FormModel.ActionFlags.Save, Name = "LoadDetail", Title = "加载明细" };
            var referToCreate = new ActionDesc { Flags = "", Name = "ReferToCreate", Title = "参照新建" };
            var predict = new ActionDesc { Flags = "", Name = "Predict", Title = "载入预报" };
            model.Actions.Add(loadDetail);
            model.Actions.Add(referToCreate);
            model.Actions.Add(predict);
            var detail = new CollectionModel
            {
                Title = "单据明细",
                PropertyName = "Details",
                ItemType = "/MainSystem/B3Butchery/BO/ProductNotice_Detail",
                DisplayFields = new[] {
                    new FieldDesc{Name="DmoID",Title = "来源单据号"},
                    new FieldDesc {Name = "Goods_ID", Title = "存货名称", DisplayField = "Goods_Name", DataKind=B3UnitedInfosConsts.DataSources.存货},
                    new FieldDesc {Name = "Goods_Code", Title = "存货编码"},
                    new FieldDesc {Name = "Goods_Spec", Title = "规格"},
                    new FieldDesc {Name = "Number", Title = "主数量"},
                    new FieldDesc {Name = "SecondNumber", Title = "辅数量"},
                    new FieldDesc {Name = "Remark", Title = "备注"}                      
                                },
                ItemFormat = "来源单据号:[DmoID]\n存货名称: [Goods_Name]\n存货编码: [Goods_Code] 规格: [Goods_Spec]\n主数量: [Number]\n辅数量: [SecondNumber]\n备注: [Remark]",
                ActionBar = "New"
            };
            model.Collections.Add(detail);
            FormModel.Register(CUSASK + "/ProductNoticeAction/ProductNoticeEdit", model);
        }


		public static void AddEQ<T>(DQueryDom query, string field, T? val, JoinAlias alias = null) where T : struct
		{
			if (val.HasValue)
			{
				if (alias != null)
					query.Where.Conditions.Add(DQCondition.EQ(alias, field, val.Value));
				else
					query.Where.Conditions.Add(DQCondition.EQ(field, val.Value));
			}
		}

		public static T ReferenceToCreate<T>(T current) where T : DepartmentWorkFlowBill, new()
		{
			T dmo = current.Clone() as T;
			#region BaseFields

			if (dmo.IsLocked)
			{
				dmo.IsLocked = false;
				dmo.LockReason = string.Empty;
			}
			dmo.ID = 0;
			dmo.PrintDegree = 0;
			dmo.CreateTime = null;
			dmo.RowVersion = 0;

			#endregion

			#region BillFields

			dmo.BillState = 单据状态.未审核;
			dmo.CreateUser_ID = null;
			dmo.CreateUser_Name = string.Empty;
			dmo.ModifyUser_ID = null;
			dmo.ModifyUser_Name = string.Empty;
			dmo.CreateTime = dmo.ModifyTime = BLContext.Now;
			dmo.CheckUser_ID = null;
			dmo.CheckUser_Name = string.Empty;
			dmo.CheckTime = null;
			dmo.FinishTime = null;
			dmo.FinishUser_ID = null;
			dmo.FinishUser_Name = string.Empty;
			dmo.NullifyTime = null;
			dmo.NullifyUser_ID = null;
			dmo.NullifyUser_Name = string.Empty;

			#endregion

			#region DepartmentWorkFlowFields

			dmo.DepartmentWorkFlow_ID = null;
			dmo.DepartmentWorkFlow_Detail_Name = string.Empty;
			dmo.DepartmentWorkFlow_Detail_ID = 0;
			dmo.MarkHandSelectWorkFlow = false;
			dmo.LastWorkFlowUser_ID = null;
			dmo.LastWorkFlowUser_Name = string.Empty;
			dmo.LastWorkFlowTime = null;
			dmo.WorkFlowFinishTime = null;

			#endregion

			return dmo;
		}
	}
}
