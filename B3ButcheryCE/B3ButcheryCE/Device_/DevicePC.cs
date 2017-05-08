using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;

namespace B3HRCE.Device_
{
    public class DevicePC : Device
    {
        public override void AttachStatusNotify(EventHandler StatusNotifyHandler)
        {

        }

        public override void AttachReadNotify(EventHandler ReadNotifyHandler)
        {

        }

        public override void ScanClose()
        {

        }

        public override void ReaderInitiated()
        {

        }

        public override string GetGetSerialNumber()
        {
            return "D300C2C236";
        }

        public override void ScanPowerOn()
        {
        }

        public override void ScanPowerOff()
        {
        }

        public override string StartScan()
        {
            Thread.Sleep(20);
            return "6934550300263";
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

        public override void ControlPanelSound()
        {
        }

        public override void ControlPanelInputMethod()
        {
        }

        public override void ShowInputPanel()
        {
        }

        public override void HiddenInputPanel()
        {
        }

        public override void HiddenTask()
        {
        }

        public override void ShowTask()
        {
        }

        public override void SetupCab(string cabFile)
        {
            Process.Start("\\Windows\\wceload.exe", string.Format("/noaskdest /delete 0 \"{0}\"", cabFile));
        }

        public override int GetDiskFreeSpace(string folder)
        {
            return 0;
        }

        public override bool IsApplicationRunning(string caption)
        {
            return false;
        }

        public override void SwitchInputPanel()
        {
            MessageBox.Show(string.Format("SwitchInputPanel"));
        }

        public override void SwitchInputMethod()
        {
            MessageBox.Show(string.Format("SwitchInputMethod"));
        }

        public override Keys ScanCodeKey
        {
            get
            {
                return Keys.F2;
            }
        }

        public override Keys SwitchInputPanelKey
        {
            get
            {
                return Keys.F3;
            }
        }

        public override Keys SwitchInputMethodKey
        {
            get
            {
                return Keys.F4;
            }
        }

        public override string DataRootDirection
        {
            get
            {
                return string.Empty;
            }
        }

        public override void SendKey(byte key)
        {
        }

        public override void PowerOffSystem()
        {
            MessageBox.Show(string.Format("PowerOffSystem"));
        }

        public override string ToString()
        {
            return "PC机";
        }
    }
}
