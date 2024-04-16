using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.BlackLists.Dtos
{

    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(BlackList))]
    public class CreateOrUpdateBlackListsInput : EntityRequestInput, IOperDto
    {

        /// <summary>
        /// 
        /// </summary>
        public string oper { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string VehicleType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RelateNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CarOwner { get; set; }

        /// <summary>
        /// 告知手机号码
        /// </summary>
        public string IdNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CompanyId
        {
            get
            {
                if (string.IsNullOrWhiteSpace(CompanyName))
                {
                    return 0;
                }
                return int.Parse(CompanyName);
            }
        }

        /// <summary>
        /// 折扣
        /// </summary>
        public decimal Rebate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsActive { get; set; }
    }
}
