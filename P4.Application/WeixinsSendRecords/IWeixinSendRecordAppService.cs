using Abp.Application.Services;
using P4.WeixinSendRecord.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.WeixinsSendRecords
{
    /// <summary>
    /// 
    /// </summary>
   public  interface IWeixinSendRecordAppService : IApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        void Insert(WeixinSendRecordDto dto);
    }
}
