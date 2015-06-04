using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    ///  author:jasnature from http://www.cnblogs.com/NatureSex/
namespace NSTool.XGPush.Core
{
    /// <summary>
    /// PUSH消息后返回的公共结果
    /// </summary>
    [Serializable]
    public class XGQueryAppTagsResult
    {
        private uint total;

        /// <summary>
        /// //应用的tag总数，注意不是本次查询返回的tag数
        /// </summary>
        public uint Total
        {
            get { return total; }
            set { total = value; }
        }

        /// <summary>
        /// tags
        /// </summary>
        private List<string> tags;

        public List<string> Tags
        {
            get { return tags; }
            set { tags = value; }
        }

    }

   
}
