using Abp.EntityFramework;
using P4.SpecialLists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Linq.Extensions;
using P4.Dictionarys;
using P4.SpecialLists.Dtos;
using System.Data.SqlClient;

namespace P4.EntityFramework.Repositories
{
    public class SpecialRepository : P4RepositoryBase<SpecialList>, ISpecialRepository
    {
        public SpecialRepository(IDbContextProvider<P4DbContext> dbContextProvider)
           : base(dbContextProvider)
        { 
        }

        public SpecialLists.Dtos.GetAllSpecialListsOutput GetAllSpecialList(SpecialLists.Dtos.SpecialListInput input)
        {
            int records = Table.Filters(input).Count();
            var query = Table.Select(c => new SpecialListsDto
            {
                Id = c.Id,
                CarOwner = c.CarOwner,
                Discount = c.Discount,
                IsActive = c.IsActive,
                PlateNumber=c.PlateNumber,
                Remarks = c.Remarks,
                VehicleType = Context.Set<DictionaryValue>().FirstOrDefault(dictype => dictype.ValueCode == c.VehicleType && dictype.TypeCode == P4Consts.VehicleType).ValueData,
                SpecilType = c.SpecilType,
                BeginDiscountTime = c.BeginDiscountTime,
                EndDiscountTime = c.EndDiscountTime,
                ParkName = Context.Set<Parks.Park>().FirstOrDefault(park => park.Id == c.ParkId).ParkName,
                ParkId = c.ParkId

            }).Orders(input).Filters(input).PageBy(input);
            return new GetAllSpecialListsOutput()
            {
                rows = query.ToList(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
          
        }


        public List<SpecialLists.Dtos.SpecialListsDto> GetAllSpecialCar(int? TenantId, int? parkid)
        {
            //if (AbpSession.TenantId != 1019)
            //{
            //    var parkid = AbpSession.ParkIds[0];
            //    var entry =
            //   _abpSpecialListRepository.GetAllList(entity => entity.ParkId == parkid);
            //    return entry.MapTo<List<Dtos.SpecialListsDto>>();
            //}
            ////景德镇错峰时间段
            //var tenantId = AbpSession.TenantId;
            //var entry1 =
            //    _abpSpecialListRepository.GetAllList(entity => entity.TenantId == tenantId && entity.SpecilType == 2);
            //return entry1.MapTo<List<Dtos.SpecialListsDto>>();
            List<SpecialLists.Dtos.SpecialListsDto> specialList = new List<SpecialLists.Dtos.SpecialListsDto>();
            if (TenantId != 1019)
            {                
                string sqlStr = @"select * from AbpSpecialList where ParkId=@parkid";
                specialList = Context.Database.SqlQuery<SpecialLists.Dtos.SpecialListsDto>(sqlStr, new SqlParameter[] { new SqlParameter("@parkid", parkid) }).ToList();
                return specialList;
            }
            //景德镇错峰时间段
            string sqlstr = @"select * from AbpSpecialList where TenantId=@TenantId and SpecilType = 2";
            specialList = Context.Database.SqlQuery<SpecialLists.Dtos.SpecialListsDto>(sqlstr, new SqlParameter[] { new SqlParameter("@TenantId", TenantId) }).ToList();
            return specialList;

            //var tenantId = AbpSession.TenantId;
            //var entry1 =
            //    _abpSpecialListRepository.GetAllList(entity => entity.TenantId == tenantId && entity.SpecilType == 2);
            //return entry1.MapTo<List<Dtos.SpecialListsDto>>();


        }
    }
}
