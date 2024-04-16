using Abp.Application.Services;
using P4.AdvancedFeaturesManager.Model;
using P4.Berthsecs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.AdvancedFeaturesManager
{
    /// <summary>
    /// 接口定义
    /// </summary>
    public interface IAdvancedFeaturesManagerAppService : IApplicationService
    {
        /// <summary>
        /// 平账
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        int FlatAccount(FlatAccountView view);

        /// <summary>
        /// 获取收费员分配的所有泊位段
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="TenantId"></param>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        List<Berthsec> getBerthsecAllListByEmpId(getBerthsecAllListByEmpIdInput view);
    }
}
