using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using P4.Businesses;
using System;

namespace P4.EmployeeCharges.Dto
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(BusinessDetail))]
    public class EmployeeChargesDto : EntityDto<long>
    {
        /// <summary>
        /// 现金刷卡收入+现金刷卡补缴
        /// </summary>
        public decimal SumIncomePlusBack { get; set; }

        /// <summary>
        /// 补缴金额
        /// </summary>
        public decimal SumRepayment { get; set; }

        /// <summary>
        /// 收费员工号
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public string Date { get; set; }
        /// <summary>
        /// 实收总和
        /// </summary>
        public decimal SumFactReceive { get; set; }

        /// <summary>
        /// 现金总和
        /// </summary>
        public decimal? XJSumFactReceive { get; set; }

        /// <summary>
        /// 刷卡总和
        /// </summary>
        public decimal? SKSumFactReceive { get; set; }

        /// <summary>
        /// 欠费总和
        /// </summary>
        public decimal SumArrearage { get; set; }

        /// <summary>
        /// 微信补缴
        /// </summary>
        public decimal WXSumRepayment { get; set; }

        /// <summary>
        /// 微信支付
        /// </summary>
        public decimal WxSumFactReceive { get; set; }
        /// <summary>
        /// 应收总和
        /// </summary>
        public decimal? SumMoney { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int CarInCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int CarOutCount { get; set; }

        /// <summary>
        /// 总现金补缴
        /// </summary>
        public decimal? XJSumRepayment { get; set; }
        /// <summary>
        /// 总刷卡补缴
        /// </summary>
        public decimal? SKSumRepayment { get; set; }
        /// <summary>
        /// 收费操作员Id
        /// </summary>
        public long? ChargeOperaId { get; set; }

        /// <summary>
        /// 收费操作员
        /// </summary>
        public string ChargeOperaName{ get;set;}

        /// <summary>
        /// 出场收费员Id
        /// </summary>
        public long? OutOperaId { get; set; }

        /// <summary>
        /// 出场收费员
        /// </summary>

        public string OutOperaName { get; set; }
        /// <summary>
        /// 预付费
        /// </summary>
        public decimal Prepaid { get; set; }

        /// <summary>
        /// 应收
        /// </summary>
        public decimal Receivable { get; set; }

        /// <summary>
        /// 实收
        /// </summary>
        public decimal FactReceive { get; set; }

        /// <summary>
        /// 欠费金额
        /// </summary>
        public decimal Arrearage { get; set; }

        /// <summary>
        /// 车辆入场时间
        /// </summary>
        public DateTime CarInTime { get; set; }

        /// <summary>
        /// 车辆出场时间
        /// </summary>
        public DateTime? CarOutTime { get; set; }

        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime? CarPayTime { get; set; }

        #region escape field

        /// <summary>
        /// 补缴金额（目前只是支持补缴一次，既补缴金额等于欠费金额）
        /// </summary>
        public decimal Repayment { get; set; }

        /// <summary>
        /// 补缴时间
        /// </summary>
        public DateTime? CarRepaymentTime { get; set; }

        /// <summary>
        /// 逃逸时间
        /// </summary>
        public DateTime? EscapeTime { get; set; }

        /// <summary>
        /// 支付类型（逃逸）
        /// </summary>
        public Int16 EscapePayStatus { get; set; }

        /// <summary>
        /// 是否支付（逃逸）
        /// </summary>
        public bool IsEscapePay { get; set; }



        /// <summary>
        /// 
        /// </summary>
        public int? EscapeTenantId { get; set; }
        #endregion

        /// <summary>
        /// 支付类型
        /// </summary>
        public Int16 PayStatus { get; set; }

        /// <summary>
        /// 是否支付
        /// </summary>
        public bool IsPay { get; set; }

        /// <summary>
        /// 停车类型
        /// </summary>
        public Int16 StopType { get; set; }

        /// <summary>
        /// 费用类型
        /// 1为正常收费
        /// 2为追缴收费
        /// </summary>
        public Int16 FeeType { get; set; }

        /// <summary>
        /// 停车时长
        /// </summary>
        public Int32? StopTime { get; set; }



        /// <summary>
        /// 开户充值总额
        /// </summary>
        public decimal CardMoney { get; set; }






        /// <summary>
        /// 实收率
        /// </summary>
        public string Yield
        {
            get
            {
                if (SumMoney.HasValue && SumMoney.Value > 0)
                {
                    return ((XJSumFactReceive == null ? 0 : XJSumFactReceive) * 100 / SumMoney).Value.ToString("#0.00") + "%";
                }
                else
                {
                    return "0.00%";
                }
            }
        }

        /// <summary>
        /// 取证收费率
        /// </summary>
        public string Teaching
        {
            get
            {
                if (CarInCount > 0 && CarOutCount > 0)
                {
                    //pda 录入数量 除以 地磁 录入数量 = 录入率
                    return (CarInCount * 100 / CarOutCount).ToString();
                }
                else
                {
                    return "0";
                }
            }
        }

        /// <summary>
        /// 总实收率(收入金额包含补缴费用)
        /// </summary>
        public string AllYield {
            get
            {
                if (SumMoney.HasValue && SumMoney.Value > 0)
                {
                    return (SumIncomePlusBack * 100 / SumMoney).Value.ToString("#0.00") + "%";
                }
                else
                {
                    return "0.00%";
                }
            }
        }

        public string ddd
        {
            get
            {
                if (SumMoney.HasValue && SumMoney.Value > 0)
                {
                    return ((XJSumFactReceive == null ? 0 : XJSumFactReceive) * 100 / (SumMoney == null ? 0 : SumMoney)).Value.ToString("#0.00") + "%";
                }
                else
                {
                    return "0.00%";
                }
            }
        }

        /// <summary>
        /// 总实收率(收入金额包含补缴费用)
        /// </summary>
        public string AllYieldd
        {
            get
            {
                if (SumMoney.HasValue && SumMoney.Value > 0)
                {
                    return (SumIncomePlusBack * 100 / (SumMoney == null ? 0 : SumMoney)).Value.ToString("#0.00") + "%";
                }
                else
                {
                    return "0.00%";
                }
            }
        }

        #region  基础数据
        /// <summary>
        /// 商户Id
        /// </summary>
        public int TenantId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int CompanyId { get; set; }
       
        /// <summary>
        /// 
        /// </summary>
        public int RegionId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ParkId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int BerthsecId { get; set; }

        /// <summary>
        /// 出场收费员名字
        /// </summary>
        public string chuchangName { get; set; }
        /// <summary>
        /// 补缴收费员名字
        /// </summary>
        public string bujiaoName { get; set; }
        #endregion
    }
}
