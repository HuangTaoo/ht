using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Forks.JsonRpc.Client;

namespace B3HRCE
{
    public partial class SysSettingDialog : Form
    {
        public SysSettingDialog()
        {
            InitializeComponent();
            Util.SetSceen(this);
            ApplyToUI();
        }

        public void ApplyToUI()
        {
            var config = SysConfig.Current;

            var usageModes = SysConfig.Current.UsageModes ?? UsageMode.empty;

            for (int i = 1; i <= 8; i = i * 2)
            {
                var enumItem = (UsageMode)Enum.ToObject(typeof(UsageMode), i);
                var item = new ListViewItem(enumItem.ToString());
                item.Tag = enumItem;
                item.Checked = (usageModes & enumItem) > 0;
                listView1.Items.Add(item);
            }

            textBoxAccountingUnitID.Text = string.Format("{0}", SysConfig.Current.AccountingUnit_ID);
            textBoxServerUrl.Text = config.ServerUrl;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            var config = SysConfig.Current;
            var urlChanged = false;
            if (config.ServerUrl != textBoxServerUrl.Text)
            {
                config.ServerUrl = textBoxServerUrl.Text;
                urlChanged = true;
            }

            var usageModes = UsageMode.empty;
            foreach (ListViewItem item in listView1.Items)
            {
                if (!item.Checked)
                {
                    continue;
                }
                usageModes = usageModes | (UsageMode)item.Tag;
            }

            config.UsageModes = usageModes;
            config.AccountingUnit_ID = long.Parse(textBoxAccountingUnitID.Text);

            config.Save();
            if (urlChanged)
            {
                RpcFacade.ReInit(config.ServerUrl);
            }
            MessageBox.Show("保存成功");
            this.Close();
        }

    }
}