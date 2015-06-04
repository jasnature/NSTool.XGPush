using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;


    ///  author:jasnature from http://www.cnblogs.com/NatureSex/
namespace NSTool.XGPush.Core
{
    /// <summary>
    /// PUSH 消息的公共参数
    /// </summary>
    [Serializable]
    [XmlInclude(typeof(XGMessage))]
    [XmlInclude(typeof(NotifyMessage))]
    public class XGPushParamBase : XGParamBase
    {
        private uint message_type;

        /// <summary>
        /// 消息类型：1：通知 2：透传消息。iOS平台请填0
        /// 必须：是
        /// </summary>
        public uint Message_type
        {
            get { return message_type; }
            set { message_type = value; }
        }

        private XGMessage message;

        /// <summary>
        /// 参见PostXG.json文件格式
        /// 必须：是
        /// </summary>
        public XGMessage Message
        {
            get { return message; }
            set { message = value; }
        }

        private uint? expire_time = null;

        /// <summary>
        /// 消息离线存储多久，单位为秒，最长存储时间3天。设为0，则不存储
        /// 默认：不存储
        /// 必须：否
        /// </summary>
        public uint? Expire_time
        {
            get { return expire_time; }
            set { expire_time = value; }
        }

        private string send_time = null;

        /// <summary>
        /// 指定推送时间，格式为year-mon-day hour:min:sec 若小于服务器当前时间，则会立即推送 
        /// 默认:立即
        /// 必须：否 
        /// </summary>
        public string Send_time
        {
            get { return send_time; }
            set { send_time = value; }
        }

        private uint? multi_pkg = null;

        /// <summary>
        /// 0表示按注册时提供的包名分发消息；1表示按access id分发消息，
        /// 所有以该access id成功注册推送的app均可收到消息。本字段对iOS平台无效 
        /// 默认：0
        /// 必须：否 
        /// </summary>
        public uint? Multi_pkg
        {
            get { return multi_pkg; }
            set { multi_pkg = value; }
        }

        private uint? environment = null;

        /// <summary>
        /// 向iOS设备推送时必填，1表示推送生产环境；2表示推送开发环境。本字段对Android平台无效 
        /// 必须：仅iOS必需
        /// </summary>
        public uint? Environment
        {
            get { return environment; }
            set { environment = value; }
        }
    }
}
