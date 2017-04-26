using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Microsoft.WindowsCE.Forms;
using B3ButcheryCE.Device_;
namespace B3HRCE.Device_
{
    public abstract class DeviceWinCE : Device
    {

        public override string StartScan()
        {
            return "NotImplemented";
        }


        [DllImport("coredll.dll", EntryPoint = "SipShowIM")]
        extern static int SipShowIM(int dwFlag);

        public override void ShowInputPanel()
        {
            SipShowIM(1);
        }

        public override void HiddenInputPanel()
        {
            SipShowIM(0);
        }

        public override void OpenWIFI()
        {
            throw new NotImplementedException();
        }

        public override void CloseWIFI()
        {

            throw new NotImplementedException();
        }

        public override bool IsWIFILoaded()
        {
            throw new NotImplementedException();
        }



        [DllImport("coredll.dll", EntryPoint = "FindWindow")]
        static extern int FindWindow(string lpWindowName, string lpClassName);

        [DllImport("coredll.dll", EntryPoint = "ShowWindow")]
        static extern int ShowWindow(int hwnd, int nCmdShow);

        const int SwShow = 5; //显示窗口常量
        const int SwHide = 0;

        public override void HiddenTask()
        {
            var hwnd = FindWindow("HHTaskBar", null);
            if (hwnd != 0)
            {
                ShowWindow(hwnd, SwHide); //隐藏任务栏
            }
        }

        public override void ShowTask()
        {
            var hwnd = FindWindow("HHTaskBar", null);
            if (hwnd != 0)
            {
                ShowWindow(hwnd, SwShow); //隐藏任务栏
            }
        }

        [DllImport("coredll.dll", EntryPoint = "GetDiskFreeSpaceEx")]
        static extern bool GetDiskFreeSpaceEx(string folder,
                out int lpFreeBytesAvailableToCaller,
                out int lpTotalNumberOfBytes,
                out int lpTotalNumberOfFreeBytes);


        public override int GetDiskFreeSpace(string folder)
        {
            if (!Util.IsWinCE)
            {
                return -1;
            }
            int i1, i2, i3;
            if (GetDiskFreeSpaceEx(folder, out i1, out i2, out i3))
            {
                return i3;
            }
            return -1;
        }



        public override void SetupCab(string setupFile)
        {
            Process.Start("\\Windows\\wceload.exe", string.Format("/noaskdest /delete 0 \"{0}\"", setupFile));
        }

        /// <summary>
        /// 设置输入法
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [DllImport("coredll.dll", SetLastError = true)]
        extern static bool SipSetCurrentIM(byte[] id);

        protected static void SipSetCurrentIM(Guid guid)
        {
            SipSetCurrentIM(guid.ToByteArray());
        }


        public override bool IsApplicationRunning(string caption)
        {
            return FindWindow(null, caption) > 0;
        }


        static bool _inputPanelDisplayed;
        public override void SwitchInputPanel()
        {
            if (!_inputPanelDisplayed)
            {
                ShowInputPanel();
            }
            else
            {
                HiddenInputPanel();
            }
            _inputPanelDisplayed = !_inputPanelDisplayed;
        }


        Guid[] _inputMethods;
        protected virtual Guid[] InputMethods
        {
            get
            {
                if (_inputMethods == null)
                {
                    var list = new List<Guid>();
                    if (SwitchInputPanelKey == EmptyKey)
                    {
                        list.Add(Guid.Empty);
                    }
                    foreach (InputMethod input in new InputPanel().InputMethods)
                    {
                        list.Add(input.Clsid);
                    }
                    _inputMethods = list.ToArray();
                }
                return _inputMethods;
            }
        }


        int _currentInputMethodIndex;
        public override void SwitchInputMethod()
        {
            if (InputMethods.Length == 0)
            {
                return;
            }
            _currentInputMethodIndex++;
            if (_currentInputMethodIndex >= InputMethods.Length)
            {
                _currentInputMethodIndex = 0;
            }

            var curValue = InputMethods[_currentInputMethodIndex];
            if (curValue == Guid.Empty)
            {
                HiddenInputPanel();
            }
            else
            {
                HiddenInputPanel();
                SipSetCurrentIM(InputMethods[_currentInputMethodIndex].ToByteArray());
                ShowInputPanel();
            }
        }

        internal static Device CreateService()
        {
            if (Util.DeviceIdentName.StartsWith("MC3190"))
            {
                return new Device_MC3190();
            }
            else if (Util.DeviceIdentName.StartsWith("SeuicScan"))
            {
                return new Device_SeuicD300WithScan();
            }
            else if (Util.DeviceIdentName.StartsWith("Seuic"))
            {
                return new Device_SeuicD300();
            }
            else if (Util.DeviceIdentName.StartsWith("Seuic6L2D"))
            {
                return new Device_Seuic6L2D();
            }           


            return new DevicePC();
        }


        //http://msdn.microsoft.com/en-us/library/aa453245.aspx
        [DllImport("coredll.dll")]
        static extern void keybd_event(byte bVk, byte bScan, byte dwFlags, byte dwExtraInfo);
        const int KeyeventfKeyup = 0x02;
        const int KeyeventfKeydown = 0x00;
        public override void SendKey(byte key)
        {
            keybd_event(0xBE, 0, KeyeventfKeydown, 0);
            keybd_event(0xBE, 0, KeyeventfKeyup, 0);
        }

        [DllImport("COREDLL.DLL")]
        private static extern void GwesPowerOffSystem();

        public override void PowerOffSystem()
        {
            GwesPowerOffSystem();
        }

        public override string ToString()
        {
            return "WinCE手持机";
        }

    }
}
