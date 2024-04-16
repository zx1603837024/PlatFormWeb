using Abp.Application.Services;
using P4.SmsManagements.Dtos;

namespace P4.SmsManagements
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISmsModelAppService : IApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllSmsModelOutput GetAllSmsModel(SearchSmsModelInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        void Delete(CreateOrUpdateDtoInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        void Update(CreateOrUpdateDtoInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        void Insert(CreateOrUpdateDtoInput input);

        /// <summary>
        /// 获取短信模板
        /// </summary>
        /// <param name="modelType"></param>
        /// <returns></returns>
        SmsModelDto GetSmsModelByType(string modelType);
    }
}
