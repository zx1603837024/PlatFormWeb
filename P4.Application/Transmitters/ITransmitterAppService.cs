using Abp.Application.Services;
using P4.Transmitters.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Transmitters
{
    public interface ITransmitterAppService : IApplicationService
    {
        GetAllTransmitterOutput GetAllTransmitter(TransmitterInput input);
        GetAllTransmitterOutput GetAllManageTransmitter(TransmitterInput input);
        List<TransmitterDto> GetAllTransmitterList();
        TransmitterDto GetTransmitterById(int id);

        string Insert(CreateOrUpdateTransmitterInput input);

        void Delete(int Id);

        string Modify(CreateOrUpdateTransmitterInput input);

        string ModifyManageTransmitter(CreateOrUpdateTransmitterInput input);
             


    }
}
