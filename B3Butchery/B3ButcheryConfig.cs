using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BWP.B3Frameworks.Attributes;
using Forks.EnterpriseServices;
using Forks.Utils.Configuration;
using TSingSoft.WebPluginFramework;

namespace BWP.B3Butchery
{
  [ConfigurationEnabled]
  public class B3ButcheryConfig
  {
    public B3ButcheryConfig()
    {
      ConfigurationUtil.Fill(this);
    }

		private BoolConfigRef mDoCheckCreatedInStore = new BoolConfigRef(false);
		[DomainConfigurationItem]
		[LogicName("生成已审核成品入库")]
		[ConfigurationItemGroup("屠宰分割")]
		[ConfigurationItemDescription("默认为“否”，选择为“是”时，手持机直接生成“已审核”【成品入库】")]
		public BoolConfigRef DoCheckCreatedInStore
		{
			get { return mDoCheckCreatedInStore; }
			set { mDoCheckCreatedInStore = value; }
		}
  }
}
