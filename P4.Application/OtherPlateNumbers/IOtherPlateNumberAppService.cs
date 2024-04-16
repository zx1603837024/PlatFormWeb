using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.OtherPlateNumbers
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOtherPlateNumberAppService : IApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Dtos.GetAllOtherPlateNumberOutput GetAll(int Id);
    }
}
