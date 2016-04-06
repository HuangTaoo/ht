using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forks.EnterpriseServices.BusinessInterfaces;
using TSingSoft.WebPluginFramework.Install;

namespace BWP.B3Butchery.DataPatch
{
	[DataPatch]
	public class ConvertDateTimeToDate : IDataPatch
	{
		public void Execute(TransactionContext context)
		{
			var sql1 = @"update B3Butchery_ProduceInput set [Time] = convert(date,[Time])";
			var sql2 = @"update B3Butchery_ProduceOutput set [Time] = convert(date,[Time])";
			context.Session.ExecuteSqlNonQuery(sql1);
			context.Session.ExecuteSqlNonQuery(sql2);
		}
	}
}
