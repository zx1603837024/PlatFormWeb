using Abp.Application.Services;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using P4.BlackLists.Dtos;
using P4.Businesses;
using P4.MonthlyCars.Dtos;
using P4.Parking.Dtos;
using P4.Weixin;
using P4.WhiteLists.Dtos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace P4.Parking
{
    /// <summary>
    /// 
    /// </summary>
    public  class ParkingAppService : ApplicationService, IParkingAppService
    {
        #region Var 
        private readonly IRepository<Weixinplates> _abpWeixinplateRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IPictureStoreAppService _pictureStoreAppService;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="abpWeixinplateRepository"></param>
        /// <param name="unitOfWorkManager"></param>
        /// <param name="pictureStoreAppService"></param>
        public ParkingAppService(IRepository<Weixinplates> abpWeixinplateRepository, IUnitOfWorkManager unitOfWorkManager, IPictureStoreAppService pictureStoreAppService)
        {
            _abpWeixinplateRepository = abpWeixinplateRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _pictureStoreAppService = pictureStoreAppService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="updatetime"></param>
        /// <returns></returns>
        public List<WeixinPlateDto> GetWeixinPlateList(DateTime updatetime)
        {
            _unitOfWorkManager.Current.DisableFilter(AbpDataFilters.SoftDelete);
            return _abpWeixinplateRepository.GetAllList(entity => entity.CreationTime >= updatetime || entity.DeletionTime >= updatetime || entity.LastModificationTime > updatetime).MapTo<List<WeixinPlateDto>>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="updatetime"></param>
        /// <param name="tenantId"></param>
        /// <param name="parkId"></param>
        /// <returns></returns>
        public List<MonthlyCarDto> GetMonthlyCarList(DateTime updatetime, int tenantId, int parkId)
        {
            string sql = "select * from AbpMonthlyCars where TenantId = @TenantId and (CreationTime >= '" + updatetime + "' or DeletionTime >= '" + updatetime + "' or LastModificationTime > '" + updatetime + "') and  (charindex(','+@parkid+ ',', ','+ ParkIds + ',') > 0 or ParkIds = '0')";
            SqlParameter[] param = new SqlParameter[] {
                new SqlParameter("@parkid", parkId+""),
                new SqlParameter("@TenantId", tenantId)
            };
            return Abp.DataProcessHelper.GetEntityFromTable<MonthlyCarDto>(Abp.Helper.SqlHelper.ExecuteDataset(Abp.Helper.SqlHelper.connectionString, CommandType.Text, sql, param).Tables[0]);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="updatetime"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public List<BlackListsDto> GetBlackCarList(DateTime updatetime, int tenantId)
        {
            string sql = "select * from AbpBlackList where TenantId = " + tenantId + " and (CreationTime >= '" + updatetime + "' or DeletionTime >= '" + updatetime + "' or LastModificationTime > '" + updatetime + "')";
            return Abp.DataProcessHelper.GetEntityFromTable<BlackListsDto>(Abp.Helper.SqlHelper.ExecuteDataset(Abp.Helper.SqlHelper.connectionString, CommandType.Text, sql).Tables[0]);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="updatetime"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public List<WhiteListsDto> GetWhiteCarList(DateTime updatetime, int tenantId)
        {
            string sql = "select * from AbpWhiteList where TenantId = " + tenantId + " and (CreationTime >= '" + updatetime + "' or DeletionTime >= '" + updatetime + "' or LastModificationTime > '" + updatetime + "')";
            return Abp.DataProcessHelper.GetEntityFromTable<WhiteListsDto>(Abp.Helper.SqlHelper.ExecuteDataset(Abp.Helper.SqlHelper.connectionString, CommandType.Text, sql).Tables[0]);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int InsertCarinDetail(string BerthNumber, string PlateNumber, string InDeviceCode, string CarType, string Prepaid, int PrepaidPayStatus, string CarInTime, Guid guid, string SensorsInCarTime, string StopType, string CardNo, int TenantId, int CompanyId, int RegionId, int ParkId, int berthsecId, int InOperaId)
        {
            string sql = "select count(Id)  from AbpBusinessDetail where guid = '" + guid + "'";
            object obj = Abp.Helper.SqlHelper.ExecuteScalar(Abp.Helper.SqlHelper.connectionString, CommandType.Text, sql);
            if (int.Parse(obj.ToString()) > 0)
            {
                return 20;//guid已经存在
            }
            sql = "insert into AbpBusinessDetail(BerthNumber, PlateNumber, InDeviceCode, CarInTime, InOperaId, CarType, Prepaid, PrepaidPayStatus, Receivable, FactReceive, Arrearage, PaymentType, EscapePayStatus, IsEscapePay, PayStatus, IsPay, StopType, FeeType, IsLock, guid, TenantId, CompanyId, RegionId, ParkId, BerthsecId, Status, IsDeleted, CreationTime, CreatorUserId) "+
              " values(@BerthNumber, @PlateNumber, @InDeviceCode, @CarInTime, @InOperaId, @CarType, @Prepaid, @PrepaidPayStatus, @Receivable, @FactReceive, @Arrearage, @PaymentType, @EscapePayStatus, @IsEscapePay, @PayStatus, @IsPay, @StopType, @FeeType, @IsLock, @guid, @TenantId, @CompanyId, @RegionId, @ParkId, @BerthsecId, @Status, @IsDeleted, @CreationTime, @CreatorUserId)";
            SqlParameter[] param = new SqlParameter[] {
                new SqlParameter("@BerthNumber", BerthNumber),
                new SqlParameter("@PlateNumber", PlateNumber),
                new SqlParameter("@InDeviceCode", InDeviceCode),
                new SqlParameter("@CarInTime", CarInTime),
                new SqlParameter("@InOperaId", InOperaId),
                new SqlParameter("@CarType", CarType),
                new SqlParameter("@Prepaid", Prepaid),
                new SqlParameter("@PrepaidPayStatus", 1),
                new SqlParameter("@Receivable", "0"),
                new SqlParameter("@FactReceive", "0"),
                new SqlParameter("@Arrearage", "0"),
                new SqlParameter("@PaymentType", "0"),
                new SqlParameter("@EscapePayStatus", "0"),
                new SqlParameter("@IsEscapePay", false),
                new SqlParameter("@PayStatus", "0"),
                new SqlParameter("@IsPay", false),
                new SqlParameter("@StopType", 1),
                new SqlParameter("@FeeType", "0"),
                new SqlParameter("@IsLock", false),
                new SqlParameter("@guid", guid),
                new SqlParameter("@TenantId", TenantId),
                new SqlParameter("@CompanyId", CompanyId),
                new SqlParameter("@RegionId", RegionId),
                new SqlParameter("@ParkId", ParkId),
                new SqlParameter("@BerthsecId", berthsecId),
                new SqlParameter("@Status", 1),
                new SqlParameter("@IsDeleted", false),
                new SqlParameter("@CreationTime", DateTime.Now),
                new SqlParameter("@CreatorUserId", "")
            };
            return Abp.Helper.SqlHelper.ExecuteNonQuery(Abp.Helper.SqlHelper.connectionString, CommandType.Text, sql, param);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int InsertCaroutDetail(Guid guid, int Status, decimal Money, decimal FactReceive,  string CarOutTime, string CarPayTime, int OutOperaId, string OutDeviceCode, int PayStatus, bool IsPay, int StopTime, string OutBatchNo)
        {
            string sql = "select Status  from AbpBusinessDetail where guid = '" + guid + "'";
            object obj = Abp.Helper.SqlHelper.ExecuteScalar(Abp.Helper.SqlHelper.connectionString, CommandType.Text, sql);
            if (obj == null)
            {
                return 22;//guid不存在
            }
            if (int.Parse(obj.ToString()) > 1)
            {
                return 201;//guid已经出场
            }

            sql = "update AbpBusinessDetail set Status = @Status, Money = @Money, FactReceive = @FactReceive, CarOutTime = @CarOutTime, CarPayTime = @CarPayTime, OutOperaId = @OutOperaId, OutDeviceCode = @OutDeviceCode, PayStatus = @PayStatus, IsPay = @IsPay, FeeType = @FeeType, StopTime = @StopTime, OutBatchNo = @OutBatchNo where guid = @guid";
            SqlParameter[] param = new SqlParameter[] {
                new SqlParameter("@Status", Status),
                new SqlParameter("@Money", Money),
                new SqlParameter("@FactReceive", FactReceive),
                new SqlParameter("@CarOutTime", CarOutTime),
                new SqlParameter("@CarPayTime", CarPayTime),
                new SqlParameter("@OutOperaId", OutOperaId),
                new SqlParameter("@OutDeviceCode", OutDeviceCode),
                new SqlParameter("@PayStatus", PayStatus),
                new SqlParameter("@IsPay", IsPay),
                new SqlParameter("@FeeType", "1"),
                new SqlParameter("@StopTime", StopTime),
                new SqlParameter("@OutBatchNo", OutBatchNo),
                new SqlParameter("@guid", guid)
            };
            return Abp.Helper.SqlHelper.ExecuteNonQuery(Abp.Helper.SqlHelper.connectionString, CommandType.Text, sql, param);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int InsertPicStore(Guid guid, Stream str, int businessid, int pictype, string createTime)
        {
            _pictureStoreAppService.InsertPicture(new Businesses.Dtos.PicMongoDto() { BusinessDetailGuid = guid, BusinessDetailId = businessid, PicType = pictype, CreateTime = createTime });
            return 1;
        }
    }
}
