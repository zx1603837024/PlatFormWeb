using Abp.Application.Services;
using P4.EmployeeBatchNoDynamicReport.Dtos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace P4.EmployeeBatchNoDynamicReport
{
    /// <summary>
    /// 
    /// </summary>
    public class EmployeeBatchNoDynamicReportAppSerVice :  ApplicationService, IEmployeeBatchNoDynamicReportAppSerVice
    {
        //private readonly IEmployeeBatchNoReportService _employeeBatchNoReport;

        ///// <summary>
        ///// 构造函数
        ///// </summary>
        ///// <param name="employeeBatchNoReport"></param>
        //public EmployeeBatchNoReportSerVice(IEmployeeBatchNoReportService employeeBatchNoReport)
        //{
        //    _employeeBatchNoReport = employeeBatchNoReport;
        //}

        /// <summary>
        /// 获取收费员明细
        /// </summary>
        /// <param name="BatchNo"></param>
        /// <param name="TenantId"></param>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        public string GetEmployDetail(string BatchNo, int TenantId, int CompanyId)
        {
            EmployeeDetail model = new EmployeeDetail();
            List<SqlParameter> SqlParam = new List<SqlParameter>();
            SqlParam.Add(new SqlParameter("@BatchNo", BatchNo));
            SqlParam.Add(new SqlParameter("@TenantId", TenantId));
            SqlParam.Add(new SqlParameter("@CompanyId", CompanyId));
            SqlParameter[] param = SqlParam.ToArray();
            List<EmployeeDetail> list = Abp.DataProcessHelper.GetEntityFromTable<EmployeeDetail>(Abp.Helper.SqlHelper.ExecuteDataset(Abp.Helper.SqlHelper.connectionString, CommandType.Text, "select AbpEmployeeCheck.BerthsecId,AbpEmployees.TrueName,AbpEmployees.DeviceCode,AbpEmployeeCheck.CheckInTime,AbpEmployeeCheck.CheckOutTime,AbpEmployeeCheck.BatchNo from AbpEmployeeCheck with(nolock)left join AbpEmployees on AbpEmployeeCheck.EmployeeId = AbpEmployees.id and AbpEmployees.IsDeleted = 0 left join AbpBerthsecs as Berthsecs on AbpEmployees.berthsecid = Berthsecs.id where AbpEmployeeCheck.BatchNo = @BatchNo and AbpEmployees.TenantId = @TenantId and AbpEmployees.CompanyId = @CompanyId", param).Tables[0]);
            if (list.Count > 0)
            {
                if(list[0].CheckInTime != null)
                    list[0].strInTime = list[0].CheckInTime.ToString("yyyy-MM-dd HH:mm:ss");
                if (list[0].CheckOutTime > DateTime.MinValue)
                    list[0].strOutTime = list[0].CheckOutTime.ToString("yyyy-MM-dd HH:mm:ss");
                model = list[0];

                foreach (var berthsecId in model.BerthsecId.Split(','))
                {
                    //List<EmployeeDetail> berthsecName = Abp.DataProcessHelper.GetEntityFromTable<EmployeeDetail>(Abp.Helper.SqlHelper.ExecuteDataset(Abp.Helper.SqlHelper.connectionString, CommandType.Text, "select AbpBerthsecs.BerthsecName from AbpBerthsecs with(nolock)").Tables[0]);
                    var berthsecName = Abp.Helper.SqlHelper.ExecuteScalar(Abp.Helper.SqlHelper.connectionString, CommandType.Text, "select AbpBerthsecs.BerthsecName from AbpBerthsecs with(nolock) where Id ="+ berthsecId + "").ToString();
                    model.BerthsecName += berthsecName + ',';       
                    
                }
                model.BerthsecName = model.BerthsecName.Substring(0, model.BerthsecName.Length - 1);
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Serialize(model);
        }
    }
}
