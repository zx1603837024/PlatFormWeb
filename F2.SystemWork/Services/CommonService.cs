using F2.SystemWork.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Caching;

namespace F2.SystemWork.Services
{
    public class CommonService
    {
        public String PropertiesInitialization()
        {
            String path = AppDomain.CurrentDomain.BaseDirectory;
            String map = path+"Maps/SqlMap.config";
            Cache cache = new Cache();
            var Default = cache.Get("Default");
            if (Default == null)
            {
                var DataStr = System.Configuration.ConfigurationManager.ConnectionStrings["Default"].ToString().Replace(" ", "");
                string[] DataArr = DataStr.Split(';');
                DataStr = "server=" + DataArr[0].Substring(11) + ";uid=" + DataArr[3].Substring(7) + ";pwd=" + DataArr[4].Substring(9) + ";database=" + DataArr[1].Substring(15);
                using (StreamWriter writer = new StreamWriter(path + "/Config/Properties.config", false))
                {
                    writer.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>\n<appSettings>\n<add key=\"Default\" value=\"" + DataStr + "\"/>\n</appSettings>");
                }
                cache.Insert("Default", DataStr);
            }
            return map;
        }
        //参数格式转换
        public Hashtable InputAnalysis<T>(T input)
        {
            Hashtable res = new Hashtable();
            int page=0;
            int rows=0;
            string filters = "";
            var entityType = typeof(T);
            PropertyInfo[] properties = entityType.GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                var Name = properties[i].Name;
                PropertyInfo proInfo = entityType.GetProperty(Name);
                var val = proInfo.GetValue(input);
                switch (Name)
                {
                    case "page":
                        page = Convert.ToInt32(val);
                        break;
                    case "rows":
                        rows = Convert.ToInt32(val);
                        break;
                    case "filters":
                        filters = Convert.ToString(val);
                        break;
                    case "sidx":
                        res.Add("OrderString", val);
                        break;
                    case "sord":
                        res.Add("OrderType", val);
                        break;
                    default:
                        res.Add(Name, val);
                        break;
                }
            }
            if (res["OrderString"] == null){
                res["OrderString"]=1;
            }
            if (page > 0 && rows>0) {
                var Start = (page - 1) * rows;
                var End = rows;
                res.Add("Start", Start);
                res.Add("End", End);
            }
            if (filters != null && filters.Length>0)
            {
                var rules = JsonConvert.DeserializeObject<Hashtable>(filters);
                List<Hashtable> rul = JsonConvert.DeserializeObject<List<Hashtable>>(Convert.ToString(rules["rules"]));
                foreach (Hashtable element in rul)
                {
                    res.Remove(element["field"]);
                    res.Add(element["field"], element["data"]);
                }
            }
            return res;
        }

        public Hashtable InputAnalysis2<T>(T input)
        {
            Hashtable res = new Hashtable();
            int page = 0;
            int rows = 0;
            string filters = "";
            var entityType = typeof(T);
            PropertyInfo[] properties = entityType.GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                var Name = properties[i].Name;
                PropertyInfo proInfo = entityType.GetProperty(Name);
                var val = proInfo.GetValue(input);
                switch (Name)
                {
                    
                    default:
                        res.Add(Name, val);
                        break;
                }
            }
            if (res["OrderString"] == null)
            {
                res["OrderString"] = 1;
            }
            if (page > 0 && rows > 0)
            {
                var Start = (page - 1) * rows;
                var End = rows;
                res.Add("Start", Start);
                res.Add("End", End);
            }
            if (filters != null && filters.Length > 0)
            {
                var rules = JsonConvert.DeserializeObject<Hashtable>(filters);
                List<Hashtable> rul = JsonConvert.DeserializeObject<List<Hashtable>>(Convert.ToString(rules["rules"]));
                foreach (Hashtable element in rul)
                {
                    res.Remove(element["field"]);
                    res.Add(element["field"], element["data"]);
                }
            }
            return res;
        }
        //发生post请求
        public string HttpPostNew(string Url, string postDataStr)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;
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
        //发生get请求
        public string HttpGetNew(string Url, string postDataStr)
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
        }
        //CONTENT-MD5加密
        public string MD5ToBase64String(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] MD5 = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(str));//MD5(注意UTF8编码)
            string result = Convert.ToBase64String(MD5, 0, MD5.Length);//Base64
            return result;
        }
        /// <summary>
        /// HMACSHA1加密
        /// </summary>
        /// <param name="text">要加密的原串</param>
        ///<param name="key">私钥</param>
        /// <returns></returns>
        public string HMACSHA1Text(string text, string key)
        {
            //HMACSHA1加密
            HMACSHA1 hmacsha1 = new HMACSHA1();
            hmacsha1.Key = System.Text.Encoding.UTF8.GetBytes(key);
            byte[] dataBuffer = System.Text.Encoding.UTF8.GetBytes(text);
            byte[] hashBytes = hmacsha1.ComputeHash(dataBuffer);
            return Convert.ToBase64String(hashBytes);
        }
        /**
         * 华峰请求接口先调用获取token然后调用接口
         * 华峰业务接口调用前调用该接口appid和appsercet方法内修改
         */
        public string GetTokenByHF(string URL, string DATA, string HfUrl)
        {
            string url = HfUrl + "/api/ev/token";
            string data = "{\"appid\": \"736f6e677368616e68755f4658323032323131303453\",\"secret\": \"5a5d43590ffff772c5500a0ba80fd535\"}";
            string response = HttpPostNew(url, data);
            Cache cache = new Cache();
            var HFToken = cache.Get("HFToken");
            WriteLogFile("HFToken：" + response);
            if (HFToken == null)
            {
                JObject joResponse = (JObject)JsonConvert.DeserializeObject(response);
                if (joResponse["status"].ToString().Equals("0"))
                {
                    JObject hFResponse = (JObject)JsonConvert.DeserializeObject(joResponse["data"].ToString());
                    cache.Insert("HFToken", hFResponse["token"].ToString(), null, System.DateTime.Now.AddSeconds((long)60 * 60 * 2), TimeSpan.Zero);
                    return HttpPostToken(URL, DATA, hFResponse["token"].ToString());
                }
                return response;
            }
            else
            {
                //如果有缓存的token先请求一次对方业务接口看能否请求成功，失败了更新获取token再请求业务接口
                string postResult = HttpPostToken(URL, DATA, HFToken.ToString());
                WriteLogFile("postResult：" + postResult);
                JObject joResponse = (JObject)JsonConvert.DeserializeObject(postResult);
                if (joResponse["errorcode"].ToString().Equals("0"))
                {
                    return postResult;
                }
                else
                {
                    try
                    {
                        string response2 = HttpPostNew(url, data);
                        joResponse = (JObject)JsonConvert.DeserializeObject(response2);
                        if (joResponse["status"].ToString().Equals("0"))
                        {
                            JObject hFResponse = (JObject)JsonConvert.DeserializeObject(joResponse["data"].ToString());
                            cache.Insert("HFToken", hFResponse["token"].ToString(), null, System.DateTime.Now.AddSeconds((long)60 * 60 * 2), TimeSpan.Zero);
                            return HttpPostToken(URL, DATA, hFResponse["token"].ToString());
                        }
                        return response2;
                    }
                    catch (Exception e)
                    {
                        WriteLogFile("华峰请求接口失败：" + joResponse);
                        return "error";
                    }
                }
            }
        }
        public class HFResponse
        {

            public string expired { get; set; }
            public string token { get; set; }

        }
        /**
         * token验证
         */
        public string HttpPostToken(string url, string data, string token)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException(url);
            }
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentNullException(url);
            }
            byte[] postBytes = Encoding.GetEncoding("utf-8").GetBytes(data);
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;//(HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = postBytes.Length;
            request.Headers.Add("Authorization", "Bearer " + token);

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
        /// <summary>
        /// 写日志，后续用Log4
        /// </summary>
        /// <param name="input"></param>
        public static void WriteLogFile(string input)
        {
            string logPath = ConfigurationManager.AppSettings["LogFilePath"].ToString();
            //判断该路径下文件夹是否存在，不存在的情况下新建文件夹
            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }
            //指定日志文件的目录
            string fname = logPath + "\\LogFile" + DateTime.Now.ToString("yyyyMMdd") + ".txt";

            //定义文件信息对象
            FileInfo finfo = new FileInfo(fname);

            if (!finfo.Exists)
            {
                FileStream fs;
                fs = File.Create(fname);
                fs.Close();
                finfo = new FileInfo(fname);
            }

            //判断文件是否存在以及是否大于2K
            //if (finfo.Length > 1024 * 1024 * 10)
            //{
            //    //文件超过10MB则重命名
            //    File.Move(Directory.GetCurrentDirectory() + "\\LogFile.txt", Directory.GetCurrentDirectory() + DateTime.Now.TimeOfDay + "\\LogFile.txt");
            //}

            //创建只写文件流
            using (FileStream fs = finfo.OpenWrite())
            {
                //根据上面创建的文件流创建写数据流
                StreamWriter w = new StreamWriter(fs);

                //设置写数据流的起始位置为文件流的末尾
                w.BaseStream.Seek(0, SeekOrigin.End);

                //写入“Log Entry : ”
                w.Write("\n\rLog Entry : ");

                //写入当前系统时间并换行
                w.Write("{0} {1} \n\r", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());

                //写入日志内容并换行
                w.Write(input + "\n\r");

                //写入------------------------------------“并换行
                w.Write("------------------------------------\n\r");

                //清空缓冲区内容，并把缓冲区内容写入基础流
                w.Flush();

                //关闭写数据流
                w.Close();
            }
        }
    }
}
