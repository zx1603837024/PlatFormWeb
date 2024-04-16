using Abp.Domain.Repositories;
using P4.MonthlyCars.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.MonthlyCars
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMonthlyCarRespository : IRepository<MonthlyCar>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllMonthlyCarsOutput GetMonthlyCarAll(MonthlyCarInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parkid"></param>
        /// <param name="TenantId"></param>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        List<MonthlyCars.Dtos.MonthlyCarDto> GetAllMonthlyCarBySql(string parkid, int TenantId, int CompanyId);
    }
}
