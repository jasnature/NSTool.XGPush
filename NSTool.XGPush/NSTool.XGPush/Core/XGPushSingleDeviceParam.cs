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
    public class XGPushSingleDeviceParam : XGPushParamBase
    {
        private string device_token;
        /// <summary>
        /// 针对某一设备推送 
        /// 必填
        /// </summary>
        public string Device_token
        {
            get { return device_token; }
            set { device_token = value; }
        }
    }
}
