using Abp.EntityFramework;
using P4.Sensors;
using P4.Sensors.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Linq.Extensions;
using P4.MultiTenancy;
using P4.Companys;
using P4.Regions;
using P4.Parks;
using P4.Berthsecs;
using P4.Dictionarys;
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;

namespace P4.EntityFramework.Repositories
{
    public class SensorsRepository : P4RepositoryBase<Sensor>, ISensorsRepository
    {
        public SensorsRepository(IDbContextProvider<P4DbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }


        /// <summary>
        /// 车检器信息(显示车检器状态 离线次数)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetSensorInfoOutPut GetSensorInfoOnlineOrNot(SearchSensorsInput input)
        {

            DbRawSqlQuery<SensorInfoDto> Sensor = null;
            //SqlParameter[] sqlParam = new SqlParameter[]{};
            List<SqlParameter> listSql = new List<SqlParameter>();
            string sqlStr = @"   select * from (select TimeDiff,ParkId,SensorStatus,BeatDatetime,SensorNumber,TenantId,SensorARPCount,ParkName,BerthsecName,ROW_NUMBER() OVER(Order by  SensorNumber DESC ) AS RowNumber from (
   select TimeDiff,AbpSensors.parkid,SensorStatus,beatdatetime,AbpSensors.sensornumber,AbpSensors.tenantid,isnull(SensorARPCount,0) as SensorARPCount,AbpParks.parkname,AbpBerthsecs.berthsecname from (
select datediff(minute,convert(varchar,beatdatetime,120),convert(varchar,getdate(),120)) as TimeDiff
  , case when datediff(minute, convert(varchar,beatdatetime,120), convert(varchar,getdate(),120)) <= "+ P4Consts.SensorDayOnline +" then '正常' else '离线' end as SensorStatus, beatdatetime, sensornumber, tenantid, parkid, berthsecid from AbpSensors where companyid in (" + string.Join(",", input.CompanyIds) + ")";

            //StrSql += @" companyid in (" + string.Join(",", input.CompanyIds) + ")";
            if (input.TenantId.HasValue)
            {
                sqlStr += " and tenantid = @tenantid ";
                listSql.Add(new SqlParameter("@tenantid", input.TenantId));
            }

            sqlStr += @" )";
            sqlStr += @"as AbpSensors left join (select count(1) as SensorARPCount,sensornumber from AbpSensorRunStatus where begintime > @begintime and  begintime < @endtime  and endtime > @begintime and endtime<@endtime and status=0 group by sensornumber) as AbpSensorRunStatus on AbpSensors.sensornumber = AbpSensorRunStatus.sensornumber left join AbpParks on AbpSensors.parkid = AbpParks.id left join AbpBerthsecs on AbpSensors.berthsecid=AbpBerthsecs.id) as ChenDeWang where 1=1 ";
            if (!string.IsNullOrWhiteSpace(input.SensorstatusS) && input.SensorstatusS != "全部")
            {
                sqlStr += @" and SensorStatus=@SensorStatus ";
                listSql.Add(new SqlParameter("@SensorStatus", input.SensorstatusS));
            }
            if (!string.IsNullOrWhiteSpace(input.SensorNumber))
            {
                sqlStr += @" and SensorNumber=@SensorNumber ";
                listSql.Add(new SqlParameter("@SensorNumber", input.SensorNumber));
            }
            if (!string.IsNullOrWhiteSpace(input.ParkId.ToString()) && input.ParkId != 0)
            {
                sqlStr += @" and  ParkId=@ParkId";
                listSql.Add(new SqlParameter("@ParkId", input.ParkId));
            }
            sqlStr += @") as HaoBa where RowNumber between @beginSize and @endSize ";
            listSql.Add(new SqlParameter("@begintime", input.begindt));
            listSql.Add(new SqlParameter("@endtime", input.enddt));
            listSql.Add(new SqlParameter("@beginSize", input.page * input.rows - input.rows + 1));
            listSql.Add(new SqlParameter("@endSize", input.page * input.rows));
            SqlParameter[] param = listSql.ToArray();
            Sensor = Context.Database.SqlQuery<SensorInfoDto>(sqlStr, param);
            var rows = Sensor.ToList();
            int records = GetSensorCount(input);
            return new GetSensorInfoOutPut()
            {
                rows = rows,
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }
        /// <summary>
        /// 停车场车检器在线率
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetSensorParkRation GetSensorPRatio(SearchSensorsInput input)
        {
            List<SqlParameter> SqlParam = new List<SqlParameter>();
            List<SqlParameter> SqlParamTow = new List<SqlParameter>();
            string SqlStr = @" select ParkName from (select * from (
 select datediff(minute,convert(varchar,beatdatetime,120),convert(varchar,getdate(),120)) as TimeDiff
  ,case when datediff(minute, convert(varchar,beatdatetime,120), convert(varchar,getdate(),120)) <= 30 then '正常' else '离线'
 end as SensorStatus ,beatdatetime,sensornumber,tenantid,parkid,berthsecid from AbpSensors where companyid in (" + string.Join(",", input.CompanyIds) + ")";


            string SqlStrTow = @" select ParkName from (select * from (
 select datediff(minute, convert(varchar, beatdatetime, 120),convert(varchar,getdate(), 120)) as TimeDiff
  ,case when datediff(minute, convert(varchar,beatdatetime,120), convert(varchar,getdate(),120)) <= 30 then '正常' else '离线'
 end as SensorStatus , beatdatetime, sensornumber, tenantid, parkid, berthsecid from AbpSensors where companyid in (" + string.Join(",", input.CompanyIds) + ")";

            if (input.TenantId.HasValue)
            {
                SqlStr += " and tenantid = @tenantid ";
                SqlParam.Add(new SqlParameter("@tenantid", input.TenantId.Value));

                SqlStrTow += " and tenantid = @tenantid ";
                SqlParamTow.Add(new SqlParameter("@tenantid", input.TenantId.Value));
            }
            SqlStr += @" )";
            SqlStr += @" as chen where SensorStatus = '正常' and parkid = @ParkId) as haoba left join AbpParks on haoba.ParkId = AbpParks.Id ";
            

            SqlStrTow += @" )";
            SqlStrTow += @" as chen where parkid=@ParkId) as haoba left join AbpParks on haoba.ParkId=AbpParks.Id ";
            SqlParam.Add(new SqlParameter("@ParkId", input.RatioParkId));
            SqlParamTow.Add(new SqlParameter("@ParkId", input.RatioParkId));
            //SqlParamTow.Add(new SqlParameter("@TenantId", input.TenantId));
            //SqlParam.Add(new SqlParameter("@TenantId", input.TenantId));
            SqlParameter[] param = SqlParam.ToArray();
            SqlParameter[] paramTow = SqlParamTow.ToArray();
            var Dt = Context.Database.SqlQuery<SensorParkRatioDto>(SqlStr, param).ToList();
            var DtTow = Context.Database.SqlQuery<SensorParkRatioDto>(SqlStrTow, paramTow).ToList();
            int NormalCount = Dt.Count();//正常的车检器数量
            int AllCount = DtTow.Count();//该车场下所有车检器数量
            List<SensorParkRatioDto> SPRD = new List<SensorParkRatioDto>();
            SensorParkRatioDto SPRDModel = new SensorParkRatioDto();
            SPRDModel.ParkName = NormalCount == 0 ? DtTow[1].ParkName : Dt[1].ParkName;
            double a = (double)NormalCount / AllCount;
            SPRDModel.Ratio = a.ToString("0.0%");
            SPRD.Add(SPRDModel);
            int records = 1;
            return new GetSensorParkRation()
            {
                rows = SPRD
                //records = records,
                //total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }

        public int GetSensorCount(SearchSensorsInput input)
        {
            DbRawSqlQuery<SensorInfoDto> Sensor = null;
            //SqlParameter[] sqlParam = new SqlParameter[]{};
            List<SqlParameter> listSql = new List<SqlParameter>();
            string sqlStr = @" select * from (select TimeDiff,ParkId,SensorStatus,BeatDatetime,SensorNumber,TenantId,SensorARPCount,ParkName,BerthsecName,ROW_NUMBER() OVER(Order by  SensorNumber DESC ) AS RowNumber from (
   select TimeDiff,AbpSensors.parkid,SensorStatus,beatdatetime,AbpSensors.sensornumber,AbpSensors.tenantid,isnull(SensorARPCount,0) as SensorARPCount,AbpParks.parkname,AbpBerthsecs.berthsecname from (
select datediff(minute,convert(varchar,beatdatetime,120),convert(varchar,getdate(),120)) as TimeDiff
  ,case when datediff(minute,convert(varchar,beatdatetime,120), convert(varchar,getdate(),120)) <= "+ P4Consts.SensorDayOnline + " then '正常' else '离线' end as SensorStatus ,beatdatetime,sensornumber,tenantid,parkid,berthsecid from AbpSensors where  companyid in (" + string.Join(",", input.CompanyIds) + ")";

            if (input.TenantId.HasValue)
            {
                sqlStr += " and tenantid = @tenantid ";
                listSql.Add(new SqlParameter("@tenantid", input.TenantId));
            }
            sqlStr += @" )";
            sqlStr += @" as AbpSensors left join (select count(1) as SensorARPCount,sensornumber from AbpSensorRunStatus where begintime>@begintime and  begintime<@endtime  and endtime>@begintime and endtime<@endtime and status=0 group by sensornumber) as AbpSensorRunStatus on AbpSensors.sensornumber=AbpSensorRunStatus.sensornumber left join AbpParks on AbpSensors.parkid=AbpParks.id left join AbpBerthsecs on AbpSensors.berthsecid=AbpBerthsecs.id) as ChenDeWang where 1=1 ";
            if (!string.IsNullOrWhiteSpace(input.SensorstatusS) && input.SensorstatusS != "全部")
            {
                sqlStr += @" and SensorStatus = @SensorStatus ";
                listSql.Add(new SqlParameter("@SensorStatus", input.SensorstatusS));
            }
            if (!string.IsNullOrWhiteSpace(input.SensorNumber))
            {
                sqlStr += @" and SensorNumber=@SensorNumber ";
                listSql.Add(new SqlParameter("@SensorNumber", input.SensorNumber));
            }
            if (!string.IsNullOrWhiteSpace(input.ParkId.ToString()) && input.ParkId != 0)
            {
                sqlStr += @" and  ParkId=@ParkId";
                listSql.Add(new SqlParameter("@ParkId", input.ParkId));
            }
            sqlStr += @") as HaoBa ";
            listSql.Add(new SqlParameter("@begintime", input.begindt));
            listSql.Add(new SqlParameter("@endtime", input.enddt));
            //listSql.Add(new SqlParameter("@beginSize", input.page * input.rows - input.rows + 1));
            //listSql.Add(new SqlParameter("@endSize", input.page * input.rows));
            //listSql.Add(new SqlParameter("@TenantId", input.TenantId));
            SqlParameter[] param = listSql.ToArray();
            Sensor = Context.Database.SqlQuery<SensorInfoDto>(sqlStr, param);
            return Sensor.ToList().Count();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Sensors.Dtos.GetAllSensorsListOutput GetAllSensorList(Sensors.Dtos.SearchSensorsInput input)
        {

            int records = 0;
            var time = DateTime.Now.AddDays(-1);
            var time1 = DateTime.Now.AddMinutes(-P4Consts.SensorDayOnline);
            var query = Table.Select(c => new SensorDto
            {
                Id = c.Id,
                Name = Context.Set<Tenant>().FirstOrDefault(dic => dic.Id == c.TenantId).Name,
                BerthNumber = c.BerthNumber,
                CompanyId = c.CompanyId,
                CompanyName = Context.Set<OperatorsCompany>().FirstOrDefault(dic => dic.Id == c.CompanyId).CompanyName,
                RegionId = c.RegionId,

                RegionName = Context.Set<Region>().FirstOrDefault(dic => dic.Id == c.RegionId).RegionName,
                ParkId = c.ParkId,
                ParkName = Context.Set<Park>().FirstOrDefault(dic => dic.Id == c.ParkId).ParkName,
                BerthsecId = c.BerthsecId,
                BerthsecName = Context.Set<Berthsec>().FirstOrDefault(dic => dic.Id == c.BerthsecId).BerthsecName,
                SensorNumber = c.SensorNumber,
                TransmitterNumber = c.TransmitterNumber,
                BeatDatetime = c.BeatDatetime,
                ParkStatus = Context.Set<DictionaryValue>().FirstOrDefault(dic => dic.ValueCode == c.ParkStatus.ToString() && dic.TypeCode == P4Consts.ParkStatus).ValueData,
                TenantId = c.TenantId,
                IsOnlineValue = Context.Set<DictionaryValue>().FirstOrDefault(dic => dic.ValueCode ==
                    (c.ParkStatus == 1 ? "1" : (time1 > (c.BeatDatetime ?? time) ? "0" : "1"))
                    && dic.TypeCode == P4Consts.IsOnline).ValueData
            });

            query = query.WhereIf(input.TenantId.HasValue, entity => entity.TenantId == input.TenantId)
                .WhereIf(input.CompanyId.HasValue, entity => entity.CompanyId == input.CompanyId)
                .OrderBy(dic => dic.BerthNumber ?? "0").Filters(input);

            records = Table.WhereIf(input.TenantId.HasValue, entity => entity.TenantId == input.TenantId)
                .WhereIf(input.CompanyId.HasValue, entity => entity.CompanyId == input.CompanyId)
                .Filters(input).Count();
            return new GetAllSensorsListOutput()
            {
                rows = query.PageBy(input).ToList(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }

        /// <summary>
        /// 车检器复位
        /// </summary>
        /// <param name="transmitterNumber"></param>
        /// <param name="sensorNumber"></param>
        public void SensorReset(string transmitterNumber, string sensorNumber)
        {
            SqlParameter[] parm = new SqlParameter[]
            {
                new SqlParameter("@TransmitterNumber", transmitterNumber),
                new SqlParameter("@SensorNumber", sensorNumber)
            };
            Abp.Helper.SqlHelper.ExecuteNonQuery(Abp.Helper.SqlHelper.connectionString,
                System.Data.CommandType.StoredProcedure, "Pro_Sensor_SensorReset", parm);
        }

        public int UpdateBerthStatus(string SensorNumber, DateTime? BeatDatetime)
        {
            SqlParameter[] param = new SqlParameter[] { 
                new SqlParameter("@SensorNumber", SensorNumber), 
                 new SqlParameter("@SensorBeatTime", BeatDatetime)
            };
            return Abp.Helper.SqlHelper.ExecuteNonQuery(Abp.Helper.SqlHelper.connectionString,
                System.Data.CommandType.Text, "update abpberths set SensorBeatTime = @SensorBeatTime where SensorNumber = @SensorNumber", param);
        }

        public int UpdateBerthStatus(DateTime? SensorsInCarTime, DateTime SensorBeatTime, Guid SensorGuid, short ParkStatus, string SensorNumber)
        {
            SqlParameter[] param = new SqlParameter[] { 
                new SqlParameter("@SensorsInCarTime", SensorsInCarTime), 
                 new SqlParameter("@SensorBeatTime", SensorBeatTime),
                 new SqlParameter("@SensorGuid", SensorGuid),
                 new SqlParameter("@ParkStatus", ParkStatus),
                 new SqlParameter("@SensorNumber", SensorNumber)
            };

            return Abp.Helper.SqlHelper.ExecuteNonQuery(Abp.Helper.SqlHelper.connectionString,
                System.Data.CommandType.Text, "update abpberths set SensorsInCarTime = @SensorsInCarTime, SensorBeatTime = @SensorBeatTime, SensorGuid = @SensorGuid, ParkStatus = @ParkStatus where SensorNumber = @SensorNumber", param);
        }

        public int SensorResetUpdateBerthStatus(DateTime SensorsOutCarTime, DateTime SensorBeatTime, short ParkStatus, string SensorNumber)
        {
            SqlParameter[] param = new SqlParameter[] { 
                new SqlParameter("@SensorsOutCarTime", SensorsOutCarTime), 
                 new SqlParameter("@SensorBeatTime", SensorBeatTime),
                 new SqlParameter("@SensorGuid", null),
                 new SqlParameter("@ParkStatus", ParkStatus),
                 new SqlParameter("@SensorNumber", SensorNumber)
            };

            return Abp.Helper.SqlHelper.ExecuteNonQuery(Abp.Helper.SqlHelper.connectionString,
                System.Data.CommandType.Text, "update abpberths set SensorsOutCarTime = @SensorsOutCarTime, SensorBeatTime = @SensorBeatTime, SensorGuid = @SensorGuid, ParkStatus = @ParkStatus where SensorNumber = @SensorNumber", param);
        }

        public int UpdateBerthCarOutStatus(DateTime? SensorsOutCarTime, DateTime SensorBeatTime, Guid? SensorGuid, short ParkStatus, string SensorNumber)
        {
            SqlParameter[] param = new SqlParameter[] { 
                new SqlParameter("@SensorsOutCarTime", SensorsOutCarTime), 
                 new SqlParameter("@SensorBeatTime", SensorBeatTime),
                 new SqlParameter("@SensorGuid", SensorGuid),
                 new SqlParameter("@ParkStatus", ParkStatus),
                 new SqlParameter("@SensorNumber", SensorNumber)
            };

            return Abp.Helper.SqlHelper.ExecuteNonQuery(Abp.Helper.SqlHelper.connectionString,
                System.Data.CommandType.Text, "update abpberths set SensorsOutCarTime = @SensorsOutCarTime, SensorBeatTime = @SensorBeatTime, SensorGuid = @SensorGuid, ParkStatus = @ParkStatus where SensorNumber = @SensorNumber", param);
        }

        /// <summary>
        /// 车检器状态更新
        /// </summary>
        /// <param name="sensor"></param>
        public void SensorStatus(Sensor sensor)
        {
            SqlParameter[] parm = new SqlParameter[]
            {
                new SqlParameter("@Battery", sensor.Battery),
                new SqlParameter("@Magnetism", sensor.Magnetism),
                new SqlParameter("@TransmitterNumber", sensor.TransmitterNumber),
                new SqlParameter("@SensorNumber", sensor.SensorNumber),
                new SqlParameter("@ParkStatus", sensor.ParkStatus)
            };
            Abp.Helper.SqlHelper.ExecuteNonQuery(Abp.Helper.SqlHelper.connectionString,
                System.Data.CommandType.StoredProcedure, "Pro_Sensor_SensorStatus", parm);
        }
    }
}
