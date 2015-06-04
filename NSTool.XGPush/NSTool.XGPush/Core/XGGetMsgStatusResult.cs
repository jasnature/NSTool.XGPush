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
    public class XGGetMsgStatusResult
    {
        private List<XGMsgStatus> list;

        /// <summary>
        /// 返回PUSH消息的状态结果集合
        /// </summary>
        public List<XGMsgStatus> List
        {
            get { return list; }
            set { list = value; }
        }
    }

    /// <summary>
    /// XG状态
    /// </summary>
    [Serializable]
    public class XGMsgStatus
    {
        
        private string push_id;

        /// <summary>
        /// pushid
        /// </summary>
        public string Push_id
        {
            get { return push_id; }
            set { push_id = value; }
        }
       
        private uint status;
        /// <summary>
        /// 0（未处理）/1（推送中）/2（推送完成）/3（推送失败）
        /// </summary>
        public uint Status
        {
            get { return status; }
            set { status = value; }
        }
       
        private string start_time;

        /// <summary>
        /// year-mon-day hour:min:sec
        /// </summary>
        public string Start_time
        {
            get { return start_time; }
            set { start_time = value; }
        }
        
        private uint finished;

        /// <summary>
        /// （已发送）
        /// </summary>
        public uint Finished
        {
            get { return finished; }
            set { finished = value; }
        }
        
        private uint totoal;

        /// <summary>
        /// 共需要发送
        /// </summary>
        public uint Totoal
        {
            get { return totoal; }
            set { totoal = value; }
        }

    }
}
