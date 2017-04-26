using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Threading;
using BWP.Compact;
using B3HRCE.FileGroupValuation_;
using Forks.JsonRpc.Client;
using Forks.JsonRpc.Client.Data;
using B3HRCE.Rpc_;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using B3HRCE.Rpc_.ClientPersonalPiece_;
using B3HRCE.PersonalPiece_;
using B3HRCE.ProductInStore_;
using B3HRCE.Rpc_.ClientProductInStore_;
using B3HRCE.Rpc_.ClientProductLink_;
using B3HRCE.ProductLink_;
using B3HRCE.OutputStatistics_;
using B3ButcheryCE.Rpc_.ClientProduceOutput_;
using B3ButcheryCE;

namespace B3HRCE
{
    public partial class MainForm : Form
    {
        private FormProcessBar myProcessBar = null;
        private delegate bool IncreaseHandle(int nValue);
        private IncreaseHandle myIncrease = null;

        private void ShowProcessBar()
        {
            myProcessBar = new FormProcessBar();
            myIncrease = new IncreaseHandle(myProcessBar.Increase);
            myProcessBar.ShowDialog();
        }

        public MainForm()
        {
            InitializeComponent();
            Util.SetSceen(this);
            InitButtons();

            if (Util.OnceLogined)
            {
                //查询用户区域和用户ID
                var domainUser = RpcFacade.Call<RpcObject>("/MainSystem/B3Frameworks/Rpcs/DomainRpc/GetCurrentDoaminUser");
                mConfig = SysConfig.Current;
                mConfig.Domain_ID = domainUser.Get<long>("Domain_ID");
                mConfig.User_ID = domainUser.Get<long>("User_ID");
                mConfig.Save();
            }

            statusBar1.Text = Util.UserStatus;

            DeleteYesTerDay<ClientProductInStoreBillSave>();
            DeleteYesTerDay<ClientProductLinkBillSave>();
            DeleteYesTerDay<ClientProduceOutputBillSave>();


            sendBillThread = new Thread(new ThreadStart(SendBillSync));
            sendBillThread.Start();
        }

        private void InitButtons()
        {
            var usageMode = SysConfig.Current.UsageModes.Value;
            buttonFileGroupValuation.Enabled = (usageMode & UsageMode.案组计件新增) > 0;
            button1.Enabled = (usageMode & UsageMode.个人计件新增) > 0;
            btn_ProductInStore.Enabled = (usageMode & UsageMode.成品入库新增) > 0;
            productLink_Btn.Enabled = (usageMode & UsageMode.生产环节新增) > 0;
        }
        //todo 

        /// <summary>
        /// 删除昨天单据数据
        /// </summary>
        private void DeleteYesTerDay<T>() where T : ClientBase
        {
            string clientBillfoder = Path.Combine(Util.DataFolder, typeof(T).Name);
            if (!Directory.Exists(clientBillfoder))
                return;
            string[] clientBillfiles = Directory.GetFiles(clientBillfoder, "*.xml");

            if (clientBillfiles.Count() != 0 && Util.OnceLogined)
            {
                foreach (var file in clientBillfiles)
                {
                    var serializer = new XmlSerializer(typeof(T));
                    using (var stream = File.Open(file, FileMode.Open))
                    {
                        var clientBill = serializer.Deserialize(stream) as T;
                        if (!clientBill.IsSend)
                            continue;
                        stream.Dispose();
                    }
                    var fileName = Path.GetFileNameWithoutExtension(file);
                    string num = Regex.Replace(fileName, @"[^\d.\d]", "");//只获取数字部分
                    int billDate = int.Parse(num.Substring(0, 8));//截取年月日
                    int nowDate = int.Parse(DateTime.Now.ToString("yyyyMMdd"));

                    if (nowDate > billDate)
                    {
                        File.Delete(file);
                    }
                }
            }
        }

        private void buttonFileGroupValuation_Click(object sender, EventArgs e)
        {
            new SelectFileGroupValuationTemplateDialog().ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new SelectPersonalPieceTemplateDialog().ShowDialog();
        }

        private void buttonSyncBaseInfo_Click(object sender, EventArgs e)
        {
            if (!Util.OnceLogined)
            {
                MessageBox.Show("您还没有登录到系统，不能同步基础信息");
                return;
            }

            syncBaseInfoThread = new Thread(new ThreadStart(BaseInfosSync));
            syncBaseInfoThread.IsBackground = true;
            syncBaseInfoThread.Start();
            ShowProcessBar();
        }

        Thread syncBaseInfoThread;
        Thread sendBillThread;
        SysConfig mConfig;

        /// <summary>
        /// 同步基础信息
        /// </summary>
        private void BaseInfosSync()
        {
            try
            {
                var usageMode = SysConfig.Current.UsageModes.Value;
                //if ((usageMode & UsageMode.案组计件新增) > 0)
                //{
                //    SyncBaseInfoUtil.SyncFileGroupValuationTemplate();
                //    this.Invoke(this.myIncrease, new object[] { 20 });
                //}
                //if ((usageMode & UsageMode.个人计件新增) > 0)
                //{
                //    SyncBaseInfoUtil.SyncPersonalPieceTemplate();
                //    this.Invoke(this.myIncrease, new object[] { 20 });
                //}
                //if ((usageMode & UsageMode.成品入库新增) > 0)
                //{
                //    SyncBaseInfoUtil.SyncProductInStoreTemplate();
                //    Invoke(this.myIncrease, new object[] { 20 });
                //}
                //if ((usageMode & UsageMode.生产环节新增) > 0)
                //{
                //    SyncBaseInfoUtil.SyncProductLinkTemplate();
                //    Invoke(this.myIncrease, new object[] { 20 });
                //}
                //if ((usageMode & UsageMode.成品入库新增) > 0 && (usageMode & UsageMode.生产环节新增) > 0)
                //{
                //    SyncBaseInfoUtil.SyncProductPlanTemplate();
                //    Invoke(this.myIncrease, new object[] { 20 });
                //}


                //SyncBaseInfoUtil.SyncProductInStoreTemplate

                SyncBaseInfoUtil.SyncStore();
                Invoke(this.myIncrease, new object[] { 50 });

                SyncBaseInfoUtil.SyncGoodsByDepartPlan();
                Invoke(this.myIncrease, new object[] { 50 });

                Invoke(this.myIncrease, new object[] { 100 });

                MessageBox.Show("数据同步完成");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private void SendBillSync()
        {
            try
            {
                while (true)
                {
                    doBillSync();
                    Thread.Sleep(60000);
                }
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex.ToString());
            }
        }

        private static void doBillSync()
        {
            #region 案组计件
            //案组计件  
            //string foder = Path.Combine(Util.DataFolder, typeof(ClientFileGroupValuationBillSave).Name);

            //if (!Directory.Exists(foder))
            //{
            //    Directory.CreateDirectory(foder);
            //}

            //string[] files = Directory.GetFiles(foder, "*.xml");

            //if (files.Count() != 0 && Util.OnceLogined)
            //{
            //    foreach (var file in files)
            //    {
            //        XmlSerializer fileGroupValuationSer = new XmlSerializer(typeof(ClientFileGroupValuationBillSave));
            //        using (var stream = File.Open(file, FileMode.Open))
            //        {
            //            var fileGroupValuation = fileGroupValuationSer.Deserialize(stream) as ClientFileGroupValuationBillSave;
            //            var obj = new RpcObject("/MainSystem/B3HR/BO/FileGroupValuation");
            //            obj.Set("AccountingUnit_ID", fileGroupValuation.AccountingUnit_ID);
            //            obj.Set("Department_ID", fileGroupValuation.Department_ID);
            //            obj.Set("FileGroup_ID", fileGroupValuation.FileGroup_ID);
            //            obj.Set("PieceItem_ID", fileGroupValuation.PieceItem_ID);
            //            obj.Set("Number", fileGroupValuation.Number);
            //            obj.Set("Domain_ID", fileGroupValuation.Domain_ID);
            //            obj.Set("IsHandsetSend", fileGroupValuation.IsHandsetSend);
            //            obj.Set("CreateUser_ID", fileGroupValuation.User_ID);
            //            var pieceItemInsertResult = RpcFacade.Call<RpcObject>("/MainSystem/B3HR/Rpcs/FileGroupValuationRpc/InsertFileGroupValuation", obj);
            //        }
            //        File.Delete(file);
            //    }
            //}
            #endregion

            #region 个人计件
            //个人计件
            //string personalPiecefoder = Path.Combine(Util.DataFolder, typeof(ClientPersonalPieceBillSave).Name);

            //if (!Directory.Exists(personalPiecefoder))
            //{
            //    Directory.CreateDirectory(personalPiecefoder);
            //}

            //string[] personalPiecefiles = Directory.GetFiles(personalPiecefoder, "*.xml");

            //if (personalPiecefiles.Count() != 0 && Util.OnceLogined)
            //{
            //    foreach (var file in personalPiecefiles)
            //    {

            //        XmlSerializer serializer = new XmlSerializer(typeof(ClientPersonalPieceBillSave));
            //        using (var stream = File.Open(file, FileMode.Open))
            //        {
            //            var personalPiece = serializer.Deserialize(stream) as ClientPersonalPieceBillSave;
            //            var obj = new RpcObject("/MainSystem/B3HR/BO/PersonalPiece");
            //            obj.Set("AccountingUnit_ID", personalPiece.AccountingUnit_ID);
            //            obj.Set("Department_ID", personalPiece.Department_ID);
            //            obj.Set("CreateTime", personalPiece.CreateTime);
            //            obj.Set("Domain_ID", personalPiece.Domain_ID);
            //            obj.Set("CreateUser_ID", personalPiece.User_ID);

            //            ManyList Details = new ManyList("/MainSystem/B3HR/BO/PersonalPiece_Detail");
            //            foreach (var detail in personalPiece.Details)
            //            {
            //                var objDetail = new RpcObject("/MainSystem/B3HR/BO/PersonalPiece_Detail");
            //                objDetail.Set("HREmployee_ID", detail.Employee_ID);
            //                objDetail.Set("Job_ID", detail.Job_ID);
            //                objDetail.Set("PieceItem_ID", detail.PieceItem_ID);
            //                objDetail.Set("Number", detail.Number);
            //                Details.Add(objDetail);
            //            }
            //            obj.Set("Details", Details);

            //            var pieceItemInsertResult = RpcFacade.Call<RpcObject>("/MainSystem/B3HR/Rpcs/PersonalPieceRpc/InsertPersonalPiece", obj);
            //        }
            //        File.Delete(file);
            //    }
            //}

            #endregion

            #region 成品入库

            //string productInStorefoder = Path.Combine(Util.DataFolder, typeof(ClientProductInStoreBillSave).Name);
            //ClientProductInStoreBillSave productInStore;
            //if (!Directory.Exists(productInStorefoder))
            //{
            //    Directory.CreateDirectory(productInStorefoder);
            //}

            //string[] productInStorefiles = Directory.GetFiles(productInStorefoder, "*.xml");

            //if (productInStorefiles.Count() != 0 && Util.OnceLogined)
            //{
            //    foreach (var file in productInStorefiles)
            //    {

            //        XmlSerializer serializer = new XmlSerializer(typeof(ClientProductInStoreBillSave));
            //        using (var stream = File.Open(file, FileMode.Open))
            //        {
            //            productInStore = serializer.Deserialize(stream) as ClientProductInStoreBillSave;
            //            if (productInStore.IsSend)
            //                continue;
            //            foreach (var storeGroupBy in productInStore.Details.GroupBy(x => x.Store_ID))
            //            {
            //                var obj = new RpcObject("/MainSystem/B3Butchery/BO/ProductInStore");
            //                obj.Set("AccountingUnit_ID", productInStore.AccountingUnit_ID);
            //                obj.Set("Department_ID", productInStore.Department_ID);
            //                obj.Set("InStoreDate", productInStore.CreateTime);
            //                obj.Set("Domain_ID", productInStore.Domain_ID);
            //                obj.Set("CreateUser_ID", productInStore.User_ID);
            //                obj.Set("InStoreType_ID", productInStore.InStoreType_ID);
            //                obj.Set("Store_ID", storeGroupBy.Key);
            //                obj.Set("DeviceId", productInStore.DeviceId);
            //                ManyList Details = new ManyList("/MainSystem/B3Butchery/BO/ProductInStore_Detail");
            //                foreach (var detail in storeGroupBy)
            //                {
            //                    var objDetail = new RpcObject("/MainSystem/B3Butchery/BO/ProductInStore_Detail");
            //                    objDetail.Set("ProductPlan_ID", detail.ProductPlanID);
            //                    objDetail.Set("Goods_ID", detail.Goods_ID);
            //                    objDetail.Set("Number", detail.MainNumber);
            //                    objDetail.Set("SecondNumber", detail.SecondNumber);
            //                    objDetail.Set("ProductionDate", DateTime.Today);
            //                    Details.Add(objDetail);
            //                }
            //                obj.Set("Details", Details);

            //                var pieceItemInsertResult = RpcFacade.Call<RpcObject>("/MainSystem/B3Butchery/Rpcs/ProductInStoreRpc/InsertProductInStore", obj);
            //            }
            //        }
            //        productInStore.IsSend = true;
            //        using (var stream = File.Create(file))
            //        {
            //            serializer.Serialize(stream,productInStore);
            //        }
            //    }
            //}
            #endregion

            #region 产出单
            SyncBillUtil.SyncProductOut();
            #endregion

            #region 生产环节

            //string productLinkFoder = Path.Combine(Util.DataFolder, typeof(ClientProductLinkBillSave).Name);
            //ClientProductLinkBillSave productLink;
            //if (!Directory.Exists(productLinkFoder))
            //{
            //    Directory.CreateDirectory(productLinkFoder);
            //}

            //string[] productLinkFiles = Directory.GetFiles(productLinkFoder, "*.xml");
            //if (productLinkFiles.Count() != 0 && Util.OnceLogined)
            //{
            //    foreach (var file in productLinkFiles)
            //    {
            //        XmlSerializer serializer = new XmlSerializer(typeof(ClientProductLinkBillSave));
            //        using (var stream = File.Open(file, FileMode.Open))
            //        {
            //            productLink = serializer.Deserialize(stream) as ClientProductLinkBillSave;
            //            if (productLink.IsSend)
            //                continue;
            //            var objType = "";
            //            switch (productLink.CollectType)
            //            {
            //                case "投入":
            //                    objType = "ProduceInput";
            //                    break;
            //                case "产出":
            //                    objType = "ProduceOutput";
            //                    break;
            //                default:
            //                    break;
            //            }
            //            var mainObj = "/MainSystem/B3Butchery/BO/" + objType;
            //            var detailObj = string.Format("/MainSystem/B3Butchery/BO/{0}_Detail", objType);
            //            var saveRpc = "/MainSystem/B3Butchery/Rpcs/ProductLinkRpc/Insert" + objType;
            //            foreach (var productPlanGroupBy in productLink.Details.GroupBy(x => x.ProductPlanID))
            //            {
            //                var obj = new RpcObject(mainObj);
            //                obj.Set("AccountingUnit_ID", productLink.AccountingUnit_ID);
            //                obj.Set("Department_ID", productLink.Department_ID);
            //                obj.Set("CreateTime", productLink.CreateTime);
            //                obj.Set("Domain_ID", productLink.Domain_ID);
            //                obj.Set("CreateUser_ID", productLink.User_ID);
            //                obj.Set("PlanNumber_ID", productPlanGroupBy.Key);
            //                obj.Set("ProductLinks_ID", productLink.ProductLinks_ID);
            //                ManyList Details = new ManyList(detailObj);
            //                foreach (var detail in productPlanGroupBy)
            //                {
            //                    var objDetail = new RpcObject(detailObj);
            //                    objDetail.Set("Goods_ID", detail.Goods_ID);
            //                    objDetail.Set("Number", detail.MainNumber);
            //                    objDetail.Set("SecondNumber", detail.SecondNumber);
            //                    Details.Add(objDetail);
            //                }
            //                obj.Set("Details", Details);
            //                RpcFacade.Call<RpcObject>(saveRpc, obj);
            //            }
            //        }
            //        productLink.IsSend = true;
            //        using (var stream = File.Create(file))
            //        {
            //            serializer.Serialize(stream, productLink);
            //        }
            //    }
            //}
            #endregion
        }

        private void btn_ProductInStore_Click(object sender, EventArgs e)
        {
            new SelectProductInStoreTemplateDialog().ShowDialog();
        }

        private void productLink_Btn_Click(object sender, EventArgs e)
        {
            new SelectProductLinkTemplateDialog().ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new OutputStatisticsForm().ShowDialog();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}