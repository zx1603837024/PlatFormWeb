/********************************************************************************
** auth： 黎通
** date： 2015/12/15 19:00:16
** desc： 尚未编写描述
** Ver.:  V1.0.0
*********************************************************************************/
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Parks.Dtos
{
    [AutoMapFrom(typeof(Park))]
    public class ParkAppDto
    {

        public string id { get; set; }// 停车场ID
        public string name { get; set; }// 停车场名称
        public string lng { get; set; }// 纬度
        public string lat { get; set; }// 经度
        public string total { get; set; }// 总车位

        public string free { get; set; }

        public string addr { get { return "测试地址"; } }//
        public string phone { get { return ""; } }// 停车场电话
        public string monthlypay { get { return "0"; } }// 是否支持月卡
        public string epay { get { return "0"; } }// 是否支持手机支付
        //public string desc { get { return ""; } }// 车场描述
        //public List<string> photo_url{get;set;}// 车场照片的url地址集合

        // 上传车场的类型，0收费，1免费
        public string type { get { return "0"; } }

        public string price { get { return "0"; } }// 当前价格（元/每小时，负数表示免费，正数表示有价格，0或“”表示没有价格信息）
    }
}
