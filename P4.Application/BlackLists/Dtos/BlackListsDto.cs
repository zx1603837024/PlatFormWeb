using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace P4.BlackLists.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(BlackList))]
    public class BlackListsDto : EntityDto<int>
    {
        /// <summary>
        /// 
        /// </summary>
        public string VehicleType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RelateNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CarOwner { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Remarks { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string IdNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsDeleted { get; set; }

    }
}
