using Abp.Application.Services;
using P4.Inducibles.Dtos;

using P4.Sensors.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Inducibles
{
    public interface IInducibleAppService : IApplicationService
    {
        GetAllInducibleListOutput GetAllInducibleList(SearchInducibleInput input);

        int Update(InsertOrUpdateInducibleInput input);
        List<Dtos.InducibleDto> GetAllInducible();

        int SaveInducible(Dtos.InducibleDto inducibleDto);

        List<SensorDto> GetEmplyBerthsecs();

        List<SensorToParkDto> GetParkToSensor();

        InducibleToAD UpdateADStutas(string equipmentId);
    }
}
