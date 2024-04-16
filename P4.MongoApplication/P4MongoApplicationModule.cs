using Abp.Modules;
using P4.MongoCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace P4.MongoApplication
{
    [DependsOn(typeof(P4CoreModule))]
    public class P4MongoApplicationModule : AbpModule
    {

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
