using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Forks.JsonRpc.Client;
using BWP.Compact.UI;
using BWP.Compact;
using BWP.Compact.Devices;
using Forks.JsonRpc.Client.Data;

namespace B3HRCE
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            Util.SetSceen(this);
            Init();
        }

        SysConfig mConfig;
        const char emptyChar = new char();

        void Init()
        {
            mConfig = SysConfig.Current;
            RpcFacade.Init(mConfig.ServerUrl, "B3ButcherCE");
            textBoxUsername.Text = mConfig.Username;
        }

        private void menuItemSysSetting_Click(object sender, EventArgs e)
        {
            new SysSettingDialog().ShowDialog();
        }

        private void menuItemExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void menuItemSysInfo_Click(object sender, EventArgs e)
        {
            new SysInfoDialog().ShowDialog();
        }

        //private void menuItemUpdate_Click(object sender, EventArgs e)
        //{
        //    new UpdateDialog().ShowDialog();
        //}

        //private void menuItemKeyPressTest_Click(object sender, EventArgs e)
        //{
        //    new KeyPressTestDialog().ShowDialog();
        //}

        //private void menuItemDebugLog_Click(object sender, EventArgs e)
        //{
        //    new DebugLogDialog().ShowDialog();
        //}

        private void linkLabelShowPassword_Click(object sender, EventArgs e)
        {
            if (textBoxPassword.PasswordChar != emptyChar)
            {
                textBoxPassword.PasswordChar = emptyChar;
            }
            else
            {
                textBoxPassword.PasswordChar = '*';
            }
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            var username = textBoxUsername.Text;
            var password = textBoxPassword.Text;
            try
            {
                this.Enabled = false;
                AppUtil.EnsureNetworkConnected();
                if (string.IsNullOrEmpty(SysConfig.Current.ServerUrl))
                {
                    throw new Exception("请先设置服务器Url");
                }
                var serverVersion = RpcFacade.Call<string>("/MainSystem/B3Butchery/Rpcs/ClientRpc/GetPdaVersion");
                if (serverVersion != Util.Version)
                {
                    throw new Exception(string.Format("服务器版本[{0}]与当前客户端版本[{1}]不匹配", serverVersion, Util.Version));
                }

                RpcFacade.Login(username, password);
                mConfig.Username = username;
                mConfig.Password = password;
                Util.OnceLogined = true;

                //记录会计单位名称 和部门ID，Name
                mConfig.AccountingUnit_Name = RpcFacade.Call<string>("/MainSystem/B3Butchery/Rpcs/BaseInfoRpc/GetAccountUnitNameById", mConfig.AccountingUnit_ID??0);
                RpcObject depart = RpcFacade.Call<RpcObject>("/MainSystem/B3Butchery/Rpcs/BaseInfoRpc/GetDepartmentBaseInfoDto");
                mConfig.Department_ID = depart.Get<long>("ID");
                mConfig.Department_Name = depart.Get<string>("Name");
                mConfig.Department_Depth = depart.Get<int?>("Department_Depth");
                mConfig.Save();

                this.Enabled = true;
                new MainForm().ShowDialog();
            }
            catch (Exception ex)
            {
                if (mConfig.Username == username && mConfig.Password == password)
                {
                    this.Enabled = true;
                    new MainForm().ShowDialog();
                }
                else
                {
                    MessageBox.Show("用户离线登录失败，"+ex.Message);
                }
            }
            finally
            {
                this.Enabled = true;
            }
        }

        private void menuItemScanTest_Click(object sender, EventArgs e)
        {
            new ScanTest().ShowDialog();
        }

        private void LoginForm_Closed(object sender, EventArgs e)
        {
            Util.OnceLogined = false;
        }

        private void LoginForm_Closing(object sender, CancelEventArgs e)
        {
            if(RpcFacade.IsLogedIn)
            {
                RpcFacade.Logout();
            }
            
        }

    }
}