using Abp.Application.Services.Dto;
using System;
using Abp.AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace P4.ParkingLot.Dto
{
    /// <summary>
    /// 创建删除包月
    /// </summary>
    public class ModifyMonthlyRentInput : EntityRequestInput, IOperDto, IInputDto
    {
        public string oper { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ParkName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ParkId
        {
            get
            {
                if (!string.IsNullOrEmpty(ParkName))
                    return int.Parse(ParkName);
                return 0;
            }
        }

        /// <summary>
        /// openid
        /// </summary>
        public string OpenId { get; set; }

        public string PlateNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 车类型
        /// </summary>
        public enum ECarType
        {
            小车 = 1,
            大车 = 2,
            摩托车 = 3,
            小型新能源车 = 4,
            大型新能源车 = 5,
            超大车 = 6
        }

        /// <summary>
        /// 车型
        /// </summary>
        public ECarType carType { get; set; }
    }
}
