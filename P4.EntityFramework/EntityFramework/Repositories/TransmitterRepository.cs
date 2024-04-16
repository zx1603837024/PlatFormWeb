using Abp.EntityFramework;
using P4.Sensors;
using P4.Transmitters;
using P4.Transmitters.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Linq.Extensions;
using P4.MultiTenancy;

namespace P4.EntityFramework.Repositories
{
    public class TransmitterRepository : P4RepositoryBase<Transmitter>, ITransmitterRepository
    {
        public TransmitterRepository(IDbContextProvider<P4DbContext> dbContextProvider)
          : base(dbContextProvider)
        { 
        }

        /// <summary>
        /// 基站管理列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Transmitters.Dtos.GetAllTransmitterOutput GetAllManageTransmitter(Transmitters.Dtos.TransmitterInput input)
        {
            int records = Table.Filters(input).Count();
            var query = Table.Select(c => new TransmitterDto
            {
                Id = c.Id,
                TransmitterName = c.TransmitterName,
                TransmitterNumber = c.TransmitterNumber,
                VoltageCaution = c.VoltageCaution,
                Address = c.Address,
                CreationTime = c.CreationTime,
                BeatDatetime = c.BeatDatetime,
                Updatetime = c.Updatetime,
                Name = Context.Set<Tenant>().FirstOrDefault(entity => entity.Id == c.TenantId).Name
            }).OrderBy(dic => dic.Id).Filters(input).PageBy(input);
            return new GetAllTransmitterOutput()
            {
                rows = query.ToList(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }
    }
}
