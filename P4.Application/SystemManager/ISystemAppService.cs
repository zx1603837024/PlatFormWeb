using Abp.Application.Services;
using P4.Berthsecs.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.SystemManager
{
    public interface ISystemAppService : IApplicationService
    {
        BerthsecDto GetBerthsecModel();
    }
}
