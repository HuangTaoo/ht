using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Windows.Forms;

namespace B3HRCE.Device_
{
    public class ScanEventArgs : EventArgs
    {
        public string BarCode { get; set; }
    }

    public abstract class Device
    {
        public event EventHandler<ScanEventArgs> ScannerReader;

        protected void OnScannerReader(object sender, ScanEventArgs e)
        {
            if (ScannerReader != null)
            {
                ScannerReader(sender, e);
            }
        }

        public EventHandler ReadNotifyHandler = null;
        public EventHandler StatusNotifyHandler = null;

        public EventHandler FormActivatedEventHandler = null;
        public EventHandler FormDeactivatedEventHandler = null;


        public abstract void AttachStatusNotify(EventHandler StatusNotifyHandler);


        public abstract void AttachReadNotify(System.EventHandler ReadNotifyHandler);

        public abstract void ScanClose();



        /// <summary>
        /// 是否初始化扫描头
        /// </summary>
        /// <returns></returns>
        public abstract void ReaderInitiated();

        /// <summary>
        /// 得到序列号
        /// </summary>
        /// <returns></returns>
        public abstract string GetGetSerialNumber();

        /// <summary>
        /// 打开扫描头电源
        /// </summary>
        public abstract void ScanPowerOn();


        /// <summary>
        /// 关闭扫描头电源
        /// </summary>
        public abstract void ScanPowerOff();

        public abstract string StartScan();

        public abstract void HardWareInit();

        public abstract void HardWareDeInit();

        public abstract bool IsWIFILoaded();

        public abstract void OpenWIFI();

        public abstract void CloseWIFI();

        public abstract void ControlPanelSound();

        public abstract void ControlPanelInputMethod();

        public abstract void ShowInputPanel();

        public abstract void HiddenInputPanel();

        public abstract void HiddenTask();

        public abstract void ShowTask();

        public abstract void SetupCab(string cabFile);

        public abstract int GetDiskFreeSpace(string folder);

        public abstract bool IsApplicationRunning(string caption);

        /// <summary>
        /// 切换是否显示输入面板
        /// </summary>
        public abstract void SwitchInputPanel();

        public abstract void SwitchInputMethod();

        public abstract Keys ScanCodeKey { get; }

        public abstract Keys SwitchInputPanelKey { get; }

        public abstract Keys SwitchInputMethodKey { get; }


        /// <summary>
        /// 数据存储根目录，一般应该是SD卡的目录
        /// </summary>
        public abstract string DataRootDirection { get; }

        public virtual string LogRootDirection
        {
            get
            {
                return DataRootDirection;
            }
        }

        public abstract void SendKey(byte key);

        public abstract void PowerOffSystem();

        public override string ToString()
        {
            return "未知手持机设备";
        }

        public string GetIP()
        {
            var addressList = Dns.GetHostEntry(Dns.GetHostName()).AddressList;

            foreach (var address in addressList)
            {
                if (address.ToString() == "127.0.0.1")
                    continue;
                if (IsValidIP(address.ToString()))
                    return address.ToString();
            }
            return string.Empty;
        }

        static bool IsValidIP(string ip)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(ip, "^[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}$"))
            {
                var ips = ip.Split('.');
                return (ips.Length == 4 || ips.Length == 6) &&
                             (System.Int32.Parse(ips[0]) < 256 &&
                                System.Int32.Parse(ips[1]) < 256 & System.Int32.Parse(ips[2]) < 256 & System.Int32.Parse(ips[3]) < 256);
            }
            return false;
        }

        public bool Connected()
        {
            return !string.IsNullOrEmpty(GetIP());
        }

        public bool NoScan
        {
            get;
            set;
        }

        public const Keys EmptyKey = Keys.F24;
    }
}
