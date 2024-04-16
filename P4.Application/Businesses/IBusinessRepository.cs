using Abp.Domain.Repositories;
using P4.Berths.Dtos;
using P4.BigScreen.Dtos;
using P4.Businesses.Dtos;
using P4.EmployeeBatchNoDynamicReport.Dtos;
using P4.EmployeeCharges.Dto;
using P4.InspectorCharges.Dtos;
using P4.ParkCharges.Dto;
using P4.ParkingLot.Dto;
using System;
using System.Collections.Generic;


namespace P4.Businesses
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBusinessRepository : IRepository<BusinessDetail, long>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Dtos.GetMoneyOutput GetMoneyTotal(Dtos.GetMoneyInput input);

        /// <summary>
        /// 获取停车场停车记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetParkRecordListOutput GetParkRecordList(GetParkRecordListInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<ParkChargesDto> GetMoneyTotalGroupbyParkEchar(ParkChargeInput input);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<ParkChargesDto> GetMoneyTotalGroupbyBerthsecEchar(ParkChargeInput input);

        /// <summary>
        /// 大屏全局金额
        /// </summary>
        /// <returns></returns>
        StatisticsMoneyDto GetStatisticsInfo(int Status);

        /// <summary>
        /// 大屏停车次数排行
        /// </summary>
        /// <returns></returns>
        List<StopTimesRankDto> StopTimesRank();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllParkChargesOutput GetMoneyTotalGroupbyPark(ParkChargeInput input);

        /// <summary>
        /// 泊位段报表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllParkChargesOutput GetMoneyTotalGroupbyBerthsec(ParkChargeInput input);

        /// <summary>
        /// 欠费报表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllParkChargesOutput GetMonthOwnBerthsec(ParkChargeInput input);

        /// <summary>
        /// 欠费报表详细
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<ParkChargesDto> GetOwnReportDetail(ParkChargeInput input);

        /// <summary>
        /// 收费员日报
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllEmployeeChargesOutput GetMoneyTotalGroupbyEmployee(EmployeeChargeInput input);
        List<EmployeeChargesDto> GetMoneyTotalGroupbyRateEchars(EmployeeChargeInput input);


        /// <summary>
        /// 收费员日报
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllEmployeeChargesOutput GetMoneyTotalGroupbyEmployeeDetail(EmployeeChargeInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<EmployeeChargesDto> GetMoneyTotalGroupbyEmployeeEchars(EmployeeChargeInput input);

        /// <summary>
        /// 泊位段收入List
        /// </summary>
        /// <returns></returns>
        List<BerthsecStatisticsDto> GetBerthsecStatisticsList(int[] berthsecsid, int[] CompanyId, int? TenantId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllEmployeeChargesOutput GetMoneyTotalGroupbyDate(EmployeeChargeInput input);

        /// <summary>
        /// 收费员今日实收
        /// </summary>
        /// <returns></returns>
        List<EmployeeFactReceiveDto> GetEmployeeFactReceiveList(int[] berthsecid, int[] CompanyId, int? TenantId);
        /// <summary>
        /// 泊位段今日金额model
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="TenantId"></param>
        /// <param name="BerthsecId"></param>
        /// <returns></returns>
        BerthsecStatisticsDto GetBerthsecStatistics(int CompanyId, int TenantId, int BerthsecId);

        /// <summary>
        /// 获取泊位使用情况
        /// </summary>
        /// <returns></returns>
        List<BerthsecsUtilization> GetBerthsecsUtilization(int TenantId,int ComplayId);

        /// <summary>
        /// 获取环比金额
        /// </summary>
        /// <param name="TenantId"></param>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        List<MoneyChain> GetMoneyChain(int TenantId, int CompanyId);

        /// <summary>
        /// 今日实收
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        EmployeeFactReceiveDto GetTodayFactReceiveList(int EmployeeId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        void RestoreDelete(long Id);


        /// <summary>
        /// 返回收费明细
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Dtos.GetBusinessDetailsOutput GetBusinessDetailsList(Dtos.GetBusinessDetailsInput input);
        /// <summary>
        /// 返回收费明细sql
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetBusinessDetailsOutput GetBusinessDetailsListSql(GetBusinessDetailsInput input);

        /// <summary>
        /// 新增收费员批次号报表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetBusinessDetailsOutput GetGetEmployeeBatchNoDynamicReportSql(GetEmployeeBatchNoDynamicReportInput input);

        /// <summary>
        /// 获取大额订单列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetBusinessDetailsOutput GetSubstantialOrderListSql(GetBusinessDetailsInput input);
        
        /// <summary>
        /// 逃逸明细
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Dtos.GetEscapeDetailsOutput GetEscapeDetailsList(Dtos.GetEscapeDetailsInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="plateNumber"></param>
        /// <param name="userId"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        GetEscapeDetailsOutputTow GetEscapeDetailsListTow(string plateNumber,long? userId,int? TenantId);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="plateNumber"></param>
        /// <param name="userId"></param>
        /// <param name="TenantId"></param>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        GetEscapeDetailsOutputTow GetEscapeDetailsListTowBySql(string plateNumber, long? userId, int? TenantId, int? CompanyId);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetEscapeDetailsOutputTow GetEscapeDetailsListByTenant(GetEscapeDetailsInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        void CheckOutDeblock(long? userId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="PlateNumber"></param>
        void PlateNumberDebLock(long? userId, string PlateNumber);

            /// <summary>
            /// 
            /// </summary>
            /// <param name="entity"></param>
            /// <returns></returns>
        bool InsertCarIn(BusinessDetail entity);

        /// <summary>
        /// 车辆入场存储过程调用
        /// </summary>
        /// <param name="BerthNumber"></param>
        /// <param name="PlateNumber"></param>
        /// <param name="CarType"></param>
        /// <param name="Prepaid"></param>
        /// <param name="CarInTime"></param>
        /// <param name="guid"></param>
        /// <param name="SensorsInCarTime"></param>
        /// <param name="StopType"></param>
        /// <param name="CardNo"></param>
        /// <param name="RegionId"></param>
        /// <param name="ParkId"></param>
        /// <param name="BerthsecId"></param>
        /// <param name="Details"></param>
        /// <param name="TenantId"></param>
        /// <param name="InOperaId"></param>
        /// <param name="InDeviceCode"></param>
        /// <returns></returns>
        bool InsertCarInPro(string BerthNumber, string PlateNumber, string CarType, string Prepaid, string CarInTime, string guid, string SensorsInCarTime, string StopType, string CardNo, int RegionId, int ParkId, int BerthsecId, string Details, int TenantId, long InOperaId, string InDeviceCode);

        /// <summary>
        /// 车辆出场调用存储过程
        /// </summary>
        /// <param name="Receivable"></param>
        /// <param name="FactReceive"></param>
        /// <param name="Arrearage"></param>
        /// <param name="CarOutTime"></param>
        /// <param name="CarPayTime"></param>
        /// <param name="OutOperaId"></param>
        /// <param name="guid"></param>
        /// <param name="SensorsOutCarTime"></param>
        /// <param name="SensorsStopTime"></param>
        /// <param name="SensorsReceivable"></param>
        /// <param name="PayStatus"></param>
        /// <param name="IsPay"></param>
        /// <param name="FeeType"></param>
        /// <param name="StopTime"></param>
        /// <param name="Money"></param>
        /// <returns></returns>
        bool InsertCarOutPro(decimal Receivable, string FactReceive, string Arrearage, string CarOutTime, string CarPayTime, string OutOperaId, string guid, string SensorsOutCarTime, string SensorsStopTime, string SensorsReceivable, string PayStatus, string IsPay, string FeeType, string StopTime, decimal Money);
 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        List<MonthBerthsUseDto> GetMonthBerthsUseList(GetBusinessDetailsInput input,int?tenantID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        List<MonthBerthsUseDto> GetMonthBerthsUseListOnlyMonth(GetBusinessDetailsInput input, int? tenantID);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="exceutiontimeBegin"></param>
        /// <param name="exceutiontimeEnd"></param>
        void CheckGuid(DateTime exceutiontimeBegin, DateTime exceutiontimeEnd);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exceutiontimeBegin"></param>
        /// <param name="exceutiontimeEnd"></param>
        void CheckGuidRepeat(DateTime exceutiontimeBegin, DateTime exceutiontimeEnd);

        /// <summary>
        /// 
        /// </summary>
        void CheckFeeBack();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        GetEscapeRankOutput GetEscapeRankList(GetEscapeRankInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetAllInspectorChargesOutput GetMoneyTotalGroupbyInspector(InspectorChargeInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<InspectorChargesDto> GetMoneyTotalGroupbyInspectorEchars(InspectorChargeInput input);
    }
}
