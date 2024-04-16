using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4.Web.Models
{
    public class SystemParamSetting
    {
        public bool ParkApprove { get; set; }
        public bool TaskScheduling { get; set; }
        public bool OperationLog { get; set; }
        public bool DataLog { get; set; }
        public bool AuditLog { get; set; }
        public bool WhiteListOperation { get; set; }
        public bool SystemMailbox { get; set; }

        /// <summary>
        /// 短信通道
        /// </summary>
        public bool SMSChannel { get; set; }

        /// <summary>
        /// 开启地图定位
        /// </summary>
        public bool MapLocation { get; set; }

        /// <summary>
        /// 全国追缴
        /// </summary>
        public bool TheRecovered { get; set; }

        /// <summary>
        /// 欠费黑名单
        /// </summary>
        public bool EscapeBlack { get; set; }

        /// <summary>
        /// 欠费黑名单金额
        /// </summary>
        public int EscapeBlackMoney { get; set; }

        /// <summary>
        /// 欠费拍照
        /// </summary>
        public bool EscapePhoto { get; set; }

        /// <summary>
        /// 欠费拍照金额
        /// </summary>
        public int EscapePhotoMoney { get; set; }

        /// <summary>
        /// 全分公司追缴
        /// </summary>
        public bool TheRecoveredCompany { get; set; }

        /// <summary>
        /// 追缴锁定功能
        /// </summary>
        public bool EscapeLock { get; set; }

        /// <summary>
        /// 试用邮箱地址
        /// </summary>
        public string EmailAdress { get; set; }

        ///<summary>
        ///达标金额标准
        ///</summary>

        public decimal TargetAmount { get; set; }

    }
}