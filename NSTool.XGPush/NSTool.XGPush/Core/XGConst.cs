using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    ///  author:jasnature from http://www.cnblogs.com/NatureSex/
namespace NSTool.XGPush.Core
{
    /// <summary>
    /// 信鸽一些cont参数
    /// </summary>
    public struct XGConst
    {
        public static readonly string SECRET_KEY = "secret_key";

        #region 提供的接口类别名称

        /// <summary>
        /// 提供的接口类别名称-push
        /// </summary>
        public static readonly string CLASS_API_NAME_PUSH = "push";

        /// <summary>
        /// 提供的接口类别名称-application
        /// </summary>
        public static readonly string CLASS_API_NAME_APPLICATION = "application";

        /// <summary>
        /// 提供的接口类别名称-tags
        /// </summary>
        public static readonly string CLASS_API_NAME_TAGS = "tags";

        #endregion

        //#region 通用参数名称

        ///// <summary>
        ///// 应用的唯一标识符名称
        ///// </summary>
        //public static readonly string ACCESS_ID_NAME = "access_id";

        ///// <summary>
        ///// 本请求的unix时间戳，用于确认请求的有效期名称
        ///// </summary>
        //public static readonly string TIMESTAMP_NAME = "timestamp";

        ///// <summary>
        ///// 配合timestamp确定请求的有效期名称
        ///// </summary>
        //public static readonly string VALID_TIME_NAME = "valid_time";

        ///// <summary>
        /////内容签名名称
        ///// </summary>
        //public static readonly string SIGN_NAME = "sign";

        //#endregion

        //#region PUSH 消息的公共参数名称

        ///// <summary>
        ///// 消息类型名称
        ///// </summary>
        //public static readonly string MESSAGE_TYPE = "message_type";

        ///// <summary>
        ///// 提交的JSON消息内容参数名称
        ///// </summary>
        //public static readonly string MESSAGE = "message";

        ///// <summary>
        ///// 消息离线存储多久参数名称
        ///// </summary>
        //public static readonly string EXPIRE_TIME = "expire_time";

        ///// <summary>
        ///// 指定推送时间参数名称
        ///// </summary>
        //public static readonly string SEND_TIME = "send_time";

        ///// <summary>
        ///// 多包推送消息参数名称
        ///// </summary>
        //public static readonly string MULTI_PKG = "multi_pkg";

        ///// <summary>
        ///// IOS平台手机使用的推送环境参数，本字段对Android平台无效 
        ///// </summary>
        //public static readonly string ENVIRONMENT = "environment"; 

        //#endregion

    }
}
