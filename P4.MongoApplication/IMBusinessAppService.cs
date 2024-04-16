using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.MongoApplication
{
    public interface IMBusinessAppService : IApplicationService
    {
        void Create();
    }
}
