using Abp.Domain.Repositories;
using P4.Dictionarys.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Dictionarys
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDictionaryRepository : IRepository<DictionaryValue>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Dtos.GetAllDictionaryValueOutput GetDictionaryAll(DictionaryValueInput input);
    }
}
