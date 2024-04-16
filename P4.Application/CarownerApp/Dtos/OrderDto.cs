using System;

namespace P4.CarownerApp.Dtos
{
    /// <summary>
    /// 订单详情
    /// </summary>
    public class OrderDto
    {
        /// <summary>
        /// 总金额
        /// </summary>
        public String total { get; set; } 

        /// <summary>
        /// 停车场名称
        /// </summary>
        public String parkname { get; set; }

        /// <summary>
        /// 停车场地址
        /// </summary>
        public String address { get; set; }


        /// <summary>
        /// 结束时间
        /// </summary>
        public String etime { get; set; }

        /// <summary>
        /// 订单状态
        /// 1：未结算
        /// 2：已经结算
        /// 3：结算，未支付
        /// 4：结算（欠费补缴），已支付
        /// </summary>
        public String state { get; set; }

        /// <summary>
        /// 入场时间
        /// </summary>
        public String btime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string orderid { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string PlateNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String parkid { get; set; }
    }
}
