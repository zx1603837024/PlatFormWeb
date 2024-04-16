using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using P4.Webchat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.WebChat.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(Ad))]
    public class AdDto : EntityDto
    {
        public virtual string AdImage { get; set; }
    }
}
