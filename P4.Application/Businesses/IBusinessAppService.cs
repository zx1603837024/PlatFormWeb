using Abp.Application.Services;
using Abp.Auditing;
using P4.Businesses.Dtos;
using P4.EmployeeBatchNoDynamicReport.Dtos;
using P4.Equipments;
using P4.ParkingLot;
using P4.ParkingLot.Dto;
using P4.Signo;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace P4.Businesses
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBusinessAppService : IApplicationService
    {



        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetMoneyOutput IndexMoneyTotal(GetMoneyInput input);

        /// <summary>
        /// 获取停车场停车记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetParkRecordListOutput GetParkRecordList(GetParkRecordListInput input);

        /// <summary>
        /// 获取停车记录图片列表
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        List<AbpParkingRecord_Pic> GetParkingRecordPicList(Guid recordId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        void Delete(long Id);

        /// <summary>
        /// 逃逸明细删除
        /// </summary>
        /// <param name="Id"></param>
        int Delete(Guid Id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        void RestoreDelete(long Id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        GetEscapeDetailsOutput GetEscapeDetailsList(GetEscapeDetailsInput input);


        /// <summary>
        /// 对账
        /// </summary>
        /// <param name="batchNo"></param>
        /// <returns></returns>
        [HttpGet]
        string GetReconciliationModel(string batchNo);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="PlateNumber"></param>
        /// <param name="id"></param>
        /// <param name="oper"></param>
        /// <returns></returns>
        int UpdateBusiness(string PlateNumber, string id, string oper);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="money"></param>
        /// <returns></returns>
        int UpdateMoney(string id, string money);

        /// <summary>
        /// 大屏展示金额明细
        /// </summary>
        /// <param name="batchNo"></param>
        /// <returns></returns>
        //string GetBigScreenMoneyDetail();

        /// <summary>
        /// 获取商户对账功能
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetEscapeDetailsOutput GetEscapeDetailsListByTenant(GetEscapeDetailsInput input);


        /// <summary>
        /// 获取分公司对账功能
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetEscapeDetailsOutput GetEscapeDetailsListByCompany(GetEscapeDetailsInput input);

        /// <summary>
        /// 根据车牌号 车辆欠费接口
        /// </summary>
        /// <param name="plateNumber"></param>
        /// <returns></returns>
        [HttpGet]
        GetEscapeDetailsOutputTow GetEscapeDetailsListTow(string plateNumber);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        GetBusinessDetailsOutput GetBusinessDetailsList(GetBusinessDetailsInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        GetBusinessDetailsOutput GetBusinessDetailsListSql(GetBusinessDetailsInput input);

        /// <summary>
        /// 新增收费员批次号报表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        GetBusinessDetailsOutput GetGetEmployeeBatchNoDynamicReportSql(GetEmployeeBatchNoDynamicReportInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operateDateBegin"></param>
        /// <param name="operateDateEnd"></param>
        /// <param name="PlateNumber"></param>
        /// <returns></returns>
        List<EquipmentGps> GetPlatenumberLocusList(string operateDateBegin, string operateDateEnd, string PlateNumber);

        /// <summary>
        /// 获取大额订单列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        GetBusinessDetailsOutput GetSubstantialOrderListSql(GetBusinessDetailsInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        BusinessDetailUserData GetBusinessDetailById(Guid Id);


        /// <summary>
        /// 恢复欠费（可恢复多少金额）
        /// </summary>
        /// <param name="id"></param>
        /// <param name="money"></param>
        void ResumeArrearage(Guid id, decimal money);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="businessdetailid"></param>
        /// <returns></returns>
        bool GetBusinessDetailPicture(Guid guid, int businessdetailid);

        /// <summary>
        /// 车辆入场接口
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
        /// <param name="RegionID"></param>
        /// <param name="ParkID"></param>
        /// <param name="BerthsecID"></param>
        /// <param name="InBatchNo"></param>
        /// <returns></returns>
        [HttpGet]
        bool InsertCarIn(string BerthNumber, string PlateNumber, string CarType, string Prepaid, string CarInTime, string guid, string SensorsInCarTime, string StopType, string CardNo, int RegionID, int ParkID, int BerthsecID, string InBatchNo);
    
        /// <summary>
        /// 车辆出场接口
        /// </summary>
        /// <param name="Receivable"></param>
        /// <param name="FactReceive"></param>
        /// <param name="CarOutTime"></param>
        /// <param name="CarPayTime"></param>
        /// <param name="guid"></param>
        /// <param name="SensorsOutCarTime"></param>
        /// <param name="SensorsStopTime"></param>
        /// <param name="SensorsReceivable"></param>
        /// <param name="PayStatus"></param>
        /// <param name="IsPay"></param>
        /// <param name="FeeType"></param>
        /// <param name="StopTime"></param>
        /// <param name="Money"></param>
        /// <param name="CardNo"></param>
        /// <param name="BerthsecID"></param>
        /// <param name="OutBatchNo"></param>
        /// <returns></returns>
        [HttpGet]
        bool InsertCarOut(decimal Receivable, string FactReceive, string CarOutTime, string CarPayTime, string guid, string SensorsOutCarTime, string SensorsStopTime, string SensorsReceivable, string PayStatus, string IsPay, string FeeType, string StopTime, decimal Money, string CardNo, int BerthsecID,string OutBatchNo);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Receivable"></param>
        /// <param name="FactReceive"></param>
        /// <param name="CarOutTime"></param>
        /// <param name="CarPayTime"></param>
        /// <param name="guid"></param>
        /// <param name="SensorsOutCarTime"></param>
        /// <param name="SensorsStopTime"></param>
        /// <param name="SensorsReceivable"></param>
        /// <param name="PayStatus"></param>
        /// <param name="IsPay"></param>
        /// <param name="FeeType"></param>
        /// <param name="StopTime"></param>
        /// <param name="Money"></param>
        /// <param name="CardNo"></param>
        /// <param name="BerthsecId"></param>
        /// <param name="OutBatchNo"></param>
        /// <returns></returns>
        [HttpGet]
        bool InsertBatchCarOut(decimal Receivable, string FactReceive, string CarOutTime, string CarPayTime, string guid, string SensorsOutCarTime, string SensorsStopTime, string SensorsReceivable, string PayStatus, string IsPay, string FeeType, string StopTime, decimal Money, string CardNo, int BerthsecId, string OutBatchNo);

        /// <summary>
        /// PDA车辆出场
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
        /// <param name="CardNo"></param>
        /// <param name="BerthsecID"></param>
        /// <returns></returns>
        [HttpGet]
        bool InsertCarOut(decimal Receivable, string FactReceive, string Arrearage, string CarOutTime, string CarPayTime, string OutOperaId, string guid, string SensorsOutCarTime, string SensorsStopTime, string SensorsReceivable, string PayStatus, string IsPay, string FeeType, string StopTime, decimal Money, string CardNo, int BerthsecID, string OutBatchNo);

       /// <summary>
       /// 
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
       /// <param name="CardNo"></param>
       /// <param name="BerthsecId"></param>
       /// <param name="OutBatchNo"></param>
       /// <returns></returns>
        [HttpGet]
        bool InsertOtherCarOut(decimal Receivable, string FactReceive, string Arrearage, string CarOutTime, string CarPayTime, string OutOperaId, string guid, string SensorsOutCarTime, string SensorsStopTime, string SensorsReceivable, string PayStatus, string IsPay, string FeeType, string StopTime, decimal Money, string CardNo, string BerthsecId, string OutBatchNo);


        /// <summary>
        /// 同步远程车辆出场数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [DisableAuditing]
        List<RemoteGuid> GetRemoteGuids();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="guid"></param>
        [HttpGet]
        void UpdateRemoteGuidStatus(string guid);

        /// <summary>
        /// 费用补缴接口
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Repayment"></param>
        /// <param name="CarRepaymentTime"></param>
        /// <param name="IsEscapePay"></param>
        /// <param name="EscapePayStatus"></param>
        /// <param name="PaymentType"></param>
        /// <param name="CardNo"></param>
        /// <param name="PaymentBatchNo"></param>
        /// <returns></returns>
        [HttpGet]
        bool FeeBack(int id, decimal Repayment, DateTime CarRepaymentTime, bool IsEscapePay, short EscapePayStatus, short PaymentType, string CardNo, string PaymentBatchNo);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="id"></param>
        /// <param name="Repayment"></param>
        /// <param name="CarRepaymentTime"></param>
        /// <param name="IsEscapePay"></param>
        /// <param name="EscapePayStatus"></param>
        /// <param name="PaymentType"></param>
        /// <param name="EscapeOperaId"></param>
        /// <param name="EscapeTenantId"></param>
        /// <param name="CardNo"></param>
        /// <param name="PayStatus"></param>
        /// <returns></returns>
        [HttpGet]
        bool FeeBack(string guid, int id, decimal Repayment, DateTime CarRepaymentTime, bool IsEscapePay, short EscapePayStatus, short PaymentType, string EscapeOperaId, string EscapeTenantId, string CardNo, string PayStatus, string PaymentBatchNo);
        /// <summary>
        /// 返回当前时间
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        string dateTimeNow();
        //车辆入场（存储过程）
        //bool InsertCarInPro(string BerthNumber, string PlateNumber, string CarType, string Prepaid, string CarInTime, string guid, string SensorsInCarTime, string StopType, string CardNo, int RegionId, int ParkId, int BerthsecId, string Details, int TenantId, long InOperaId, string InDeviceCode);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [DisableAuditing]
        string CheckEquipmentStatus();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<BusinessDetail> GetAllLockBusiness();
        /// <summary>
        /// 收费员签退 解锁欠费车辆
        /// </summary>
        [HttpGet]
        void CheckOutDeblock(string PlateNumber);

        /// <summary>
        /// 
        /// </summary>
        [HttpGet]
        void CheckOutDeblockByEmployee();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PlateNumber"></param>
        [HttpGet]
        void PlateNumberDebLock(string PlateNumber);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="BD"></param>
        [HttpGet]
        void UpdateBusinessLock(BusinessDetail BD);

        /// <summary>
        /// 获取车位使用及消费情况信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<MonthBerthsUseDto> GetMonthBerthsUseList(GetBusinessDetailsInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<MonthBerthsUseDto> GetMonthBerthsUseListOnlyMonth(GetBusinessDetailsInput input);
        /// <summary>
        /// 插入进出场完整数据
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
        [HttpGet]
        void CheckFeeBack();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="BusinessDetailId"></param>
        /// <returns></returns>
        List<BusinessDetailPictureDto> GetPictureList(long BusinessDetailId);



        /// <summary>
        /// 道闸图片
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        List<BusinessDetailPictureDto> GetSignoPictureList(List<SignoParkInformation> list);




        /// <summary>
        /// 
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        List<BusinessDetailPictureDto> GetPictureList(Guid guid);

        /// <summary>
        /// 删除停车记录
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        bool DeleteBusinessDetial(Guid guid);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Receivable"></param>
        /// <param name="FactReceive"></param>
        /// <param name="CarOutTime"></param>
        /// <param name="CarPayTime"></param>
        /// <param name="guid"></param>
        /// <param name="SensorsOutCarTime"></param>
        /// <param name="SensorsStopTime"></param>
        /// <param name="SensorsReceivable"></param>
        /// <param name="PayStatus"></param>
        /// <param name="IsPay"></param>
        /// <param name="FeeType"></param>
        /// <param name="StopTime"></param>
        /// <param name="Money"></param>
        /// <param name="CardNo"></param>
        /// <param name="BeforeDiscount"></param>
        /// <param name="DiscountMoney"></param>
        /// <param name="DiscountRate"></param>
        /// <param name="BerthsecId"></param>
        /// <param name="OutBatchNo"></param>
        /// <returns></returns>
        [HttpGet]
        bool DiscountInsertCarOut(decimal Receivable, string FactReceive, string CarOutTime, string CarPayTime, string guid, string SensorsOutCarTime, string SensorsStopTime, string SensorsReceivable, string PayStatus, string IsPay, string FeeType, string StopTime, decimal Money, string CardNo, decimal BeforeDiscount, decimal DiscountMoney, double DiscountRate, int BerthsecId, string OutBatchNo);

        /// <summary>
        /// 
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
        /// <param name="CardNo"></param>
        /// <param name="BeforeDiscount"></param>
        /// <param name="DiscountMoney"></param>
        /// <param name="DiscountRate"></param>
        /// <param name="OutBatchNo"></param>
        /// <returns></returns>
        [HttpGet]
        bool DiscountInsertCarOut(decimal Receivable, string FactReceive, string Arrearage, string CarOutTime, string CarPayTime, string OutOperaId, string guid, string SensorsOutCarTime, string SensorsStopTime, string SensorsReceivable, string PayStatus, string IsPay, string FeeType, string StopTime, decimal Money, string CardNo, decimal BeforeDiscount, decimal DiscountMoney, double DiscountRate,string OutBatchNo);



        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Repayment"></param>
        /// <param name="CarRepaymentTime"></param>
        /// <param name="IsEscapePay"></param>
        /// <param name="EscapePayStatus"></param>
        /// <param name="PaymentType"></param>
        /// <param name="CardNo"></param>
        /// <param name="BeforeDiscount"></param>
        /// <param name="DiscountMoney"></param>
        /// <param name="DiscountRate"></param>
        /// <returns></returns>
        [HttpGet]
        bool DiscountFeeBack(int id, decimal Repayment, DateTime CarRepaymentTime, bool IsEscapePay, short EscapePayStatus, short PaymentType, string CardNo, decimal BeforeDiscount, decimal DiscountMoney, double DiscountRate, string PaymentBatchNo);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="id"></param>
        /// <param name="Repayment"></param>
        /// <param name="CarRepaymentTime"></param>
        /// <param name="IsEscapePay"></param>
        /// <param name="EscapePayStatus"></param>
        /// <param name="PaymentType"></param>
        /// <param name="EscapeOperaId"></param>
        /// <param name="EscapeTenantId"></param>
        /// <param name="CardNo"></param>
        /// <param name="PayStatus"></param>
        /// <param name="BeforeDiscount"></param>
        /// <param name="DiscountMoney"></param>
        /// <param name="DiscountRate"></param>
        /// <returns></returns>
        [HttpGet]
        bool DiscountFeeBack(string guid, int id, decimal Repayment, DateTime CarRepaymentTime, bool IsEscapePay, short EscapePayStatus, short PaymentType, string EscapeOperaId, string EscapeTenantId, string CardNo, string PayStatus, decimal BeforeDiscount, decimal DiscountMoney, double DiscountRate, string PaymentBatchNo);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int CreatePicture(PicMongoDto entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        PicMongoDto GetPicture(PicMongoDto entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        BusinessDetailsDto GetOrderById(int Id);

        /// <summary>
        /// 取消追缴金额
        /// </summary>
        /// <param name="escapeId"></param>
        /// <returns></returns>
        int CancelEscapePay(List<string> escapeId);

       /// <summary>
       /// 
       /// </summary>
       /// <param name="input"></param>
       /// <returns></returns>
        GetEscapeRankOutput GetEscapeRankList(GetEscapeRankInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="BeginTime"></param>
        /// <param name="EndTime"></param>
        /// <returns></returns>
        BusinessDecisionDto GetBusinessDecisionDto(DateTime BeginTime, DateTime EndTime);

    }
}
