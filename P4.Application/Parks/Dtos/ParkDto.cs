using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;

namespace P4.Parks.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(Park))]
    public class ParkDto : EntityDto<int>
    {

        /// <summary>
        /// 
        /// </summary>
        public int RegionId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RegionName { get; set; }

        /// <summary>
        /// 停车场
        /// </summary>
        public string ParkName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierUserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorUserName { get; set; }

        /// <summary>
        /// 停车场类型
        /// </summary>
        public Int16 ParkType { get; set; }

        /// <summary>
        /// 停车场类型
        /// </summary>
        public string ParkTypeName
        {
            get {
                if (ParkType == 2)
                    return "路侧车场";
                else if (ParkType == 1)
                    return "封闭车场";
                return "未知车场";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Mark { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int BerthCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int TenantId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// 泊位开始编号
        /// </summary>
        [MaxLength(10)]
        public virtual string BeginNumber { get; set; }

        /// <summary>
        /// 泊位结束编号
        /// </summary>
        [MaxLength(10)]
        public virtual string EndNumber { get; set; }

        /// <summary>
        /// 其他编号
        /// 用,隔开
        /// </summary>
        [MaxLength(500)]
        public virtual string OtherNumber { get; set; }

        /// <summary>
        /// 在停车位数
        /// </summary>
        public int ZTNumber { get; set; }

        /// <summary>
        /// 总车位数
        /// </summary>
        public int SumNumber { get; set; }

        /// <summary>
        /// 智博平台ID
        /// </summary>
        public string ZhiBoParkId { get; set; }
    }
}
