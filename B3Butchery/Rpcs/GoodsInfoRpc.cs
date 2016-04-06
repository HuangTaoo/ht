using System.Linq;
using BWP.B3Frameworks.BO.NamedValueTemplate;
using BWP.B3UnitedInfos.BO;
using BWP.Web.Utils;
using Forks.EnterpriseServices.JsonRpc;
using Forks.Utils;

namespace BWP.B3Butchery.Rpcs
{
  [Rpc]
  public static class GoodsInfoRpc {
    [Rpc]
    public static GoodsInfo Get(long? id ) {
 
      if(id==null)
        return null;
      var goodsInfo = new GoodsInfo();

      var goods = WebBLUtil.GetSingleDmo<Goods>("ID", id, "SecondUnit", "UnitConvertDirection", "MainUnit", "MainUnitRatio", "SecondUnitRatio");
      goodsInfo.Goods_SecondUnit = goods.SecondUnit;
      goodsInfo.Goods_MainUnit = goods.MainUnit;
      goodsInfo.Goods_UnitConvertDirection = goods.UnitConvertDirection;
      goodsInfo.Goods_MainUnitRatio = goods.MainUnitRatio;
      goodsInfo.Goods_SecondUnitRatio = goods.SecondUnitRatio;
      return goodsInfo;
    }
 
  }
}