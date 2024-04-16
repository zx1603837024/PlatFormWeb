using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using P4.Card;

namespace P4.CardCode.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(IpassCardCode))]
    public class IPassCardCodeDto: EntityDto
    {
        /// <summary>
        /// 
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsActive { get; set; }

       
    }
}
