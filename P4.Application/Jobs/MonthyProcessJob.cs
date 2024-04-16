using Abp.BackgroundJobs;
using Abp.Configuration;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Helper;
using Newtonsoft.Json;
using P4.Businesses;
using P4.MonthlyCars;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http;

namespace P4.Jobs
{
    /// <summary>
    /// 包月车辆定时处理
    /// </summary>
    public class MonthyProcessJob : BackgroundJob<List<MonthlyCar>>, ITransientDependency
    {
        #region Var
        private readonly ISettingStore _settingStore;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="businessDetailRepository"></param>
        /// <param name="settingStore"></param>
        public MonthyProcessJob(IRepository<BusinessDetail, long> businessDetailRepository, ISettingStore settingStore)
        {
            _settingStore = settingStore;
        }


        /// <summary>
        /// 执行方法,包月车辆提前三天提醒
        /// </summary>
        /// <param name="args"></param>
        public override void Execute(List<MonthlyCar> args)
        {
            foreach (var item in args)
            {
                //发送短信
                string isSendSms = _settingStore.GetSettingOrNull(item.TenantId, null, "SMSChannel").Value;//短信通道，如果启用，可发送短信，不过不启用，不发送短信
                if (isSendSms.ToLower() == "true")
                {
                    DataTable dtLast = SqlHelper.ExecuteDataset(SqlHelper.connectionString, CommandType.Text, $"select AppName from AbpWeixinConfig  where TenantId = '{item.TenantId}' ").Tables[0];
                    string smsResult = "Sms接口调用失败！";
                    SmsSubmaiMailDto qm = new SmsSubmaiMailDto();
                    qm.appid = "65798";
                    qm.content = $@"【{Convert.ToString(dtLast.Rows[0]["AppName"])}】尊敬的车主，您的爱车({item.PlateNumber})在我公司的包月服务即将到期，感谢您的支持，祝您身体健康，家庭幸福！";
                    qm.signature = "6d239f8e8ca2c85a351842c077417704";
                    qm.to = item.Telphone;
                    using (HttpClient client1 = new HttpClient())
                    {
                        HttpResponseMessage response = null;
                        HttpContent content = new StringContent(JsonConvert.SerializeObject(qm));
                        content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                        response = client1.PostAsync("https://api-v4.mysubmail.com/sms/send", content).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            smsResult = response.Content.ReadAsStringAsync().Result;
                            string sql = "insert into AbpSms(SmsMsg, TelePhones, SmsResult, CreationTime, SmsCount, TenantId) values(@SmsMsg, @TelePhones, @SmsResult, @CreationTime, @SmsCount, @TenantId)";
                            SqlParameter[] param = new SqlParameter[] {
                                      new SqlParameter("@SmsMsg", qm.content),
                                      new SqlParameter("@TelePhones", item.Telphone),
                                      new SqlParameter("@SmsResult", smsResult),
                                      new SqlParameter("@CreationTime", DateTime.Now),
                                      new SqlParameter("@SmsCount", 1),
                                      new SqlParameter("@TenantId", item.TenantId)
                                };
                            sql += $@";update AbpMonthlyCars set IsSms = 1 where Telphone = '{item.Telphone}' and PlateNumber = '{item.PlateNumber}' ;";
                            SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, System.Data.CommandType.Text, sql, param);
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class SmsSubmaiMailDto
    {
        /// <summary>
        /// appId
        /// </summary>
        public string appid { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string to { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 密钥
        /// </summary>
        public string signature { get; set; }

    }
}
