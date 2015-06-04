using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    ///  author:jasnature from http://www.cnblogs.com/NatureSex/
namespace NSTool.XGPush.Core
{
    /// <summary>
    /// 信鸽操作接口枚举,如查询、设置、删除等
    /// </summary>
    [Serializable]
    public enum XGMethod
    {
        /// <summary>
        /// Push消息（包括通知和透传消息）给单个设备
        /// </summary>
        single_device = 0,

        /// <summary>
        /// Push消息（包括通知和透传消息）给单个账户或别名
        /// </summary>
        single_account = 1,

        /// <summary>
        /// Push消息（包括通知和透传消息）给app的所有设备
        /// </summary>
        all_device = 2,

        /// <summary>
        /// Push消息（包括通知和透传消息）给tags指定的设备
        /// </summary>
        tags_device = 4,

        /// <summary>
        /// 查询群发消息发送状态
        /// </summary>
        get_msg_status = 8,

        /// <summary>
        /// 查询应用覆盖的设备数
        /// </summary>
        get_app_device_num = 16,

        /// <summary>
        /// 查询应用的Tags
        /// </summary>
        query_app_tags = 32,

        /// <summary>
        /// 取消尚未触发的定时群发任务
        /// </summary>
        cancel_timing_task = 64

    }
}
