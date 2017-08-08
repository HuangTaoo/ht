using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Forks.JsonRpc.Client;
using Forks.JsonRpc.Client.Data;

namespace B3ButcheryCE.Util_
{
    public static class UserProfileSetUtil
    {
        public static DomainUserProfileUtil GetDomainUserProfileUtil()
        {
            try
            {
                var domainUserProfileUtilRpc = RpcFacade.Call<RpcObject>("/MainSystem/B3_JiuLian/Rpcs/ButcherTouchScreenRpc/DomainUserProfileUtilRpc/GetDomainUserProfileUtil");
                var domainUserProfileUtil = new DomainUserProfileUtil
                {
                    Store_ID = domainUserProfileUtilRpc.Get<long?>("Store_ID"),
                    Store_Name = domainUserProfileUtilRpc.Get<string>("Store_Name"),
                    InStoreType_ID = domainUserProfileUtilRpc.Get<long?>("InStoreType_ID"),
                    InStoreType_Name = domainUserProfileUtilRpc.Get<string>("InStoreType_Name"),
                    FrozenStore_ID = domainUserProfileUtilRpc.Get<long?>("FrozenStore_ID"),
                    FrozenStore_Name = domainUserProfileUtilRpc.Get<string>("FrozenStore_Name"),
                    OtherInStoreType_ID = domainUserProfileUtilRpc.Get<long?>("OtherInStoreType_ID"),
                    OtherInStoreType_Name = domainUserProfileUtilRpc.Get<string>("OtherInStoreType_Name"),
                    OtherOutStoreAccountingUnit_ID = domainUserProfileUtilRpc.Get<long?>("OtherOutStoreAccountingUnit_ID"),
                    OtherOutStoreAccountingUnit_Name = domainUserProfileUtilRpc.Get<string>("OtherOutStoreAccountingUnit_Name"),
                    OtherOutStoreStore_ID = domainUserProfileUtilRpc.Get<long?>("OtherOutStoreStore_ID"),
                    OtherOutStoreStore_Name = domainUserProfileUtilRpc.Get<string>("OtherOutStoreStore_Name")
                };
                return domainUserProfileUtil;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }

    public class DomainUserProfileUtil
    {
        //默认仓库ID
        public long? Store_ID { get; set; }

        //默认仓库名称
        public string Store_Name { get; set; }

        //默认入库类型ID
        public long? InStoreType_ID { get; set; }

        //默认入库类型Name
        public string InStoreType_Name { get; set; }

        //默认速冻库ID
        public long? FrozenStore_ID { get; set; }

        //默认速冻库Name
        public string FrozenStore_Name { get; set; }

        //默认速冻入库类型ID
        public long? OtherInStoreType_ID { get; set; }

        //默认速冻入库类型Name
        public string OtherInStoreType_Name { get; set; }

        //默认成品入库会计单位ID
        public long? AccountingUnit_ID { get; set; }

        //默认成品入库会计单位Name
        public string AccountingUnit_Name { get; set; }

        //默认其他出库会计单位
        public long? OtherOutStoreAccountingUnit_ID { get; set; }

        //默认其他出库会计单位
        public string OtherOutStoreAccountingUnit_Name { get; set; }

        //默认其他出库仓库
        public long? OtherOutStoreStore_ID { get; set; }

        //默认其他出库仓库
        public string OtherOutStoreStore_Name { get; set; }
    }
}
