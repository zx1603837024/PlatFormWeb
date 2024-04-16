using Abp.Application.Services;
using System;
using Abp.Helper;
using P4.WeixinSendRecord.Dtos;

namespace P4.WeixinsSendRecords
{
    /// <summary>
    /// 
    /// </summary>
    public class WeixinSendRecordAppService : ApplicationService, IWeixinSendRecordAppService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        public void Insert(WeixinSendRecordDto dto)
        {
            SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, System.Data.CommandType.Text, "INSERT INTO WeixinSendRecords(TenantId, PlateNumber, openId, nickName, SUMArrearage, SendWeixinNumber, CreationTime) VALUES(" + dto.args + ",'" + dto.PlateNumber + "','" + dto.openId + "','" + dto.nickName + "'," + dto.SUMArrearage + "," + dto.SendWeixinNumber + ",'" + dto.SendWeixinDatetime + "')");
        }
    }
}
