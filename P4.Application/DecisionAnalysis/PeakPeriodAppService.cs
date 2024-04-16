using Abp.Application.Services;
using Abp.Domain.Repositories;
using P4.DecisionAnalysis.Dtos;
using System.Collections.Generic;
using System.Linq;
using Abp.AutoMapper;
using Abp.Linq.Extensions;
using System.Data.SqlClient;
using System.Data;

namespace P4.DecisionAnalysis
{
    /// <summary>
    /// 预警管理
    /// </summary>
    public class PeakPeriodAppService : ApplicationService, IPeakPeriodAppService
    {
        #region Var
        private readonly IRepository<PeakPeriod> _abpPeakPeriodRepository;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="abpPeakPeriodRepository"></param>
        public PeakPeriodAppService(IRepository<PeakPeriod> abpPeakPeriodRepository)
        {
            _abpPeakPeriodRepository = abpPeakPeriodRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Dtos.GetAllPeakPeriodOutput GetAllPeakPeriodList(Dtos.SearchPeakPriodInput input)
        {

            int records = _abpPeakPeriodRepository.GetAll().Filters(input).ToList().Count;
            return new GetAllPeakPeriodOutput()
            {
                rows = _abpPeakPeriodRepository.GetAll().Orders(input).Filters(input).PageBy(input).ToList().MapTo<List<PeakPeriodDto>>(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }

        /// <summary>
        /// 获取预警数据
        /// </summary>
        /// <param name="count"></param>
        /// <param name="isactive"></param>
        /// <returns></returns>
        public List<Dtos.PeakPeriodDto> GetTopPeakPeriodList(int count, bool? isactive)
        {
            return _abpPeakPeriodRepository.GetAll().Where(entity => entity.IsActive == false).OrderByDescending(p => p.CreationTime).Take(count).ToList().MapTo<List<Dtos.PeakPeriodDto>>();
        }

        /// <summary>
        /// 获取利用率
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetUtilizationHourListOutput GetUtilizationHourList(SearchUtilizationInput input)
        {
            List<UtilizationHourDto> list = new List<UtilizationHourDto>();
            if (input.begintime.HasValue == false || input.berthsecId.HasValue == false)
                return new GetUtilizationHourListOutput() { rows = list };
            SqlParameter[] param = new SqlParameter[]{
                new SqlParameter("@berthsecid", input.berthsecId.Value),
                new SqlParameter("@begintime", input.begintime.Value.ToString("yyyy-MM-dd 00:00:00")),
                new SqlParameter("@endtime", input.endtime.Value.ToString("yyyy-MM-dd 23:59:59")),
                new SqlParameter("@page", input.page),
                new SqlParameter("@row", input.rows),

            };

            DataSet ds = Abp.Helper.SqlHelper.ExecuteDataset(Abp.Helper.SqlHelper.connectionString, "Pro_BerthsecUtilizationHour", param);

            DataTable dt = ds.Tables[0];
            DataTable dtcount = ds.Tables[1];
            
            UtilizationHourDto model = null;

            foreach (DataRow dr in dt.Rows)
            {
                model = new UtilizationHourDto();
                model.berthsecid = int.Parse(dr["berthsecid"].ToString());
                model.BerthsecName = dr["BerthsecName"].ToString();
                model.DateTimeValue = dr["Time"].ToString();
                model.Utilization0 = dr["Utilization0"].ToString();
                model.Utilization1 = dr["Utilization1"].ToString();
                model.Utilization2 = dr["Utilization2"].ToString();
                model.Utilization3 = dr["Utilization3"].ToString();
                model.Utilization4 = dr["Utilization4"].ToString();
                model.Utilization5 = dr["Utilization5"].ToString();
                model.Utilization6 = dr["Utilization6"].ToString();
                model.Utilization7 = dr["Utilization7"].ToString();
                model.Utilization8 = dr["Utilization8"].ToString();
                model.Utilization9 = dr["Utilization9"].ToString();
                model.Utilization10 = dr["Utilization10"].ToString();
                model.Utilization11 = dr["Utilization11"].ToString();
                model.Utilization12 = dr["Utilization12"].ToString();
                model.Utilization13 = dr["Utilization13"].ToString();
                model.Utilization14 = dr["Utilization14"].ToString();
                model.Utilization15 = dr["Utilization15"].ToString();
                model.Utilization16 = dr["Utilization16"].ToString();
                model.Utilization17 = dr["Utilization17"].ToString();
                model.Utilization18 = dr["Utilization18"].ToString();
                model.Utilization19 = dr["Utilization19"].ToString();
                model.Utilization20 = dr["Utilization20"].ToString();
                model.Utilization21 = dr["Utilization21"].ToString();
                model.Utilization22 = dr["Utilization22"].ToString();
                model.Utilization23 = dr["Utilization23"].ToString();
                model.Status = bool.Parse(dr["status"].ToString()) == true ? 1 : 0;
                list.Add(model);
            }
            int records = int.Parse(dtcount.Rows[0]["count"].ToString());
            return new GetUtilizationHourListOutput()
            {
                rows = list,
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<BerthsecAnalysisResultDto> GetBerthsecAnalysisResultList(SearchBerthsecAnalysisResultInput input)
        {
            List<BerthsecAnalysisResultDto> list = new List<BerthsecAnalysisResultDto>();
            if (input.BeginTime.HasValue == false || input.EndTime.HasValue == false || input.ParkId.HasValue == false)
            {
                return list;
            }

            SqlParameter[] param = new SqlParameter[] { 
                new SqlParameter("@begintime", input.BeginTime.Value.ToString("yyyy-MM-dd 00:00:00")),
                new SqlParameter("@endtime", input.EndTime.Value.AddDays(1).ToString("yyyy-MM-dd 00:00:00")),
                new SqlParameter("@parkid", input.ParkId.Value)
            };

            DataTable dt = Abp.Helper.SqlHelper.ExecuteDataset(Abp.Helper.SqlHelper.connectionString, "Pro_BerthsecAnalysisResult", param).Tables[0];
            BerthsecAnalysisResultDto model = null;
            foreach (DataRow dr in dt.Rows)
            {
                model = new BerthsecAnalysisResultDto();
                model.BerthsecId = dr["BerthsecId"].ToString();
                model.BerthsecName = dr["BerthsecName"].ToString();
                model.Arrearage = dr["Arrearage"].ToString().Length > 0 ? dr["Arrearage"].ToString() : "0";
                model.FactReceive = dr["FactReceive"].ToString().Length > 0 ? dr["FactReceive"].ToString() : "0";
                model.Money = dr["Money"].ToString().Length > 0 ? dr["Money"].ToString() : "0";
                model.Repayment = dr["Repayment"].ToString().Length > 0 ? dr["Repayment"].ToString() : "0";
                model.Turnover = dr["Turnover"].ToString().Length > 0 ? dr["Turnover"].ToString() : "0";
                model.Utilization = dr["Utilization"].ToString().Length > 0 ? dr["Utilization"].ToString() : "0";
                model.BerthCount = dr["BerthCount"].ToString().Length > 0 ? dr["BerthCount"].ToString() : "0";//泊位总数
                model.RateName = dr["RateName"].ToString();
                model.StopTime = dr["StopTime"].ToString().Length > 0 ? dr["StopTime"].ToString() : "0";//总停车时长
                model.StopTimes = dr["StopTimes"].ToString().Length > 0 ? dr["StopTimes"].ToString() : "0";//总停车次数
                model.NoStopTime = dr["NoStopTime"].ToString().Length > 0 ? dr["NoStopTime"].ToString() : "0";//免费总停车时长
                model.NoStopTimes = dr["NoStopTimes"].ToString().Length > 0 ? dr["NoStopTimes"].ToString() : "0";//免费总停车次数
                model.Remark = dr["Remark"].ToString();//费率备注
                list.Add(model);
            }
            return list;
        }
    }
}
