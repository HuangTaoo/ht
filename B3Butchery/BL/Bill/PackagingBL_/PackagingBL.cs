
using BWP.B3Butchery.BO;
using BWP.B3Frameworks.BL;
using Forks.EnterpriseServices;
using Forks.EnterpriseServices.BusinessInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BWP.B3Butchery.BL
{
  [BusinessInterface(typeof(PackagingBL))]
  [LogicName("包装物配置单")]
  public interface IPackagingBL : IDomainBillBL<Packaging>
  {

  }

  public class PackagingBL : DomainBillBL<Packaging>, IPackagingBL
  {
    protected override void beforeSave(Packaging dmo)
    {
      base.beforeSave(dmo);
      if (string.IsNullOrWhiteSpace(dmo.Name))
      {
        throw new Exception("名称不能为空");
      }
    }
  }
}
