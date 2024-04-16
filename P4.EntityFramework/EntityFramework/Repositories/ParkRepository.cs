using P4.Parks;
using P4.Parks.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Linq.Extensions;
using P4.Regions;
using Abp.EntityFramework;
using Abp.AutoMapper;
using System.Data.SqlClient;
using P4.ParkingLot.Dto;
using P4.ParkingLot.Dtos;

namespace P4.EntityFramework.Repositories
{
    public class ParkRepository : P4RepositoryBase<Park>, IParkRepository
    {

        public ParkRepository(IDbContextProvider<P4DbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }


        public GetAllParkOutput GetParkAll(ParkInput input)
        {
            int records = Table.Filters(input).Count();
            return new GetAllParkOutput()
            {
                rows = Table.Orders(input).Filters(input).PageBy(input).MapTo<List<ParkDto>>(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
                
            };
        }

        /// <summary>
        /// 获取所有的封闭停车场
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllParkOutput GetCurbPark(ParkInput input)
        {
            int records = Table.Filters(input).Where(u => u.IsDeleted == false && u.ParkType == 1).Count();
            return new GetAllParkOutput()
            {
                rows = Table.Orders(input).Filters(input).Where(u => u.IsDeleted == false && u.ParkType == 1).PageBy(input).MapTo<List<ParkDto>>(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }

        /// <summary>
        /// 获取所有通道
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetParkChannelOutput GetAllChannel(GetParkChannelInput input)
        {
            var filter = Context.AbpParkChannel.AsQueryable();
            if (input.filters != null)
            {
                filter = filter.Filters(input);
            }
            int records = filter.Count();
            return new GetParkChannelOutput()
            {
                rows = filter.Orders(input).PageBy(input).MapTo<List<ParkChannelDto>>(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };

        }


        public List<ParkAppDto> GetParkAppAll()
        {
            string sql = "select * from (select Parkname as name, convert(varchar(20),BerthCount) as total ,x as lat, y as lng, convert(varchar(20), id) as id  from AbpParks where X is not null and TenantId = 1) AS Park left join (select convert(varchar(20), count(1)) as free, ParkId from AbpBerths where parkstatus = 0 group by parkid) as BerthCount on Park.id = ParkId";

            return Context.Database.SqlQuery<ParkAppDto>(sql, new SqlParameter[] { }).ToList();
        }

        /// <summary>
        /// 停车场地图
        /// </summary>
        /// <param name="TenantId"></param>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        public List<ParkInfoMap> GetParkInfoMapList(int? TenantId, List<int> CompanyIds)
        {
            List<SqlParameter> sqlP = new List<SqlParameter>();

            string tenantsql = @" and tenantid = @tenantid ";
            string companysql = @" and companyid in (" + string.Join(",", CompanyIds) + ") ";
            string SqlStr = @"select A_park.id,ParkName,X,Y,isnull(BerthsecCount,0) as BerthsecCount,isnull(A_Berth.BerthCount,0) as BerthCount,isnull(BerthOnTimeCount,0) as BerthOnTimeCount,isnull(BerthNotTimeCount,0) as BerthNotTimeCount from (
select * from AbpParks where isdeleted=0 ";
            if (TenantId.HasValue)
            {
                SqlStr += tenantsql;
                sqlP.Add(new SqlParameter("@tenantid", TenantId));
            }

            SqlStr += companysql;
            SqlStr += @") as A_park 
left join(select count(id) as BerthsecCount,ParkId from AbpBerthsecs 
where isdeleted = 0 and isactive = 1 " + (TenantId.HasValue == true ? tenantsql : "") + companysql + " group by ParkId) as A_Berthsec on A_park.id=A_Berthsec.ParkId left join(select count(id) as BerthCount,ParkId from AbpBerths where isactive=1 " + (TenantId.HasValue == true ? tenantsql : "") + companysql + " group by ParkId) as A_Berth on A_park.id=A_Berth.ParkId left join(select count(id) as BerthOnTimeCount,ParkId from AbpBerths where isactive=1 and berthstatus=1 " + (TenantId.HasValue == true ? tenantsql : "") + companysql + " group by ParkId) as A_BerthOnTime on A_park.id=A_BerthOnTime.ParkId left join (select count(id) as BerthNotTimeCount,ParkId from AbpBerths where isactive=1 and berthstatus=2 " + (TenantId.HasValue == true ? tenantsql : "") + companysql + " group by ParkId) as A_BerthNotTime on A_park.id=A_BerthNotTime.ParkId where X is not null order by id";
            SqlParameter[] sqlParameter = sqlP.ToArray();
            return Context.Database.SqlQuery<ParkInfoMap>(SqlStr, sqlParameter).ToList();

        }

        public List<ParkDto> GetParkNumber(int? tenantID)
        {
            string TenantID = tenantID.ToString();
            string sqlStr = "";
            if (TenantID == null || TenantID == "")
            {
                sqlStr = @"select Id,ParkName,X,Y,ISNULL(ZTNumber,0) as ZTNumber,ISNULL(SumNumber,0) as SumNumber from 
(select Id,ParkName,X,Y from dbo.AbpParks) as a
left join
(select ParkId,count(BerthNumber) as ZTNumber from dbo.AbpBerths 
where BerthStatus=1 
group by ParkId) as b 
on a.Id=b.ParkId left join
(select ParkId,count(BerthNumber) as SumNumber from dbo.AbpBerths 
group by ParkId) as c on a.Id=c.ParkId";
            }
            else
            {
                sqlStr = @"select Id,ParkName,X,Y,ISNULL(ZTNumber,0) as ZTNumber,ISNULL(SumNumber,0) as SumNumber from 
(select Id,ParkName,X,Y from dbo.AbpParks where TenantId=@tenantId) as a
left join
(select ParkId,count(BerthNumber) as ZTNumber from dbo.AbpBerths 
where BerthStatus=1 and TenantId=@tenantId
group by ParkId) as b 
on a.Id=b.ParkId left join
(select ParkId,count(BerthNumber) as SumNumber from dbo.AbpBerths 
where TenantId=@tenantId
group by ParkId) as c on a.Id=c.ParkId";
            }
            List<ParkDto> Listdto = Context.Database.SqlQuery<ParkDto>(sqlStr, new SqlParameter[] { new SqlParameter("@tenantId", TenantID) }).ToList();

            return Listdto;
        }
    }
}
