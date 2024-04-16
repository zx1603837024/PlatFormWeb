using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.App
{
    /// <summary>
    /// App账号
    /// </summary>
    [Table("AbpAppAccounts")]
    public class AppAccount : Entity<long>
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [MaxLength(30)]
        public virtual string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [MaxLength(50)]
        public virtual string Password { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [MaxLength(20)]
        public virtual string Phone { get; set; }

        /// <summary>
        /// 手机验证码
        /// </summary>
        [MaxLength(10)]
        public virtual string PhoneCode { get; set; }

        /// <summary>
        /// 发送验证码时间
        /// </summary>
        public virtual DateTime CodeTime { get; set; }

        /// <summary>
        /// 钱包
        /// </summary>
        public virtual decimal Wallet { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        [MaxLengthAttribute(30)]
        public virtual string VersionNum { get; set; }

        /// <summary>
        /// 是否锁定
        /// </summary>
        public virtual bool IsLock { get; set; }

        public virtual DateTime CreationTime { get; set; }

        public virtual DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// 车牌一
        /// </summary>
        [MaxLength(10)]
        public virtual string PlateNumber1 { get; set; }

        /// <summary>
        /// 是否认证
        /// </summary>
        public virtual bool Status1 { get; set; }

        /// <summary>
        /// 车牌二
        /// </summary>
        [MaxLength(10)]
        public virtual string PlateNumber2 { get; set; }

        /// <summary>
        /// 是否认证
        /// </summary>
        public virtual bool Status2 { get; set; }

        /// <summary>
        /// 车牌三
        /// </summary>
        [MaxLength(10)]
        public virtual string PlateNumber3 { get; set; }

        /// <summary>
        /// 是否认证
        /// </summary>
        public virtual bool Status3 { get; set; }
    }
}
