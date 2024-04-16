using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4
{
    /// <summary>
    /// 
    /// </summary>
    public class P4LogDbContext : Abp.Zero.EntityFramework.AbpZeroDbContext<Tenant, Role, User>
    {

         /// <summary>
        /// 
        /// </summary>
        public P4DbContext() : base("LogDefault")
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nameOrConnectionString"></param>
        public P4DbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {

        }
    }
}
