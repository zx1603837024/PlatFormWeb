/********************************************************************************
** auth： 黎通
** date： 2016/1/5 18:26:03
** desc： 尚未编写描述
** Ver.:  V1.0.0
*********************************************************************************/
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using P4.App;

namespace P4.CarownerApp.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(AppAccount))]
    public class AppAccountOutput :EntityDto<long>, IOutputDto
    {
        /// <summary>
        /// 钱包
        /// </summary>
        public virtual decimal Wallet { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Phone { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public virtual bool Status1 { get; set; }

        /// <summary>
        /// 车牌号1
        /// </summary>
        public virtual string PlateNumber1 { get; set; }


        public virtual bool Status2 { get; set; }

        /// <summary>
        /// 车牌号2
        /// </summary>
        public virtual string PlateNumber2 { get; set; }


        public virtual bool Status3 { get; set; }

        /// <summary>
        /// 车牌号3
        /// </summary>
        public virtual string PlateNumber3 { get; set; }
    }
}
