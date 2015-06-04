using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using NSTool.XGPush.Base;
using NSTool.XGPush.Core;

    ///  author:jasnature from http://www.cnblogs.com/NatureSex/
namespace NSTool.XGPush
{
    /// <summary>
    /// QQ信鸽提供者，暂不提供GET请求方式，全部POST
    /// </summary>
    public class QQXGProvider : IMessagePushProvider, IDisposable
    {
        #region 内部字段

        private volatile static Dictionary<Type, PropertyInfo[]> cacheParamType = null;

        private System.Threading.ReaderWriterLock lockrw = null;

        private HttpClientHelper httpClient;
        private MD5CryptionXG md5;
        private Encoding encoder = Encoding.UTF8;

        private string requestProtocol = "http";
        private string requestMethod = "POST";

        private string apiUrl = "openapi.xg.qq.com";

        private string apiVersion = "v2";

        private string apiClass = "push";

        private string apiFullUrl = null;

        #endregion

        #region 属性

        /// <summary>
        /// 获取完整请求路径，不包含请求方法method类型和params参数
        /// </summary>
        public string ApiFullUrl
        {
            get { return apiFullUrl; }
        }

        /// <summary>
        /// API提供的接口类别，默认push
        /// 可以在XGConst类中找到已经定义的名称，
        /// 参考CLASS_API_NAME打头的定义
        /// </summary>
        public string ApiClass
        {
            get { return apiClass; }
            set
            {
                apiClass = value;
                Reset();
            }
        }

        /// <summary>
        /// 表示当前api的版本号，如默认为v2
        /// </summary>
        public string ApiVersion
        {
            get { return apiVersion; }
            set
            {
                apiVersion = value;
                Reset();
            }
        }

        /// <summary>
        /// 发送请求URL地址，不要包含协议头，如:http://
        /// </summary>
        public string ApiUrl
        {
            get { return apiUrl; }
            set
            {
                apiUrl = value;
                Reset();
            }
        }

        /// <summary>
        /// 多线程安全缓存参数类型集合
        /// </summary>
        private PropertyInfo[] GetCacheParamType<T>(T pb)
        {
            Type pbtype = pb.GetType();
            try
            {
                if (cacheParamType.ContainsKey(pbtype))
                {
                    return cacheParamType[pbtype];
                }
                else
                {
                    lockrw.AcquireWriterLock(1000);
                    if (!cacheParamType.ContainsKey(pbtype))
                    {
                        PropertyInfo[] pis = pbtype.GetProperties().OrderBy(p => p.Name).ToArray();
                        cacheParamType.Add(pbtype, pis);
                        return pis;
                    }
                    else
                    {
                        return cacheParamType[pbtype];
                    }
                }
            }
            finally
            {
                if (lockrw.IsReaderLockHeld)
                {
                    lockrw.ReleaseReaderLock();
                }
                if (lockrw.IsWriterLockHeld)
                {
                    lockrw.ReleaseWriterLock();
                }
            }

        }

        #endregion

        #region 构造函数
        private void Reset()
        {
            apiFullUrl = string.Concat(requestProtocol, "://", apiUrl, "/", apiVersion, "/", apiClass);
        }

        private void Initital()
        {
            Reset();
            httpClient = new HttpClientHelper();
            md5 = new MD5CryptionXG();
            cacheParamType = new Dictionary<Type, PropertyInfo[]>(10);
            lockrw = new ReaderWriterLock();
            httpClient.Encoding = encoder;
        }

        /// <summary>
        /// 使用默认的参数构造
        /// </summary>
        public QQXGProvider()
        {
            Initital();
        }

        public QQXGProvider(string apiclass)
        {
            this.apiClass = apiclass;
            Initital();
        }

        public QQXGProvider(string apiurl, string apiversion, string apiclass)
        {
            this.apiUrl = apiurl;
            this.apiVersion = apiversion;
            this.apiClass = apiclass;
            Initital();
        }

        #endregion

        #region 静态方法

        /// 获取时间戳  
        /// </summary>  
        /// <returns></returns>  
        public static uint GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToUInt32(ts.TotalSeconds);
        }

        #endregion

        #region 公开的操作方法

        //public XGResult<TResult> PushMethod<TParma, TResult>(XGMethod method, TParma parm)
        //    where TParma : XGPushParamBase
        //{
        //    XGResult<TResult> result = null;
        //    switch (method)
        //    {
        //        case XGMethod.single_device:
        //            break;
        //        case XGMethod.single_account:
        //            break;
        //        case XGMethod.all_device:
        //            result = PushAllDevices(parm as XGPushAllDeviceParam);
        //            break;
        //        case XGMethod.tags_device:
        //            break;
        //        case XGMethod.get_msg_status:
        //            break;
        //        case XGMethod.get_app_device_num:
        //            break;
        //        case XGMethod.query_app_tags:
        //            break;
        //        case XGMethod.cancel_timing_task:
        //            break;
        //        default:
        //            break;
        //    }

        //    return result;
        //}


        /// <summary>
        /// PUSH消息给单个设备
        /// </summary>
        /// <param name="sdParm"></param>
        /// <returns></returns>
        public XGResult<string> PushSingleDevice(XGPushSingleDeviceParam sdParm)
        {
            if (string.IsNullOrEmpty(sdParm.Device_token)) throw new ArgumentException("sdParm.Device_token参数不能为空!");
            ParamBaseValidata(sdParm);
            PushParamBaseValidata(sdParm);
            ParamBaseCreate(sdParm, XGMethod.single_device);

            string pushSingleDeviceUrl = string.Format("{0}/{1}", apiFullUrl, XGMethod.single_device.ToString());
            string postData = CreateXGParamPostStr<XGPushSingleDeviceParam>(sdParm, "&");
            System.Diagnostics.Trace.Write("\r\n===postData的值：" + postData);

            string result = httpClient.Post(pushSingleDeviceUrl, postData);
            System.Diagnostics.Trace.Write("\r\n===返回result的值：" + result);
            return JsonHelper.FromJson<XGResult<string>>(result);

        }

        /// <summary>
        /// PUSH消息给单个账户或别名
        /// </summary>
        /// <param name="saParm"></param>
        /// <returns></returns>
        public XGResult<string> PushSingleAccount(XGPushSingleAccountParam saParm)
        {
            if (string.IsNullOrEmpty(saParm.Account)) throw new ArgumentException("saParm.Account参数不能为空!");
            ParamBaseValidata(saParm);
            PushParamBaseValidata(saParm);
            ParamBaseCreate(saParm, XGMethod.single_account);

            string pushSingleAccountUrl = string.Format("{0}/{1}", apiFullUrl, XGMethod.single_account.ToString());
            string postData = CreateXGParamPostStr<XGPushSingleAccountParam>(saParm, "&");
            System.Diagnostics.Trace.Write("\r\n===postData的值：" + postData);

            string result = httpClient.Post(pushSingleAccountUrl, postData);
            System.Diagnostics.Trace.Write("\r\n===返回result的值：" + result);
            return JsonHelper.FromJson<XGResult<string>>(result);

        }

        /// <summary>
        /// PUSH消息到所有设备
        /// </summary>
        /// <param name="adParm"></param>
        /// <returns></returns>
        public XGResult<XGPushResult> PushAllDevices(XGPushAllDeviceParam adParm)
        {
            ParamBaseValidata(adParm);
            PushParamBaseValidata(adParm);
            ParamBaseCreate(adParm, XGMethod.all_device);
            string pushAllDeviceUrl = string.Format("{0}/{1}", apiFullUrl, XGMethod.all_device.ToString());
            string postData = CreateXGParamPostStr<XGPushAllDeviceParam>(adParm, "&");
            System.Diagnostics.Trace.Write("\r\n===postData的值：" + postData);

            string result = httpClient.Post(pushAllDeviceUrl, postData);
            System.Diagnostics.Trace.Write("\r\n===返回result的值：" + result);
            return JsonHelper.FromJson<XGResult<XGPushResult>>(result);

        }

        /// <summary>
        /// PUSH消息给tags指定的设备
        /// </summary>
        /// <param name="tdParm"></param>
        /// <returns></returns>
        public XGResult<XGPushResult> PushTagsDevice(XGPushTagsDeviceParam tdParm)
        {
            if (tdParm.Tags_list == null || tdParm.Tags_list.Count <= 0) throw new ArgumentException("tdParm.Tags_list参数不能为空!或者个数不能为零。");
            if (string.IsNullOrEmpty(tdParm.Tags_op)) throw new ArgumentException("tdParm.Tags_op参数不能为空!");
            if (!tdParm.Tags_op.Equals("AND") && !tdParm.Tags_op.Equals("OR"))
            {
                throw new ArgumentException("tdParm.Tags_op参数只能为（AND，OR）2个参数之一!");
            }
            ParamBaseValidata(tdParm);
            PushParamBaseValidata(tdParm);
            ParamBaseCreate(tdParm, XGMethod.tags_device);
            string pushTagsDeviceUrl = string.Format("{0}/{1}", apiFullUrl, XGMethod.tags_device.ToString());
            string postData = CreateXGParamPostStr<XGPushTagsDeviceParam>(tdParm, "&");
            System.Diagnostics.Trace.Write("\r\n===postData的值：" + postData);

            string result = httpClient.Post(pushTagsDeviceUrl, postData);
            System.Diagnostics.Trace.Write("\r\n===返回result的值：" + result);
            return JsonHelper.FromJson<XGResult<XGPushResult>>(result);

        }

        /// <summary>
        /// 查询群发消息发送状态
        /// </summary>
        /// <param name="gsParm"></param>
        /// <returns></returns>
        public XGResult<XGGetMsgStatusResult> PushGetMsgStatus(XGPushGetMsgStatusParam gsParm)
        {
            if (gsParm.Push_ids == null || gsParm.Push_ids.Count <= 0) throw new ArgumentException("tdParm.Tags_list参数不能为空!或者个数不能为零。");
            ParamBaseValidata(gsParm);
            //PushParamBaseValidata(gsParm);
            ParamBaseCreate(gsParm, XGMethod.get_msg_status);
            string getMsgStatusUrl = string.Format("{0}/{1}", apiFullUrl, XGMethod.get_msg_status.ToString());
            string postData = CreateXGParamPostStr<XGPushGetMsgStatusParam>(gsParm, "&");
            System.Diagnostics.Trace.Write("\r\n===postData的值：" + postData);

            string result = httpClient.Post(getMsgStatusUrl, postData);
            System.Diagnostics.Trace.Write("\r\n===返回result的值：" + result);
            return JsonHelper.FromJson<XGResult<XGGetMsgStatusResult>>(result);

        }


        /// <summary>
        /// 查询应用覆盖的设备数，注意只有apiClass=application的时候才有效
        /// </summary>
        /// <param name="gadnParm"></param>
        /// <returns></returns>
        public XGResult<XGDeviceNumResult> GetAppDeviceNum(XGPushGetAppDeviceNumParam gadnParm)
        {
            if (!apiClass.Equals("application")) throw new ArgumentException("请设置apiClass=application！");
            ParamBaseValidata(gadnParm);
            //PushParamBaseValidata(gsParm);
            ParamBaseCreate(gadnParm, XGMethod.get_app_device_num);
            string getAppDeviceNumUrl = string.Format("{0}/{1}", apiFullUrl, XGMethod.get_app_device_num.ToString());
            string postData = CreateXGParamPostStr<XGPushGetAppDeviceNumParam>(gadnParm, "&");
            System.Diagnostics.Trace.Write("\r\n===postData的值：" + postData);

            string result = httpClient.Post(getAppDeviceNumUrl, postData);
            System.Diagnostics.Trace.Write("\r\n===返回result的值：" + result);
            return JsonHelper.FromJson<XGResult<XGDeviceNumResult>>(result);

        }

        /// <summary>
        /// 查询应用的Tags，注意只有apiClass=tags的时候才有效
        /// </summary>
        /// <param name="qatParm"></param>
        /// <returns></returns>
        public XGResult<XGQueryAppTagsResult> QueryAppTags(XGPushQueryAppTagsParam qatParm)
        {
            if (!apiClass.Equals("tags")) throw new ArgumentException("请设置apiClass=tags！");
            ParamBaseValidata(qatParm);
            //PushParamBaseValidata(gsParm);
            ParamBaseCreate(qatParm, XGMethod.query_app_tags);
            string queryAppTagsUrl = string.Format("{0}/{1}", apiFullUrl, XGMethod.query_app_tags.ToString());
            string postData = CreateXGParamPostStr<XGPushQueryAppTagsParam>(qatParm, "&");
            System.Diagnostics.Trace.Write("\r\n===postData的值：" + postData);

            string result = httpClient.Post(queryAppTagsUrl, postData);
            System.Diagnostics.Trace.Write("\r\n===返回result的值：" + result);
            return JsonHelper.FromJson<XGResult<XGQueryAppTagsResult>>(result);

        }


        /// <summary>
        /// 取消尚未触发的定时群发任务
        /// </summary>
        /// <param name="cttParm"></param>
        /// <returns></returns>
        public XGResult<XGStatusResult> PushCancelTimingTask(XGOtherCommonParam cttParm)
        {
            ParamBaseValidata(cttParm);
            //PushParamBaseValidata(gsParm);
            ParamBaseCreate(cttParm, XGMethod.cancel_timing_task);
            string cancelTimingTaskUrl = string.Format("{0}/{1}", apiFullUrl, XGMethod.cancel_timing_task.ToString());
            string postData = CreateXGParamPostStr<XGOtherCommonParam>(cttParm, "&");
            System.Diagnostics.Trace.Write("\r\n===postData的值：" + postData);

            string result = httpClient.Post(cancelTimingTaskUrl, postData);
            System.Diagnostics.Trace.Write("\r\n===返回result的值：" + result);
            return JsonHelper.FromJson<XGResult<XGStatusResult>>(result);

        }

        #endregion

        #region 私有辅助方法

        /// <summary>
        /// 公共参数验证
        /// </summary>
        /// <param name="xgb"></param>
        private void ParamBaseValidata(XGParamBase xgb)
        {
            if (xgb == null) throw new ArgumentException("xgb参数不能为空！");
            if (httpClient == null) throw new ArgumentException("httpClient不能为空！");
            if (xgb.Access_id <= 0) throw new ArgumentException("xgb.Access_id参数错误！");
            if (xgb.Secret_Key == null) throw new ArgumentException("xgb.Secret_Key参数错误！");
        }
        /// <summary>
        /// PUSH参数验证
        /// </summary>
        /// <param name="xgb"></param>
        private void PushParamBaseValidata(XGPushParamBase xgppb)
        {
            if (xgppb == null) throw new ArgumentException("xgppb参数不能为空！");
            if (xgppb.Message_type < 0) throw new ArgumentException("xgppb.Message_type参数错误！");
            if (xgppb.Message == null) throw new ArgumentException("xgppb.Message参数错误！");
        }

        /// <summary>
        /// 基本参数生成
        /// </summary>
        /// <param name="xgppb"></param>
        private void ParamBaseCreate(XGParamBase xgppb, XGMethod xgm)
        {
            if (!xgppb.Timestamp.HasValue) xgppb.Timestamp = GetTimeStamp();
            if (string.IsNullOrEmpty(xgppb.Sign))
            {
                string signUrl = string.Format("{0}/{1}/{2}/{3}", apiUrl, apiVersion, apiClass, xgm.ToString());
                xgppb.Sign = CreateRequestSign(xgppb, signUrl);
                System.Diagnostics.Trace.Write("\r\n===Sign-MD5后的值：" + xgppb.Sign);
            }
        }

        /// <summary>
        /// 解析对象值到POST格式，如果为NULL，则不解析
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xpb"></param>
        /// <param name="splitStr"></param>
        /// <returns></returns>
        private string CreateXGParamPostStr<T>(T xpb, string splitStr, bool isUrlEncode = true)
        {
            string postbase = null;
            //升序排序名称,到时候在把属性缓存起来就行了
            //已经加入缓存-2014-10-08
            PropertyInfo[] pis = GetCacheParamType<T>(xpb);//xpb.GetType().GetProperties().OrderBy(p => p.Name).ToArray();
            object tmpvalue = null;
            foreach (PropertyInfo item in pis)
            {
                tmpvalue = item.GetValue(xpb, null);
                if (tmpvalue != null)
                {
                    if (item.Name.Equals("secret_key", StringComparison.InvariantCultureIgnoreCase))
                        continue;
                    
                    //特殊的属性处理为json格式
                    if (item.Name.Equals("Message", StringComparison.CurrentCultureIgnoreCase)
                        || item.Name.Equals("Tags_list", StringComparison.CurrentCultureIgnoreCase)
                        || item.Name.Equals("Push_ids", StringComparison.CurrentCultureIgnoreCase)
                        )
                    {
                        tmpvalue = JsonHelper.ToJson(tmpvalue);
                    }
                    if (isUrlEncode)
                    {
                        postbase += HttpUtility.UrlEncode(item.Name.ToLower()) + "=" + HttpUtility.UrlEncode(tmpvalue.ToString()) + splitStr;
                    }
                    else
                    {
                        postbase += item.Name.ToLower() + "=" + tmpvalue.ToString() + splitStr;
                    }
                }
            }
            if (!string.IsNullOrEmpty(postbase)) return postbase.TrimEnd(splitStr.ToCharArray());
            else return "";
        }

        /// <summary>
        /// 创建内容签名
        /// 生成规则： 
        ///A）提取请求方法method（GET或POST）； 
        ///B）提取请求url信息，包括Host字段的IP或域名和URI的path部分，注意不包括Host的端口和Path的querystring。请在请求中带上Host字段，否则将视为无效请求。
        /// 比如openapi.xg.qq.com/v2/push/single_device或者10.198.18.239/v2/push/single_device;
        /// C）将请求参数（不包括sign参数）格式化成K=V方式； 
        ///注意：计算sign时所有参数不应进行urlencode； 
        ///D）将格式化后的参数以K的字典序升序排列，拼接在一起， 
        ///E）拼接请求方法、url、排序后格式化的字符串以及应用的secret_key； 
        ///F）将E形成字符串计算MD5值，形成一个32位的十六进制（字母小写）字符串，即为本次请求sign（签名）的值； 
        ///Sign=MD5($http_method$url$k1=$v1$k2=$v2$secret_key); 该签名值基本可以保证请求是合法者发送且参数没有被修改，但无法保证不被偷窥。
        /// 例如： POST请求到接口http://openapi.xg.qq.com/v2/push/single_device，
        ///有四个参数，access_id=123，timestamp=1386691200，Param1=Value1，Param2=Value2，secret_key为abcde。
        ///则上述E步骤拼接出的字符串为POSTopenapi.xg.qq.com/v2/push/single_deviceParam1=Value1Param2=Value2access_id=123timestamp=1386691200abcde，
        ///计算出该字符串的MD5为ccafecaef6be07493cfe75ebc43b7d53，以此作为sign参数的值
        /// </summary>
        /// <param name="xpb"></param>
        /// <param name="requestUrl">地址不要包含协议头</param>
        /// <returns></returns>
        private string CreateRequestSign(XGParamBase xpb, string requestUrl)
        {
            string noJoinPostData = CreateXGParamPostStr<XGParamBase>(xpb, "", false);
            string sign = string.Format("{0}{1}{2}{3}", requestMethod, requestUrl, noJoinPostData, xpb.Secret_Key);
            System.Diagnostics.Trace.Write("\r\n===Sign拼接的格式：" + sign);
            return md5.GenerateMD5(sign).ToLower();
        }

        #endregion

        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            if (md5 != null) md5.Dispose();
        }
    }
}
