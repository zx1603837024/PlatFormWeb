using System;
using System.Collections.Generic;
using Abp.BackgroundJobs;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P4.AppManage.Dto;
using Abp.Helper;
using Abp.Configuration;
using P4.SmsManagements.Dtos;
using P4.SmsManagements;
using Abp.Dependency;

namespace P4.Jobs
{
    /// <summary>
    /// 
    /// </summary>
    public class SmsNoticeProcessJob : BackgroundJob<int>, ITransientDependency
    {
        #region Var
        private readonly ISettingStore _settingStore;
        private readonly ISmsManagementAppService _smsManagementAppService;
        private readonly ISmsModelAppService _smsModelAppService;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="settingStore"></param>
        /// <param name="smsManagementAppService"></param>
        /// <param name="smsModelAppService"></param>
        public SmsNoticeProcessJob(ISettingStore settingStore, ISmsManagementAppService smsManagementAppService, ISmsModelAppService smsModelAppService)
        {
            _settingStore = settingStore;
            _smsManagementAppService = smsManagementAppService;
            _smsModelAppService = smsModelAppService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public override void Execute(int args)
        {
            var noticeModel = Abp.DataProcessHelper.GetEntityFromTable<NoticeDto>(SqlHelper.ExecuteDataset(SqlHelper.connectionString, System.Data.CommandType.Text, "SELECT AbpBusinessDetail.PlateNumber, SUMArrearage = SUM(Arrearage), TelePhone ,ExtOtherAccount.TenantId ,SendSmsDatetime ,SendSmsNumber FROM ExtOtherAccount LEFT JOIN ExtOtherPlateNumber ON ExtOtherAccount.Id = ExtOtherPlateNumber.AssignedOtherAccountId LEFT JOIN AbpBusinessDetail with(nolock) ON  AbpBusinessDetail.PlateNumber = ExtOtherPlateNumber.PlateNumber WHERE AbpBusinessDetail.IsDeleted = 0 AND AbpBusinessDetail.Status = 3 AND ExtOtherAccount.TenantId = " + args + " GROUP BY AbpBusinessDetail.PlateNumber, TelePhone, ExtOtherAccount.TenantId,SendSmsDatetime ,SendSmsNumber  HAVING ExtOtherAccount.TelePhone IS NOT NULL;").Tables[0]);

            foreach (var NoticeModel in noticeModel)
            {
                if (NoticeModel.SendSmsDatetime.AddDays(int.Parse(_settingStore.GetSettingOrNull(args, null, "IsolationTime").Value)) <= DateTime.Now && NoticeModel.SendSmsNumber < int.Parse(_settingStore.GetSettingOrNull(args, null, "SendTimes").Value) &&
                    NoticeModel.SUMArrearage > int.Parse(_settingStore.GetSettingOrNull(args, null, "UnpaidMoney").Value))//欠款金额
                {
                    NoticeModel.SendSmsDatetime = DateTime.Now;
                    NoticeModel.SendSmsNumber += 1;

                    _smsManagementAppService.SendSmsNoTenantByTenant(new SmsAccountDto()
                    {
                        Destnumbers = NoticeModel.TelePhone,
                        Msg = string.Format(_smsModelAppService.GetSmsModelByType("UnpaidRecoveryModel").SmsContext, NoticeModel.PlateNumber, NoticeModel.SUMArrearage)
                    }, NoticeModel.TenantId);

                    SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, System.Data.CommandType.Text, "UPDATE ExtOtherAccount SET SendSmsDatetime = '" + NoticeModel.SendSmsDatetime + "',SendSmsNumber = " + NoticeModel.SendSmsNumber + " WHERE TelePhone = '" + NoticeModel.TelePhone + "'");

                    SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, System.Data.CommandType.Text, "INSERT INTO SmsSendRecords(TenantId, PlateNumber, TelePhone, SUMArrearage, CreationTime, SendSmsNumber) VALUES(" + args + ", '" + NoticeModel.PlateNumber + "', '" + NoticeModel.TelePhone + "', " + NoticeModel.SUMArrearage + ", '" + NoticeModel.SendSmsDatetime + "', " + NoticeModel.SendSmsNumber + ")");
                }
                else if (NoticeModel.SUMArrearage < int.Parse(_settingStore.GetSettingOrNull(args, null, "UnpaidMoney").Value) && NoticeModel.SendSmsNumber != 0)
                {
                    NoticeModel.SendSmsNumber = 0;

                    SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, System.Data.CommandType.Text, "UPDATE ExtOtherAccount SET SendSmsNumber = " + NoticeModel.SendSmsNumber + " WHERE TelePhone = '" + NoticeModel.TelePhone + "'");
                }
            }
        }
    }
}
