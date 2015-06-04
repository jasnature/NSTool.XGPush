using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    ///  author:jasnature from http://www.cnblogs.com/NatureSex/
namespace NSTool.XGPush.Core
{

    /// <summary>
    /// 基本返回消息
    /// </summary>
    /// <typeparam name="TResult">result结果的具体类型</typeparam>
    [Serializable]
    public class XGResult<TResult>
    {
        private ReturnMsgCode ret_code;

        /// <summary>
        /// 返回码
        /// </summary>
        public ReturnMsgCode Ret_code
        {
            get { return ret_code; }
            set { ret_code = value; }
        }

        private string err_msg;

        /// <summary>
        /// 请求出错时的错误信息 
        /// </summary>
        public string Err_msg
        {
            get { return err_msg; }
            set { err_msg = value; }
        }

        private TResult result;

        /// <summary>
        /// 返回结果，如果不清楚就是string的json类型
        /// </summary>
        public TResult Result
        {
            get { return result; }
            set { result = value; }
        }
       
    }
}
