using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSTool.XGPush;
using NSTool.XGPush.Base;
using NSTool.XGPush.Core;
using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace FrameworkUnitTest
{
    /// <summary>
    /// 部分QQ信鸽单元测试，其他功能类推
    /// create by http://www.cnblogs.com/NatureSex/
    /// author:jasnature
    /// 
    /// 
    /// 
    /// 请安装对应 信鸽测试的APK或者公司的测试apk进行测试
    /// </summary>
    [TestClass]
    public class QQXGUnit
    {
        
        /// <summary>
        /// 推送所有设备
        /// </summary>
        [TestMethod]
        public void XGTestAllDevice()
        {
            QQXGProvider qqxg = new QQXGProvider();
            XGPushAllDeviceParam xgp = new XGPushAllDeviceParam();
            xgp.Timestamp = null; //1299865775;
            xgp.Valid_time = 600; //600;
            xgp.Sign = null;
            //实际key请使用实际的，下面仅演示作用
            //============测试key1==============
            xgp.Access_id = 2100025233;
            //xgp.Access_Key = "ARQ4CB14Q92X";
            xgp.Secret_Key = "6ae193c85570ad1cc8fc9540560093b1";
            //============测试key2==============
            //xgp.Access_id = 2100025346;
            //xgp.Access_Key = "AUP1I5W741WJ";
            //xgp.Secret_Key = "f2391810bc98c0d7435ec7c96b8f524f";
            xgp.Message_type = 1;
            xgp.Expire_time = 3600;
            xgp.Message = new NotifyMessage()
            {
                Custom_content = new  SerializableDictionary<string,string>(){
                  {"type","123"},{"type1","456"}
                },
                //Accept_time = new System.Collections.Generic.List<AcceptTime>(){
                //  new AcceptTime(){ Start=new XGTime(){ Hour="18", Min="02"}, End=new XGTime(){ Hour="18", Min="30"}}
                //},
                Clearable = 1,
                Title = "XGTestAllDevice-50%",
                Content = "有中文10%，人们有1%以及，$abcdef", //中文测试
                Vibrate = 1,
                Ring = 1,
                Action = new NotifyMessageAction()
                {
                    Action_type = 2,
                    Browser = new NotifyMessageAction_Browser()
                    {
                        Url = "http://baidu.com",
                        Confirm = 0
                    },
                    Intent = "http://baidu.com",
                    Activity = "XGPushDemo"
                }
            };
            //xgp.Send_time = DateTime.Now.AddMinutes(2).ToString("yyyy-MM-dd HH:mm:ss");

            //POSTopenapi.xg.qq.com/v2/push/all_deviceaccess_id=2100025233timestamp=1399859926valid_time=6006ae193c85570ad1cc8fc9540560093b1
            //string xml = SerializeToXML<XGPushAllDeviceParam>(xgp);
            //XGPushAllDeviceParam t = DeSerializeXML<XGPushAllDeviceParam>(xml);
            XGResult<XGPushResult> a = qqxg.PushAllDevices(xgp);
            //Console.WriteLine(xml);
            //Console.WriteLine(t.ToString());
        }


        /// <summary>
        /// 单个 Device_token
        /// </summary>
        [TestMethod]
        public void XGTestSingleDevice()
        {
            QQXGProvider qqxg = new QQXGProvider();
            XGPushSingleDeviceParam xgp = new XGPushSingleDeviceParam();
            xgp.Timestamp = null;
            xgp.Valid_time = 600; //600;
            xgp.Sign = null;
            //============测试DEMO==============
            xgp.Access_id = 2100025233;
            //xgp.Access_Key = "ARQ4CB14Q92X";
            xgp.Secret_Key = "6ae193c85570ad1cc8fc9540560093b1";
            //============测试DEMO==============
            //xgp.Access_id = 2100025346;
            //xgp.Access_Key = "AUP1I5W741WJ";
            //xgp.Secret_Key = "f2391810bc98c0d7435ec7c96b8f524f";
            xgp.Message_type = 1;
            xgp.Message = new NotifyMessage()
            {
                Title = "XGTestSingleDevice",
                Content = "XGTestSingleDevice",
                Vibrate = 1
            };
            xgp.Device_token = "ef8d1c5b866652d14e62bbee2aac3b28b7ed2bf9";
            //POSTopenapi.xg.qq.com/v2/push/all_deviceaccess_id=2100025233timestamp=1399859926valid_time=6006ae193c85570ad1cc8fc9540560093b1
            XGResult<string> a = qqxg.PushSingleDevice(xgp);
        }

        /// <summary>
        /// 单个账户
        /// </summary>
        [TestMethod]
        public void XGTestSingleAccount()
        {
            QQXGProvider qqxg = new QQXGProvider();
            XGPushSingleAccountParam xgp = new XGPushSingleAccountParam();
            xgp.Timestamp = null;
            xgp.Valid_time = 600; //600;
            xgp.Sign = null;
            //============测试DEMO==============
            //xgp.Access_id = 2100025233;
            //xgp.Access_Key = "ARQ4CB14Q92X";
            //xgp.Secret_Key = "6ae193c85570ad1cc8fc9540560093b1";
            //============测试DEMO==============
            xgp.Access_id = 2100025346;
            //xgp.Access_Key = "AUP1I5W741WJ";
            xgp.Secret_Key = "f2391810bc98c0d7435ec7c96b8f524f";
            xgp.Message_type = 1;
            xgp.Message = new NotifyMessage()
            {
                Title = "XGTestSingleAccount",
                Content = "XGTestSingleAccount",
                Vibrate = 1,
                Ring = 1
            };
            xgp.Account = "ffff";
            //POSTopenapi.xg.qq.com/v2/push/all_deviceaccess_id=2100025233timestamp=1399859926valid_time=6006ae193c85570ad1cc8fc9540560093b1
            XGResult<string> a = qqxg.PushSingleAccount(xgp);
        }

        /// <summary>
        /// 按标签推送
        /// </summary>
        [TestMethod]
        public void XGTestTagsDevice()
        {
            QQXGProvider qqxg = new QQXGProvider();
            XGPushTagsDeviceParam xgp = new XGPushTagsDeviceParam();
            xgp.Timestamp = null;
            xgp.Valid_time = 600; //600;
            xgp.Sign = null;
            //============测试DEMO==============
            //xgp.Access_id = 2100025233;
            //xgp.Access_Key = "ARQ4CB14Q92X";
            //xgp.Secret_Key = "6ae193c85570ad1cc8fc9540560093b1";
            //============测试DEMO==============
            xgp.Access_id = 2100025346;
            //xgp.Access_Key = "AUP1I5W741WJ";
            xgp.Secret_Key = "f2391810bc98c0d7435ec7c96b8f524f";
            xgp.Message_type = 1;
            xgp.Message = new NotifyMessage()
            {
                Title = "XGTestTagsDevice",
                Content = "XGTestTagsDevice",
                Vibrate = 1,
                Ring = 1
            };
            xgp.Tags_list = new System.Collections.Generic.List<string>();
            xgp.Tags_list.Add("nature");
            xgp.Tags_op = "OR";
            //POSTopenapi.xg.qq.com/v2/push/all_deviceaccess_id=2100025233timestamp=1399859926valid_time=6006ae193c85570ad1cc8fc9540560093b1
            XGResult<XGPushResult> a = qqxg.PushTagsDevice(xgp);
            string a1 = a.Err_msg;
        }

        /// <summary>
        /// 消息状态
        /// </summary>
        [TestMethod]
        public void XGTestGetMsgStatus()
        {
            QQXGProvider qqxg = new QQXGProvider();
            XGPushGetMsgStatusParam xgp = new XGPushGetMsgStatusParam();
            xgp.Timestamp = null;
            xgp.Valid_time = 600; //600;
            xgp.Sign = null;
            //============测试DEMO==============
            //xgp.Access_id = 2100025233;
            //xgp.Access_Key = "ARQ4CB14Q92X";
            //xgp.Secret_Key = "6ae193c85570ad1cc8fc9540560093b1";
            //============测试DEMO==============
            xgp.Access_id = 2100025346;
            //xgp.Access_Key = "AUP1I5W741WJ";
            xgp.Secret_Key = "f2391810bc98c0d7435ec7c96b8f524f";
            xgp.Push_ids = new System.Collections.Generic.List<Push_Id_Obj>();
            xgp.Push_ids.Add(new Push_Id_Obj() { Push_id = "152947" });
            xgp.Push_ids.Add(new Push_Id_Obj() { Push_id = "152944" });
            xgp.Push_ids.Add(new Push_Id_Obj() { Push_id = "139303" });
            XGResult<XGGetMsgStatusResult> a = qqxg.PushGetMsgStatus(xgp);
        }

        /// <summary>
        /// 应用添加的设备数
        /// </summary>
        [TestMethod]
        public void XGTestGetAppDeviceNum()
        {
            QQXGProvider qqxg = new QQXGProvider("application");
            XGPushGetAppDeviceNumParam xgp = new XGPushGetAppDeviceNumParam();
            xgp.Timestamp = null;
            xgp.Valid_time = 600; //600;
            xgp.Sign = null;
            //============测试DEMO==============
            //xgp.Access_id = 2100025233;
            //xgp.Access_Key = "ARQ4CB14Q92X";
            //xgp.Secret_Key = "6ae193c85570ad1cc8fc9540560093b1";
            //============测试DEMO==============
            xgp.Access_id = 2100025346;
            //xgp.Access_Key = "AUP1I5W741WJ";
            xgp.Secret_Key = "f2391810bc98c0d7435ec7c96b8f524f";
            XGResult<XGDeviceNumResult> a = qqxg.GetAppDeviceNum(xgp);
        }

        /// <summary>
        /// 查询标签
        /// </summary>
        [TestMethod]
        public void XGTestQueryAppTags()
        {
            QQXGProvider qqxg = new QQXGProvider("tags");
            XGPushQueryAppTagsParam xgp = new XGPushQueryAppTagsParam();
            xgp.Timestamp = null;
            xgp.Valid_time = 600; //600;
            xgp.Sign = null;
            //============测试DEMO==============
            //xgp.Access_id = 2100025233;
            //xgp.Access_Key = "ARQ4CB14Q92X";
            //xgp.Secret_Key = "6ae193c85570ad1cc8fc9540560093b1";
            //============测试DEMO==============
            xgp.Access_id = 2100025346;
            //xgp.Access_Key = "AUP1I5W741WJ";
            xgp.Secret_Key = "f2391810bc98c0d7435ec7c96b8f524f";

            XGResult<XGQueryAppTagsResult> a = qqxg.QueryAppTags(xgp);
        }

        /// <summary>
        /// 取消定时发送的任务
        /// </summary>
        [TestMethod]
        public void XGTestCancelTimingTask()
        {
            QQXGProvider qqxg = new QQXGProvider();
            XGOtherCommonParam xgp = new XGOtherCommonParam();
            xgp.Timestamp = null;
            xgp.Valid_time = 600; //600;
            xgp.Sign = null;
            //============测试DEMO==============
            //xgp.Access_id = 2100025233;
            //xgp.Access_Key = "ARQ4CB14Q92X";
            //xgp.Secret_Key = "6ae193c85570ad1cc8fc9540560093b1";
            //============测试DEMO==============
            xgp.Access_id = 2100025346;
            //xgp.Access_Key = "AUP1I5W741WJ";
            xgp.Secret_Key = "f2391810bc98c0d7435ec7c96b8f524f";
            xgp.Push_id = "139303";
            XGResult<XGStatusResult> a = qqxg.PushCancelTimingTask(xgp);
        }


        
        /// <summary>
        /// 测试数据序列化
        /// </summary>
        [TestMethod]
        public void XGTestJosn()
        {
            XGMessage xg = new XGMessage();
            xg.Custom_content = new SerializableDictionary<string, string>();
            xg.Custom_content.Add("k1", "123");
            xg.Custom_content.Add("k2", "456");
            xg.Accept_time = new System.Collections.Generic.List<AcceptTime>();
            xg.Accept_time.Add(new AcceptTime() { Start = new XGTime() { Hour = "09", Min = "00" }, End = new XGTime() { Hour = "12", Min = "00" } });
            xg.Accept_time.Add(new AcceptTime() { Start = new XGTime() { Hour = "13", Min = "00" }, End = new XGTime() { Hour = "15", Min = "00" } });
            string xml = SerializeToXML<XGMessage>(xg);
            Console.WriteLine(xml);
            string a = JsonHelper.ToJson(xg);
            Console.WriteLine(a);
        }


        //"localhost/TestHttp", "home", "index"

        /// <summary>
        /// 序列化对象为的XML数据
        /// </summary>
        /// <param name="data">数据内容</param>
        private string SerializeToXML<T>(T data)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                new XmlSerializer(typeof(T)).Serialize(ms, data);
                return Encoding.UTF8.GetString(ms.ToArray());
            }

        }

        /// <summary>
        /// 从流中反序列化一个XML文件
        /// </summary>
        /// <param name="stream">数据流</param>
        /// <returns></returns>
        public T DeSerializeXML<T>(string xml)
        {
            using (TextReader tr = new StringReader(xml))
            {
                return (T)new XmlSerializer(typeof(T)).Deserialize(tr);
            }
        }
    }
}
