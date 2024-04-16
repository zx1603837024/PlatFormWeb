using Abp.Application.Services;

namespace P4.EmployeeBatchNoDynamicReport
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEmployeeBatchNoDynamicReportAppSerVice : IApplicationService
    {
        /// <summary>
        /// 获取收费员明细
        /// </summary>
        /// <returns></returns>
        string GetEmployDetail(string BatchNo, int TenantId, int CompanyId);
    }
}
