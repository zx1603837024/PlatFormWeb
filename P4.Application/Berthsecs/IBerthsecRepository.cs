using Abp.Domain.Repositories;
using P4.Berthsecs.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Berthsecs
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBerthsecRepository: IRepository<Berthsec>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllBerthsecsListOutput GetAllBerthsecsList(SearchBerthsecInput input);

        /// <summary>
        /// 道闸
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllBerthsecsListOutput GetAllSignoParkList(SearchBerthsecInput input);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        GetAllBerthsecCheckListOutput GetAllBerthsecCheckLiist(BerthsecCheckInput input, int Status);
        /// <summary>
        /// 巡查员获取泊位段
        /// </summary>
        /// <param name="LParkIds"></param>
        /// <returns></returns>
        List<BerthsecDto> LoadListBerhtsec(List<int> LParkIds);
        /// <summary>
        /// PDA收费员签退泊位段
        /// </summary>
        /// <param name="berthsecID"></param>
        /// <param name="DeviceCode"></param>
        /// <param name="employessID"></param>
        void EmployeeCheckOutBerthsec(int berthsecID, string DeviceCode, int employessID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="BerthsecId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        int BerthsecBatchCheckOutBGO(List<int> BerthsecId, int UserId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        List<BerthsecDto> LoadToSql(List<int> Ids);
    }
}
