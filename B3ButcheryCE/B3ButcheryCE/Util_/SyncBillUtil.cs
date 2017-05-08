﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using B3ButcheryCE.Rpc_.ClientProduceOutput_;
using B3HRCE;
using System.Xml.Serialization;
using Forks.JsonRpc.Client.Data;
using Forks.JsonRpc.Client;
using BWP.Compact;

namespace B3ButcheryCE
{
    public class SyncBillUtil
    {
        public static void SyncProductOut()
        {
            try
            {
                #region 产出单
                string productOutFoder = Path.Combine(Util.DataFolder, typeof(ClientProduceOutputBillSave).Name);
                ClientProduceOutputBillSave productOut;
                if (!Directory.Exists(productOutFoder))
                {
                    Directory.CreateDirectory(productOutFoder);
                }
                string[] productOutFiles = Directory.GetFiles(productOutFoder, "*.xml");
                if (productOutFiles.Count() != 0 && Util.OnceLogined)
                {
                    foreach (var file in productOutFiles)
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(ClientProduceOutputBillSave));
                        using (var stream = File.Open(file, FileMode.Open))
                        {
                            productOut = serializer.Deserialize(stream) as ClientProduceOutputBillSave;

                            if (productOut.IsSend)
                            {
                                continue;
                            }
                            var mainObj = "/MainSystem/B3Butchery/BO/ProduceOutput";
                            var detailObj = "/MainSystem/B3Butchery/BO/ProduceOutput_Detail";

                            var obj = new RpcObject(mainObj);
                            obj.Set("AccountingUnit_ID", productOut.AccountingUnit_ID);
                            obj.Set("Department_ID", productOut.Department_ID);
                            obj.Set("CreateTime", productOut.CreateTime);
                            obj.Set("Domain_ID", productOut.Domain_ID);
                            obj.Set("CreateUser_ID", productOut.User_ID);
                            //obj.Set("PlanNumber_ID", productPlanGroupBy.Key);
                            //obj.Set("ProductLinks_ID", productLink.ProductLinks_ID);
                            ManyList Details = new ManyList(detailObj);

                            foreach (var detail in productOut.Details)
                            {
                                var objDetail = new RpcObject(detailObj);
                                objDetail.Set("Goods_ID", detail.Goods_ID);
                                objDetail.Set("Number", detail.Goods_Number);
                                Details.Add(objDetail);
                            }
                            obj.Set("Details", Details);

                            RpcFacade.Call<long>("/MainSystem/B3Butchery/Rpcs/ProduceOutputRpc/PdaInsert", obj);
                        }

                        productOut.IsSend = true;
                        using (var stream = File.Create(file))
                        {
                            serializer.Serialize(stream, productOut);
                        }
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex.ToString());
            }
           
        }
    }
}
