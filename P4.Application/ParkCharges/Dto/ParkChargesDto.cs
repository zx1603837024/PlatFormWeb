using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using P4.Businesses;
using P4.Employees;
using P4.MultiTenancy;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.ParkCharges.Dto
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(BusinessDetail))]
    public class ParkChargesDto : EntityDto<long>
    {

        /// <summary>
        /// 当月欠费总额
        /// </summary>
        public decimal? SumOwn { get; set; }
        /// <summary>
        /// 当月已追缴
        /// </summary>
        public decimal? SumRecovered { get; set; }
        /// <summary>
        /// 跨月追缴
        /// </summary>
        public decimal? SpanSumRecovered { get; set; }
        /// <summary>
        /// 删除前月追缴
        /// </summary>
        public decimal? DelSumRecovered { get; set; }
        /// <summary>
        /// 月份
        /// </summary>
        public string Month { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? SumOwnRec { get; set; }

        /// <summary>
        /// 账号支付
        /// </summary>
        public decimal? AccSumFactReceive { get; set; }

        /// <summary>
        /// 账号补缴
        /// </summary>
        public decimal? AccSumRepayment { get; set; }

        /// <summary>
        /// 其他金额
        /// </summary>
        public decimal? AccSumOtherMoney { get; set; }


        /// <summary>
        /// 实收总和
        /// </summary>

        public decimal? SumFactReceive { get; set; }

        /// <summary>
        /// 现金总和
        /// </summary>
        public decimal? XJSumFactReceive { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private decimal? _sensorSumReceivable;
        /// <summary>
        /// 车检器应收
        /// </summary>
        public decimal? SensorSumReceivable
        {
            get { return _sensorSumReceivable.HasValue ? _sensorSumReceivable.Value : 0; }
            set { _sensorSumReceivable = value; }
        }

        private decimal? _onlineSumFactReceive;
        /// <summary>
        /// 在线支付
        /// </summary>
        public decimal? OnlineSumFactReceive
        {
            get { return _onlineSumFactReceive.HasValue ? _onlineSumFactReceive.Value : 0; }
            set { _onlineSumFactReceive = value; }
        }

        /// <summary>
        /// 地磁停车次数
        /// </summary>
        public int SensorTimes { get; set; }

        /// <summary>
        /// pos停车次数
        /// </summary>
        public int PosTimes { get; set; }

        /// <summary>
        /// 进场车辆数
        /// </summary>
        public int PosInTimes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int PosOutTimes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private decimal? _SKSumFactReceive;

        /// <summary>
        /// 刷卡总和
        /// </summary>
        public decimal? SKSumFactReceive
        {
            get
            {
                if (_SKSumFactReceive.HasValue)
                    return _SKSumFactReceive;
                else
                    return 0;
            }
            set { _SKSumFactReceive = value; }
        }

        /// <summary>
        /// 欠费总和
        /// </summary>
        public decimal? SumArrearage { get; set; }

        /// <summary>
        /// 应收总和
        /// </summary>
        public decimal? SumMoney { get; set; }


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
        /// 实收率
        /// </summary>
        public string Yield
        {
            get
            {
                if (SumMoney.HasValue && SumMoney.Value > 0)
                {
                    return ((XJSumFactReceive.HasValue ? XJSumFactReceive.Value : 0) * 100 / SumMoney).Value.ToString("#0.00") + "%";
                }
                else
                {
                    return "0.00%";
                }
            }
        }


        private decimal? _SumRepayment;

        /// <summary>
        /// 欠费补缴
        /// </summary>
        public decimal? SumRepayment
        {
            get
            {
                if (_SumRepayment.HasValue)
                    return _SumRepayment.Value;
                else
                    return 0;
            }
            set { _SumRepayment = value; }
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
        /// 停车场名称
        /// </summary>
        public string ParkName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int BerthsecId { get; set; }
        #endregion
        
    }
}
