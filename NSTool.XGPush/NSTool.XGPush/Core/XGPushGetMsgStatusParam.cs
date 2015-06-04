using Newtonsoft.Json;
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
    public class XGPushGetMsgStatusParam : XGParamBase
    {
        private List<Push_Id_Obj> push_ids;

        [JsonProperty("push_ids")]
        public List<Push_Id_Obj> Push_ids
        {
            get { return push_ids; }
            set { push_ids = value; }
        }

    }

    [Serializable]
    public class Push_Id_Obj
    {
        private string push_id;

        [JsonProperty("push_id")]
        public string Push_id
        {
            get { return push_id; }
            set { push_id = value; }
        }
    }
}
