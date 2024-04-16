using Abp.Domain.Repositories;
using P4.Sensors;
using P4.Transmitters.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Transmitters
{
    public interface ITransmitterRepository : IRepository<Transmitter>
    {
        GetAllTransmitterOutput GetAllManageTransmitter(TransmitterInput input);
    }
}
