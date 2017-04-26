using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using BWP.Compact;
using System.IO;
using System.Reflection;
using Microsoft.Win32;
using System.Xml.Serialization;

namespace B3HRCE
{
    internal static class Util
    {
           

        public static bool OnceLogined { get; set; }

        public delegate void NoArgumentDelegate();

        public const string Version = "20170419";

        public static object DialogReturnValue { get; set; }

        public static T GetNotNullValue<T>(object value, string message)
        {
            if (value == null)
            {
                throw new Exception(message);
            }
            return (T)value;
        }

        internal static bool ExistError(Func<bool> action, string message)
        {
            if (action())
            {
                MessageBox.Show(message);
                return true;
            }
            return false;
        }

        internal static bool TryParseDecimal(string text, Action<decimal> actionValue, string message)
        {
            try
            {
                var value = decimal.Parse(text);
                actionValue(value);
                return true;
            }
            catch
            {
                MessageBox.Show(message);
                return false;
            }
        }

        public static bool IsWinCE { get; private set; }

        internal static string Folder { get; private set; }

        static Util()
        {
            Debug.WriteLine("Util Init");
            IsWinCE = Environment.OSVersion.Platform == PlatformID.WinCE;
            LogUtil.Info(string.Format("IsWinCE:{0}", IsWinCE));
            Folder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase)
                    .Replace("file:\\", "");
            LogUtil.Info(string.Format("RootFolder:{0}", RootFolder));
        }

        public static string DeviceIdentName
        {
            get
            {
                if (IsWinCE)
                {
                    return Registry.LocalMachine.OpenSubKey("Ident").GetValue("Name").ToString();
                }
                return "WinCE";
            }
        }

        public static bool Connected
        {
            get
            {
                return HardwareUtil.Device.Connected();
            }
        }

        public static string UserStatus
        {
            get
            {
                return OnceLogined ? "在线" : "离线";
            }
        }

        /// <summary>
        /// 程序所在的根目录
        /// </summary>
        internal static string RootFolder
        {
            get
            {
                return Folder;
            }
        }

        public static string DataFolder
        {
            get
            {
                var folder = Path.Combine(RootFolder, "Data");

                if (!string.IsNullOrEmpty(HardwareUtil.DataRootDirectory))
                {
                    if (!Directory.Exists(HardwareUtil.DataRootDirectory))
                    {
                        throw new Exception("存储卡目录不存在");
                    }
                    folder = Path.Combine(HardwareUtil.DataRootDirectory, "B3ButcheryCEData");
                }

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                return folder;
            }
        }

        public static void SetSceen(Form form)
        {
            if (HardwareUtil.Device.ToString() == "PC机")
            {
                return;
            }
            form.Width = Screen.PrimaryScreen.Bounds.Width;
            form.Height = Screen.PrimaryScreen.Bounds.Height;
            form.MaximizeBox = false;
            form.MinimizeBox = false;
        }
    }
}
