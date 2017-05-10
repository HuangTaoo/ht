using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace B3ButcheryCE.Rpc_
{
    public class ClientBase
    {
        /// <summary>
        /// 是否发送
        /// </summary>
        private bool isSend = false;
        public bool IsSend
        {
            get { return isSend; }
            set { isSend = value; }
        }
    }
}
