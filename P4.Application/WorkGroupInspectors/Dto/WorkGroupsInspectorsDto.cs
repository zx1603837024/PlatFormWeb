using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using P4.Berthsecs.Dto;
using P4.Inspectors.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace P4.WorkGroupInspectors.Dto
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(WorkGroupsInspectors))]
    public class WorkGroupsInspectorsDto : EntityDto<long>
    {
        /// <summary>
        /// 
        /// </summary>
        [MaxLength(50)]
        public string WorkGroupName { get; set; }

        /// <summary>
        /// 商户
        /// </summary>
        public int TenantId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TenantName { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 分公司
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? LastModifierUserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? CreatorUserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsStatic { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierUserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorUserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<BerthsecDto> Berthsecs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<InspectorDto> Inspectors { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private string _inspectors = null;

        /// <summary>
        /// 
        /// </summary>
        private string _berthsec = null;

        /// <summary>
        /// 
        /// </summary>
        public string InspectorsString
        {

            get
            {
                if (!string.IsNullOrWhiteSpace(_inspectors))
                    return _inspectors;
                if (Inspectors == null || Inspectors.Count == 0)
                    return null;
                foreach (var inspector in Inspectors)
                {
                    _inspectors += inspector.TrueName + ",";
                }
                return _inspectors.Substring(0, _inspectors.Length - 1);
            }
            set
            {
                _inspectors = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string BerthSecsString
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_berthsec))
                    return _berthsec;
                if (Berthsecs == null || Berthsecs.Count == 0)
                    return null;
                foreach (var berthsec in Berthsecs)
                {
                    _berthsec += berthsec.BerthsecName + ",";
                }
                return _berthsec.Substring(0, _berthsec.Length - 1);
            }
            set
            {
                _berthsec = value;
            }
        }
    }
}
