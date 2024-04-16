using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using P4.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.AppManage.Dto
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(AppAccount))]
    public class AppManageDto : EntityDto<long>
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public virtual string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public virtual string Password { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public virtual string Phone { get; set; }

        /// <summary>
        /// 钱包
        /// </summary>
        public virtual decimal Wallet { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public virtual string VersionNum { get; set; }

        /// <summary>
        /// 是否锁定
        /// </summary>
        public virtual bool IsLock { get; set; }

        /// <summary>
        /// 拍摄时间
        /// </summary>
        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// 车牌一
        /// </summary>
        public virtual string PlateNumber1 { get; set; }

        /// <summary>
        /// 是否认证
        /// </summary>
        public virtual bool Status1 { get; set; }

        /// <summary>
        /// 车牌二
        /// </summary>
        public virtual string PlateNumber2 { get; set; }

        /// <summary>
        /// 是否认证
        /// </summary>
        public virtual bool Status2 { get; set; }

        /// <summary>
        /// 车牌三
        /// </summary>
        public virtual string PlateNumber3 { get; set; }

        /// <summary>
        /// 是否认证
        /// </summary>
        public virtual bool Status3 { get; set; }
    }
}
