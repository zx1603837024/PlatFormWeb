using Abp.BackgroundJobs;
using Abp.Configuration;
using Abp.Dependency;
using Abp.Helper;
using P4.AppManage.Dto;
using P4.WeixinsSendRecords;
using System;
using System.Net.Http;

namespace P4.Jobs
{
    /// <summary>
    /// 
    /// </summary>
    public class WeixinNoticeProcessJob : BackgroundJob<int>, ITransientDependency
    {
        #region Var
        private readonly ISettingStore _settingStore;
        private readonly IWeixinSendRecordAppService _weixinSendRecordAppService;
        HttpClient client = new HttpClient();
        #endregion


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="settingStore"></param>
        /// <param name="weixinSendRecordAppService"></param>
        public WeixinNoticeProcessJob(ISettingStore settingStore, IWeixinSendRecordAppService weixinSendRecordAppService)
        {
            _settingStore = settingStore;
            _weixinSendRecordAppService = weixinSendRecordAppService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public override void Execute(int args)
        {
            string WeixinUrl = SqlHelper.ExecuteScalar(SqlHelper.connectionString, System.Data.CommandType.Text, "select domain from AbpWeixinConfig where TenantId = " + args).ToString();
            var noticeModel = Abp.DataProcessHelper.GetEntityFromTable<NoticeDto>(SqlHelper.ExecuteDataset(SqlHelper.connectionString, System.Data.CommandType.Text, "SELECT SUM(Arrearage) AS SUMArrearage, PlateNumber, openId, WeixinTuser.TenantId, SendWeixinNumber, SendWeixinDatetime, nickName FROM WeixinTuser LEFT JOIN AbpBusinessDetail with(nolock) ON PlateNumber IN (CarNumber1, CarNumber2, CarNumber3) WHERE IsDeleted = 0 AND Status = 3 GROUP BY PlateNumber, openId, WeixinTuser.TenantId, WeixinTuser.nickName, SendWeixinNumber, SendWeixinDatetime HAVING WeixinTuser.TenantId = " + args + " AND openId IS NOT NULL").Tables[0]);

            foreach (var entity in noticeModel)
            {
                if (entity.SendWeixinDatetime.AddDays(int.Parse(_settingStore.GetSettingOrNull(args, null, "IsolationTime").Value)) <= DateTime.Now &&
                    (entity.SendWeixinNumber < int.Parse(_settingStore.GetSettingOrNull(args, null, "SendTimes").Value) || 
                    int.Parse(_settingStore.GetSettingOrNull(args, null, "SendTimes").Value) == 0) &&
                    entity.SUMArrearage >= int.Parse(_settingStore.GetSettingOrNull(args, null, "UnpaidMoney").Value))
                {
                    entity.SendWeixinNumber += 1;
                    entity.SendWeixinDatetime = DateTime.Now;
                    //oifnk0yIaQTk7qUNquEHtgOdL1lI
                    //" + entity.openId + "
                    client.GetAsync(WeixinUrl + "/ajax/SendEscapeNoticeWeixin?Carnumber=" + entity.PlateNumber + "&touser=" + entity.openId + "&Money=" + entity.SUMArrearage + "&Name=" + entity.nickName);

                    SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, System.Data.CommandType.Text, "UPDATE WeixinTuser SET SendWeixinDatetime = '" + entity.SendWeixinDatetime + "',SendWeixinNumber = " + entity.SendWeixinNumber + " WHERE openId = '" + entity.openId + "'");


                    _weixinSendRecordAppService.Insert(new WeixinSendRecord.Dtos.WeixinSendRecordDto() { args = args, PlateNumber = entity.PlateNumber, openId = entity.openId, nickName = entity.nickName, SUMArrearage = entity.SUMArrearage, SendWeixinNumber = entity.SendWeixinNumber, SendWeixinDatetime = entity.SendWeixinDatetime });
                }
                else if (entity.SUMArrearage < int.Parse(_settingStore.GetSettingOrNull(args, null, "UnpaidMoney").Value) && entity.SendWeixinNumber != 0)
                {
                    entity.SendWeixinNumber = 0;

                    SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, System.Data.CommandType.Text, "UPDATE WeixinTuser SET SendWeixinNumber = " + entity.SendWeixinNumber + " WHERE openId = '" + entity.openId + "'");
                }
            }
        }
    }
}
