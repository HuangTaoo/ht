using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSingSoft.WebControls2;

namespace BWP.Web.Pages.TypeIocs
{
  public class ProductNoticeEditIoc
  {
    public interface BuildDetail 
    {
       void Invoke(DFEditGrid grid);
    }
  }
}
