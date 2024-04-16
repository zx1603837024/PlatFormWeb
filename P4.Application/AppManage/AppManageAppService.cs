using Abp.Application.Services;
using Abp.Domain.Repositories;
using P4.App;
using P4.AppManage.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Linq.Extensions;
using Abp.AutoMapper;

namespace P4.AppManage
{

    /// <summary>
    /// App用户管理
    /// </summary>
    public class AppManageAppService : ApplicationService, IAppManageAppService
    {
        #region Var
        private readonly IRepository<AppAccount, long> _abpAppAccountRepository;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="abpAppAccountRepository"></param>
        public AppManageAppService(IRepository<AppAccount, long> abpAppAccountRepository)
        {
            _abpAppAccountRepository = abpAppAccountRepository;
        }

        /// <summary>
        /// 车主端登录
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public string CarLogin(string mobile)
        {
            return "123456";
        }

        /// <summary>
        /// App管理
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllAppOutput GetAll(SearchAppInput input)
        {

            int records = _abpAppAccountRepository.GetAll().Filters(input).Count();
            return new GetAllAppOutput()
            {
                rows = _abpAppAccountRepository.GetAll().Filters(input).OrderByDescending(e => e.Id)
                .PageBy(input).ToList().MapTo<List<AppManageDto>>(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }
    }
}
