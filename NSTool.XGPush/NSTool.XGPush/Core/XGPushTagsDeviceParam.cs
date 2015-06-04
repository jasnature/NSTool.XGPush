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
    public class XGPushTagsDeviceParam : XGPushParamBase
    {
        private List<string> tags_list;

        /// <summary>
        /// json格式[“tag1”,”tag2”,”tag3”] 
        /// =必填=
        /// </summary>
        public List<string> Tags_list
        {
            get { return tags_list; }
            set { tags_list = value; }
        }

        private string tags_op;

        /// <summary>
        /// 取值为AND或OR 
        /// =必填=
        /// </summary>
        public string Tags_op
        {
            get { return tags_op; }
            set { tags_op = value; }
        }

       

    }
}
