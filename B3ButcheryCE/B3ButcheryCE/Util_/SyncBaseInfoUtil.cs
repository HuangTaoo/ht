using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Forks.JsonRpc.Client;
using Forks.JsonRpc.Client.Data;
using System.IO;
using B3HRCE.Rpc_;
using System.Xml.Serialization;
using B3HRCE.Rpc_.ClientPersonalPiece_;
using B3HRCE.Rpc_.ClientProductInStore_;
using B3HRCE.Rpc_.ClientProductLink_;
using B3ButcheryCE.Rpc_;
using B3ButcheryCE.Rpc_.BaseInfo_;

namespace B3HRCE
{
    public static class SyncBaseInfoUtil
    {

        public static void SyncStore()
        {
            var folder = Path.Combine(Util.DataFolder, typeof(ClientStore).Name);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            var files = Directory.GetFiles(folder, "*.xml");
            foreach (var file in files)
            {
                File.Delete(file);
            }

            var list = RpcFacade.Call<List<RpcObject>>("/MainSystem/B3Butchery/Rpcs/BaseInfoRpc/SyncStores");

            List<ClientStore> clientList = list.Select(x => (new ClientStore { ID = x.Get<long>("ID"), Name = x.Get<string>("Name"), BarCode =x.Get<string>("Code")})).ToList<ClientStore>();

            XmlSerializer serializer = new XmlSerializer(typeof(List<ClientStore>));
            using (var stream = File.Open(Path.Combine(folder, DateTime.Now.ToString("yyyy-MM-dd") + ".xml"), FileMode.Create))
            {
                serializer.Serialize(stream, clientList);
            }

 
        }

        /// <summary>
        /// 根据部门生产计划同步存货 每次都删除重新创建
        /// </summary>
        public static void SyncGoodsByDepartPlan()
        {
            var folder = Path.Combine(Util.DataFolder, typeof(ClientGoods).Name);
            if (!Directory.Exists(folder))
            { 
                Directory.CreateDirectory(folder);
            }

            var files= Directory.GetFiles(folder, "*.xml");
            foreach (var file in files)
            {
                File.Delete(file);
            }

            var list = RpcFacade.Call<IList<RpcObject>>("/MainSystem/B3Butchery/Rpcs/GoodsInfoRpc/GetByDepartPlan", SysConfig.Current.Department_ID);

            List<ClientGoods> clientList = list.Select(x => (ClientUtil.CreateClientGoods(x))).ToList<ClientGoods>();

            XmlSerializer serializer = new XmlSerializer(typeof(List<ClientGoods>));
            using (var stream = File.Open(Path.Combine(folder, DateTime.Now.ToString("yyyy-MM-dd") + ".xml"), FileMode.Create))
            {
                serializer.Serialize(stream, clientList);
            }


        }



        public static void SyncFileGroupValuationTemplate()
        {
            var rowVersionList = RpcFacade.Call<IList<RpcObject>>("/MainSystem/B3HR/Rpcs/FileGroupValuationRpc/GetRowVersion", SysConfig.Current.AccountingUnit_ID).Select(obj => new Tuple<long, int>(obj.Get<long>("ID"), obj.Get<int>("RowVersion"))).ToList();
            var folder = Path.Combine(Util.DataFolder, typeof(ClientFileGroupValuation).Name);
            BeforeWriteFile(rowVersionList, folder);

            var templateList = RpcFacade.Call<IList<RpcObject>>("/MainSystem/B3HR/Rpcs/FileGroupValuationRpc/GetFileGroupValuationTemplate", rowVersionList.Select(x => new long?(x.Item1)).ToArray());
            foreach (var template in templateList)
            {

                var clientBill = new ClientFileGroupValuation();
                clientBill.ID = template.Get<long>("ID");
                clientBill.Name = template.Get<string>("Name");
                clientBill.Department_ID = template.Get<long>("Department_ID");
                clientBill.Department_Name = template.Get<string>("Department_Name");
                var rowVersion = template.Get<int>("RowVersion");

                var fileGroupDetailList = template.Get<IList<RpcObject>>("FileGroupDetails");
                foreach (var detail in fileGroupDetailList)
                {
                    var fileGroupDetail = new ClientFileGroupValuation_FileGroupDetail();
                    fileGroupDetail.FileGroup_ID = detail.Get<long>("FileGroup_ID");
                    fileGroupDetail.FileGroup_Name = detail.Get<string>("FileGroup_Name");
                    fileGroupDetail.FileGroupCode = detail.Get<string>("FileGroup_Code");
                    clientBill.FileGroupDetails.Add(fileGroupDetail);
                }

                var pieceItemDetailList = template.Get<IList<RpcObject>>("PieceItemDetails");
                foreach (var detail in pieceItemDetailList)
                {
                    var pieceItemDetail = new ClientFileGroupValuation_PieceItemDetail();
                    pieceItemDetail.PieceItem_ID = detail.Get<long>("PieceItem_ID");
                    pieceItemDetail.PieceItem_Name = detail.Get<string>("PieceItem_Name");
                    pieceItemDetail.PieceItem_Code = detail.Get<string>("PieceItem_Code");
                    clientBill.PieceItemDetails.Add(pieceItemDetail);
                }

                XmlSerializer serializer = new XmlSerializer(typeof(ClientFileGroupValuation));
                using (var stream = File.Open(Path.Combine(folder, clientBill.ID + ".xml"), FileMode.Create))
                {
                    serializer.Serialize(stream, clientBill);
                }

                using (var stream = File.CreateText(Path.Combine(folder, clientBill.ID + ".ver")))
                {
                    stream.Write(rowVersion);
                }
            }
        }

        public static void SyncPersonalPieceTemplate()
        {
            var rowVersionList = RpcFacade.Call<IEnumerable<RpcObject>>("/MainSystem/B3HR/Rpcs/PersonalPieceRpc/GetRowVersion", SysConfig.Current.AccountingUnit_ID).Select(obj => new Tuple<long, int>(obj.Get<long>("ID"), obj.Get<int>("RowVersion"))).ToList();
            var folder = Path.Combine(Util.DataFolder, typeof(ClientPersonalPiece).Name);
            BeforeWriteFile(rowVersionList, folder);
            var templateList = RpcFacade.Call<IEnumerable<RpcObject>>("/MainSystem/B3HR/Rpcs/PersonalPieceRpc/GetPersonalPieceTemplate", rowVersionList.Select(x => new long?(x.Item1)).ToArray());

            foreach (var template in templateList)
            {
                var clientBill = new ClientPersonalPiece();
                clientBill.ID = template.Get<long>("ID");
                clientBill.Name = template.Get<string>("Name");
                clientBill.Department_ID = template.Get<long>("Department_ID");
                clientBill.Department_Name = template.Get<string>("Department_Name");
                var rowVersion = template.Get<int>("RowVersion");

                var employeeDetailList = template.Get<IList<RpcObject>>("EmployeeDetails");
                foreach (var detail in employeeDetailList)
                {
                    var employeeDetail = new ClientPersonalPiece_EmployeeDetail();
                    employeeDetail.Employee_ID = detail.Get<long>("HREmployee_ID");
                    employeeDetail.Employee_Name = detail.Get<string>("HREmployee_Name");
                    employeeDetail.Employee_Code = detail.Get<string>("HREmployee_Code");
                    clientBill.EmployeeDetails.Add(employeeDetail);
                }

                var pieceItemDetailList = template.Get<IList<RpcObject>>("PieceItemDetails");
                foreach (var detail in pieceItemDetailList)
                {
                    var pieceItemDetail = new ClientPersonalPiece_PieceItemDetail();
                    pieceItemDetail.PieceItem_ID = detail.Get<long>("PieceItem_ID");
                    pieceItemDetail.PieceItem_Name = detail.Get<string>("PieceItem_Name");
                    pieceItemDetail.PieceItem_Code = detail.Get<string>("PieceItem_Code");
                    pieceItemDetail.Job_ID = detail.Get<long?>("Job_ID");
                    pieceItemDetail.Job_Name = detail.Get<string>("Job_Name");
                    clientBill.PieceItemDetails.Add(pieceItemDetail);
                }

                XmlSerializer serializer = new XmlSerializer(typeof(ClientPersonalPiece));
                using (var stream = File.Open(Path.Combine(folder, clientBill.ID + ".xml"), FileMode.Create))
                {
                    serializer.Serialize(stream, clientBill);
                }


                using (var stream = File.CreateText(Path.Combine(folder, clientBill.ID + ".ver")))
                {
                    stream.Write(rowVersion);
                }
            }
        }

        public static void SyncProductInStoreTemplate()
        {
            var rowVersionList = RpcFacade.Call<IEnumerable<RpcObject>>("/MainSystem/B3Butchery/Rpcs/ProductInStoreRpc/GetRowVersion", SysConfig.Current.AccountingUnit_ID).Select(obj => new Tuple<long, int>(obj.Get<long>("ID"), obj.Get<int>("RowVersion"))).ToList();
            var folder = Path.Combine(Util.DataFolder, typeof(ClientProductInStore).Name);
            BeforeWriteFile(rowVersionList, folder);
            var templateList = RpcFacade.Call<IEnumerable<RpcObject>>("/MainSystem/B3Butchery/Rpcs/ProductInStoreRpc/GetProductInStoreTemplate", rowVersionList.Select(x => new long?(x.Item1)).ToArray());

            foreach (var template in templateList)
            {
                var clientBill = new ClientProductInStore();
                clientBill.ID = template.Get<long>("ID");
                clientBill.Name = template.Get<string>("Name");
                clientBill.Department_ID = template.Get<long>("Department_ID");
                clientBill.Department_Name = template.Get<string>("Department_Name");
                clientBill.InStoreType_ID = template.Get<long?>("InStoreType_ID");
                clientBill.InStoreType_Name = template.Get<string>("InStoreType_Name");
                var rowVersion = template.Get<int>("RowVersion");

                var storeDetailList = template.Get<IList<RpcObject>>("StoreDetails");
                foreach (var detail in storeDetailList)
                {
                    var storeDetail = new ClientProductInStore_StoreDetail();
                    storeDetail.Store_ID = detail.Get<long>("Store_ID");
                    storeDetail.Store_Name = detail.Get<string>("Store_Name");
                    storeDetail.Store_Code = detail.Get<string>("Store_Code");
                    clientBill.StoreDetails.Add(storeDetail);
                }

                var goodsDetailList = template.Get<IList<RpcObject>>("GoodsDetails");
                foreach (var detail in goodsDetailList)
                {
                    var goodsDetail = new ClientProductInStore_GoodsDetail();
                    goodsDetail.Goods_ID = detail.Get<long>("Goods_ID");
                    goodsDetail.Goods_Name = detail.Get<string>("Goods_Name");
                    goodsDetail.Goods_Code = detail.Get<string>("Goods_Code");
                    goodsDetail.Goods_UnitConvertDirection = string.Format("{0}", detail.Get<object>("Goods_UnitConvertDirection"));
                    goodsDetail.Goods_MainUnitRatio = detail.Get<decimal?>("Goods_MainUnitRatio");
                    goodsDetail.Goods_SecondUnitRatio = detail.Get<decimal?>("Goods_SecondUnitRatio");
                    clientBill.GoodsDetails.Add(goodsDetail);
                }

                XmlSerializer serializer = new XmlSerializer(typeof(ClientProductInStore));
                using (var stream = File.Open(Path.Combine(folder, clientBill.ID + ".xml"), FileMode.Create))
                {
                    serializer.Serialize(stream, clientBill);
                }


                using (var stream = File.CreateText(Path.Combine(folder, clientBill.ID + ".ver")))
                {
                    stream.Write(rowVersion);
                }
            }
        }

        public static void SyncProductLinkTemplate()
        {
            var rowVersionList = RpcFacade.Call<IEnumerable<RpcObject>>("/MainSystem/B3Butchery/Rpcs/ProductLinkRpc/GetRowVersion", SysConfig.Current.AccountingUnit_ID).Select(obj => new Tuple<long, int>(obj.Get<long>("ID"), obj.Get<int>("RowVersion"))).ToList();
            var folder = Path.Combine(Util.DataFolder, typeof(ClientProductLink).Name);
            BeforeWriteFile(rowVersionList, folder);
            var templateList = RpcFacade.Call<IEnumerable<RpcObject>>("/MainSystem/B3Butchery/Rpcs/ProductLinkRpc/GetProductLinkTemplate", rowVersionList.Select(x => new long?(x.Item1)).ToArray());

            foreach (var template in templateList)
            {
                var clientBill = new ClientProductLink();
                clientBill.ID = template.Get<long>("ID");
                clientBill.Name = template.Get<string>("Name");
                clientBill.Department_ID = template.Get<long>("Department_ID");
                clientBill.Department_Name = template.Get<string>("Department_Name");
                clientBill.CollectType = string.Format("{0}", template.Get<object>("CollectType"));
                clientBill.ProductLinks_ID = template.Get<long?>("ProductLinks_ID");
                if (Util.ExistError(() => string.IsNullOrEmpty(clientBill.CollectType), string.Format("No.{0} 模板中采集方式不能为空", clientBill.ID)))
                {
                    return;
                }
                var rowVersion = template.Get<int>("RowVersion");
                var detailList = template.Get<IList<RpcObject>>("Details");
                foreach (var detail in detailList)
                {
                    var d = new ClientProductLink_GoodsDetail();
                    d.Goods_ID = detail.Get<long>("Goods_ID");
                    d.Goods_Name = detail.Get<string>("Goods_Name");
                    d.Goods_Code = detail.Get<string>("Goods_Code");
                    d.Goods_UnitConvertDirection = string.Format("{0}", detail.Get<object>("Goods_UnitConvertDirection"));
                    d.Goods_MainUnitRatio = detail.Get<decimal?>("Goods_MainUnitRatio");
                    d.Goods_SecondUnitRatio = detail.Get<decimal?>("Goods_SecondUnitRatio");
                    clientBill.Details.Add(d);
                }

                XmlSerializer serializer = new XmlSerializer(typeof(ClientProductLink));
                using (var stream = File.Open(Path.Combine(folder, clientBill.ID + ".xml"), FileMode.Create))
                {
                    serializer.Serialize(stream, clientBill);
                }

                using (var stream = File.CreateText(Path.Combine(folder, clientBill.ID + ".ver")))
                {
                    stream.Write(rowVersion);
                }
            }
        }

        public static void SyncProductPlanTemplate()
        {
            var rowVersionList = RpcFacade.Call<IEnumerable<RpcObject>>("/MainSystem/B3Butchery/Rpcs/ProductPlanRpc/GetRowVersion", SysConfig.Current.AccountingUnit_ID).Select(obj => new Tuple<long, int>(obj.Get<long>("ID"), obj.Get<int>("RowVersion"))).ToList();
            var folder = Path.Combine(Util.DataFolder, typeof(ClientProductPlan).Name);
            BeforeWriteFile(rowVersionList, folder);
            var productPlanResult = RpcFacade.Call<IList<RpcObject>>("/MainSystem/B3Butchery/Rpcs/ProductPlanRpc/GetProductPlanInfo", rowVersionList.Select(x => new long?(x.Item1)).ToArray());
            foreach (var obj in productPlanResult)
            {
                var clientBill = new ClientProductPlan();
                clientBill.ID = obj.Get<long>("ID");
                clientBill.PlanNumber = obj.Get<string>("PlanNumber");
                clientBill.SyncDate = DateTime.Today;
                clientBill.PlanDate = obj.Get<DateTime>("PlanDate");
                var rowVersion = obj.Get<int>("RowVersion");
                XmlSerializer serializer = new XmlSerializer(typeof(ClientProductPlan));
                using (var stream = File.Open(Path.Combine(folder, clientBill.ID + ".xml"), FileMode.Create))
                {
                    serializer.Serialize(stream, clientBill);
                }

                using (var stream = File.CreateText(Path.Combine(folder, clientBill.ID + ".ver")))
                {
                    stream.Write(rowVersion);
                }
            }
        }

        static void BeforeWriteFile(List<Tuple<long, int>> rowVersionList, string folder)
        {
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            var verFiles = Directory.GetFiles(folder, "*.ver");

            foreach (var file in verFiles)
            {
                var dbFile = file.Replace(".ver", ".xml");
                var id = long.Parse(file.Substring(file.LastIndexOf('\\') + 1).Replace(".ver", ""));
                var v = rowVersionList.Where(x => x.Item1 == id);
                if (v.Count() == 0)
                {
                    File.Delete(file);
                    File.Delete(file.Replace("ver", "xml"));
                }
                else
                {
                    var first = v.First();
                    using (var reader = File.OpenText(file))
                    {
                        if (File.Exists(dbFile) && int.Parse(reader.ReadToEnd()) == first.Item2)
                            rowVersionList.Remove(first);
                    }
                }
            }

        }
    }
}
