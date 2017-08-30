using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeChatSearchOrder
{
    public class ReturnData
    {
        public int total { get; set; }

        public List<OrderListInfo> rows { get; set; }
    }
}