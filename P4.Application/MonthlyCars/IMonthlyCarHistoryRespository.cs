
using Abp.Domain.Repositories;
using P4.MonthlyCars.Dtos;
using System.Collections.Generic;

namespace P4.MonthlyCars
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMonthlyCarHistoryRespository : IRepository<MonthlyCarHistory, long>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<MonthlyCarHistoryDto> GetMonthlyCarHistoryAll(MonthlyCarHistoryInput input);
    }
}
