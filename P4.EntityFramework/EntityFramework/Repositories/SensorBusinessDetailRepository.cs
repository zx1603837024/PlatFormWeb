using Abp.EntityFramework;
using P4.Parks;
using P4.SensorBusinessDetails;
using P4.SensorBusinessDetails.Dtos;
using P4.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using Abp.AutoMapper;
using Abp.Application.Services.Dto;
using Newtonsoft.Json;

namespace P4.EntityFramework.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class SensorBusinessDetailRepository : P4RepositoryBase<SensorBusinessDetail, long>, ISensorBusinessDetailRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public SensorBusinessDetailRepository(IDbContextProvider<P4DbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="abpsession"></param>
        /// <returns></returns>
        public GetAllSensorBusinessDetailOutput GetAllSensorBusinessDetaillist(SensorBusinessDetailInput input, IAbpSession abpsession)
        {
            int records = 0;

            

            var QueryTable = Table.WhereIf(abpsession.TenantId.HasValue, entity => entity.TenantId == abpsession.TenantId).WhereIf(!string.IsNullOrWhiteSpace(abpsession.CompanyId.ToString()) && abpsession.CompanyId.ToString() != "0", entity => entity.CompanyId == abpsession.CompanyId);
            if (!string.IsNullOrWhiteSpace(input.filters))
            {
                FilterDto _filterDto = JsonConvert.DeserializeObject(input.filters, typeof(FilterDto)) as FilterDto;
                foreach (var entity in _filterDto.rules)
                {
                    switch (entity.field)
                    {
                        case "ParkName":
                            var tempvalue = int.Parse(entity.data);
                            QueryTable = QueryTable.Where(entry => entry.ParkId == tempvalue);
                            break;
                        case "BerthsecName":
                            var tempvalue1 = int.Parse(entity.data);
                            QueryTable = QueryTable.Where(entry => entry.BerthsecId == tempvalue1);
                            break;
                        case "BerthNumber":
                            QueryTable = QueryTable.Where(entry => entry.BerthNumber == entity.data);
                            break;
                        case "SensorNumber":
                            QueryTable = QueryTable.Where(entry => entry.SensorNumber == entity.data);
                            break;
                        default:
                            break;
                    }
                }
            }


            List<SensorBusinessDetailDto> query = new List<SensorBusinessDetailDto>();

            records = QueryTable.Count();
            query = QueryTable.Select(c => new SensorBusinessDetailDto
           {
               Id = c.Id,
               ParkId=c.ParkId.Value,
               BerthsecId=c.BerthsecId.Value,
               ParkName = Context.Set<Park>().FirstOrDefault(entity => entity.Id == c.ParkId).ParkName,
               BerthsecName = Context.Set<Berthsecs.Berthsec>().FirstOrDefault(entity => entity.Id == c.BerthsecId).BerthsecName,
               BerthNumber = c.BerthNumber,
               SensorNumber = c.SensorNumber,
               PlateNumber = c.PlateNumber,
               Receivable = c.Receivable,
               CarInTime = c.CarInTime,
               CarOutTime = c.CarOutTime,
               StopTime = c.StopTime
           }).Orders(input).PageBy(input).ToList();
            return new GetAllSensorBusinessDetailOutput()
            {
                rows = query.ToList(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }
    }
}
