using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Rates.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(Rate))]
    public class RateDto : EntityDto<int>
    {
        /// <summary>
        /// 
        /// </summary>
       
        public string RateName { get; set; }

        /// <summary>
        /// 
        /// </summary>

        public string RateJson { get; set; }

        private string _ratePDA;
      
        /// <summary>
        /// 
        /// </summary>
        public string RatePDA
        {
            get { return _ratePDA; }
            set { _ratePDA = RateJson; }
        }
        /// <summary>
        /// 是否激活
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 费率备注
        /// </summary>

        public string Remark { get; set; }


        public int TenantId { get; set; }
        public string TenantName { get; set; }
        public int CompanyId { get; set; }  
      
        public string CompanyName { get; set; }

        public DateTime CreationTime { get; set; }

        public long? CreatorUserId { get; set; }

        public DateTime? LastModificationTime { get; set; }

        public long? LastModifierUserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierUserName { get; set; }
        public string CreatorUserName { get; set; }
    }


}
