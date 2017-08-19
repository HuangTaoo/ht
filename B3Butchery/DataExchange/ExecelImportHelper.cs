using System;
using BWP.B3Frameworks;
using BWP.B3Frameworks.Utils;
using BWP.B3UnitedInfos.DataExchange;
using DocumentFormat.OpenXml.Spreadsheet;
using Forks.EnterpriseServices.DataForm;
using Forks.EnterpriseServices.DomainObjects2;
using Forks.EnterpriseServices.DomainObjects2.DQuery;
using Forks.EnterpriseServices.SqlDoms;
using TSingSoft.WebPluginFramework;

namespace BWP.B3Butchery.DataExchange {
  class ExecelImportHelper {
   public static bool TryGetID<TDmo>(ExecelImportColumn col ,Row row, string fieldsName, ref long dmoID) {
      string name = default(string);
      if (!col.TryGetValue(row, ref name)) {
        return false;
      }

      var logicName = DFInfo.Get(typeof(TDmo)).LogicName;

      if (dmoID > 0) {
        var verifyNameQuery = new DQueryDom(new JoinAlias(typeof(TDmo)));
        verifyNameQuery.Columns.Add(DQSelectColumn.Field(fieldsName));
        verifyNameQuery.Where.Conditions.Add(DQCondition.EQ("ID", dmoID));
        verifyNameQuery.Range = SelectRange.Top(1);

        var verifyName = (string)verifyNameQuery.EExecuteScalar<string>();

        if (verifyName != name) {
          throw new Exception(string.Format("{0}字段{4}:{1}和已得到ID:{2}上的名称:{3}不一致", logicName, name, dmoID, verifyName,fieldsName));
        }
        return false;
      }

      var query = new DQueryDom(new JoinAlias(typeof(TDmo)));
      if (TypeUtil.IsWithinDomain<TDmo>()) {
        query.Where.Conditions.Add(DQCondition.EQ("Domain_ID", DomainContext.Current.ID));
      }
      query.Where.Conditions.Add(DQCondition.EQ(fieldsName, name));
      query.Range = SelectRange.Top(2);
      query.Columns.Add(DQSelectColumn.Field("ID"));
      var result = query.EExecuteList<long>();

      if (result.Count == 0) {
        throw new Exception(string.Format("未能发现字段{2}为{0}的{1}", name, logicName,fieldsName));
      }
       
      dmoID = result[0];
      return true;
    }
  }
}
