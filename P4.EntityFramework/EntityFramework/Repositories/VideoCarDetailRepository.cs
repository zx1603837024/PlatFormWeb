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
using P4.VideoCarStopData;
using P4.VideoCarStopData.Dtos;
using P4.VideoCars;
using System.Net.Http;
using System.Data.SqlClient;
using Abp.Helper;

namespace P4.EntityFramework.Repositories
{
    public class VideoCarDetailRepository : P4RepositoryBase<VideoCarBusinessDetail, long>, IVideoCarDetailRepository
    {
        public VideoCarDetailRepository(IDbContextProvider<P4DbContext> dbContextProvider)
: base(dbContextProvider)
        {

        }

        public VideoCarOutDto GetAllList(VideoCarInputDto input, IAbpSession abpsession)
        {
            int records = 0;

            var QueryTable = Table.WhereIf(abpsession.TenantId.HasValue, entity => entity.TenantId == abpsession.TenantId).WhereIf(!string.IsNullOrWhiteSpace(abpsession.CompanyId.ToString()) && abpsession.CompanyId.ToString() != "0", entity => entity.CompanyId == abpsession.CompanyId);
            if (!string.IsNullOrWhiteSpace(input.filters))
            {
                FilterDto _filterDto = JsonConvert.DeserializeObject(input.filters, typeof(FilterDto)) as FilterDto;
                foreach (var entity in _filterDto.rules)
                {
                    switch (entity.field)
                    {
                        case "ParkName":
                            var tempvalue = int.Parse(entity.data == "" ? "0" : entity.data);
                            if (tempvalue == 0)
                            {
                                break;
                            }
                            QueryTable = QueryTable.Where(entry => entry.ParkId == tempvalue);
                            break;
                        case "BerthsecName":
                            var tempvalue1 = int.Parse(entity.data);
                            QueryTable = QueryTable.Where(entry => entry.BerthsecId == tempvalue1);
                            break;
                        case "BerthNumber":
                            QueryTable = QueryTable.Where(entry => entry.BerthNumber == entity.data);
                            break;
                        case "VedioCarNumber":
                            QueryTable = QueryTable.Where(entry => entry.VedioCarNumber == entity.data);
                            break;
                        default:
                            break;
                    }
                }
            }


            List<GetAllVideoCarDto> query = new List<GetAllVideoCarDto>();

            records = QueryTable.Count();
            query = QueryTable.Select(c => new GetAllVideoCarDto
            {
                Id = c.Id,
                ParkId = c.ParkId.Value,
                BerthsecId = c.BerthsecId.Value,
                ParkName = Context.Set<Park>().FirstOrDefault(entity => entity.Id == c.ParkId).ParkName,
                BerthsecName = Context.Set<Berthsecs.Berthsec>().FirstOrDefault(entity => entity.Id == c.BerthsecId).BerthsecName,
                BerthNumber = c.BerthNumber,
                VideoCarNumber = c.VedioCarNumber,
                PlateNumber = c.PlateNumber,
                Receivable = c.Receivable,
                CarInTime = c.CarInTime,
                CarOutTime = c.CarOutTime,
                StopTime = c.StopTime,
                BeforePathURL = c.BeforePathURL,
                AfterPathURL = c.AfterPathURL,
                VideoTypeName = ((VideoCarType)Context.Set<VideoCar>().FirstOrDefault(x => x.VideoCarNumber == c.VedioCarNumber).VedioEqType).ToString(),
                OutBeforePathURL = c.OutBeforePathURL,
                OutAfterPathURL = c.OutAfterPathURL,
                Speed = c.Speed,
                VideoCarPlate = Context.Set<VideoCar>().FirstOrDefault(x => x.VideoCarNumber == c.VedioCarNumber).VideoCarPlate
            }).Orders(input).PageBy(input).ToList();

            records = query.Count();

            return new VideoCarOutDto()
            {
                rows = query.ToList(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }

        public void UpdatePlate(CreateOrUpdateVideoCarDto input, IAbpSession abpsession)
        {
            //注：巡检车先不推送消息，并更新停车信息，因为巡检车可能优先级较低
            string sql = string.Empty;
            //sql += $"update A set PlateNumber = '{input.PlateNumber}' from AbpBusinessDetail A left join AbpVideoEquipBusinessDetail B on A.[guid]=B.[guid] where A.Id = '{input.id}';";
            //sql += $"update A set RelateNumber = '{input.PlateNumber}' from AbpBerths A left join AbpVideoEquipBusinessDetail B on A.[guid]=B.[guid] where A.Id = '{input.id}';";
            sql += $"update AbpVideoCarBusinessDetail set PlateNumber = '{input.PlateNumber}' where Id = '{input.id}';";

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

            #region 发送微信出场信息给修改后的车主
            //int IdTemp = Convert.ToInt32(input.id);
            //var QueryTable = Table.FirstOrDefault(x => x.Id == IdTemp);
            //string ParkName = Context.Set<Berthsecs.Berthsec>().FirstOrDefault(entity => entity.Id == QueryTable.BerthsecId).BerthsecName;
            //HttpClient client = new HttpClient();
            //string url = "http://www.fxintel.top/weixin_guide/message/SendOutPark?carNumber=" + input.PlateNumber + "&parkName=" + ParkName + "&carOutTime=" + QueryTable.CarOutTime + "&carInTime=" + QueryTable.CarInTime;
            //HttpResponseMessage response = client.GetAsync(url).Result;
            //if (response.IsSuccessStatusCode)
            //{
            //    InsertWeixinPushLog(new WeixinPushModel() { CreationTime = DateTime.Now, PlateNumber = input.PlateNumber, PushContent = url, PushType = "CarOutMsg", TenantId = Convert.ToInt32(abpsession.TenantId) });
            //}
            #endregion
            return;

        }

        private void InsertWeixinPushLog(WeixinPushModel model)
        {
            string sql = "insert into AbpWeixinPushMsg(CreationTime, TenantId, PushType, PushContent, PlateNumber) values(@CreationTime, @TenantId, @PushType, @PushContent, @PlateNumber) SELECT @@IDENTITY as Id";
            SqlParameter[] param = new SqlParameter[] {
                new SqlParameter("@CreationTime", model.CreationTime),
                new SqlParameter("@TenantId", model.TenantId),
                new SqlParameter("@PushType", model.PushType),
                new SqlParameter("@PushContent", model.PushContent),
                new SqlParameter("@PlateNumber", model.PlateNumber)
            };
            SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, System.Data.CommandType.Text, sql, param);
        }
    }
}
