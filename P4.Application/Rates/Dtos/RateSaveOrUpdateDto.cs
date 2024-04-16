using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Rates.Dtos
{

    [AutoMapFrom(typeof(Rate))]

    public class RateSaveOrUpdateDto : EntityRequestInput, IOperDto
    {
        public string oper { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RateName { get; set; }

        /// <summary>
        /// 
        /// </summary>

        //public string RateJson { get; set; }

        //private string _ratePDA;

        //public string RatePDA
        //{
        //    get { return _ratePDA; }
        //    set { _ratePDA = RateJson; }
        //}
        /// <summary>
        /// 是否激活
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 费率备注
        /// </summary>

        public string Remark { get; set; }


        //public int TenantId { get; set; }
        //public string TenantName { get; set; }
        //public int CompanyId { get; set; }

        //public string CompanyName { get; set; }
        
    }

}

