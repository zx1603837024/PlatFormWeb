using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.AppManage.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public class NoticeDto
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual int TenantId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual decimal SUMArrearage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string PlateNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string IsDeleted { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string openId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string TelePhone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string CarNumber1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string CarNumber2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string CarNumber3 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string CreatorUserId { get; set; }   

        /// <summary>
        /// 
        /// </summary>
        public virtual string nickName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int SendWeixinNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime SendWeixinDatetime { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public virtual int SendSmsNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime SendSmsDatetime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string SendTimes { get; set; }

    }
}
