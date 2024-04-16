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
    /// 接口实现
    /// </summary>
    public class AdvancedFeaturesManagerAppService : ApplicationService, IAdvancedFeaturesManagerAppService
    {
        #region Var
        private readonly IAdvancedFeaturesManagerRepository _advancedFeaturesManagerRepository;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="advancedFeaturesManagerRepository"></param>
        public AdvancedFeaturesManagerAppService(IAdvancedFeaturesManagerRepository advancedFeaturesManagerRepository)
        {
            _advancedFeaturesManagerRepository = advancedFeaturesManagerRepository;
        }

        /// <summary>
        /// 平账
        /// </summary>
        /// <param name="view"></param>
        public int FlatAccount(FlatAccountView view)
        {
            var list = new List<int>();
            list.Add(-1);
            view.TenantId = AbpSession.TenantId;
            view.CompanyId = AbpSession.CompanyId;
            view.CompanyIds = view.CompanyId.HasValue == false ? AbpSession.CompanyIds ?? list : new List<int> { view.CompanyId.Value };

            return _advancedFeaturesManagerRepository.FlatAccount(view);
        }

        /// <summary>
        /// 获取收费员分配的所有泊位段
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public List<Berthsec> getBerthsecAllListByEmpId(getBerthsecAllListByEmpIdInput view)
        {
            var list = new List<int>();
            list.Add(-1);
            view.TenantId = AbpSession.TenantId;
            view.CompanyId = AbpSession.CompanyId;
            view.CompanyIds = view.CompanyId.HasValue == false ? AbpSession.CompanyIds ?? list : new List<int> { view.CompanyId.Value };

            return _advancedFeaturesManagerRepository.getBerthsecAllListByEmpId(view);
        }

      
    }
}
