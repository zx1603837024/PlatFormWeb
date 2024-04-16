using Abp.Application.Services;
using P4.DataLogs.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.DataLogs
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDataLogsAppService : IApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        GetAllDataLogsOutput GetTopDataLog(int count);

        /// <summary>
        /// 
        /// </summary>
        GetAllDataLogsOutput GetAllDataLogByPage(DataLogInput input);

        /// <summary>
        /// 
        /// </summary>
        GetAllDataLogItemsOutput GetAllDataLogItemById(DataLogItemInput input);

        /// <summary>
        /// 
        /// </summary>
        void DeleteDatalogInfo(long Id);
    }
}
