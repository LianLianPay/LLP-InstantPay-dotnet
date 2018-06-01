using System;

/**
* 商户基础 配置
* @author guoyx e-mail:guoyx@lianlian.com
* @date:2013-6-25 下午01:45:40
* @version :1.0
*
*/

namespace LLYTPay
{
	public class PartnerConfig
	{

		// RSA银通公钥
		public static string YT_PUB_KEY     = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCSS/DiwdCf/aZsxxcacDnooGph3d2JOj5GXWi+q3gznZauZjkNP8SKl3J2liP0O6rU/Y/29+IUe+GTMhMOFJuZm1htAtKiu5ekW0GlBMWxf4FPkYlQkPE0FtaoMP3gYfh+OwI+fIRrpW3ySn3mScnc6Z700nU/VYrRkfcSCbSnRwIDAQAB";
		// RSA商户私钥
		public static string TRADER_PRI_KEY = "MIICeQIBADANBgkqhkiG9w0BAQEFAASCAmMwggJfAgEAAoGBAK5oCtldgp6A0cdjiRjYTJ3fFxBZ55bmFqNxCSICEMp4loS2a0Qm42bov5V3zmmgbhnOJh13dEZU8a1SEeoiYiiKE0F5Pan+RPt329F5YXLfT0h87N3YPmzfx2xiSJa3snmLy0gQHyo/+TfrO59HnY3DuB7aX0U5MkSMQauBiORLAgMBAAECgYEArQiCgwe4eQN7nePN+D1ZPmRA4LMiBt9+5GdYVUpRWF/tjfviTnp6sPYIZgW4X6mQsr+Jp0CFtuW95WSAa5fzY3ulxchf34Hxt1j4h/9IcMAc0qPdB0HTF7M06eEpVLZ5GnbpepStB18HBaGO66kqnwUZ38Hub2EZ8vwN8O8+q1ECQQDaATZ7W9TmPovPhUlerbYEzM2v2IpkhIaziDpwV152bta215fql1iwKINibCta/PYU7spZSLcNpJumHz93OUYJAkEAzM2W2hgujCnZm8trR+c7aY6tlo/8GSRRvFVxnfUOGKYdALXGGYoWE5donH5/2Yfkzzivqar/e/kc+qbFTLCMswJBAL2WRG0vRY0eY7QLM+1UoHC4M0Bzzpbv8bz8AeZk9M+GQNAt2f23tPctpGTZsTKlvtQhfnP7GsaQmpPzpNvoQRECQQCeGmRHT323pKMiG2Jhase50HR/k/345snWi1ufpktQigQ/xRP+KVSroSoYDavjIX5o3oj1gVWjvgc6FL6hWnXzAkEAlyzQsNoFu65uN8Cdc3bHm01H6UHsCdjR5p3KGK3RbAydAbbXInE3003duU/JxWh8P7fVpJohXQc4eh4bIBEhuw==";
		// MD5 KEY
		public static string MD5_KEY        = "201408071000001543test_20140812";
		// 接收异步通知地址
		public static string NOTIFY_URL = "http://yourdomain/notify_url.aspx";  //请变更yourdomain为你的域名（及端口）
		// 支付结束后返回地址
		public static string URL_RETURN = "http://yourdomain/urlReturn.aspx";    //请变更为您的返回地址
		// 商户编号
		public static string            OID_PARTNER="201408071000001543";     //请变更为您的商户号
		// 签名方式 RSA或MD5
		public static string            SIGN_TYPE="MD5";    					//请选择签名方式
		// 接口版本号，固定1.0
		public static string VERSION        = "1.0";
		// 业务类型，连连支付根据商户业务为商户开设的业务类型； （101001：虚拟商品销售、109001：实物商品销售、108001：外部账户充值）
		public static string BUSI_PARTNER   = "101001";   //请选择业务类型
	}
}

