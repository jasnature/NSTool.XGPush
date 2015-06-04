using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    ///  author:jasnature from http://www.cnblogs.com/NatureSex/
namespace NSTool.XGPush.Core
{
    /// <summary>
    /// 查询应用的Tags的参数
    /// </summary>
    [Serializable]
    public class XGPushQueryAppTagsParam : XGParamBase
    {
        private uint start = 0;

        /// <summary>
        /// 开始值，默认值0
        /// </summary>
        public uint Start
        {
            get { return start; }
            set { start = value; }
        }

        private uint limit = 100;

        /// <summary>
        /// 限制数量，默认值100
        /// </summary>
        public uint Limit
        {
            get { return limit; }
            set { limit = value; }
        }


    }
}
