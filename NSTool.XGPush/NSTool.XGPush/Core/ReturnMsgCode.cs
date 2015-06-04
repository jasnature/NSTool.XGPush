using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    ///  author:jasnature from http://www.cnblogs.com/NatureSex/
namespace NSTool.XGPush.Core
{
    /// <summary>
    /// 返回消息码
    /// </summary>
    [Serializable]
    public enum ReturnMsgCode
    {
        /// <summary>
        /// 调用成功 
        /// </summary>
        Call_Success = 0,

        /// <summary>
        /// 参数错误 
        /// </summary>
        Param_Error = -1,

        /// <summary>
        /// 请求时间戳不在有效期内 
        /// </summary>
        Timestamp_Invalid_Error = -2,

        /// <summary>
        /// sign校验无效 
        /// </summary>
        Sign_Error = -3,

        /// <summary>
        /// 鉴权错误 
        /// </summary>
        Authentication_Error = 20,

        /// <summary>
        /// 推送的token没有在信鸽中注册 
        /// </summary>
        TokenNoFound_Error = 40,

        /// <summary>
        /// 推送的账号没有在信鸽中注册 
        /// </summary>
        No_Registration_Error = 48,

        /// <summary>
        /// 消息字符数超限
        /// </summary>
        Msg_Too_Long_Error = 73,

        /// <summary>
        /// 请求过于频繁，请稍后再试 
        /// </summary>
        Request_Frequent_Error = 76,
        
        /// <summary>
        /// 内部错误 
        /// </summary>
        Server_Error = 500
               
    }
}
