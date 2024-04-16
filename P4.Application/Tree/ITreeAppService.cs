using Abp.Application.Services;
using P4.DataPermissions;
using P4.Permissions.Dto;
using System.Collections.Generic;

namespace P4.Tree
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITreeAppService : IApplicationService
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        string OperPermission(string id,string type);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        string DataPermission(string id,string type);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zlist"></param>
        /// <returns></returns>
        DataPermission AnalyzeDataPermission(List<ZTree> zlist);
    }
}
