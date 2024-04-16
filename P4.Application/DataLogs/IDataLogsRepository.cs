using P4.DataLogs.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.DataLogs
{
    public interface IDataLogsRepository
    {
        GetAllDataLogsOutput GetTopDataLog(int count);

        GetAllDataLogsOutput GetAllDataLogByPage(DataLogInput input);
    }
}
