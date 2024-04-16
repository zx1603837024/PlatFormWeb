using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.Menus
{
    /// <summary>
    /// 
    /// </summary>
    [Table("AbpMenus")]
    [Serializable]
    public class Menu : Entity<int>, ICreationAudited<Users.User>, IDeletionAudited, IMustHaveTenant, IPassivable
    {

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(32)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(30)]
        public virtual string FatherCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int Level { get; set; }

        /// <summary>
        /// 转换成本地语言
        /// </summary>
        [MaxLength(64)]
        public virtual string LocalizableString { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(200)]
        public virtual string Icon { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(200)]
        public virtual string Url { get; set; }

        /// <summary>
        /// 是否需校验权限
        /// </summary>
        public virtual bool RequiresAuthentication { get; set; }

        /// <summary>
        /// 验证权限标识
        /// </summary>
        [MaxLength(30)]
        public virtual string RequiredPermissionName { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public virtual int Order { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual bool IsDeleted { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int TenantId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual long? DeleterUserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime? DeletionTime { get; set; }

        /// <summary>
        /// 层级体系
        /// </summary>
        [MaxLength(500)]
        public virtual string Discriminator { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Users.User CreatorUser { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual long? CreatorUserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// 静态功能标记
        /// </summary>
        public virtual bool IsStatic { get; set; }

        /// <summary>
        /// 功能菜单标记
        /// </summary>
        public virtual bool IsFunction { get; set; }

        /// <summary>
        /// 菜单类型
        /// </summary>
        [MaxLength(30)]
        public virtual string MenuType { get; set; }
    }
}
