using P4.Collectors.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Collectors
{
    public interface ICollectorRepository
    {
        /// <summary>
        /// 获取年统计金额
        /// </summary>
        /// <returns></returns>
        GetYearTotalOutput GetYearBussinessCount();

        /// <summary>
        /// 获取当月收费统计
        /// </summary>
        /// <returns></returns>
        List<GetMonthTotalOutput> GetMonthBussinessCount();
    }
}
