using Abp.Application.Services;
using P4.VideoEquipBusinessDetails.Dtos;
using P4.VideoEquipFaultsNs.Dtos;

namespace P4.VideoEquipFaultsNs
{
    /// <summary>
    /// 接口，异常数据推送
    /// </summary>
    public interface IVideoEquipFaultsAppService : IApplicationService
    {
        /// <summary>
        /// 注册获取异常数据列表的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        VeqFaultOutput GetAllVideoEqFaults(VeqFaultInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        void Delete(int Id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        VeqFaultOutput GetAllVideoEqFaults1(VeqFaultInput input);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        VeqFaultOutput GetAllVideoEqFaults2(VeqFaultInput input);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        void Delete1(int Id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        void Delete2(int Id);
    }
}
