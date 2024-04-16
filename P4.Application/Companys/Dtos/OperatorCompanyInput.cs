using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Companys.Dtos
{
   public  class OperatorCompanyInput :EntityRequestInput, IOperDto, IPagedResultRequest, IFilters
   {

       public string oper { get; set; }

       public int page { get; set; }

       public int rows { get; set; }

       /// <summary>
       /// 过滤条件
       /// </summary>
       public string filters { get; set; }


       public bool _search { get; set; }
   }
}
