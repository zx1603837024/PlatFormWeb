using Abp.Application.Services;
using P4.Berthsecs.Dto;
using P4.Signo.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Signo
{
    /// <summary>
    /// 
    /// </summary>
     public interface ISignoParkInformationAppService: IApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        GetIllegalOpenOutput GetIllegalOpenOutputList(SearchBerthsecInput input);
    }
}
