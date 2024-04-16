using Abp.Application.Services;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P4.RechargeRule.Dtos;
using Abp.AutoMapper;
using Abp.Linq.Extensions;

namespace P4.RechargeRule
{
    /// <summary>
    /// 
    /// </summary>
    public class RechargeRuleAppService : ApplicationService, IRechargeRuleAppService
    {
        #region Var

        private readonly IRepository<P4.OtherAccounts.RechargeRule> _abpRechargeRuleRepository;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="abpRechargeRuleRepository"></param>
        public RechargeRuleAppService(IRepository<P4.OtherAccounts.RechargeRule> abpRechargeRuleRepository)
        {
            _abpRechargeRuleRepository = abpRechargeRuleRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public void Delete(int Id)
        {
            _abpRechargeRuleRepository.Delete(Id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllRechargeRuleOutput GetAll(SearchRechargeRuleInput input)
        {
            int records = _abpRechargeRuleRepository.Count();
            return new GetAllRechargeRuleOutput()
            {
                rows = _abpRechargeRuleRepository.GetAll().OrderByDescending(entity => entity.Id).Filters(input).PageBy(input).ToList().MapTo<List<RechargeRuleDto>>(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        public void Insert(CreateOrUpdateRechargeRuleInput input)
        {
            _abpRechargeRuleRepository.Insert(input.MapTo<OtherAccounts.RechargeRule>());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        public void Update(CreateOrUpdateRechargeRuleInput input)
        {
            _abpRechargeRuleRepository.Update(input.MapTo<OtherAccounts.RechargeRule>());
        }
    }
}
