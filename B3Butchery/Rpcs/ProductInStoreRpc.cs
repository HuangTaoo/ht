using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using BWP.B3Butchery.Rpcs.RpcObject;
using BWP.B3Frameworks;
using BWP.B3Frameworks.BO.NamedValueTemplate;
using Forks.EnterpriseServices.BusinessInterfaces;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using Forks.EnterpriseServices.JsonRpc;
using Forks.Utils;
using TSingSoft.WebPluginFramework;
using Forks.EnterpriseServices.SqlDoms;
using BWP.B3Frameworks.Utils;

namespace BWP.B3Butchery.Rpcs
{
	[Rpc]
	public static class ProductInStoreRpc
	{

    [Rpc]
    public static ProductInStore Load(long id)
    {
      var bl = BIFactory.Create<IProductInStoreBL>();
      var dmo = bl.Load(id);
      return dmo;
    }

	  [Rpc]
    public static List<ProductInStoreSimpleDto> GetSimpleList(int pageIndex,int pageSize)
    {
      var list = new List<ProductInStoreSimpleDto>();
      var query=new DQueryDom(new JoinAlias(typeof(ProductInStore)));
      query.Where.Conditions.Add(DQCondition.EQ("BillState",单据状态.未审核));
      query.Where.Conditions.Add(DQCondition.EQ("Domain_ID",DomainContext.Current.ID));
      query.OrderBy.Expressions.Add(DQOrderByExpression.Create("ID",true));
      

      query.Columns.Add(DQSelectColumn.Field("ID"));
      query.Columns.Add(DQSelectColumn.Field("InStoreDate"));
      query.Columns.Add(DQSelectColumn.Field("Store_Name"));

      query.Range=new SelectRange(pageSize*pageIndex,pageSize);
	    using (var session=Dmo.NewSession())
	    {
	      using (var reader=session.ExecuteReader(query))
	      {
	        while (reader.Read())
	        {
	          var dto=new ProductInStoreSimpleDto();
	          dto.ID = (long) reader[0];
	          dto.InStoreDate = (DateTime) reader[1];
	          dto.Store_Name = (string) reader[2];
            list.Add(dto);
	        }
	      }
	    }
      return list;

    }

	  //只返回第一条明细
	  [Rpc]
	  public static List<ProductInStoreWithDetailDto> GetListOnlyOneDetail(int pageIndex,int pageSize)
      {
	    var list =new List<ProductInStoreWithDetailDto>();
      var dmoQuery=new DmoQuery(typeof(ProductInStore));
      dmoQuery.Range=new SelectRange(pageSize * pageIndex, pageSize);
      dmoQuery.OrderBy.Expressions.Add(DQOrderByExpression.Create("ID",true));
      dmoQuery.Where.Conditions.Add(DQCondition.EQ("Domain_ID",DomainContext.Current.ID));
	    var dmoList=dmoQuery.EExecuteList().Cast<ProductInStore>();
	    foreach (ProductInStore inStore in dmoList)
	    {
	      var dto = new ProductInStoreWithDetailDto();
	      dto.ID = inStore.ID;
	      dto.Date = inStore.InStoreDate;
	      var detail = inStore.Details.FirstOrDefault();
	      if (detail != null)
	      {
          dto.Goods_Name = detail.Goods_Name;
          dto.Number = detail.Number;
          dto.SecondNumber = detail.SecondNumber;
          list.Add(dto);
        }
	    }
	    return list;
	  }

	  [Rpc]
    public static long AppInsert(ProductInStore dmo)
	  {
	    var bl = BIFactory.Create<IProductInStoreBL>();
	    foreach (ProductInStore_Detail detail in dmo.Details)
	    {
        DmoUtil.RefreshDependency(detail,"Goods_ID");
	      if (detail.Number == null && detail.SecondNumber.HasValue)
	      {
	        detail.Number = detail.SecondNumber * detail.Goods_MainUnitRatio / detail.Goods_SecondUnitRatio;
	      }
	    }
      bl.InitNewDmo(dmo);
      bl.Insert(dmo);
	    return dmo.ID;
	  }

	  /// <summary>
    /// 审核成品入库单
    /// </summary>
    /// <param name="id"></param>
    [Rpc]
    public static void ProductInStoreCheck(long id) {
      var bl = BIFactory.Create<IProductInStoreBL>();
      var bo = bl.Load(id); 
      bl.Check(bo);
    }
    /// <summary>
    /// 获取成品入库单单号和入库时间
    /// </summary>
    /// <returns></returns>
    [Rpc]
    public static List<RpcEasyProductInStore> GetProductInStoreList() {

      var query = new DQueryDom(new JoinAlias(typeof(ProductInStore)));
      query.Columns.Add(DQSelectColumn.Field("ID"));
      query.Columns.Add(DQSelectColumn.Field("InStoreDate"));
      query.Where.Conditions.Add(DQCondition.EQ("BillState", 单据状态.未审核));
      OrganizationUtil.AddOrganizationLimit(query, typeof(ProductInStore));
      query.Where.Conditions.Add(DQCondition.IsNotNull(DQExpression.Field("InStoreDate")));
      try
      {
       return  query.EExecuteList<long, DateTime>().Select(x => new RpcEasyProductInStore(x.Item1, x.Item2)).ToList();
      }
      catch (Exception)
      {
        return new List<RpcEasyProductInStore>(); 
      }
      
    }
    /// <summary>
    /// 根据成品入库单号获取存货名称和数量
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Rpc]
    public static List<RpcEasyProductInStore_Detail> GetRpcEasyProductInStoreDetailById(long id)
    {
      var ris = new JoinAlias(typeof(ProductInStore));
      var risDetail = new JoinAlias(typeof(ProductInStore_Detail));
      var query = new DQueryDom(ris);
      query.From.AddJoin(JoinType.Left, new DQDmoSource(risDetail), DQCondition.EQ(ris, "ID", risDetail, "ProductInStore_ID"));
      query.Columns.Add(DQSelectColumn.Field("Goods_Name", risDetail));
      query.Columns.Add(DQSelectColumn.Field("Number", risDetail));
      query.Columns.Add(DQSelectColumn.Field("SecondNumber", risDetail));
      query.Where.Conditions.Add(DQCondition.EQ("ID", id));
      try
      {
        return query.EExecuteList<string, object, Money<decimal>?>().Select(x => new RpcEasyProductInStore_Detail(x.Item1, x.Item2, x.Item3)).ToList();
      }
      catch (Exception)
      {
        return new List<RpcEasyProductInStore_Detail>();
      }
    }

		[Rpc]
		public static IList<EntityRowVersion> GetRowVersion(long? accountingUnit)
		{
			var query = new DQueryDom(new JoinAlias(typeof(ProductInStoreTemplate)));
			query.Where.Conditions.Add(DQCondition.EQ("AccountingUnit_ID", accountingUnit));
			query.Columns.Add(DQSelectColumn.Field("ID"));
			query.Columns.Add(DQSelectColumn.Field("RowVersion"));
			query.OrderBy.Expressions.Add(DQOrderByExpression.Create("ID"));
			query.Where.Conditions.Add(DQCondition.EQ("Stopped", false));
			return query.EExecuteList<long, int>().Select(x => new EntityRowVersion(x.Item1, x.Item2)).ToList();
		}

		[Rpc]
		public static IList<ProductInStoreTemplate> GetProductInStoreTemplate(long[] id)
		{
			if (id.Length == 0)
				return new List<ProductInStoreTemplate>();
			var query = new DmoQuery(typeof(ProductInStoreTemplate));
			query.OrderBy.Expressions.Add(DQOrderByExpression.Create("ID"));
			query.Where.Conditions.Add(DQCondition.InList(DQExpression.Field("ID"), id.Select(x => DQExpression.Value(x)).ToArray()));
			return query.EExecuteList().Cast<ProductInStoreTemplate>().ToList();
		}

		[Rpc]
		public static DFDataTable SelectInStoreType(long domainID)
		{
			var query = new DQueryDom(new JoinAlias(typeof(InStoreType)));
			query.Columns.Add(DQSelectColumn.Field("ID"));
			query.Columns.Add(DQSelectColumn.Field("Name"));
			query.Where.Conditions.Add(DQCondition.EQ("Domain_ID", domainID));
			return new DFDataAdapter(new LoadArguments(query)).Fill();
		}

		[Rpc]
		public static void InsertProductInStore(ProductInStore dmo)
		{
            var query = new DQueryDom(new JoinAlias(typeof(ProductInStore)));
            query.Columns.Add(DQSelectColumn.Field("DeviceId"));
            query.Where.Conditions.Add(DQCondition.EQ("InStoreDate", dmo.InStoreDate));
            var deviceIds= query.EExecuteList<string>();
		    if (deviceIds.Contains(dmo.DeviceId))
                return;
			using (var context = new TransactionContext())
			{
				foreach (var d in dmo.Details)
				{
					d.Price = 0;
					d.Money = 0;
					d.ProductionDate = GetProductPlanDate(d.ProductPlan_ID);
				}
				var bl = BIFactory.Create<IProductInStoreBL>(context);
				dmo.InStoreDate = BLContext.Today;
				//dmo.BillState = 单据状态.已审核;
				dmo.IsHandsetSend = true;
				bl.Insert(dmo);
				if (new B3ButcheryConfig().DoCheckCreatedInStore.Value)
					bl.Check(dmo);
				context.Commit();
			}
		}

		static DateTime? GetProductPlanDate(long? productPlanID)
		{
			if (productPlanID == null)
				return null;
			var query = new DQueryDom(new JoinAlias(typeof(ProductPlan)));
			query.Columns.Add(DQSelectColumn.Field("Date"));
			query.Where.Conditions.Add(DQCondition.EQ("ID", productPlanID));
			return query.EExecuteScalar<DateTime?>();
		}
	}

	[RpcObject]
	public class EntityRowVersion
	{
		public long ID { private set; get; }

		public int RowVersion { private set; get; }

		public EntityRowVersion()
		{ }

		public EntityRowVersion(long id, int rowVersion)
		{
			ID = id;
			RowVersion = rowVersion;
		}
	}
   /// <summary>
  /// 成品入库存货单单号与入库时间，只要字段类型一致可用这个类传值
  /// </summary>
  [RpcObject]
  public class RpcEasyProductInStore
  {
    public long ID { get; set; }
    public DateTime InStoreDate { get; set; }

    public RpcEasyProductInStore()
    {
    }
    public RpcEasyProductInStore(long id, DateTime inStoreDate) {
      ID = id;
      InStoreDate = inStoreDate;
    }
  }
  /// <summary>
  /// 成品入库存货名称与数量，只要字段类型一致可用这个类传值
  /// </summary>
  [RpcObject]
  public class RpcEasyProductInStore_Detail
  {
    public string Goods_Name { get; set; }
    public object Number { get; set; }
    public Money<decimal>? SecondNumber { get; set; }

    public RpcEasyProductInStore_Detail(){}
    public RpcEasyProductInStore_Detail(string name, object number, Money<decimal>? secondNumber)
    {
      Goods_Name = name;
      Number=number;
      SecondNumber = secondNumber;
    }
  }
}
