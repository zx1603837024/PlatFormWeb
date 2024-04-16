using Abp.Application.Services.Dto;
using Newtonsoft.Json;
using P4.Berthsecs.Dto;
using P4.Signo.Dtos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Signo
{
    /// <summary>
    /// 
    /// </summary>
    public class SignoParkInformationAppService : P4AppServiceBase, ISignoParkInformationAppService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetIllegalOpenOutput GetIllegalOpenOutputList(SearchBerthsecInput input)
        {
            string sqlwhere = "";
            if (AbpSession.TenantId.HasValue)
                sqlwhere += " and AbpSignoParkInformations.TenantId = " + AbpSession.TenantId.Value;
            if (AbpSession.CompanyId.HasValue)
                sqlwhere += " and AbpSignoParkInformations.CompanyId = " + AbpSession.CompanyId.Value;
            #region filter
            if (!string.IsNullOrWhiteSpace(input.filters))
            {
                FilterDto _filterDto = JsonConvert.DeserializeObject(input.filters, typeof(FilterDto)) as FilterDto;
                foreach (var entity in _filterDto.rules)
                {
                    switch (entity.field)
                    {
                        case "BerthsecName":
                            sqlwhere += " and AbpSignoParkInformations.BerthsecId = " + entity.data;
                            break;

                        case "SignoDeviceNo":
                            if(entity.op == "cn")//包含
                            {
                                sqlwhere += " and AbpIllegalOpenSignoPicture.SignoDeviceNo like '%" + entity.data + "%'";
                            }else if(entity.op == "eq")
                            {
                                sqlwhere += " and AbpIllegalOpenSignoPicture.SignoDeviceNo = '" + entity.data + "'";
                            }
                            else if(entity.op == "ne")
                            {
                                sqlwhere += " and AbpIllegalOpenSignoPicture.SignoDeviceNo <> '" + entity.data + "'";
                            }
                            break;
                        case "PhotoTime":
                            if (entity.op == "le")//小于
                            {
                                sqlwhere += " and PhotoTime <= '" + entity.data + "'";
                            }
                            else if (entity.op == "ge")
                            {//大于
                                sqlwhere += " and PhotoTime >= '" + entity.data + "'";
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            #endregion
            string sqlother = "select AbpIllegalOpenSignoPicture.Id,BusinessDetailId,PhotoTime,AbpIllegalOpenSignoPicture.SignoDeviceNo,BusinessDetailPictureUrl,ParkDoorName,TrueName as UpLoadUserName,BerthsecName from AbpIllegalOpenSignoPicture with(nolock) left join AbpSignoParkInformations on AbpIllegalOpenSignoPicture.SignoDeviceNo = AbpSignoParkInformations.SignoDeviceNo left join AbpEmployees on AbpIllegalOpenSignoPicture.CreatorUserId = AbpEmployees.Id left join AbpBerthsecs on AbpSignoParkInformations.BerthsecId = AbpBerthsecs.Id where 1 = 1 " + sqlwhere + "";
            List<SqlParameter> SqlParam = new List<SqlParameter>();
            SqlParam.Add(new SqlParameter("@BeginSize", input.page * input.rows - input.rows + 1));
            SqlParam.Add(new SqlParameter("@EndSize", input.page * input.rows));
            var result = Abp.DataProcessHelper.GetEntityFromTable<IllegalOpenSignoDto>(Abp.Helper.SqlHelper.ExecuteDataset(Abp.Helper.SqlHelper.connectionString, CommandType.Text, sqlother, SqlParam.ToArray()).Tables[0]);
            int records = result.Count;
            return new GetIllegalOpenOutput
            {
                rows = result,
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }
    }
}
