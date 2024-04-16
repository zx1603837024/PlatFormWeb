using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using P4.Smss;
using Abp.Application.Services.Dto;

namespace P4.SmsManagements.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(Sms))]
    public class SmsDto : EntityDto<long>
    {
        /// <summary>
        /// 
        /// </summary>
        public string SmsMsg { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SmsResult
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string XMLStr 
        {
            get
            {
                string a = SmsResult.Replace("<", "&#060");
                a = a.Replace("?", "&#063");
                a = a.Replace(" ", "&#032");
                a = a.Replace("=", "&#061");
                a = a.Replace("\"", "&#034 ");
                a = a.Replace(".", "&#046 ");
                a = a.Replace("/", "&#047");
                a = a.Replace(">", "&#062");
                return a;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 短信条数
        /// </summary>
        public int SmsCount { get; set; }
    }
}
