using Abp.Application.Services;
using Abp.Domain.Repositories;
using P4.SmsManagements.Dtos;
using P4.Smss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Configuration;
using P4.MultiTenancy;
using System.Text;
using Abp.Linq.Extensions;
using System.Net;
using System.IO;
using System.Web;
using P4.AppManage.Dto;

namespace P4.SmsManagements
{

    /// <summary>
    /// 短信管理
    /// </summary>
    public class SmsManagementAppService : ApplicationService, ISmsManagementAppService
    {
        #region Var
        HttpClient client = new HttpClient();
        private readonly IRepository<Sms, long> _abpSmsRepository;
        private readonly IRepository<Tenant> _abpTenantRepository;
        private readonly ISettingStore _settingStore;
        private readonly ISmsModelAppService _smsModelAppService;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="abpSmsRepository"></param>
        /// <param name="settingStore"></param>
        /// <param name="abpTenantRepository"></param>
        /// <param name="smsModelAppService"></param>
        public SmsManagementAppService(IRepository<Sms, long> abpSmsRepository, ISettingStore settingStore, IRepository<Tenant> abpTenantRepository, ISmsModelAppService smsModelAppService)
        {

            HttpClientHandler handler = new HttpClientHandler
            {
                Credentials = new System.Net.NetworkCredential("username", "password")
            };
            client = new HttpClient(handler);

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));

            _abpSmsRepository = abpSmsRepository;
            _settingStore = settingStore;
            _abpTenantRepository = abpTenantRepository;
            _smsModelAppService = smsModelAppService;
        }


        /// <summary>
        /// 发送短信
        /// </summary>
        /// <returns></returns>
        public async void SendSms(SmsAccountDto input)
        {
            SendSmsNoTenant(input);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        public void SendSmsNoTenant(SmsAccountDto input)
        {
            string isSendSms = _settingStore.GetSettingOrNull(AbpSession.TenantId, null, "SMSChannel").Value;//短信通道，如果启用，可发送短信，不过不启用，不发送短信
            if (isSendSms.ToLower() == "true")
            {
                string smsResult = "Sms接口调用失败！";
                SetUri();
                input.UserId = _settingStore.GetSettingOrNull(AbpSession.TenantId, null, "Abp.Sms.UserId").Value;
                input.Password = _settingStore.GetSettingOrNull(AbpSession.TenantId, null, "Abp.Sms.Password").Value;

                if (AbpSession.TenantId.HasValue)
                    input.Sign = _abpTenantRepository.Load(AbpSession.TenantId.Value).Sign;

                HttpResponseMessage response = null;
                if (_settingStore.GetSettingOrNull(AbpSession.TenantId, null, "Abp.Sms.Method").Value == "Get")
                {
                    response = client.GetAsync(_settingStore.GetSettingOrNull(AbpSession.TenantId, null, "Abp.Sms.AddressUri").Value + input.ToString()).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        smsResult = response.Content.ReadAsStringAsync().Result;
                        _abpSmsRepository.Insert(new Sms()
                        {
                            SmsMsg = input.MsgValue + input.SignValue,
                            SmsResult = smsResult,
                            SmsCount = 1,
                            TelePhones = input.Destnumbers
                        });
                    }
                    else
                    {
                        Logger.Error("短信通道访问异常");
                    }

                }
                else
                {
                    string SwiftNumber = (int.Parse(_settingStore.GetSettingOrNull(AbpSession.TenantId, null, "Abp.Sms.SwiftNumber").Value) + 1).ToString();

                    if (AbpSession.TenantId.HasValue)

                        Abp.Helper.SqlHelper.ExecuteNonQuery(Abp.Helper.SqlHelper.connectionString, System.Data.CommandType.Text, "update AbpSettings set Value = '" + SwiftNumber + "' where TenantId = " + AbpSession.TenantId.Value + " and Name = 'Abp.Sms.SwiftNumber'");
                    else
                        Abp.Helper.SqlHelper.ExecuteNonQuery(Abp.Helper.SqlHelper.connectionString, System.Data.CommandType.Text, "update AbpSettings set Value = '" + SwiftNumber + "' where TenantId is NULL and Name = 'Abp.Sms.SwiftNumber'");

                    string postJsonTpl = "\"account\":\"{0}\",\"password\":\"{1}\",\"phone\":\"{2}\",\"report\":\"false\",\"msg\":\"{3}\"";
                    //" + input.SignValue + "
                    string jsonBody = string.Format(postJsonTpl, input.UserId, input.Password, input.Destnumbers, "【253云通讯】" + HttpContext.Current.Server.UrlEncode(input.MsgValue));



                    string result = doPostMethodToObj(_settingStore.GetSettingOrNull(AbpSession.TenantId, null, "Abp.Sms.AddressUrl").Value + _settingStore.GetSettingOrNull(AbpSession.TenantId, null, "Abp.Sms.AddressUri").Value, "{" + jsonBody + "}");

                    
                    _abpSmsRepository.Insert(new Sms()
                    {
                        SmsMsg = input.MsgValue + input.SignValue,
                        SmsResult = result,
                        SmsCount = 1,
                        TelePhones = input.Destnumbers
                    });

                    //response = client.PostAsXmlAsync(_settingStore.GetSettingOrNull(AbpSession.TenantId, null, "Abp.Sms.AddressUri").Value + "?content=[{\"acceptPhone\":\"" + input.Destnumbers + "\",\"smsContent\":\"" + System.Web.HttpUtility.UrlEncode(input.Sign + input.Msg) + "\",\"sendTime\":\"\", \"priority\":\"9\",\"smsComeId\":\"" + SwiftNumber.PadLeft(20, '0') + "\",\"smsAccount\":\"" + input.UserId + "\",\"smsPwd\":\"" + input.Password + "\"}]", default(object)).Result;
                }
               
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        public void SendSmsNoTenantByTenant(SmsAccountDto input, int TenantId)
        {
            string isSendSms = _settingStore.GetSettingOrNull(TenantId, null, "SMSChannel").Value;//短信通道，如果启用，可发送短信，不过不启用，不发送短信
            if (isSendSms.ToLower() == "true")
            {
                string smsResult = "Sms接口调用失败！";
                SetUri(TenantId);
                input.UserId = _settingStore.GetSettingOrNull(TenantId, null, "Abp.Sms.UserId").Value;
                input.Password = _settingStore.GetSettingOrNull(TenantId, null, "Abp.Sms.Password").Value;

                //if (AbpSession.TenantId.HasValue)
                input.Sign = _abpTenantRepository.Load(TenantId).Sign;

                HttpResponseMessage response = null;
                if (_settingStore.GetSettingOrNull(TenantId, null, "Abp.Sms.Method").Value == "Get")
                {
                    response = client.GetAsync(_settingStore.GetSettingOrNull(TenantId, null, "Abp.Sms.AddressUri").Value + input.ToString()).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        smsResult = response.Content.ReadAsStringAsync().Result;
                        //_abpSmsRepository.Insert(new Sms()
                        //{
                        //    TenantId = TenantId,
                        //    SmsMsg = input.MsgValue + input.SignValue,
                        //    SmsResult = smsResult,
                        //    SmsCount = 1,
                        //    TelePhones = input.Destnumbers
                        //});
                    }
                    else
                    {
                        Logger.Error("短信通道访问异常");
                    }
                }
                else
                {

                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="jsonBody"></param>
        /// <returns></returns>
        private static string doPostMethodToObj(string url, string jsonBody)
        {
            string result = String.Empty;
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            // Create NetworkCredential Object 
            NetworkCredential admin_auth = new NetworkCredential("username", "password");

            // Set your HTTP credentials in your request header
            httpWebRequest.Credentials = admin_auth;

            // callback for handling server certificates
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(jsonBody);
                streamWriter.Flush();
                streamWriter.Close();
                HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            return result;
        }


        /// <summary>
        /// 设置发送地址
        /// </summary>
        private async void SetUriAsync()
        {
            client.BaseAddress = new Uri(_settingStore.GetSettingOrNull(AbpSession.TenantId, null, "Abp.Sms.AddressUrl").Value);
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetUri()
        {
            client.BaseAddress = new Uri(_settingStore.GetSettingOrNull(AbpSession.TenantId, null, "Abp.Sms.AddressUrl").Value);
        }

        private void SetUri(int TenantId)
        {
            client.BaseAddress = new Uri(_settingStore.GetSettingOrNull(TenantId, null, "Abp.Sms.AddressUrl").Value);
        }

        /// <summary>
        /// 未使用的代码 ByWRS
        /// </summary>
        /// <param name="input"></param>
        public async void GetSmsAccount(Dtos.GetSmsAccountInput input)
        {
            string smsResult = "Sms接口调用失败！";
            SetUri();
            input.UserId = _settingStore.GetSettingOrNull(AbpSession.TenantId, null, "Abp.Sms.UserId").Value;
            input.Password = _settingStore.GetSettingOrNull(AbpSession.TenantId, null, "Abp.Sms.Password").Value;
            HttpResponseMessage response = client.GetAsync(_settingStore.GetSettingOrNull(AbpSession.TenantId, null, "Abp.Sms.AddressUri").Value + input.ToString()).Result;
            if (response.IsSuccessStatusCode)
            {
                smsResult = response.Content.ReadAsStringAsync().Result;
            }
        }

        /// <summary>
        /// 获取所有发短信记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllSmsOutput GetSmsList(SearchSmssInput input)
        {
            int records = _abpSmsRepository.GetAll().Filters(input).ToList().Count;
            return new GetAllSmsOutput()
            {
                rows = _abpSmsRepository.GetAll().Orders(input).Filters(input).PageBy(input).ToList().MapTo<List<SmsDto>>(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }

        /// <summary>
        /// 发送短信；未使用的代码，ByWRS
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="msg"></param>
        public void SendMsg(string phone, string msg)
        {
            SendSmsNoTenant(new SmsAccountDto() { Msg = msg, Destnumbers = phone });
        }
    }
}
