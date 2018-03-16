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
    private BoolConfigRef mProductInStoreChooseDate = new BoolConfigRef(false);
    [DomainConfigurationItem]
    [LogicName("成品入库选择存货生产日期")]
    [ConfigurationItemGroup("屠宰分割")]
    [ConfigurationItemDescription("成品入库选择存货{生产日期}取{入库时间}还是当天时间；{描述}=默认否默认为“否”取当天时间")]
    public BoolConfigRef ProductInStoreChooseDate
    {
      get { return mProductInStoreChooseDate; }
      set { mProductInStoreChooseDate = value; }
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

    StringConfigRef mProductInStoreCursorField = new StringConfigRef("Number");
    [LogicName("成品入库单新增存货后光标位置")]
    [ConfigurationItemDescription("默认Number，可选Number（数量）、SecondNumber（辅数量）等字段")]
    [DomainConfigurationItem]
    public StringConfigRef ProductInStoreCursorField
    {
      get { return mProductInStoreCursorField; }
      set { mProductInStoreCursorField = value; }
    }

    StringConfigRef mProduceFinishCursorField = new StringConfigRef("Number");
    [LogicName("生产完工单新增存货后光标位置")]
    [ConfigurationItemDescription("默认Number，可选Number（数量）、SecondNumber（辅数量）等字段")]
    [DomainConfigurationItem]
    public StringConfigRef ProduceFinishCursorField
    {
      get { return mProduceFinishCursorField; }
      set { mProduceFinishCursorField = value; }
    }

    StringConfigRef mProductNoticeCursorField = new StringConfigRef("Number");
    [LogicName("生产通知单新增存货后光标位置")]
    [ConfigurationItemDescription("默认Number，可选Number（数量）、SecondNumber（辅数量）等字段")]
    [DomainConfigurationItem]
    public StringConfigRef ProductNoticeCursorField
    {
      get { return mProductNoticeCursorField; }
      set { mProductNoticeCursorField = value; }
    }
  }
}
