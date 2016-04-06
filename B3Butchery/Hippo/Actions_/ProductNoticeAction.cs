using System;
using System.Collections.Generic;
using System.Linq;
using BWP.B3Butchery.BL;
using BWP.B3Butchery.BO;
using BWP.B3Butchery.Hippo.QueryObjs;
using BWP.B3Frameworks;
using BWP.B3Frameworks.BO.MoneyTemplate;
using BWP.B3Frameworks.BO.NamedValueTemplate;
using BWP.B3Frameworks.Utils;
using Bwp.Hippo;
using Forks.EnterpriseServices.BusinessInterfaces;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using Forks.EnterpriseServices.JsonRpc;
using Forks.EnterpriseServices.SqlDoms;
using Forks.Utils;
using TSingSoft.WebPluginFramework;

namespace BWP.B3Butchery.Hippo.Actions_
{
    [Rpc]
    public static class ProductNoticeAction
    {
        [Rpc]
        public static ListData Query(ListData data)
        {
            var queryobj = (ProductNoticeQueryObj)data.QueryObject;
            var query = new DQueryDom(new JoinAlias(typeof(ProductNotice)));
            query.Columns.Add(DQSelectColumn.Field("ID"));
            query.Columns.Add(DQSelectColumn.Field("BillState"));
            query.Columns.Add(DQSelectColumn.Field("Date"));
            query.Columns.Add(DQSelectColumn.Field("Customer_Name"));
            query.Columns.Add(DQSelectColumn.Field("AccountingUnit_Name"));
            query.Columns.Add(DQSelectColumn.Field("Department_Name"));
            query.Columns.Add(DQSelectColumn.Field("Employee_Name"));
            query.Columns.Add(DQSelectColumn.Field("ProductionUnit_Name"));
            query.OrderBy.Expressions.Add(DQOrderByExpression.Create("ID", true));
            HippoUtil.AddEQ(query, "ID", queryobj.ID);
            HippoUtil.AddEQ(query, "BillState", queryobj.BillState);
            HippoUtil.AddEQ(query, "AccountingUnit_ID", queryobj.AccountingUnit_ID);
            HippoUtil.AddEQ(query, "Department_ID", queryobj.Department_ID);
            HippoUtil.AddEQ(query, "Employee_ID", queryobj.Employee_ID);
            HippoUtil.AddEQ(query, "ProductionUnit_ID", queryobj.ProductionUnit_ID);
            if (queryobj.MinTime.HasValue)
                query.Where.Conditions.Add(DQCondition.GreaterThanOrEqual("Date", queryobj.MinTime.Value));
            if (queryobj.MaxTime.HasValue)
                query.Where.Conditions.Add(DQCondition.LessThanOrEqual("Date", queryobj.MaxTime.Value));
            query.Where.Conditions.Add(DQCondition.EQ("Domain_ID", DomainContext.Current.ID));
            query.Range = new SelectRange(data.Start, data.Count);
            var pagedData = new DFDataAdapter(new LoadArguments(query)).PagedFill();
            data.Start = 0;
            data.Count = (int)pagedData.TotalCount;
            data.Data = pagedData.Data;
            return data;
        }

        [Rpc]
        public static FormData FormActions(string action, FormData data)
        {
            var productInput = (ProductNotice)data.MainObject;
            var bl = BIFactory.Create<IProductNoticeBL>();

            switch (action)
            {
                case FormActionNames.Load:
                    var dom = bl.Load(productInput.ID);
                    data.MainObject = dom;
                    break;
                case FormActionNames.Save:
                    BeforeSave(productInput);
                    if (productInput.ID == 0)
                    {
                        bl.InitNewDmo(productInput);
                        UpdateDetail(productInput);
                        bl.Insert(productInput);
                        data.MainObject = productInput;
                        return FormActions(FormActionNames.Load, data);
                    }
                    UpdateDetail(productInput);
                    bl.Update(productInput);
                    return FormActions(FormActionNames.Load, data);
                case FormActionNames.New:
                    var dmo = new ProductNotice();
                    data.MainObject = dmo;
                    break;
                case FormActionNames.Prev:
                    var prevDmo = GetPrevOrNext(productInput.ID);
                    if (prevDmo == null)
                        throw new IndexOutOfRangeException("Current is first");
                    data.MainObject = prevDmo;
                    break;
                case FormActionNames.Next:
                    var nextDmo = GetPrevOrNext(productInput.ID, false);
                    if (nextDmo == null)
                        throw new IndexOutOfRangeException("Current is last");
                    data.MainObject = nextDmo;
                    break;
                case "LoadDetail":
                    LoadDetail(productInput);
                    break;
                case "ReferToCreate":
                    data.MainObject = HippoUtil.ReferenceToCreate(productInput);
                    break;
                case "Predict":
                    LoadPredictDetail(productInput);
                    UpdateDetail(productInput);
                    break;
                default:
                    throw new ArgumentException("Unknown action: " + action);
            }
            return data;
        }

        static ProductNotice GetPrevOrNext(long currentID, bool prev = true)
        {
            var query = new DmoQuery(typeof(ProductNotice));
            query.Where.Conditions.Add(DQCondition.EQ("Domain_ID", DomainContext.Current.ID));
            query.Where.Conditions.Add(prev ? DQCondition.LessThan("ID", currentID) : DQCondition.GreaterThan("ID", currentID));
            query.OrderBy.Expressions.Add(DQOrderByExpression.Create("ID", prev));
            query.Range = SelectRange.Top(1);
            return query.EExecuteScalar<ProductNotice>();
        }

        private static void BeforeSave(ProductNotice pro)
        {
            var noGoodsDetails = pro.Details.Where(x => x.Goods_ID == 0 || x.Number == null).ToList();
            foreach (var item in noGoodsDetails)
                pro.Details.Remove(item);
        }

        static void LoadDetail(ProductNotice dmo)
        {
            dmo.Details.Clear();
            var query = new DQueryDom(new JoinAlias(typeof(ProductNotice_Detail)));
            query.Columns.Add(DQSelectColumn.Field("Goods_ID"));
            query.Columns.Add(DQSelectColumn.Field("Goods_Name"));
            query.Columns.Add(DQSelectColumn.Field("Goods_Code"));
            query.Where.Conditions.Add(DQCondition.EQ("ProductNotice_ID", dmo.ID));
            var list=query.EExecuteList<long, string, string>().Select(x => new ProductNotice_Detail { ProductNotice_ID = dmo.ID, Goods_ID = x.Item1, Goods_Name = x.Item2, Goods_Code = x.Item3 });
            var removeDetail = dmo.Details.Where(detail => list.All(x => x.Goods_ID != detail.Goods_ID)).ToList();
            foreach (var remove in removeDetail)
                dmo.Details.Remove(remove);
            foreach (var add in list.Where(add => dmo.Details.All(x => x.Goods_ID != add.Goods_ID)))
            {
                dmo.Details.Add(add);
            }
        }

        private static void UpdateDetail(ProductNotice dmo)
        {
            foreach (var detail in dmo.Details)
            {
                DmoUtil.RefreshDependency(detail, "Goods_ID");
            }
        }

        private static void LoadPredictDetail(ProductNotice dmo)
        {
            if (dmo.Customer_ID == null)
                throw new Exception("请先选择客户");
            if (dmo.Date == null)
            {
                throw new ArgumentException("请先选择日期");
            }
            //var selectedList = new List<ProductNotice_Detail>();
            var query = SaleForecastQuery((long)dmo.Customer_ID);
            using (var context = new TransactionContext())
            {
                using (var reader = context.Session.ExecuteReader(query))
                {
                    while (reader.Read())
                    {
                        var selectDmo = new ProductNotice_Detail();
                        selectDmo.DmoID = (long)reader[0];

                        selectDmo.Goods_ID = (long) reader[1];
                        selectDmo.Number = (Money<decimal>?) reader[2];
                        selectDmo.Goods_MainUnit = reader[3].ToString();
                        selectDmo.Price = (Money<decimal>?) reader[4];
                        selectDmo.Money = (Money<金额>?)reader[5];
                        //selectedList.Add(selectDmo);
                        dmo.Details.Add(selectDmo);
                    }
                }
            }
        }

        static DQueryDom SaleForecastQuery(long customer)
        {
            if (!PluginManager.Current.Installed("B3Sale")) return null;
            var detail = new JoinAlias(Type.GetType("BWP.B3Sale.BO.SaleForecast_Detail, B3Sale"));
            var bill = new JoinAlias(Type.GetType("BWP.B3Sale.BO.SaleForecast, B3Sale"));
            var dom = new DQueryDom(bill);
            dom.From.AddJoin(JoinType.Inner, new DQDmoSource(detail), DQCondition.EQ(detail, "SaleForecast_ID", bill, "ID"));
            dom.Columns.Add(DQSelectColumn.Create(DQExpression.Field(bill, "ID"), "Forecase_ID"));
            dom.Columns.Add(DQSelectColumn.Field("SaleGoods_ID", detail));
            dom.Columns.Add(DQSelectColumn.Create(DQExpression.Subtract(GetNumber(detail,"UnitNum"), GetNumber(detail,"AlreadyToOrderUnitNum")).ECastType<Money<decimal>?>(), "Number"));
            dom.Columns.Add(DQSelectColumn.Field("Unit",detail));
            dom.Columns.Add(DQSelectColumn.Field("Price", detail));
            dom.Columns.Add(DQSelectColumn.Field("Money", detail));
            dom.Where.Conditions.Add(DQCondition.EQ(bill, "Customer_ID", customer));
            dom.Where.Conditions.Add(DQCondition.EQ(bill, "BillState", 单据状态.已审核));
            dom.Where.Conditions.Add(DQCondition.GreaterThan(GetNumber(detail, "UnitNum"), GetNumber(detail, "AlreadyToOrderUnitNum")));
            dom.Where.Conditions.Add(DQCondition.EQ(bill, "Domain_ID", DomainContext.Current.ID));
            return dom;
        }

        private static IDQExpression GetNumber(JoinAlias bill,string title)
        {
            return DQExpression.IfNull(DQExpression.Field(bill, title), DQExpression.ConstValue(0));
        }
    }
}
