using Abp.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public class MySettingProvider : SettingProvider
    {
        /// <summary>
        /// 
        /// </summary>
        public const string DefaultRowSize = "DefaultRowsSize";


        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            //throw new NotImplementedException();
            return new[] { 
                new SettingDefinition(DefaultRowSize, "5")
            };
        }
    }
}
