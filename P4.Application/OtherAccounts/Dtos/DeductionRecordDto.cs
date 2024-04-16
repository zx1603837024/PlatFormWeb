using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace P4.OtherAccounts.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(DeductionRecord))]
    public class DeductionRecordDto: EntityDto<long>
    {
        /// <summary>
        /// 卡号
        /// </summary>
        [MaxLength(20)]
        public virtual string CardNo { get; set; }

        /// <summary>
        /// 费用类型
        /// 1续费，2消费，3预付，4返还
        /// </summary>
        public virtual Int16 OperType { get; set; }

        /// <summary>
        /// 卡操作类型
        /// </summary>
        public virtual string OperTypeName {
            get {
                switch (OperType)
                { 
                    case 1:
                        return "续费";
                    case 2:
                        return "消费";
                    case 3:
                        return "预付";
                    case 4:
                        return "返还";
                    default:
                        return "未知"; 
                }
            }
        }

        /// <summary>
        /// 金额
        /// </summary>
        public virtual decimal Money { get; set; }

        /// <summary>
        /// 处理前金额
        /// </summary>
        public virtual decimal BeginMoney { get; set; }

        /// <summary>
        /// 处理后金额
        /// </summary>
        public virtual decimal EndMoney { get; set; }

        /// <summary>
        /// 成功状态
        /// true成功
        /// false未成功
        /// </summary>
        public virtual bool PayStatus { get; set; }


        public virtual DateTime InTime { get; set; }

        public virtual string InTimeStr 
        {
            get 
            {
                return string.IsNullOrWhiteSpace(InTime.ToString()) != true ? InTime.ToString() : null;
            }
        }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(30)]
        public virtual string Remark { get; set; }
        /// <summary>
        /// 账户余额
        /// </summary>
        public virtual decimal Wallet { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public virtual string UserName { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public virtual string TelePhone { get; set; }



        /// <summary>
        /// 商户 统计总的充值金额
        /// </summary>
        public virtual decimal SumCZ_Money { get; set; }
        /// <summary>
        /// 商户 统计总的消费金额
        /// </summary>
        public virtual decimal SumXF_Money { get; set; }
        /// <summary>
        /// 商户 统计总的预付金额
        /// </summary>
        public virtual decimal SumYF_Money { get; set; }
        /// <summary>
        /// 商户 统计总的返还金额
        /// </summary>
        public virtual decimal SumFH_Money { get; set; }
        /// <summary>
        /// 商户 统计总的余额
        /// </summary>
        public virtual decimal SumWallet_Money { get; set; }
        /// <summary>
        /// 商户名字
        /// </summary>
        public virtual string TenancyName { get; set; }
        /// <summary>
        /// 商户ID
        /// </summary>
        public virtual int TenantId { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public virtual string PlateNumber { get; set; }

        /// <summary>
        /// 商户 统计时间段充值金额
        /// </summary>
        public virtual decimal SumCZ_Money_A { get; set; }
        /// <summary>
        /// 商户 统计时间段消费金额
        /// </summary>
        public virtual decimal SumXF_Money_A { get; set; }
        /// <summary>
        /// 商户 统计时间段预付金额
        /// </summary>
        public virtual decimal SumYF_Money_A { get; set; }
        /// <summary>
        /// 商户 统计时间段返还金额
        /// </summary>
        public virtual decimal SumFH_Money_A { get; set; }

        /// <summary>
        /// 实际卡号
        /// </summary>
        public virtual string ProductNo { get; set; }

        public virtual DateTime EnabledTime { get; set; }


    }
}
