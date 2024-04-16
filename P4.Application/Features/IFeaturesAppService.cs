using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Features
{
    /// <summary>
    /// 
    /// </summary>
    public interface IFeaturesAppService : IApplicationService
    {
        List<Dtos.FeaturesDto> GetAll();
    }
}
