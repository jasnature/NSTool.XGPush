using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSTool.XGPush.Base;


    ///  author:jasnature from http://www.cnblogs.com/NatureSex/
namespace NSTool.XGPush.Core
{
    /// <summary>
    /// 推送消息格式定义
    /// </summary>
    [Serializable]
    public class XGMessage
    {
        private string title;

        /// <summary>
        /// 标题，必填
        /// </summary>
        [JsonProperty("title")]
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private string content;

        /// <summary>
        /// 内容，必填
        /// </summary>
        [JsonProperty("content")]
        public string Content
        {
            get { return content; }
            set { content = value; }
        }

        private SerializableDictionary<string,string> custom_content;

        /// <summary>
        /// 用户自定义的key-value，选填
        /// </summary>
        [JsonProperty("custom_content")]
        public SerializableDictionary<string, string> Custom_content
        {
            get { return custom_content; }
            set { custom_content = value; }
        }

        private List<AcceptTime> accept_time;

        /// <summary>
        /// 允许推送给用户的时段，选填
        /// </summary>
        [JsonProperty("accept_time")]
        public List<AcceptTime> Accept_time
        {
            get { return accept_time; }
            set { accept_time = value; }
        }



    }

    /// <summary>
    /// 允许推送给用户的时段
    /// </summary>
   [Serializable]
    public class AcceptTime
    {
        private XGTime start;

        [JsonProperty("start")]
        public XGTime Start
        {
            get { return start; }
            set { start = value; }
        }

        private XGTime end;

        [JsonProperty("end")]
        public XGTime End
        {
            get { return end; }
            set { end = value; }
        }

        /// <summary>
        /// 校验时间格式和范围
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            if (this.start.IsValid() && this.end.IsValid())
                return true;
            else
                return false;
        }
    }

    /// <summary>
    /// 预定时间格式
    /// </summary>
    [Serializable]
    public class XGTime
    {
        private string hour;

         [JsonProperty("hour")]
        public string Hour
        {
            get { return hour; }
            set { hour = value; }
        }

        private string min;

         [JsonProperty("min")]
        public string Min
        {
            get { return min; }
            set { min = value; }
        }

        /// <summary>
        /// 校验格式和范围
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            try
            {
                int h = int.Parse(this.hour);
                int m = int.Parse(this.min);
                if (h >= 0 && h <= 23 && m >= 0 && m <= 59)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
    }

}
