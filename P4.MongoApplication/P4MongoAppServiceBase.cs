using Abp.Application.Services;
using P4.MongoCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.MongoApplication
{
    public class P4MongoAppServiceBase : ApplicationService
    {
        protected P4MongoAppServiceBase()
        {
            LocalizationSourceName = P4Consts.LocalizationSourceName;
        }

    }
}
