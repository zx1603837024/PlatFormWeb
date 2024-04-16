using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
namespace P4.OtherAccounts.Dtos
{
    /// <summary>
    /// 第三方账号管理
    /// </summary>
    [AutoMapFrom(typeof(OtherAccount))]
    public class OtherAccountDto : EntityDto<long>
    {
        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Client { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TelePhone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsPhoneConfirmed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AuthenticationSource { get; set; }


        /// <summary>
        /// 是否可用 ture 可用 false 退卡
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsLock { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModificationTimeString
        {
            get
            {
                //判断最后修改时间是否为空
                if (LastModificationTime == null)
                {
                    return "";
                }
                else
                {
                    return Convert.ToDateTime(LastModificationTime).ToString("yyyy-MM-dd HH:mm:ss");
                }

            }
        }

        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNo { get; set; }

        /// <summary>
        /// 生成编号
        /// </summary>
        public virtual string ProductNo { get; set; }

        /// <summary>
        /// 是否激活
        /// true：激活
        /// false：未激活
        /// </summary>
        public virtual bool IsEnabled { get; set; }

        /// <summary>
        /// 激活时间
        /// </summary>
        public virtual DateTime? EnabledTime { get; set; }

        /// <summary>
        /// 激活用户id
        /// </summary>
        public virtual long? EnabledUserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string EnabledUserName { get; set; }

        /// <summary>
        /// 钱包
        /// </summary>
        public decimal Wallet { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string PlateNumber { get; set; }

        /// <summary>
        /// 是否自动扣款
        /// </summary>
        public bool AutoDeduction { get; set; }
    }
}
