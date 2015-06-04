using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Net.Cache;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Threading;

    ///  author:jasnature from http://www.cnblogs.com/NatureSex/
namespace NSTool.XGPush.Base
{
    /// <summary>
    /// Http同步访问-操作工具类(类似.NET中的WebClient)
    /// 如果不能满足需求！请使用WebClient
        ///  author:jasnature from http://www.cnblogs.com/NatureSex/
    /// </summary>
    public sealed class HttpClientHelper
    {

        static HttpClientHelper()
        {
            LoadCookiesFromDisk();
        }

        private Encoding encoding = Encoding.Default;
        string respHtml = "";
        private WebProxy proxy;
        private static CookieContainer cc;
        private WebHeaderCollection requestHeaders;
        private WebHeaderCollection responseHeaders;
        private int bufferSize = 65536;//缓冲字节大小

        /// <summary>
        /// 上传进度
        /// </summary>
        public event EventHandler<UploadEventArgs> UploadProgressChanged;

        /// <summary>
        /// 下载进度
        /// </summary>
        public event EventHandler<DownloadEventArgs> DownloadProgressChanged;

        #region 属性
        /// <summary>    
        /// 创建WebClient的实例    
        /// </summary>    
        public HttpClientHelper()
        {
            requestHeaders = new WebHeaderCollection();
            responseHeaders = new WebHeaderCollection();
        }
        /// <summary>    
        /// 设置发送和接收的数据缓冲大小    
        /// </summary>    
        public int BufferSize
        {
            get { return bufferSize; }
            set { bufferSize = value; }
        }
        /// <summary>    
        /// 获取响应头集合    
        /// </summary>    
        public WebHeaderCollection ResponseHeaders
        {
            get { return responseHeaders; }
        }
        /// <summary>    
        /// 获取请求头集合    
        /// </summary>    
        public WebHeaderCollection RequestHeaders
        {
            get { return requestHeaders; }
        }
        /// <summary>    
        /// 获取或设置代理    
        /// </summary>    
        public WebProxy Proxy
        {
            get { return proxy; }
            set { proxy = value; }
        }
        /// <summary>    
        /// 获取或设置请求与响应的文本编码方式    
        /// </summary>    
        public Encoding Encoding
        {
            get { return encoding; }
            set { encoding = value; }
        }
        /// <summary>    
        /// 获取或设置响应的html代码    
        /// </summary>    
        public string RespHtml
        {
            get { return respHtml; }
            set { respHtml = value; }
        }
        /// <summary>    
        /// 获取或设置与请求关联的Cookie容器    
        /// </summary>    
        public CookieContainer CookieContainer
        {
            get { return cc; }
            set { cc = value; }
        } 
        #endregion

        /// <summary>    
        ///  GET方式获取网页源代码    
        /// </summary>    
        /// <param name="url">网址</param>    
        /// <returns></returns>    
        public string Get(string url)
        {
            HttpWebRequest request = CreateRequest(url, "GET");
            respHtml = encoding.GetString(GetData(request));
            return respHtml;
        }
        /// <summary>    
        /// 下载网页源文件    
        /// </summary>    
        /// <param name="url">文件URL地址</param>    
        /// <param name="filename">文件保存完整路径</param>    
        public void Get(string url, string filename)
        {
            FileStream fs = null;
            try
            {
                HttpWebRequest request = CreateRequest(url, "GET");
                byte[] data = GetData(request);
                fs = new FileStream(filename, FileMode.Create, FileAccess.Write);
                
                fs.Write(data, 0, data.Length);
            }
            finally
            {
                if (fs != null) fs.Close();
            }
        }
        /// <summary>    
        /// 从指定URL下载字节数据
        /// </summary>    
        /// <param name="url">网址</param>    
        /// <returns></returns>    
        public byte[] GetHtmlByteData(string url)
        {
            HttpWebRequest request = CreateRequest(url, "GET");
            return GetData(request);
        }
        /// <summary>    
        /// 向指定URL发送文本数据    
        /// </summary>    
        /// <param name="url">网址</param>    
        /// <param name="postData">urlencode编码的文本数据</param>    
        /// <returns></returns>    
        public string Post(string url, string postData)
        {
            byte[] data = encoding.GetBytes(postData);
            return Post(url, data);
        }


        /// <summary>    
        /// 向指定URL发送字节数据，ContentType = "application/x-www-form-urlencoded"
        /// </summary>    
        /// <param name="url">网址</param>    
        /// <param name="postData">发送的字节数组</param>    
        /// <returns></returns>    
        public string Post(string url, byte[] postData)
        {
            HttpWebRequest request = CreateRequest(url, "POST");
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postData.Length;
            request.KeepAlive = true;
            PostData(request, postData);
            respHtml = encoding.GetString(GetData(request));
            return respHtml;
        }

        /// <summary>    
        /// 向指定网址发送mulitpart编码的数据    
        /// </summary>    
        /// <param name="url">网址</param>    
        /// <param name="mulitpartForm">进行Multipart形式的编码的内容</param>    
        /// <returns></returns>    
        public string Post(string url, MultipartEncode mulitpartForm)
        {
            HttpWebRequest request = CreateRequest(url, "POST");
            request.ContentType = mulitpartForm.ContentType;
            request.ContentLength = mulitpartForm.FormData.Length;
            request.KeepAlive = true;
            PostData(request, mulitpartForm.FormData);
            respHtml = encoding.GetString(GetData(request));
            return respHtml;
        }
        
        /// <summary>    
        /// 读取请求返回的数据    
        /// </summary>    
        /// <param name="request">请求对象</param>    
        /// <returns></returns>    
        private byte[] GetData(HttpWebRequest request)
        {
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            responseHeaders = response.Headers;
            //保存cookies到硬盘
            //SaveCookiesToDisk();

            DownloadEventArgs args = new DownloadEventArgs();
            //得到数据字节大小
            if (responseHeaders[HttpResponseHeader.ContentLength] != null)
                args.TotalBytes = Convert.ToInt32(responseHeaders[HttpResponseHeader.ContentLength]);

            MemoryStream ms = new MemoryStream();
            int count = 0;
            byte[] buf = new byte[bufferSize];
            while ((count = stream.Read(buf, 0, buf.Length)) > 0)
            {
                ms.Write(buf, 0, count);
                if (this.DownloadProgressChanged != null)
                {
                    args.BytesReceived += count;
                    args.ReceivedData = new byte[count];
                    Array.Copy(buf, args.ReceivedData, count);
                    this.DownloadProgressChanged(this, args);
                }
            }
            
            //是否要解压流    
            if (ResponseHeaders[HttpResponseHeader.ContentEncoding] != null)
            {
                MemoryStream msTemp = new MemoryStream();
                count = 0;
                buf = new byte[100];
                switch (ResponseHeaders[HttpResponseHeader.ContentEncoding].ToLower())
                {
                    case "gzip":
                        GZipStream gzip = new GZipStream(ms, CompressionMode.Decompress);
                        while ((count = gzip.Read(buf, 0, buf.Length)) > 0)
                        {
                            msTemp.Write(buf, 0, count);
                        }
                        try
                        {
                            return msTemp.ToArray();
                        }
                        finally
                        {
                            gzip.Flush();
                            gzip.Close();
                        }
                    case "deflate":
                        DeflateStream deflate = new DeflateStream(ms, CompressionMode.Decompress);
                        while ((count = deflate.Read(buf, 0, buf.Length)) > 0)
                        {
                            msTemp.Write(buf, 0, count);
                        }
                        try
                        {
                            return msTemp.ToArray();
                        }
                        finally
                        {
                            deflate.Flush();
                            deflate.Close();
                        }
                    default:
                        break;
                }
                msTemp.Flush();
                msTemp.Close();
            }
            
            try
            {
                return ms.ToArray();
            }
            finally
            {
                ms.Flush();
                ms.Close();
                stream.Flush();
                stream.Close();
            }
        }
        /// <summary>    
        /// 发送请求数据    
        /// </summary>    
        /// <param name="request">请求对象</param>    
        /// <param name="postData">请求发送的字节数组</param>    
        private void PostData(HttpWebRequest request, byte[] postData)
        {
            int offset = 0;
            int sendBufferSize = bufferSize;
            int remainBytes = 0;
            Stream stream = request.GetRequestStream();
            UploadEventArgs args = new UploadEventArgs();
            args.TotalBytes = postData.Length;
            while ((remainBytes = postData.Length - offset) > 0)
            {
                if (sendBufferSize > remainBytes) sendBufferSize = remainBytes;
                stream.Write(postData, offset, sendBufferSize);
                offset += sendBufferSize;
                if (this.UploadProgressChanged != null)
                {
                    args.BytesSent = offset;
                    this.UploadProgressChanged(this, args);
                }
            }
            stream.Flush();
            stream.Close();
        }

        /// <summary>    
        /// 创建一个HTTP请求
        /// </summary>    
        /// <param name="url">URL地址</param>   
        /// <param name="method">请求方法(POST,GET)</param> 
        /// <returns></returns>    
        private HttpWebRequest CreateRequest(string url, string method)
        {
            Uri uri = new Uri(url);

            if (uri.Scheme == "https")
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(this.CheckValidationResult);

            // Set a default policy level for the "http:" and "https" schemes.
            //设置一个默认的缓存策略给http和https配置
            HttpRequestCachePolicy policy = new HttpRequestCachePolicy(HttpRequestCacheLevel.Revalidate);
            HttpWebRequest.DefaultCachePolicy = policy;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AllowAutoRedirect = false;
            request.AllowWriteStreamBuffering = false;
            request.Method = method;
            if (proxy != null) 
                request.Proxy = proxy;
            request.CookieContainer = cc;
            foreach (string key in requestHeaders.Keys)
            {
                request.Headers.Add(key, requestHeaders[key]);
            }
            requestHeaders.Clear();
            return request;
        }


        private bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }


        /// <summary>    
        /// 将Cookie保存到磁盘    
        /// </summary>    
        private static void SaveCookiesToDisk()
        {
            string cookieFile = System.Environment.GetFolderPath(Environment.SpecialFolder.Cookies) + "\\HttpClient.cookie";
            FileStream fs = null;
            try
            {
                fs = new FileStream(cookieFile, FileMode.Create);
                BinaryFormatter formater = new BinaryFormatter();
                formater.Serialize(fs, cc);
            }
            finally
            {
                if (fs != null)
                {
                    fs.Flush();
                    fs.Close();
                }
            }
        }
        /// <summary>    
        /// 从磁盘加载Cookie    
        /// </summary>    
        private static void LoadCookiesFromDisk()
        {
            cc = new CookieContainer();
            string cookieFile = System.Environment.GetFolderPath(Environment.SpecialFolder.Cookies) + "\\HttpClient.cookie";
            if (!System.IO.File.Exists(cookieFile))
                return;
            FileStream fs = null;
            try
            {
                fs = new FileStream(cookieFile, FileMode.Open, FileAccess.Read, FileShare.Read);
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formater = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                cc = (CookieContainer)formater.Deserialize(fs);
            }
            finally
            {
                if (fs != null)
                {
                    fs.Flush();
                    fs.Close();
                } 
            }
        }

       

    }
}
