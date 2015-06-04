using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    ///  author:jasnature from http://www.cnblogs.com/NatureSex/
namespace NSTool.XGPush.Core
{
    /// <summary>
    /// PUSH消息后返回的公共结果
    /// </summary>
    [Serializable]
    public class XGPushResult
    {
        private string push_id;

        /// <summary>
        /// 表示给app下发的任务id
        /// </summary>
        public string Push_id
        {
            get { return push_id; }
            set { push_id = value; }
        }
    }

    /// <summary>
    /// 设备数对象
    /// </summary>
    [Serializable]
    public class XGDeviceNumResult
    {
        private string device_num;

        /// <summary>
        /// 设备数
        /// </summary>
        public string Device_num
        {
            get { return device_num; }
            set { device_num = value; }
        }

       
    }

    /// <summary>
    /// 返回的其他状态
    /// </summary>
    [Serializable]
    public class XGStatusResult
    {
        private uint status;

        /// <summary>
        /// 0为成功，其余为失败
        /// </summary>
        public uint Status
        {
            get { return status; }
            set { status = value; }
        }
    }
}
