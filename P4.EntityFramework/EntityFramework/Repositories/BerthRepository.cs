using Abp.AutoMapper;
using Abp.EntityFramework;
using Abp.Linq.Extensions;
using P4.Berths;
using P4.Berths.Dtos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace P4.EntityFramework.Repositories
{

    /// <summary>
    /// 
    /// </summary>
    public class BerthRepository : P4RepositoryBase<Berth, long>, IBerthRepository
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public BerthRepository(IDbContextProvider<P4DbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        /// <summary>
        /// 获取收费员管理的泊位段
        /// </summary>
        /// <returns></returns>
        public List<BerthDto> GetAllBerths(List<int> BerthsecId)
        {
            var temp = Context.Database.SqlQuery<BerthDto>("select * from AbpBerths with(nolock) where BerthsecId in (" + string.Join(",", BerthsecId) + ") and IsActive = 1 ", new SqlParameter[] { }).ToList();
            return temp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllBerthOutput GetBerthAll(BerthInput input, int? tenantId, int? companyId)
        {
            int records = Table.Filters(input).Count();
            var query = Table.Where(d => d.TenantId == tenantId && d.CompanyId == companyId).Select(c => new BerthDto
            {
                Id = c.Id,
                BerthsecId = c.BerthsecId,
                RegionId = c.RegionId,
                ParkId = c.ParkId,
                InCarTime = c.InCarTime,
                SensorNumber = c.SensorNumber,
                OutCarTime = c.OutCarTime,
                RelateNumber = c.RelateNumber,
                IsActive = c.IsActive,
                BerthNumber = c.BerthNumber,
                BerthStatus = c.BerthStatus,
                ParkName = c.Park.ParkName,
                Name = c.Tenant.Name,
                guid = c.guid,
                BerthsecName = Context.Set<Berthsecs.Berthsec>().FirstOrDefault(entity => entity.Id == c.BerthsecId).BerthsecName,
                RegionName = Context.Set<Regions.Region>().FirstOrDefault(entity => entity.Id == c.RegionId).RegionName
            });
            if (string.IsNullOrWhiteSpace(input.filters))
            {
                query = query.Where(c => c.RegionId != 1);
            }
            query = query.OrderBy(c => c.BerthsecId).Filters(input).PageBy(input);
            return new GetAllBerthOutput()
            {
                rows = query.ToList(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }


        /// <summary>
        /// 车辆进场  泊位在停
        /// </summary>
        /// <param name="BerthNumber"></param>
        /// <param name="BerthsecId"></param>
        /// <param name="PlateNumber"></param>
        /// <param name="CarInTime"></param>
        /// <param name="guid"></param>
        /// <param name="CarType"></param>
        /// <param name="CardNo"></param>
        /// <param name="Prepaid"></param>
        public void CarInUpdateBerhs(string BerthNumber, int BerthsecId, string PlateNumber, DateTime? CarInTime,
            Guid guid, short CarType, string CardNo, decimal Prepaid)
        {
            string sqlStr =
                string.Format(@"update AbpBerths set BerthStatus='1',RelateNumber='{0}',InCarTime='{1}',guid='{2}',
CarType={3},OutCarTime=null,CardNo='{4}',Prepaid={5} where BerthNumber='{6}' and IsActive=1 and BerthsecId={7}",
                    PlateNumber, CarInTime, guid, CarType, CardNo, Prepaid, BerthNumber, BerthsecId);
            Context.Database.ExecuteSqlCommand(sqlStr);
        }

        /// <summary>
        ///  获取地磁数据（PDA专用）
        /// </summary>
        /// <param name="SyncTime"></param>
        /// <param name="BerthsecID"></param>
        /// <returns></returns>
        public List<BerthSensorDto> GetAllBerthSensor(string SyncTime, List<int> BerthsecID)
        {
            if (SyncTime.Length > 10)
            {
                DateTime temp = DateTime.ParseExact(SyncTime, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture).AddMinutes(-5);
                var model = Table.Where(entry => BerthsecID.Contains(entry.BerthsecId) && (entry.InCarTime >= temp || (entry.SensorBeatTime >= temp && entry.SensorNumber != null))).ToList().MapTo<List<BerthSensorDto>>();
                return model;
            }
            else
            {
                var model = Table.Where(entity => BerthsecID.Contains(entity.BerthsecId)).ToList().MapTo<List<BerthSensorDto>>();
                return model;
            }
        }
    }
}
