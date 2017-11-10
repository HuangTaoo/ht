using System.Linq;
using BWP.B3Butchery.BO;
using BWP.B3Frameworks;
using BWP.B3Frameworks.BL;
using BWP.B3Frameworks.BO.MoneyTemplate;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.BusinessInterfaces;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using Forks.EnterpriseServices.SqlDoms;
using Forks.Utils;
using TSingSoft.WebPluginFramework;

namespace BWP.B3Butchery.BL {
  [BusinessInterface(typeof(ProduceFinishBL))]
  [LogicName("生产完工单")]
  public interface IProduceFinishBL : IDepartmentWorkFlowBillBL<ProduceFinish> {

  }

  public class ProduceFinishBL : DepartmentWorkFlowBillBL<ProduceFinish>, IProduceFinishBL {

    protected override void beforeSave(ProduceFinish dmo) {
      dmo.AllNum = dmo.Details.Sum(x => (x.Number ?? 0).Value);
      dmo.AllSecondNumber = dmo.Details.Sum(x => (x.SecondNumber ?? 0).Value);
      base.beforeSave(dmo);
    }

    protected override void doCheck(ProduceFinish dmo) {
      base.doCheck(dmo);
      UpdateDoneNumber(dmo);
    }

    protected override void doUnCheck(ProduceFinish dmo) {
      base.doUnCheck(dmo);
      UpdateDoneNumber(dmo);
    }

    private void UpdateDoneNumber(ProduceFinish dmo) {
      var ids = dmo.Details.Select(x => x.ProductNotice_Detail_ID).ToList();
      var detail = new JoinAlias(typeof(ProduceFinish_Detail));
      var bill = new JoinAlias(typeof(ProduceFinish));
      var dom = new DQueryDom(detail);
      dom.From.AddJoin(JoinType.Inner, new DQDmoSource(bill), DQCondition.EQ(bill, "ID", detail, "ProduceFinish_ID"));
      dom.EAddCheckedCondition(bill);
      dom.Where.Conditions.EFieldInList(DQExpression.Field(detail, "ProductNotice_Detail_ID"), ids);

      dom.Columns.Add(DQSelectColumn.Sum(detail, "Number"));
      dom.Columns.Add(DQSelectColumn.Field("ProductNotice_Detail_ID", detail));
      dom.GroupBy.Expressions.Add(DQExpression.Field(detail, "ProductNotice_Detail_ID"));

      var tupleList = dom.EExecuteList<Money<decimal>?, long>(Session);
      foreach (var id in ids) {
        var number = tupleList.Where(x => x.Item2 == id).Sum(x => ((x.Item1 ?? 0).Value));

        var update = new DQUpdateDom(typeof(ProductNotice_Detail));
        update.Columns.Add(new DQUpdateColumn("DoneNumber", number));
        update.Where.Conditions.Add(DQCondition.EQ("ID", id));
        Session.ExecuteNonQuery(update);
      }

    }
  }
}
