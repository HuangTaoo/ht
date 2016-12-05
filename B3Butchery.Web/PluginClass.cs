using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TSingSoft.WebPluginFramework;
using BWP.B3Butchery.Web;
using BWP.B3System;
using BWP.B3Butchery.Hippo;

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
    }
  }
}
