using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.ParkingLot.Dto
{
    /// <summary>
    /// 停车记录
    /// </summary>
    public class ParkingRecordEntity
    {
        /// <summary>
        /// 停车编号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 订单ID
        /// </summary>
        public long OrderId { get; set; }

        /// <summary>
        /// 商户名称
        /// </summary>
        public string TenantName { get; set; }

        /// <summary>
        /// 分公司名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 入场通道
        /// </summary>
        public string InChannel { get; set; }

        /// <summary>
        /// 出场通道
        /// </summary>
        public string OutChannel { get; set; }

        /// <summary>
        /// 停车场名称
        /// </summary>
        public string ParkName { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string PlateNumber { get; set; }

        /// <summary>
        /// 停车时长(分钟)
        /// </summary>
        public int? StopTime { get; set; }

        /// <summary>
        /// 停车时长描述
        /// </summary>
        public string StopTimeStr {
            get {
                if (!StopTime.HasValue) return "";
                if (StopTime.Value <= 0) return "不足1分钟";
                var day = "";
                var hours = "";
                var minute = "";
                if (StopTime < 60)
                {
                    minute= $"{StopTime.Value}分钟";
                }
                else if(StopTime< 60*24)
                {
                    hours = $"{StopTime.Value / 60}小时";
                    minute = StopTime.Value % 60 == 0 ? "" : $"{StopTime.Value % 60}分钟";
                }
                else
                {
                    day = $"{StopTime.Value / (60 * 24)}天";
                    var otherHours = StopTime.Value % (60 * 24);
                    hours = otherHours == 0 ? "" : $"{otherHours / 60}小时";
                    var otherMinutes = otherHours % 60;
                    minute = otherMinutes == 0 ? "" : $"{otherMinutes}分钟";
                }
                return $"{day}{hours}{minute}";
            }
        }
    
        /// <summary>
        /// 应付金额
        /// </summary>
        public decimal? PayableAmount { get; set; }

        /// <summary>
        /// 实收金额
        /// </summary>
        public decimal? FactReceive { get; set; }

        /// <summary>
        /// 优惠金额
        /// </summary>
        public decimal? DiscountMoney { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public string OrderStatus { get; set; }

        /// <summary>
        /// 驶入时间
        /// </summary>
        public DateTime? CarInTime { get; set; }

        /// <summary>
        /// 驶入时间
        /// </summary>
        public string CarInTimeStr 
        { 
            get {
                if (!CarInTime.HasValue) return "";
                else return CarInTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
            } 
        }

        /// <summary>
        /// 车辆驶出时间
        /// </summary>
        public DateTime? CarOutTime { get; set; }

        /// <summary>
        /// 车辆驶出时间
        /// </summary>
        public string CarOutTimeStr { get { return CarOutTime.HasValue ? CarOutTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : ""; } }

        /// <summary>
        /// 停车类型
        /// </summary>
        public string CarType { get; set; }

        /// <summary>
        /// 区域名称
        /// </summary>
        public string RegionName { get; set; }
    }
}
