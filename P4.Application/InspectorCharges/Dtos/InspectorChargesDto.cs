using Abp.Application.Services.Dto;
using System;

namespace P4.InspectorCharges.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class InspectorChargesDto : EntityDto<long>
    {
        /// <summary>
        /// 现金刷卡收入+现金刷卡补缴
        /// </summary>
        public Int32 SumIncomePlusBack { get; set; }
        /// <summary>
        /// 收费员工号
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 实收总和
        /// </summary>
        public Int32 SumFactReceive { get; set; }

        /// <summary>
        /// 现金总和
        /// </summary>
        public Int32 XJSumFactReceive { get; set; }

        /// <summary>
        /// 刷卡总和
        /// </summary>
        public Int32 SKSumFactReceive { get; set; }

        /// <summary>
        /// 欠费总和
        /// </summary>
        public Int32 SumArrearage { get; set; }
        /// <summary>
        /// 应收总和
        /// </summary>
        public Int32 SumMoney { get; set; }
        /// <summary>
        /// 总现金补缴
        /// </summary>
        public Int32 XJSumRepayment { get; set; }
        /// <summary>
        /// 总刷卡补缴
        /// </summary>
        public Int32 SKSumRepayment { get; set; }
        /// <summary>
        /// 收费操作员Id
        /// </summary>
        public long ChargeOperaId { get; set; }

        /// <summary>
        /// 收费操作员
        /// </summary>
        public string ChargeOperaName { get; set; }

        /// <summary>
        /// 发卡金额
        /// </summary>
        public decimal CardMoney { get; set; }


    }
}
