using P4.EmployeeLoggings;
using P4.EmployeeLoggings.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Linq.Extensions;
using P4.Employees;
using Abp.EntityFramework;
using P4.Parks;
using Abp.AutoMapper;
using P4.Companys;

using System.Data.Entity.SqlServer;
using System.Data.SqlClient;

namespace P4.EntityFramework.Repositories
{
    public class EmployeeCheckRepository : P4RepositoryBase<EmployeeCheck, long>, IEmployeeCheckRepository
    {

        public EmployeeCheckRepository(IDbContextProvider<P4DbContext> dbContextProvider)
            : base(dbContextProvider)
        { 

        }

        /// <summary>
        /// 收费员日志
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllEmployeeCheckOutput GetAllEmployeeChecklist(EmployeeCheckInput input)
        {
            int records = Table.Filters(input).Count();
            var query = Table.Select(c => new EmployeeCheckDto
            {
                Id = c.Id,
                EmployeeId = c.EmployeeId,
                TrueName = Context.Set<Employee>().FirstOrDefault(entity => entity.Id == c.EmployeeId).TrueName,
                CompanyName = Context.Set<OperatorsCompany>().FirstOrDefault(entity => entity.Id == c.CompanyId).CompanyName,
                CheckIn = c.CheckIn,
                CheckInTime = c.CheckInTime,
                CheckOut = c.CheckOut,
                CheckOutTime = c.CheckOutTime,
                DeviceCode = c.DeviceCode,
                BatchNo = c.BatchNo,
                ParkID = c.ParkId,
                BerthsecID = c.BerthsecId,
                IsRepeal = c.IsRepeal,
                IsNormal = c.IsNormal,
                Remark = c.Remark

            }).Orders(input).Filters(input).PageBy(input).ToList();

            foreach (var item in query)
            {
                foreach (var parkId in item.ParkID.Split(','))
                {
                    var temp = Context.Set<Park>().FirstOrDefault(entity => entity.Id.ToString() == parkId);
                    if (default(Park) != temp)
                        item.ParkName += temp.ParkName + ',';
                }
                if (!string.IsNullOrWhiteSpace(item.ParkName))
                    item.ParkName = item.ParkName.Substring(0, item.ParkName.Length - 1);
                foreach (var berthsecId in item.BerthsecID.Split(','))
                {
                    var temp = Context.Set<Berthsecs.Berthsec>().FirstOrDefault(entity => entity.Id.ToString() == berthsecId);
                    if (default(Berthsecs.Berthsec) != temp)
                        item.BerthsecName += temp.BerthsecName + ',';
                }
                if (!string.IsNullOrWhiteSpace(item.BerthsecName))
                    item.BerthsecName = item.BerthsecName.Substring(0, item.BerthsecName.Length - 1);
            }

            return new GetAllEmployeeCheckOutput()
            {
                rows = query,
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public List<EmployeeCheckDto> GetAllEmployeeCheckToplist(long Id)
        {
            var temp = Table.Select(c => new EmployeeCheckDto
            {
                Id = c.Id,
                EmployeeId = c.EmployeeId,
                BerthsecID = c.BerthsecId,
                ParkID = c.ParkId,
                TrueName = Context.Set<Employee>().FirstOrDefault(entity => entity.Id == c.EmployeeId).TrueName,
                CheckIn = c.CheckIn,
                BatchNo = c.BatchNo,
                CheckInTime = c.CheckInTime,
                CheckOut = c.CheckOut,
                CheckOutTime = c.CheckOutTime,
                DeviceCode = c.DeviceCode
            }).Where(entity => entity.EmployeeId == Id).OrderByDescending(p => p.CheckInTime).Take(20).ToList();
            List<EmployeeCheckDto> list = new List<EmployeeCheckDto>();
            foreach (var v in temp)
            {
                foreach (var berthsecId in v.BerthsecID.Split(','))
                {
                    var berthsec = Context.Set<Berthsecs.Berthsec>().FirstOrDefault(entity => entity.Id.ToString() == berthsecId);
                    if (default(Berthsecs.Berthsec) != berthsec)
                        v.BerthsecName += berthsec.BerthsecName + ',';
                }
                v.BerthsecName = v.BerthsecName.Substring(0, v.BerthsecName.Length - 1);

                foreach (var parkId in v.ParkID.Split(','))
                {
                    var park = Context.Set<Park>().FirstOrDefault(entity => entity.Id.ToString() == parkId);
                    if (default(Park) != park)
                        v.ParkName += park.ParkName + ',';
                }
                v.ParkName = v.ParkName.Substring(0, v.ParkName.Length - 1);
                list.Add(v);
            }
            return list;
        }
    }
}
