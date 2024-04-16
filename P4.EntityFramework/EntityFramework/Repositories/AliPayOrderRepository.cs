using Abp.Domain.Repositories;
using Abp.EntityFramework;
using P4.AliPay;
using P4.AliPay.Dto;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;

namespace P4.EntityFramework.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class AliPayOrderRepository : P4RepositoryBase<AliPayOrder>, IAliPayOrderRepository
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IRepository<AliPayOrder> _abpAliPayOrderRepository;
        public AliPayOrderRepository(IDbContextProvider<P4DbContext> dbContextProvider, IRepository<AliPayOrder> abpAliPayOrderRepository)
            : base(dbContextProvider)
        {
            _abpAliPayOrderRepository = abpAliPayOrderRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<AliPayOrdersDto> GetAliPayOrdersCount(SearchAliPayOrdersInput input)
        {
            string sqlwhere = "";
            if (input.TenantId.HasValue)
            {
                sqlwhere += " and TenantId = " + input.TenantId.Value;
            }

            if (input.CompanyIds.Count > 0)
            {
                string temp = "";
                foreach (var companyid in input.CompanyIds)
                {
                    temp += companyid + ",";
                }
                temp += "0";
                sqlwhere += " and CompanyId in (" + temp + ")";
            }
            else
            {
                sqlwhere += " and CompanyId in (0)";
            }

            string StrSql = @"select sum(total_amount) as total_amount from AliPayOrders where 1 = 1 " + sqlwhere;
            string StrSql1 = @"select sum(B.total_amount) as total_amount,count(B.useGroup) as AliPayOrdersCount from (select *,1 AS useGroup from AliPayOrders with(nolock) where gmt_payment between @BeginTime and @EndTime " + sqlwhere + ")  as B group by useGroup";
            List<AliPayOrdersDto> BusinessTable = null;
            if (!string.IsNullOrWhiteSpace(input.begindt.ToString()) && !string.IsNullOrWhiteSpace(input.enddt.ToString()))
            {
                List<SqlParameter> SqlParam = new List<SqlParameter>();
                SqlParam.Add(new SqlParameter("@BeginTime", input.begindt));
                SqlParam.Add(new SqlParameter("@EndTime", input.enddt));
                SqlParameter[] param = SqlParam.ToArray();
                BusinessTable = Context.Database.SqlQuery<AliPayOrdersDto>(StrSql1, param).ToList();
            }
            else
            {
                BusinessTable = Context.Database.SqlQuery<AliPayOrdersDto>(StrSql).ToList();
            }
            return BusinessTable;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllAliPayOrdersOutput GetAllAliPayOrderDetailsql(SearchAliPayOrdersInput input)
        {

            List<SqlParameter> SqlParam = new List<SqlParameter>();
            DbRawSqlQuery<AliPayOrdersDto> AliPayOrdersTable = null;
            string sqlwhere = "";
            if (input.TenantId.HasValue)
            {
                sqlwhere += " and TenantId = " + input.TenantId.Value;
            }

            if (input.CompanyIds.Count > 0)
            {
                string temp = "";
                foreach (var companyid in input.CompanyIds)
                {
                    temp += companyid + ",";
                }
                temp += "0";
                sqlwhere += " and CompanyId in (" + temp + ")";
            }
            else
            {
                sqlwhere += " and CompanyId in (0)";
            }

            string StrSql = @"SELECT  Id, CompanyId, TenantId, total_amount, buyer_logon_id, subject, body, trade_no, out_trade_no, gmt_create, gmt_payment, trade_status, PayType, CreationTime, guid,B.RowNumber from (
select *,ROW_NUMBER() OVER(Order by Id DESC ) AS RowNumber  FROM  AliPayOrders";
            StrSql += @" where (gmt_payment between @BeginTime and @EndTime) " + sqlwhere + ")as B where RowNumber between @BeginSize and @EndSize";
            SqlParam.Add(new SqlParameter("@BeginSize", input.page * input.rows - input.rows + 1));
            SqlParam.Add(new SqlParameter("@EndSize", input.page * input.rows));
            SqlParam.Add(new SqlParameter("@BeginTime", input.begindt));
            SqlParam.Add(new SqlParameter("@EndTime", input.enddt));
            SqlParameter[] param = SqlParam.ToArray();
            int records = 0;
            AliPayOrdersTable = Context.Database.SqlQuery<AliPayOrdersDto>(StrSql, param);
            var rows = AliPayOrdersTable.ToList();
            //var rows = _abpAliPayOrderRepository.GetAll().Orders(input).Filters(input).PageBy(input).ToList().MapTo<List<AliPayOrdersDto>>();
            List<AliPayOrdersDto> BBBB = GetAliPayOrdersCount(input);
            AliPayOrderUserData aoud = new AliPayOrderUserData();
            aoud.trade_no = "金额汇总";
            if (BBBB.Count > 0)
            {
                records = BBBB[0].AliPayOrdersCount;
                aoud.total_amount = BBBB[0].total_amount;
            }
            return new GetAllAliPayOrdersOutput()
            {
                rows = rows,
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0),
                userdata = aoud
            };
        }
    }
}
