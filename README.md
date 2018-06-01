# LLP-InstantPay-dotnet

欢迎来到连连实时付款的.NET仓库， 本仓库包含接入的示例代码及必要的说明。

## HTTP请求说明

连连统一要求在请求连连提供的接口时使用以下的格式:

```text
curl {API_ENDPOINT} \
-H "Content-type:application/json;charset=utf-8" \
-d '{YOUR_REQUEST_BODY}'
```

## 配置公私钥

示例代码中的公私钥配置在```PartnerConfig.cs```中， 请仔细阅读[连连开放平台-配置公私钥](https://zealous-kare-7abde4.netlify.com/docs/development/signature-key-generation)， 依据文档配置商户公私钥与连连提供的公钥。

## 付款申请API

调用付款申请的示例代码如下。建预处理请求参数后需要对其进行[加密](https://zealous-kare-7abde4.netlify.com/docs/send-money/instant/api-encrypt)生成```pay_load```， 之后直接做HTTP请求即可。

> 请求参数的详细说明见[连连开放平台-付款申请API](https://zealous-kare-7abde4.netlify.com/apis/instant-apply)。

```csharp
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

var pay = @"{""pay_load"":""" + json + @""",""oid_partner"":""201609130000219023""}";



byte[] bytes = Encoding.UTF8.GetBytes(pay);

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
```