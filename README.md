# NSTool.XGPush
腾讯信鸽推送 使用C#语法开发的 .NET版 sdk封装，支持android和部分ios

部分例子：也可以参考代码里面的FrameworkUnitTest单元测试例子

//推送所有设备

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

{
"type","123"},{"type1","456"}
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
                Url = "http://xxxxxx.com",
                Confirm = 0
            },
            
Intent = "http://xxxxxxx.com",
            
Activity = "XGPushDemo"
        }
    
}


}
