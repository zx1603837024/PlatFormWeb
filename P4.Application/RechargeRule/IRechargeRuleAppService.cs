using Abp.Application.Services;
using P4.RechargeRule.Dtos;

namespace P4.RechargeRule
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRechargeRuleAppService : IApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        void Insert(CreateOrUpdateRechargeRuleInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        void Update(CreateOrUpdateRechargeRuleInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        void Delete(int Id);

        GetAllRechargeRuleOutput GetAll(SearchRechargeRuleInput input);
    }
}
