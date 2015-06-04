using System;
using System.Collections.Generic;
using System.Text;

///  author:jasnature from http://www.cnblogs.com/NatureSex/
namespace NSTool.XGPush.Base
{
    /// <summary>    
    /// 上传数据参数
        ///  author:jasnature from http://www.cnblogs.com/NatureSex/
    /// </summary>    
    public class UploadEventArgs : EventArgs
    {
        int bytesSent;
        int totalBytes;
        /// <summary>    
        /// 已发送的字节数    
        /// </summary>    
        public int BytesSent
        {
            get { return bytesSent; }
            set { bytesSent = value; }
        }
        /// <summary>    
        /// 总字节数    
        /// </summary>    
        public int TotalBytes
        {
            get { return totalBytes; }
            set { totalBytes = value; }
        }
    }
}
