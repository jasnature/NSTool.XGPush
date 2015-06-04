using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    ///  author:jasnature from http://www.cnblogs.com/NatureSex/
namespace NSTool.XGPush.Core
{
    /// <summary>
    /// Push消息（包括通知和透传消息）给单个设备
    /// </summary>
    [Serializable]
    public class XGPushSingleAccountParam : XGPushParamBase
    {
        private string account;

        /// <summary>
        /// 针对某一账号推送 
        /// =必填=
        /// </summary>
        public string Account
        {
            get { return account; }
            set { account = value; }
        }

    }
}
