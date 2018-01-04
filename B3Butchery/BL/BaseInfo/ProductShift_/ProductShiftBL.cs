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



    [BusinessInterface(typeof(ProductShiftBL))]
    [LogicName("生产班组")]
    public interface IProductShiftBL : IDomainBaseInfoBL<ProductShift>
    { }

    public class ProductShiftBL : DomainBaseInfoBL<ProductShift>, IProductShiftBL
    {
    }
}
