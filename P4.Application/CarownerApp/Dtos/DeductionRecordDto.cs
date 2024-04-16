using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using P4.OtherAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.CarownerApp.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(DeductionRecord))]
    public class DeductionRecordDto : EntityDto<long>
    {
        /// <summary>
        /// 费用类型
        /// 1开卡（充值，续费），2消费
        /// ，3预付，4返还 
        /// </summary>
        public virtual Int16 OperType { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public virtual decimal Money { get; set; }

        /// <summary>
        /// 成功状态
        /// true成功
        /// false未成功
        /// </summary>
        public virtual bool PayStatus { get; set; }

        /// <summary>
        /// 支付时间
        /// </summary>
        public virtual DateTime InTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }
    }
}
