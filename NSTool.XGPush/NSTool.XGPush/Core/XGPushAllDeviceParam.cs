using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;


    ///  author:jasnature from http://www.cnblogs.com/NatureSex/
namespace NSTool.XGPush.Core
{
    /// <summary>
    /// Push消息（包括通知和透传消息）给app的所有设备的参数
    /// </summary>
    [Serializable]
    public class XGPushAllDeviceParam : XGPushParamBase
    {
        //备注，目前继承XGPushParamBase就满足要求
    }

    /// <summary>
    /// 查询应用覆盖的设备数
    /// </summary>
   [Serializable]
    public class XGPushGetAppDeviceNumParam : XGParamBase
    {
        //备注，目前继承XGParamBase就满足要求
    }
}
