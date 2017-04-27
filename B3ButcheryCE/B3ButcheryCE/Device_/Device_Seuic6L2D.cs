using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using SCAN.Scanner2D;
using B3HRCE.Device_;

namespace B3ButcheryCE.Device_
{
    public class Device_Seuic6L2D : Device_SeuicD300
    {


        public override void ScanPowerOn()
        {
            Scanner.Instance().OnScanedEvent += new Action<Scanner.CodeInfo>(Device_Seuic6LWithScan_OnScannerReader);
            Scanner.Enable();//启用扫描
         
        }

        void Device_Seuic6LWithScan_OnScannerReader(Scanner.CodeInfo obj)
        {
            OnScannerReader(this, new ScanEventArgs() { BarCode = obj.barcode });
        }

        public override void ScanPowerOff()
        {
            Scanner.Instance().OnScanedEvent -= new Action<Scanner.CodeInfo>(Device_Seuic6LWithScan_OnScannerReader);
            Scanner.Disable(); //禁用扫描功能
        }

        public override string ToString()
        {
            return "Seuic6L2D";
        }
    }
}
