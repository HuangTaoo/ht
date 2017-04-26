using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Forks.JsonRpc.Client;
using BWP.Compact;
using System.Net;
using System.IO;
using System.Threading;
using BWP.Compact.Devices;

namespace B3HRCE
{
    public partial class UpdateDialog : Form
    {

        public class UpdateException : Exception
        {
            public UpdateException(string message)
                : base(message)
            {
            }
        }


        public UpdateDialog()
        {
            InitializeComponent();
            Util.SetSceen(this);
        }

        private void BeginUpdate()
        {
            if (string.IsNullOrEmpty(SysConfig.Current.ServerUrl))
            {
                throw new Exception("请先设置服务器Url");
            }

            var url = RpcFacade.Call<string>("/MainSystem/System/ClientCenter/GetClientUpdateUrl", AppUtil.AppName, Util.Version);

            if (string.IsNullOrEmpty(url))
            {
                MessageBox.Show("当前程序已经是最终版本");
                this.Close();
                return;
            }


            if (!AppUtil.IsWinCE)
            {
                MessageBox.Show("当前程序有新的版本可以下载");
                this.Close();
                return;
            }


            url = SysConfig.Current.ServerUrl + url;

            withProgreeLabel1.Text = "下载新的安装包";

            var request = HttpWebRequest.Create(url);

            var response = request.GetResponse();

            if (response.ContentType != "application/octet-stream")
            {
                throw new UpdateException("下载文件类型不正确" + response.ContentType);
            }

            var responseStream = response.GetResponseStream();

            var setupFile = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "B3ButcheryCESetup.CAB");


            int bufferLength = 4096;

            withProgreeLabel1.Maximum = Convert.ToInt32(response.ContentLength / bufferLength) + 2;

            withProgreeLabel1.ShowProgressBar();


            byte[] buffer = new byte[bufferLength];
            using (var file = File.Open(setupFile, FileMode.Create))
            {
                int count = 0;
                do
                {
                    count = responseStream.Read(buffer, 0, buffer.Length);
                    file.Write(buffer, 0, count);
                    withProgreeLabel1.Value++;
                } while (count > 0);
            }

            response.Close();

            withProgreeLabel1.Text = "下载完成,开始安装";
            Thread.Sleep(1000);
            withProgreeLabel1.HiddenProgressBar();

            Device.Current.SetupCab(setupFile);
            Application.Exit();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                BeginUpdate();
            }
            catch (UpdateException ex)
            {
                LogUtil.Warn(ex.ToString());
                MessageBox.Show("更新失败:" + ex.Message);
                this.Close();
            }
        }

    }
}