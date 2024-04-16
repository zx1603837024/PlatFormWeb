
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.SmsManagements.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class SmsAccountDto : EntityDto<int>
    {
        /// <summary>
        /// 
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 采用;隔开
        /// </summary>
        public string Destnumbers { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SignValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MsgValue { get; set; }


        private string sign;
        /// <summary>
        /// 公司签名
        /// </summary>
        public string Sign
        {
            get
            {
                if (string.IsNullOrEmpty(sign))
                    sign = "道路停车";
                SignValue = sign;
                return Abp.Text.Formatting.FormatString.FormatStringToUTF8("【" + sign + "】");
            }
            set
            {
                sign = value;
            }
        }

        private string msg;

        /// <summary>
        /// 
        /// </summary>
        public string Msg
        {
            get
            {
                MsgValue = msg;
                return Abp.Text.Formatting.FormatString.FormatStringToUTF8(msg);
            }
            set
            {
                msg = value;
            }
        }

        /// <summary>
        /// 定时发送时间
        /// </summary>
        public DateTime? Sendtime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "{\"id\": 1,\"method\":\"send\",\"params\":{\"userid\":\"" + UserId + "\",\"password\":\"" + Password + "\",\"submit\": [{\"content\":\"" + Msg + "\",\"phone\":\"" + Destnumbers + "\"}] }}";
        }
    }
}
