using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Card.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(IPassBlackCard))]
    public class IPassBlackCardDto : EntityDto
    {
        /// <summary>
        /// 卡号
        /// </summary>
        [MaxLength(20)]
        public string CardNo { get; set; }
    }
}
