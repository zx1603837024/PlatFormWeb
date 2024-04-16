using System;
using System.Collections;
using Newtonsoft.Json;
using System.Text;
using System.Web;
using F2.SystemWork.Services;
using System.IO;
using System.Net;

namespace F2.OptionSystem.Service.impl
{
    public class ZSVideoServiceImpl : ZSVideoService
    {
        public object zspdns(String input)
        {
            try
            {
                var param = JsonConvert.DeserializeObject<Hashtable>(input);
                var AccessKeyId = "GFiPUyFx22126T1yHo2fO69e1VYkNz3j";
                var AccessKeySecret = "degiq4BFI2Diz465HK8GyvqIvoL5WDvZ";
                var expires = (DateTime.Now.AddMinutes(10).ToUniversalTime().Ticks - 621355968000000000) / 10000000;
                var Paramstr = "{\"sn\":\"" + param["Sn"] + "\"}";

                CommonService encryptionUtils = new CommonService();
                string ContentMD5 = encryptionUtils.MD5ToBase64String(Paramstr);
                var txt = "POST" + "\n" + ContentMD5 + "\n" + "application/json" + "\n" + expires + "\n" + "/v2/server_api/devices/pdns";


                var signature = HttpUtility.UrlDecode(encryptionUtils.HMACSHA1Text(txt, AccessKeySecret));


                var ssw = HttpPostNew("http://www.parkways.cn/v2/server_api/devices/pdns?accesskey_id=" + AccessKeyId + "&signature=" + signature + "&expires=" + expires, Paramstr);
                return ssw;
            }
            catch(Exception e)
            {

                return "error";
            }
            
        }

        //发生post请求
        public string HttpPostNew(string Url, string postDataStr)
        {
            byte[] postBytes = Encoding.GetEncoding("utf-8").GetBytes(postDataStr);
            HttpWebRequest request = WebRequest.Create(Url) as HttpWebRequest;//(HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = postBytes.Length;
            Stream myRequestStream = request.GetRequestStream();
            myRequestStream.Write(postBytes, 0, postBytes.Length);
            myRequestStream.Close();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
        }
    }
}
