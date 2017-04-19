using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using SCAN.Scanner1D;

namespace B3HRCE.Device_
{
    public class Device_SeuicD300WithScan : Device_SeuicD300
    {
        public override void ScanPowerOn()
        {
            Scanner.Instance().OnScannerReader += new Scanner.ScannerReader(Device_SeuicD300WithScan_OnScannerReader);
            Scanner.SSI_EnableScan();  //开启扫描功能
        }

        void Device_SeuicD300WithScan_OnScannerReader(Scanner.BARCODE barCode)
        {
            OnScannerReader(this, new ScanEventArgs() { BarCode = barCode.code });
        }

        public override void ScanPowerOff()
        {
            Scanner.Instance().OnScannerReader -= new Scanner.ScannerReader(Device_SeuicD300WithScan_OnScannerReader);
            Scanner.SSI_DisableScan(); //禁用扫描功能
        }

        public override string ToString()
        {
            return "SeuicD300WithScan";
        }
    }


    public class Device_SeuicD300 : DeviceWinCE
    {
        public override string ToString()
        {
            return "SeuicD300NoScan";
        }


        public override void ScanPowerOn()
        {
        }

        public override void ScanPowerOff()
        {
        }


        [DllImport("D300SysUI.dll")]
        public static extern bool D300SysUI_GetUUID(byte[] strUUID, int UUIDSize);

        public override string GetGetSerialNumber()
        {
            byte[] byUUID = new byte[64];
            D300SysUI_GetUUID(byUUID, 64);
            return Encoding.Unicode.GetString(byUUID, 0, byUUID.Length).Trim('\0');
            //return "seuictest001";
        }


        public override void HardWareInit()
        {

        }

        public override void HardWareDeInit()
        {
        }

        public override bool IsWIFILoaded()
        {
            return true;
        }

        public override void OpenWIFI()
        {
        }

        public override void CloseWIFI()
        {
        }

        public override System.Windows.Forms.Keys ScanCodeKey
        {
            get
            {
                return Keys.F24;
            }
        }

        public override System.Windows.Forms.Keys SwitchInputPanelKey
        {
            get
            {
                return EmptyKey;
            }
        }

        public override System.Windows.Forms.Keys SwitchInputMethodKey
        {
            get { return Keys.Escape; }
        }

        public override string DataRootDirection
        {
            get
            {
                return LogRootDirection;
                //return "\\User_Storage";
            }
        }

        public override string LogRootDirection
        {
            get
            {
                if (Directory.Exists("\\User_Storage"))
                {
                    return "\\User_Storage";
                }
                else if (Directory.Exists("\\Storage Card"))
                {
                    return "\\Storage Card";
                }
                else
                {
                    throw new Exception("内存卡不存在");
                }
            }
        }

        public override void AttachStatusNotify(EventHandler StatusNotifyHandler)
        {
            throw new NotImplementedException();
        }

        public override void AttachReadNotify(EventHandler ReadNotifyHandler)
        {
            throw new NotImplementedException();
        }

        public override void ScanClose()
        {
            throw new NotImplementedException();
        }

        public override void ReaderInitiated()
        {
            throw new NotImplementedException();
        }

        public override void ControlPanelSound()
        {
            throw new NotImplementedException();
        }

        public override void ControlPanelInputMethod()
        {
            throw new NotImplementedException();
        }
    }
}
