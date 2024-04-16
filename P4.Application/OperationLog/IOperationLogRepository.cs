using P4.OperationLog.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.OperationLog
{
    public interface IOperationLogRepository
    {
        GetAllOperationLogOutput GetAllOperationLogList(OperationLogInput input);

        /// <summary>
        /// 获取最新记录条数
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        GetAllOperationLogOutput GetOperationLogToTopList(int count);
    }
}
