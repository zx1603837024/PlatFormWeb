using Abp.Application.Services;
using P4.CardCode.Dtos;

namespace P4.CardCode
{
    /// <summary>
    /// 
    /// </summary>
    public  interface IIPassCardCodeAppService : IApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAlllPassCardCodeOutput GetIPassCardCodeList(GetAllIPassCardCodeInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        GetAlllPassCardCodeOutput GetIPassCardCodeList();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string InsertIPassCardCode(CreateOrUpdateIPassCardCodeInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        void DeleteIPassCardCode(CreateOrUpdateIPassCardCodeInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string ModifyIPassCardCode(CreateOrUpdateIPassCardCodeInput input);
        
    }
}
