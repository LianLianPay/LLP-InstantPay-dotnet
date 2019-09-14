using System;
using System.Collections.Generic;
using System.IO;

using System.Security.Cryptography;
using System.Text;
using System.Linq;
using System.Net;
using System.Text;
using LLYTPay;

/**
 * DEMO仅提供商户参考方便对接实时付款，具体对接看先细看实时付款API商户接口说明书
 * (商户测试期间需要用正式的数据测试，测试时默认单笔单日单月额度50，等测试OK，和连连技术核对过业务对接逻辑后，申请走上线流程打开额度）
 * 对于返回码4002和4004的疑似订单（文档中有说明），不能系统直接调用确认接口，要人工审核后才能调用
 * 实时付款对接重大原则：
 * // 出现异常时要调用付款结果查询接口，明确订单状态，不能私自设置订单为失败状态，以免造成这笔订单在连连付款成功了，而商户设置为失败
 * 步骤： 1.先对参数设置
 *        2.对参数加签YinTongUtil.addSign   商户私钥加签
 *        3.对参数加密LianLianPaySecurity.encrypt（查询接口不需要加密） 银通公钥加密
 *        4.对响应内容sign不为空时验签YinTongUtil.checkSign    验签用的是银通公钥
 *        5.根据返回code码处理逻辑（详细看文档说明，特别关注红色字和异常机制）
 */
namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
          //com.lianlianpay.security.utils.LianLianPaySecurity.isNull("1");
          
         SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();

            sParaTemp.Add("acct_name", "测试");//版本号
            sParaTemp.Add("api_version", "1.0");//版本号
            sParaTemp.Add("bank_name", "招商银行");

            sParaTemp.Add("card_no", "6245882402098823");
            sParaTemp.Add("dt_order", DateTime.Now.ToString("yyyyMMddHHmmss"));

            sParaTemp.Add("flag_card", "0");
            sParaTemp.Add("info_order", "打款");//订单描述
            sParaTemp.Add("money_order", "0.01");
            sParaTemp.Add("no_order",  DateTime.Now.ToString("yyyyMMddHHmmss"));
            sParaTemp.Add("notify_url", "http://ht.laidaibei.com/notify_url.aspx");
            sParaTemp.Add("oid_partner", "201609130000219023");//商户编号
            sParaTemp.Add("platform", "jd.com");
            sParaTemp.Add("sign_type", "RSA");
            //签名
            string sign = YinTongUtil.addSign(sParaTemp, PartnerConfig.TRADER_PRI_KEY, string.Empty);
            sParaTemp["sign"] = sign;
            
             string reqJson = YinTongUtil.dictToJson(sParaTemp);

             var json = "";
             try
             {
             	json = com.lianlianpay.security.utils.LianLianPaySecurity.encrypt(reqJson, PartnerConfig.YT_PUB_KEY);
             }
             catch (Exception ex)
             {
             	Console.WriteLine("异常信息："+ex.ToString());
             }

            var http = (HttpWebRequest)WebRequest.Create(new Uri("https://instantpay.lianlianpay.com/paymentapi/payment.htm"));
            http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "POST";
            Console.WriteLine(reqJson);

           
			var pay = ($"{{\"pay_load\":\"{json}\",\"oid_partner\":\"{PartnerConfig.OID_PARTNER}\"}}").Replace("\r\n", "\\r\\n");
			byte[] bytes = Encoding.GetEncoding("ISO-8859-1").GetBytes(pay);

            Stream newStream = http.GetRequestStream();
            newStream.Write(bytes, 0, bytes.Length);
            newStream.Close();

            var response = http.GetResponse();

            var stream = response.GetResponseStream();
            var sr = new StreamReader(stream);
            var content = sr.ReadToEnd();
			//调用付款申请接口，同步返回0000，是指创建连连支付单成功，订单处于付款处理中状态，最终的付款状态由异步通知告知
			//出现1002，2005，4006，4007，4009，9999这6个返回码时或者没返回码，抛exception（或者对除了0000之后的code都查询一遍查询接口）调用付款结果查询接口，
			//明确订单状态，不能私自设置订单为失败状态，以免造成这笔订单在连连付款成功了，而商户设置为失败,用户重新发起付款请求,造成重复付款，商户资金损失
            Console.WriteLine(content);
			//对连连响应报文内容需要用连连公钥验签
            Console.ReadKey();
        }
        
       
    }
    
}
