using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using P4.TicketCss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.TicketManagement.Dto
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(TicketStyle))]
    public class TicketStyleDto : EntityDto<int>
    {
        /// <summary>
        /// 类型
        /// </summary>
        public  string Status { get; set; }

        /// <summary>
        /// 二维码
        /// </summary>
        public  string TwoBarCode { get; set; }

        /// <summary>
        /// 文本
        /// </summary>
        public  string Text { get; set; }
    }
}
