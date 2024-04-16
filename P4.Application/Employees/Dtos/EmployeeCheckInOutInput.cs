using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Employees.Dtos
{
    public class EmployeeCheckInOutInput :IInputDto
    {
        public bool CheckIn { get; set; }

        public DateTime CheckInTime { get; set; }

        public bool CheckOut { get; set; }


        public DateTime CheckOutTime { get; set; }


        public string DeviceCode { get; set; }


        public string CheckOuttype { get; set; }

        /// <summary>
        /// 后台签退
        /// </summary>
        public long CheckOutUserId { get; set; }

        public int RegionId { get; set; }

        public int ParkId { get; set; }

        public int BerthsecId { get; set; }

    }
}
