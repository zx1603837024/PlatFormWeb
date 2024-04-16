using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Dictionarys.Dtos
{

    [AutoMapFrom(typeof(DictionaryValue))]
   public  class GetAllValueDataDto 
   {
       public string ValueCode { get; set; }
       public string ValueData { get; set; }
       public string TypeCode { get; set; }
    } 
}
