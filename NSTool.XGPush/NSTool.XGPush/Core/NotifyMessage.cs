using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;


    ///  author:jasnature from http://www.cnblogs.com/NatureSex/
namespace NSTool.XGPush.Core
{
    /// <summary>
    /// 该类目前只对通知有效，推送透传消息时不用设置
    /// </summary>
    [Serializable]
    [XmlInclude(typeof(NotifyMessage))]
    public class NotifyMessage : XGMessage
    {
        private uint? builder_id;

        /// <summary>
        /// 自定义通知样式，若不清楚其含义可不用设置。自定义通知样式的使用方法参见终端SDK文档
        /// ==选填==
        /// </summary>
        [JsonProperty("builder_id")]
        public uint? Builder_id
        {
            get { return builder_id; }
            set { builder_id = value; }
        }


        private uint n_id = 0;

        /// <summary>
        /// 通知id，==选填==。
        /// 若大于0，则会覆盖先前弹出的相同id通知；
        /// 若为0，展示本条通知且不影响其他通知；
        /// 若为-1，将清除先前弹出的所有通知，仅展示本条通知。默认为0
        /// </summary>
        [JsonProperty("n_id")]
        public uint N_id
        {
            get { return n_id; }
            set { n_id = value; }
        }

        private uint ring = 0;

        /// <summary>
        /// 是否响铃，0否，1是，下同。==选填==，默认0
        /// </summary>
        [JsonProperty("ring")]
        public uint Ring
        {
            get { return ring; }
            set { ring = value; }
        }

        private uint vibrate = 0;

        /// <summary>
        /// 是否振动，==选填==，默认0
        /// </summary>
        [JsonProperty("vibrate")]
        public uint Vibrate
        {
            get { return vibrate; }
            set { vibrate = value; }
        }

        private uint clearable = 1;

        /// <summary>
        /// 通知栏是否可清除，==选填==，默认1
        /// </summary>
        [JsonProperty("clearable")]
        public uint Clearable
        {
            get { return clearable; }
            set { clearable = value; }
        }

        private NotifyMessageAction action;

        /// <summary>
        /// 动作，必填
        /// </summary>
        [JsonProperty("action")]
        public NotifyMessageAction Action
        {
            get { return action; }
            set { action = value; }
        }

    }

    /// <summary>
    /// 通知消息动作
    /// </summary>
    [Serializable]
    public class NotifyMessageAction
    {
        private uint action_type;

        /// <summary>
        /// 动作类型，1打开activity或app本身，2打开浏览器，3打开Intent 
        /// </summary>
        [JsonProperty("action_type")]
        public uint Action_type
        {
            get { return action_type; }
            set { action_type = value; }
        }

        private NotifyMessageAction_Browser browser;

        /// <summary>
        /// url：打开的url，confirm是否需要用户确认
        /// </summary>
        [JsonProperty("browser")]
        public NotifyMessageAction_Browser Browser
        {
            get { return browser; }
            set { browser = value; }
        }

        private string activity;

        [JsonProperty("activity")]
        public string Activity
        {
            get { return activity; }
            set { activity = value; }
        }

        private string intent;

        [JsonProperty("intent")]
        public string Intent
        {
            get { return intent; }
            set { intent = value; }
        }
    }

    /// <summary>
    /// url：打开的url，confirm是否需要用户确认
    /// </summary>
    [Serializable]
    public class NotifyMessageAction_Browser
    {
        private string url;

        [JsonProperty("url")]
        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        private uint confirm = 1;

        [JsonProperty("confirm")]
        public uint Confirm
        {
            get { return confirm; }
            set { confirm = value; }
        }
    }
}
