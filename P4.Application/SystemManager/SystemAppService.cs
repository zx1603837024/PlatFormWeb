using Abp.Application.Services;
using P4.Berthsecs.Dto;
using P4.SystemManager.Dtos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.SystemManager
{
    /// <summary>
    /// 系统启动参数下载管理
    /// </summary>
    public class SystemAppService : ApplicationService, ISystemAppService
    {
        public SystemAppService()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public BerthsecDto GetBerthsecModel()
        {
            SqlParameter[] param = new SqlParameter[] { 
                new SqlParameter("@berthsecid", AbpSession.BerthsecIds[0])
            };

            DataSet ds = Abp.Helper.SqlHelper.ExecuteDataset(Abp.Helper.SqlHelper.connectionString, "Pro_DownParameter", param);

            var test = Abp.DataProcessHelper.GetEntityFromTable<DBerthsecDto>(ds.Tables[0]);

            var test1 = Abp.DataProcessHelper.GetEntityFromTable<DBerthDto>(ds.Tables[1]);
            return null;
        }
    }
}
