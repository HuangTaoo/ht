using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using BWP.Compact;
using System.IO;
using System.Diagnostics;
using B3HRCE.Device_;


namespace B3HRCE
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [MTAThread]
        static void Main()
        {
                                

            AppUtil.Init("B3ButcheryCE");
            var lockFile = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.Personal), AppUtil.AppName + ".lock");
            //try
            //{
                using (var file = File.OpenWrite(lockFile))
                {
                    doMain();
                }
            //}
            //catch (IOException ex)
            //{
            //    if (ex.Message.IndexOf(lockFile) >= 0)
            //    {
            //        MessageBox.Show("当前程序只能启动一个实例");
            //        return;
            //    }
            //    MessageBox.Show(ex.Message); ;
            //    AppUtil.UnCatchedException(ex);
            //    throw ex;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //    AppUtil.UnCatchedException(ex);
            //    throw ex;
            //}
            //finally
            //{
            //    Device.Current.ShowTask();
            //}

        }

        private static void doMain()
        {
            try
            {
                //程序主入口初始化扫描头
                HardwareUtil.HardWareInit();

                //HardwareUtil.ScanPowerOn();

                HardwareUtil.HiddenTask();

                LogUtil.Init();

                Application.Run(new LoginForm());
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
            finally
            {
                HardwareUtil.ScanPowerOff();
                HardwareUtil.HardWareDeInit();
                HardwareUtil.ShowTask();
                Process.GetCurrentProcess().Kill();//关掉所有关联进程
            }
        }
    }
}