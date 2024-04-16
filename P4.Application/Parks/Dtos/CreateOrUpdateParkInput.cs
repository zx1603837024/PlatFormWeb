using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace P4.Parks.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapTo(typeof(Park))]
    public class CreateOrUpdateParkInput : EntityDto, IInputDto, IOperDto
    {
        /// <summary>
        /// 
        /// </summary>
        public string ParkName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int RegionId
        {
            get
            {
                if (!string.IsNullOrEmpty(RegionName))
                    return int.Parse(RegionName);
                return 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string RegionName { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string Mark { get; set; }

        public int CompanyId { get; set; }

        public int TenantId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int BerthCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ParkTypeName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int16 ParkType
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ParkTypeName))
                {
                    return 1;
                }
                else
                    return Int16.Parse(ParkTypeName);
            }
        }

        /// <summary>
        /// 操作类型
        /// </summary>
        public string oper { get; set; }

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
        /// 停车场平台ID
        /// </summary>
        [MaxLength(50)]
        public string ZhiBoParkId { get;  set; }
    }
}
