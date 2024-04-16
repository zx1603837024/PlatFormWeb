using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace P4.Regions.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapTo(typeof(Region))]
    public class CreateOrUpdateDtoInput : EntityDto, IInputDto, IOperDto
    {
        /// <summary>
        ///  
        /// </summary>
        public int FatherId
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(30)]
        public string RegionName
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(100)]
        public string Mark
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string oper { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(10)]
        public virtual string WorkRuleTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(10)]
        public virtual string OffRuleTime { get; set; }
    }
}
