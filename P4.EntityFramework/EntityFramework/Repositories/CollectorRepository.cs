using Abp.EntityFramework;
using P4.Collectors;
using P4.Collectors.Dtos;
using P4.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.EntityFramework.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class CollectorRepository : P4RepositoryBase<BerthsecReport>, ICollectorRepository
    {
         /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public CollectorRepository(IDbContextProvider<P4DbContext> dbContextProvider)
            : base(dbContextProvider)
        { 

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Collectors.Dtos.GetYearTotalOutput GetYearBussinessCount()
        {
            DateTime now = DateTime.Now;
            int nowyear = now.Year;
            DateTime day1 = new DateTime(now.Year, 1, 1);
            DateTime beginTime = day1;
            DateTime endTime = now;

            var query = Table.Where(ber => ber.Time >= beginTime && ber.Time <= endTime);

            var TReceivable = query.Sum(r => r.Receivable);
            var TFactReceive = query.Sum(r => r.FactReceive);
            var TArrearage = query.Sum(r => r.Arrearage);
            var TRepayment = query.Sum(r => r.Repayment);

            return new GetYearTotalOutput()
            {
                YearNum = nowyear,
                Receivable = TReceivable.HasValue == true ? TReceivable.Value : 0,
                FactReceive = TFactReceive.HasValue == true ? TFactReceive.Value : 0,
                Arrearage = TArrearage.HasValue == true ? TArrearage.Value : 0,
                Repayment = TRepayment.HasValue == true ? TRepayment.Value : 0
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<GetMonthTotalOutput> GetMonthBussinessCount()
        {
            //DateTime now = DateTime.Now;
            //int nowmonth = now.Month;
            //DateTime day1 = new DateTime(now.Year, now.Month, 1);
            //DateTime beginTime = day1;
            //DateTime endTime = now;


            //var query = Table.Where(ber => ber.Time >= beginTime && ber.Time <= endTime).GroupBy(bts => bts.Time.Day);

            

            //List<GetMonthTotalOutput> monthlist = new List<GetMonthTotalOutput>();
            //List<BerthsecReport> berthsecReportList = _abpBerthsecReportRepository.GetAllList(ber => ber.Time >= beginTime && ber.Time <= endTime);
            //var MonthDatas = berthsecReportList.GroupBy(bts => bts.Time.Day);

            //foreach (var monthdata in MonthDatas)
            //{
            //    monthlist.Add(new GetMonthTotalOutput()
            //    {
            //        monthday = monthdata.Key,
            //        Receivable = Convert.ToDecimal(monthdata.Sum(p => p.Receivable)),
            //        FactReceive = Convert.ToDecimal(monthdata.Sum(p => p.FactReceive)),
            //        Arrearage = Convert.ToDecimal(monthdata.Sum(p => p.Arrearage))

            //    });
            //}
            //return monthlist;
            return null;
        }
    }
}
