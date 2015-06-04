using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.IO.Compression;
using System.Net.Cache;

namespace NSTool.XGPush.Base
{

    /// <summary>
    /// HTTP上传操作
    /// 对上传的文件和文本数据进行Multipart形式的编码
    /// 添加文件请使用AddFlie()添加字符串请使用AddString()
    ///  author:jasnature from http://www.cnblogs.com/NatureSex/
    /// </summary>    
    public sealed class MultipartEncode
    {
        private Encoding encoding;
        private MemoryStream ms;
        private string boundary;//分割线
        private byte[] formData;
        /// <summary>    
        /// 获取编码后的字节数组    
        /// </summary>    
        public byte[] FormData
        {
            get
            {
                if (formData == null)
                {
                    byte[] buffer = encoding.GetBytes("--" + this.boundary + "--\r\n");
                    ms.Write(buffer, 0, buffer.Length);
                    formData = ms.ToArray();
                    ms.Flush();
                    ms.Close();
                }
                return formData;
            }
        }
        /// <summary>    
        /// 获取此编码内容的类型    
        /// </summary>    
        public string ContentType
        {
            get { return string.Format("multipart/form-data; boundary={0}", this.boundary); }
        }
        /// <summary>    
        /// 获取或设置对字符串采用的编码类型    
        /// </summary>    
        public Encoding StringEncoding
        {
            set { encoding = value; }
            get { return encoding; }
        }
        /// <summary>    
        /// 实例化    
        /// </summary>    
        public MultipartEncode()
        {
            boundary = string.Format("--{0}--", Guid.NewGuid());
            ms = new MemoryStream();
            encoding = Encoding.Default;
        }
        /// <summary>    
        /// 添加一个文件    
        /// </summary>    
        /// <param name="name">文件域名称</param>    
        /// <param name="filename">文件的完整路径</param>    
        public void AddFlie(string name, string filename)
        {
            if (!System.IO.File.Exists(filename))
                throw new FileNotFoundException("尝试添加不存在的文件。", filename);
            FileStream fs = null;
            byte[] fileData = { };
            try
            {
                fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
                fileData = new byte[fs.Length];
                fs.Read(fileData, 0, fileData.Length);
                this.AddFlie(name, Path.GetFileName(filename), fileData, fileData.Length);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Flush(); fs.Close();
                }
            }
        }
        /// <summary>    
        /// 添加一个文件    
        /// </summary>    
        /// <param name="name">文件域名称</param>    
        /// <param name="filename">文件名</param>    
        /// <param name="fileData">文件二进制数据</param>    
        /// <param name="dataLength">二进制数据大小</param>    
        public void AddFlie(string name, string filename, byte[] fileData, int dataLength)
        {
            if (dataLength <= 0 || dataLength > fileData.Length)
            {
                dataLength = fileData.Length;
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("--{0}\r\n", this.boundary);
            sb.AppendFormat("Content-Disposition: form-data; name=\"{0}\";filename=\"{1}\"\r\n", name, filename);
            sb.AppendFormat("Content-Type: {0}\r\n", CommonHelper.GetContentType(filename));
            sb.Append("\r\n");
            byte[] buf = encoding.GetBytes(sb.ToString());
            ms.Write(buf, 0, buf.Length);
            ms.Write(fileData, 0, dataLength);
            byte[] crlf = encoding.GetBytes("\r\n");
            ms.Write(crlf, 0, crlf.Length);

        }
        /// <summary>    
        /// 添加字符串    
        /// </summary>    
        /// <param name="name">文本域名称</param>    
        /// <param name="value">文本值</param>    
        public void AddString(string name, string value)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("--{0}\r\n", this.boundary);
            sb.AppendFormat("Content-Disposition: form-data; name=\"{0}\"\r\n", name);
            sb.Append("\r\n");
            sb.AppendFormat("{0}\r\n", value);
            byte[] buf = encoding.GetBytes(sb.ToString());
            ms.Write(buf, 0, buf.Length);
        }

    }
}
