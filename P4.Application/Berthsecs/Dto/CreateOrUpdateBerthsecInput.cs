using Abp.AutoMapper;
using Abp.Application.Services.Dto;
namespace P4.Berthsecs.Dto
{


    /// <summary>
    /// 
    /// </summary>
    [AutoMapTo(typeof(Berthsec))]
    public class CreateOrUpdateBerthsecInput : EntityDto, IInputDto, IOperDto
    {
        /// <summary>
        /// 
        /// </summary>
        public string BerthsecName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RegionName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int BeginNumeber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int EndNumeber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CustomNumeber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ParkName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string oper { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int RegionId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RateName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RateName1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RateName2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ParkId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool UseStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool PushStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CompanyId { get; set; }
    }
}
