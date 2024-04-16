using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.ParkCharges.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public class ParkChargesData
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
        /// 账号支付
        /// </summary>
        public decimal? AccSumFactReceive { get; set; }

        /// <summary>
        /// 账号补缴
        /// </summary>
        public decimal? AccSumRepayment { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? AccSumOtherMoney { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string ParkName { get; set; }
        /// <summary>
        /// 实收总和
        /// </summary>
        public decimal? SumFactReceive { get; set; }

        /// <summary>
        /// 现金总和
        /// </summary>
        public decimal? XJSumFactReceive { get; set; }

        /// <summary>
        /// 刷卡总和
        /// </summary>
        public decimal? SKSumFactReceive { get; set; }

        /// <summary>
        /// 在线支付
        /// </summary>
        public decimal? OnlineSumFactReceive { get; set; }
        /// <summary>
        /// 欠费总和
        /// </summary>
        public decimal? SumArrearage { get; set; }
        /// <summary>
        /// 应收总和
        /// </summary>
        public decimal? SumMoney { get; set; }
        /// <summary>
        /// 总现金补缴
        /// </summary>
        public decimal? SumRepayment { get; set; }

        /// <summary>
        /// 车检器应收总和
        /// </summary>
        public decimal? SensorSumReceivable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? PosTimes { get; set; }

        /// <summary>
        /// Pos进场次数
        /// </summary>
        public int? PosInTimes { get; set; }
        /// <summary>
        /// Pos出场次数
        /// </summary>
        public int? PosOutTimes { get; set; }
        /// <summary>
        /// 地磁停车次数
        /// </summary>
        public int? SensorTimes { get; set; }
    }
}
