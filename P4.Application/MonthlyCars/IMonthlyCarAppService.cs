using Abp.Application.Services;
using P4.MonthlyCars.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace P4.MonthlyCars
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMonthlyCarAppService : IApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<string> Insert(CreateOrUpdateMonthlyCarInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<string> Update(CreateOrUpdateMonthlyCarInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        void Update(CreateOrUpdateMonthlyCarAbnormalInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        void Delete(int Id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        void Delete(long Id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllMonthlyCarsOutput GetMonthlyCarList(MonthlyCarInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllMonthlyCarHistoryOutput GetMonthlyCarHistoryList(MonthlyCarHistoryInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllMonthlyCarAbnormalOutput GetMonthlyCarAbnormalList(MonthlyCarAbnormalInput input);

        /// <summary>
        /// 收费员下载包月卡车辆
        /// </summary>
        /// <returns></returns>
        List<MonthlyCarDto> GetAllMonthlyCar();

        /// <summary>
        /// 包月卡续费
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        int MonthRenew(CreateOrUpdateMonthlyCarInput input);

        /// <summary>
        /// 获取包月卡数据（单条）
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        MonthlyCarDto GetModel(int Id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllMonthlyCarHistoryOutput GetMonthlyCarHistoryAll(MonthlyCarHistoryInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="BeginTime"></param>
        /// <param name="EndTime"></param>
        /// <returns></returns>
        DecisionDto GetDecisionModel(DateTime BeginTime, DateTime EndTime);

        /// <summary>
        /// 通过车牌号查询包月车辆信息
        /// </summary>
        /// <param name="CarNumber"></param>
        /// <returns></returns>
        [HttpGet]
        string GetMonthyCarByCarNumber(string CarNumber);

    }
}
