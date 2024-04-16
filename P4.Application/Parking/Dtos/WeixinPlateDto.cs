using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using P4.Weixin;
using System.ComponentModel.DataAnnotations;

namespace P4.Parking.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(Weixinplates))]
    public class WeixinPlateDto : EntityDto
    {
        /// <summary>
        /// 
        /// </summary>
        [MaxLength(6)]
        public string Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int orders { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ValueCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
