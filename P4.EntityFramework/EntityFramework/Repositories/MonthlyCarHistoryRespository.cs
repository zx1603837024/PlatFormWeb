using Abp.EntityFramework;
using P4.MonthlyCars;
using P4.MonthlyCars.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Linq.Extensions;
using Abp.Domain.Repositories;

namespace P4.EntityFramework.Repositories
{
    public class MonthlyCarHistoryRespository : P4RepositoryBase<MonthlyCarHistory, long>, IMonthlyCarHistoryRespository
    {
        private readonly IRepository<Dictionarys.DictionaryValue> _abpDictionaryValueRepository;
        public MonthlyCarHistoryRespository(IDbContextProvider<P4DbContext> dbContextProvider, IRepository<Dictionarys.DictionaryValue> abpDictionaryValueRepository)
            : base(dbContextProvider)
        {
            _abpDictionaryValueRepository = abpDictionaryValueRepository;
        }

        public List<MonthlyCarHistoryDto> GetMonthlyCarHistoryAll(MonthlyCarHistoryInput input)
        {
            //int records = Table.Filters(input).Count();
            var query = Table.Where(entity=>entity.CreationTime > input.operateDateBegin && entity.CreationTime < input.operateDateEnd).Select(c => new MonthlyCarHistoryDto
            {
                Id = c.Id,
                BeginTime = c.BeginTime,
                EndTime = c.EndTime,
                Money = c.Money,
                Status=c.Status,
                VehicleOwner = Context.Set<MonthlyCar>().FirstOrDefault(d =>d.Id== c.MonthlyCarId).VehicleOwner,
                //ParkName = Context.Set<MonthlyCar>().FirstOrDefault(d => d.Id == c.MonthlyCarId)
                MonthyType =Context.Set<MonthlyCar>().FirstOrDefault(d => d.Id == c.MonthlyCarId).MonthyType,
                PlateNumber = Context.Set<MonthlyCar>().FirstOrDefault(d => d.Id == c.MonthlyCarId).PlateNumber,
                Telphone = Context.Set<MonthlyCar>().FirstOrDefault(d => d.Id == c.MonthlyCarId).Telphone,
                ParkIds = Context.Set<MonthlyCar>().FirstOrDefault(d => d.Id == c.MonthlyCarId).ParkIds,
                CreationTime = c.CreationTime
            });
            return query.ToList();
            //return new GetAllMonthlyCarHistoryOutput()
            //{
            //    rows = query.ToList(),
            //    records = records,
            //    total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            //};
        }
    }
}
