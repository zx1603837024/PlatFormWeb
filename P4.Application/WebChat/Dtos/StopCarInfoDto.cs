using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.WebChat.Dtos
{
    public class StopCarInfoDto
    {

        public Int64 Id { get; set; }

        public string PlateNumber { get; set; }

        public string CarInTime { get; set; }

        public string CarOutTime { get; set; }

        public decimal Money { get; set; }

        public Guid guid { get; set; }

        /// <summary>
        /// 预缴金额
        /// </summary>
        public decimal Prepaid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Receivable { get; set; }

        /// <summary>
        /// 总实收
        /// </summary>
        public decimal FactReceive { get; set; }

        /// <summary>
        /// 欠费
        /// </summary>
        public decimal Arrearage { get; set; }

        /// <summary>
        /// 追缴
        /// </summary>
        public decimal Repayment { get; set; }

        public string EmployeeName { get; set; }

        public string Remark { get; set; }

        public string BerthsecName { get; set; }


        public int StopTime { get; set; }

        /// <summary>
        /// 停车时长
        /// </summary>
        public string StopTimes
        {
            get
            {
                int Getstoptime = Convert.ToInt32(StopTime);
                TimeSpan ts = new TimeSpan(0, 0, Getstoptime, 0);
                string dateDiff = "";
                if (Getstoptime >= 1440)//判断是否大于24小时
                {
                    dateDiff = ts.Days.ToString() + "天" + ts.Hours.ToString() + "小时" + ts.Minutes.ToString() + "分钟";
                }
                else
                {
                    dateDiff = ts.Hours.ToString() + "小时" + ts.Minutes.ToString() + "分钟";
                }
                return dateDiff;
            }
        } 

        public List<BusinessPicDto> BusinessPicList { get; set; }
    }
}
