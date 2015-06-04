using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


    ///  author:jasnature from http://www.cnblogs.com/NatureSex/
namespace NSTool.XGPush.Core
{
    /// <summary>
    /// 信鸽通用参数
    /// </summary>
    [Serializable]
    [XmlInclude(typeof(XGMessage))]
    [XmlInclude(typeof(NotifyMessage))]
    public class XGParamBase
    {
        private long access_id;

        /// <summary>
        /// 应用的唯一标识符，在提交应用时管理系统返回 
        /// 必须：是
        /// </summary>
        public long Access_id
        {
            get { return access_id; }
            set { access_id = value; }
        }

        private string access_Key;

        /// <summary>
        /// 访问码
        /// </summary>
        public string Access_Key
        {
            get { return access_Key; }
            set { access_Key = value; }
        }

        private string secret_Key;

        /// <summary>
        /// 授权码
        /// </summary>
        public string Secret_Key
        {
            get { return secret_Key; }
            set { secret_Key = value; }
        }

        private uint? timestamp;

        /// <summary>
        /// =================注意未设置将自动生成==================
        /// 本请求的unix时间戳，用于确认请求的有效期。默认情况下，请求时间戳与服务器时间（北京时间）偏差大于600秒则会被拒绝
        /// 必须：是 
        /// </summary>
        public uint? Timestamp
        {
            get { return timestamp; }
            set { timestamp = value; }
        }

        private uint? valid_time = null;

        /// <summary>
        /// 配合timestamp确定请求的有效期，单位为秒，最大值为600。若不设置此参数或参数值非法，则按默认值600秒计算有效期 
        /// 必须：否
        /// </summary>
        public uint? Valid_time
        {
            get { return valid_time; }
            set { valid_time = value; }
        }

        private string sign = null;

        /// <summary>
        /// =================注意未设置将自动生成==================
        ///内容签名。生成规则： 
        ///A）提取请求方法method（GET或POST）； 
        ///B）提取请求url信息，包括Host字段的IP或域名和URI的path部分，注意不包括Host的端口和Path的querystring。
        ///请在请求中带上Host字段，否则将视为无效请求。
        ///比如openapi.xg.qq.com/v2/push/single_device或者hostIP地址/v2/push/single_device;
        ///C）将请求参数（不包括sign参数）格式化成K=V方式； 
        ///注意：计算sign时所有参数不应进行urlencode； 
        ///D）将格式化后的参数以K的字典序升序排列，拼接在一起， 
        ///E）拼接请求方法、url、排序后格式化的字符串以及应用的secret_key； 
        ///F）将E形成字符串计算MD5值，形成一个32位的十六进制（字母小写）字符串，即为本次请求sign（签名）的值； 
        ///Sign=MD5($http_method$url$k1=$v1$k2=$v2$secret_key); 该签名值基本可以保证请求是合法者发送且参数没有被修改，但无法保证不被偷窥。
        ///例如： POST请求到接口http://openapi.xg.qq.com/v2/push/single_device，
        ///有四个参数，access_id=123，timestamp=1386691200，Param1=Value1，Param2=Value2，secret_key为abcde。
        ///则上述E步骤拼接出的字符串为POSTopenapi.xg.qq.com/v2/push/single_deviceParam1=Value1Param2=Value2access_id=123timestamp=1386691200abcde，
        ///计算出该字符串的MD5为ccafecaef6be07493cfe75ebc43b7d53，以此作为sign参数的值
        /// ==================================
        /// 必须：是 
        /// </summary>
        public string Sign
        {
            get { return sign; }
            set { sign = value; }
        }
    }

    /// <summary>
    /// 其他常用的参数
    /// </summary>
    [Serializable]
    public class XGOtherCommonParam : XGParamBase
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
    }

}
