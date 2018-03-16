using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSingSoft.WebPluginFramework;
using BWP.B3Butchery.Web;
using BWP.B3System;
using BWP.B3Butchery.Hippo;
using BWP.Web.WebControls;

namespace BWP.B3Butchery
{

  public class PluginClass : IPluginClass
  {
    public void OnInit()
    {
      ChoiceBoxProvider.Register();
      SubSystem.RegisterSubSystem(new SubSystem("B3Butchery", "屠宰分割")
      {
        IconUrl = "~/Images/ChildSystemIcons/B3Butchery.png",
        NotSelectedIconUrl = "~/Images/ChildSystemIcons/B3Butchery_NotSelected.png",
        DisplayOrder = -500
      });
			HippoUtil.Register();
      
      MultiViewSwitcher.Register("存货编辑", "屠宰分割", "~/B3Butchery/BaseInfos/ButcheryGoods_/ButcheryGoodsEdit.aspx", new string[]{
        "B3Sale.存货.访问"
      });
      MultiViewSwitcher.Register("存货新建", "屠宰分割", "~/B3Butchery/BaseInfos/ButcheryGoods_/ButcheryGoodsEdit.aspx", new string[]{
        "B3Sale.存货.新建"
      });



    }
  }
}
