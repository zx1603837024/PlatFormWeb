using Abp.Modules;
using Abp.MongoDb;
using Abp.Configuration.Startup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Configuration;
using P4.MongoCore;

namespace P4.MongoDB
{
    /// <summary>
    /// 
    /// </summary>
    [DependsOn(typeof(AbpMongoDbModule), typeof(P4CoreModule))]
    public class P4MongoDataModule : AbpModule
    {

        public override void PreInitialize()
        {
            //Configuration.DefaultNameOrConnectionString = "Default";

            //MongoDB数据库
            var mongoDB = Configuration.Modules.AbpMongoDb();

            mongoDB.ConnectionString = ConfigurationManager.ConnectionStrings["MongoDB"].ConnectionString;
            //MongoDB数据库名称
            mongoDB.DatatabaseName = ConfigurationManager.ConnectionStrings["MDBName"].ConnectionString;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
