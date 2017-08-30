using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization.Json;
using System.Data;

namespace WeChatSearchOrder
{
    /// <summary>
    /// Summary description for WeChatSearchOrder
    /// </summary>
    public class WeChatSearchOrder : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                var code = context.Request["code"].ToString().Trim();
                var orders = new List<OrderListInfo>();
                var str = " select  top 10 * from V_Sale_Order where BillCode='" + code + "' or OBillCode='" + code + "' order by billcode ";
                DbHelperSQL dbhelp = new DbHelperSQL();
                DataSet ds = dbhelp.Query(str);
                DataTable dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    orders.Add(new OrderListInfo()
                    {
                        BCode = dr["BillCode"].ToString(),
                        DN = dr["DeliveryNum"].ToString(),
                        Exp = dr["ECode"].ToString(),
                        OBCode = dr["OBillCode"].ToString(),
                        State = dr["StateName"].ToString()
                    });
                }
                ReturnData rd = new ReturnData()
                {
                    total = dt.Rows.Count,
                    rows = orders
                };
                DataContractJsonSerializer json = new DataContractJsonSerializer(rd.GetType());
                json.WriteObject(context.Response.OutputStream, rd);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write(ex.Message);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}