using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using System.Windows.Forms;
using BWP.Compact.Devices;

namespace B3ButcheryCE
{
    public static class HardwareUtil
    {
        static HardwareUtil()
        {
            Device = !Util.IsWinCE ? new DevicePC() : DeviceContext.Current;
        }

        internal static Device Device { get; private set; }


        public static void ScanClose()
        {
            Device.ScanClose();
        }

        public static void AttachStatusNotify(EventHandler StatusNotifyHandler)
        {
            Device.AttachStatusNotify(StatusNotifyHandler);
        }

        public static void AttachReadNotify(EventHandler ReadNotifyHandler)
        {
            Device.AttachReadNotify(ReadNotifyHandler);
        }

        public static string GetGetSerialNumber()
        {
            return Device.GetGetSerialNumber();
        }

        public static void ReaderInitiated()
        {
            Device.ReaderInitiated();
        }

        public static void ScanPowerOn()
        {
            Device.ScanPowerOn();
        }

        public static void ScanPowerOff()
        {
            Device.ScanPowerOff();
        }

        public static void StartScan()
        {
            Device.StartScan();
        }

        public static void HardWareDeInit()
        {
            Device.HardWareDeInit();
        }


        public static void HardWareInit()
        {
            Device.HardWareInit();
        }


        public static bool IsWIFILoaded()
        {
            return Device.IsWIFILoaded();

        }

        public static void CloseWIFI()
        {
            Device.CloseWIFI();
        }

        public static void OpenWIFI()
        {
            Device.OpenWIFI();
        }

        public static string DataRootDirectory
        {
            get
            {
                return Device.DataRootDirection;
            }
        }

        public static void ShowInputPanel()
        {
            Device.ShowInputPanel();
        }

        public static void HiddenInputPanel()
        {
            Device.HiddenInputPanel();
        }

        public static void HiddenTask()
        {
            Device.HiddenTask();
        }

        public static void ShowTask()
        {
            Device.ShowTask();
        }

        public static void PowerOffSystem()
        {
            Device.PowerOffSystem();
        }


        public static class ControlPanel
        {
            public static void InputPanel()
            {
                Device.ControlPanelInputMethod();
            }

            internal static void Sound()
            {
                Device.ControlPanelSound();
            }
        }

        public static void SetupCab(string cabFile)
        {
            Device.SetupCab(cabFile);
        }

        public static void SwitchInputPanel()
        {
            Device.SwitchInputPanel();
        }

        public static void SwitchInputMethod()
        {
            Device.SwitchInputMethod();
        }

        public static class HotKey
        {
            public static Keys ScanCodeKey
            {
                get
                {
                    return Device.ScanCodeKey;
                }
            }

            public static Keys SwitchInputPanelKey
            {
                get
                {
                    return Device.SwitchInputPanelKey;
                }
            }

            public static Keys SwitchInputMethodKey
            {
                get
                {
                    return Device.SwitchInputMethodKey;
                }
            }
        }

        public static string DataRootDirection
        {
            get
            {
                return Device.DataRootDirection;
            }
        }

        internal static void SendKey(byte p)
        {
            Device.SendKey(p);
        }
    }
}
