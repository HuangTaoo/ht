using BWP.WinFormBase;
using Forks.JsonRpc.Client;
using Forks.JsonRpc.Client.Data;

namespace B3HuaDu_TouchScreen.Config
{
  public  class AppFactory
  {
    public static AppConfig AppConfig {
      get
      {
        var config= XmlUtil.DeserializeFromFile<AppConfig>();
        return config;
      }
    }

    private static AppContext _appContext;
    public static AppContext AppContext {
      get
      {
        if (_appContext == null)
        {
          _appContext = SetAppContext();
        }
        return _appContext;
      }
    }

    private  static  AppContext SetAppContext()
    {
      var context=new AppContext();
      var config = AppConfig;
      context.ServerUrl = config.ServerUrl;
      context.AccountingUnit_ID = config.AccountUnit_ID;
      context.AccountingUnit_Name = RpcFacade.Call<string>("/MainSystem/B3Butchery/Rpcs/BaseInfoRpc/GetAccountUnitNameById", context.AccountingUnit_ID);
      var userObj = RpcFacade.Call<RpcObject>("/MainSystem/MainSystem/Rpcs/UserRpc/GetCurrentUser");
      context.User_ID =userObj.Get<long>("ID");
      context.User_Name = userObj.Get<string>("Name"); 
      var depObj= RpcFacade.Call<RpcObject>("/MainSystem/B3Butchery/Rpcs/BaseInfoRpc/GetDepartmentBaseInfoDto");
      context.Department_ID = depObj.Get<long?>("ID");
      context.Department_Name = depObj.Get<string>("Name");
      context.Department_Depth = depObj.Get<int?>("Department_Depth");
      return context;
    }
  }
}
