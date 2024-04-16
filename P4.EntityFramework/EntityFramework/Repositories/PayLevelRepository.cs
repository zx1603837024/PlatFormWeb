using Abp.EntityFramework;
using P4.Parks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using Abp.AutoMapper;
using Abp.Application.Services.Dto;
using Newtonsoft.Json;
using P4.VideoEquips;
using P4.VideoEquipFaultsNs;
using P4.VideoEquipFaultsNs.Dtos;
using static P4.FXEnum;
using P4.PayLevelSettingsNs;
using P4.PayLevelSetting;
using P4.PayLevelSettingsNs.Dtos;
using System.Data.SqlClient;
using Abp.Helper;
using Abp.Domain.Repositories;

namespace P4.EntityFramework.Repositories
{
    public class PayLevelRepository : P4RepositoryBase<PayLevelSettings, int>, IPayLevelRepository
    {
        private readonly IRepository<PayLevelSettings, int> _VideoFaultrepository;
        public PayLevelRepository(IDbContextProvider<P4DbContext> dbContextProvider, IRepository<PayLevelSettings, int> a) : base(dbContextProvider)
        {
            _VideoFaultrepository = a;
        }

        public PayLevelOutput GetAllVideoEqFaultsList(PayLevelInput input, IAbpSession abpsession)
        {
            int records = 0;

            var QueryTable = Table.WhereIf(abpsession.TenantId.HasValue, entity => entity.TenantId == abpsession.TenantId);
            if (!string.IsNullOrWhiteSpace(input.filters))
            {
                FilterDto _filterDto = JsonConvert.DeserializeObject(input.filters, typeof(FilterDto)) as FilterDto;
                foreach (var entity in _filterDto.rules)
                {
                    switch (entity.field)
                    {
                        case "DeviceName":
                            QueryTable = QueryTable.Where(entry => entry.DeviceName == entity.data);
                            break;
                        case "DeviceOrder":
                            var tempvalue1 = int.Parse(entity.data);
                            QueryTable = QueryTable.Where(entry => entry.DeviceOrder == tempvalue1);
                            break;
                        case "LastUser":
                            QueryTable = QueryTable.Where(entry => entry.LastUser == entity.data);
                            break;
                        default:
                            break;
                    }
                }
            }


            List<PayLevelSettingsDto> query = new List<PayLevelSettingsDto>();

            records = QueryTable.Count();
            query = QueryTable.Select(c => new PayLevelSettingsDto
            {
                Id = c.Id,
                DeviceOrder = c.DeviceOrder,
                DeviceType = c.DeviceType,
                LastUpdateTime=c.LastUpdateTime,
                LastUser = c.LastUser,
                DeviceName = c.DeviceName,
            }).Orders(input).PageBy(input).ToList();

            records = query.Count();

            return new PayLevelOutput()
            {
                rows = query.ToList(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }

        public void Update1(CreateOrUpdatePayLevel input, IAbpSession abpsession)
        {
            string sql = string.Empty;
           
            sql += $"update AbpPayLevelSettings set DeviceOrder = '{input.DeviceOrder}' where Id = '{input.Id}';";

            #region 事务提交
            SqlConnection conn = new SqlConnection(SqlHelper.connectionString);
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.Transaction = tran;
            try
            {
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                tran.Commit();
                conn.Close();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                return;
            }
            #endregion

            
            return;

        }
        public void Add1(CreateOrUpdatePayLevel input, IAbpSession abpsession)
        {

            PayLevelSettings entity = new PayLevelSettings();
            entity.DeviceName = getName(input.DeviceName);
            entity.DeviceOrder = input.DeviceOrder;
            entity.IsDelete = 0;
            entity.DeviceType = 1;
            entity.LastUser = abpsession.UserName;
            entity.LastUpdateTime = DateTime.Now;
            entity.TenantId = abpsession.TenantId;
            _VideoFaultrepository.Insert(entity);
            return;

        }

        public string getName(string type)
        {
            if (type == "1") 
            {
                return "手持机";
            }
            else if (type == "2")
            {
                return "地磁";
            }
            else if (type == "3")
            {
                return "高位摄像";
            }
            else if (type == "4")
            {
                return "视频桩";
            }
            else if (type == "5")
            {
                return "路牙机";
            }
            else 
            {
                return "巡检车";
            }
        }
    }
}
