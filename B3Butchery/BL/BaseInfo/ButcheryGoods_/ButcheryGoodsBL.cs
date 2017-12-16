using BWP.B3Butchery.BO;
using BWP.B3Frameworks.BL;
using BWP.B3UnitedInfos;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.BusinessInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSingSoft.WebPluginFramework;



namespace BWP.B3Butchery.BL
{


    [BusinessInterface(typeof(ButcheryGoodsBL))]
    [LogicName("存货")]
    public interface IButcheryGoodsBL : IBaseInfoBL<ButcheryGoods>
    {

    }

    public class ButcheryGoodsBL : BaseInfoBL<ButcheryGoods>, IButcheryGoodsBL
    {

        protected override bool AllowSameName
        {
            get
            {
                return GlobalFlags.get(B3UnitedInfosConsts.GlobalFlags.存货名称允许重复)
                  || base.AllowSameName;
            }
        }

        protected override void beforeSave(ButcheryGoods dmo)
        {
            base.beforeSave(dmo);

          
        }

      
    }
}
