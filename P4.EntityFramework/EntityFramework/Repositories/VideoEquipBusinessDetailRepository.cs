using Abp.EntityFramework;
using P4.Parks;
using P4.SensorBusinessDetails;
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
using P4.VideoEquipBusinessDetails;
using P4.VideoEquips;
using P4.VideoEquipBusinessDetails.Dtos;
using static P4.FXEnum;
using System.Data.SqlClient;
using Abp.Helper;
using System.Net.Http;

namespace P4.EntityFramework.Repositories
{
    public class VideoEquipBusinessDetailRepository : P4RepositoryBase<VideoEquipBusinessDetail, long>, IVideoEquipBusinessDetailRepository
    {
        public VideoEquipBusinessDetailRepository(IDbContextProvider<P4DbContext> dbContextProvider)
    : base(dbContextProvider)
        {

        }


        public GetAllVideoEqBusinessDetailOutput GetAllVideoEqBusinessDetaillist(VideoEquipBusinessDetailInput input, IAbpSession abpsession)
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
                            var tempvalue = int.Parse(entity.data == ""? "0": entity.data);
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
                        case "VedioEqNumber":
                            QueryTable = QueryTable.Where(entry => entry.VedioEqNumber == entity.data);
                            break;
                        default:
                            break;
                    }
                }
            }


            //List<VideoEqBusinessDetailDto> query = new List<VideoEqBusinessDetailDto>();

            records = QueryTable.Count();

            var query = QueryTable.Select(c => new VideoEqBusinessDetailDto
            {
                Id = c.Id,
                ParkId = c.ParkId.Value,
                BerthsecId = c.BerthsecId.Value,
                ParkName = Context.Set<Park>().FirstOrDefault(entity => entity.Id == c.ParkId).ParkName,
                BerthsecName = Context.Set<Berthsecs.Berthsec>().FirstOrDefault(entity => entity.Id == c.BerthsecId).BerthsecName,
                BerthNumber = c.BerthNumber,
                VedioEqNumber = c.VedioEqNumber,
                PlateNumber = c.PlateNumber,
                Receivable = c.Receivable,
                CarInTime = c.CarInTime,
                CarOutTime = c.CarOutTime,
                StopTime = c.StopTime,
                OssPathURL =c.OssPathURL,
                Trust = c.Trust,
                OutOssPathURL = c.OutOssPathURL,
                VideoTypeName = ((VideoEqType)Context.Set<VideoEquip>().FirstOrDefault(x => x.VedioEqNumber == c.VedioEqNumber).VedioEqType).ToString(),
                DetailOssPathURL = c.DetailOssPathURL,
                FixOssPathURL = c.FixOssPathURL,
            }).Where(x=>x.VideoTypeName == VideoEqType.高位摄像.ToString() && x.AuditStatus==1);


            records = query.Count();
            query = query.Orders(input).PageBy(input);

            return new GetAllVideoEqBusinessDetailOutput()
            {
                rows = query.ToList(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }


        public GetAllVideoEqBusinessDetailOutput GetAllVideoPileBusinessDetaillist(VideoEquipBusinessDetailInput input, IAbpSession abpsession)
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
                        case "VedioEqNumber":
                            QueryTable = QueryTable.Where(entry => entry.VedioEqNumber == entity.data);
                            break;
                        default:
                            break;
                    }
                }
            }


            //List<VideoEqBusinessDetailDto> query = new List<VideoEqBusinessDetailDto>();

            records = QueryTable.Count();
            var query = QueryTable.Select(c => new VideoEqBusinessDetailDto
            {
                Id = c.Id,
                ParkId = c.ParkId.Value,
                BerthsecId = c.BerthsecId.Value,
                ParkName = Context.Set<Park>().FirstOrDefault(entity => entity.Id == c.ParkId).ParkName,
                BerthsecName = Context.Set<Berthsecs.Berthsec>().FirstOrDefault(entity => entity.Id == c.BerthsecId).BerthsecName,
                BerthNumber = c.BerthNumber,
                VedioEqNumber = c.VedioEqNumber,
                PlateNumber = c.PlateNumber,
                Receivable = c.Receivable,
                CarInTime = c.CarInTime,
                CarOutTime = c.CarOutTime,
                StopTime = c.StopTime,
                OssPathURL = c.OssPathURL,
                Trust = c.Trust,
                OutOssPathURL = c.OutOssPathURL,
                VideoTypeName = ((VideoEqType)Context.Set<VideoEquip>().FirstOrDefault(x => x.VedioEqNumber == c.VedioEqNumber).VedioEqType).ToString(),
                DetailOssPathURL = c.DetailOssPathURL,
                FixOssPathURL = c.FixOssPathURL,
            }).Where(x => x.VideoTypeName == VideoEqType.无源免布线视频桩.ToString() || x.VideoTypeName == VideoEqType.有线供电视频桩.ToString());
           


            records = query.Count();
            query = query.Orders(input).PageBy(input);

            return new GetAllVideoEqBusinessDetailOutput()
            {
                rows = query.ToList(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }

        public GetAllVideoEqBusinessDetailOutput GetAllVideoRoadRollBusinessDetaillist(VideoEquipBusinessDetailInput input, IAbpSession abpsession)
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
                        case "VedioEqNumber":
                            QueryTable = QueryTable.Where(entry => entry.VedioEqNumber == entity.data);
                            break;
                        default:
                            break;
                    }
                }
            }


            //List<VideoEqBusinessDetailDto> query = new List<VideoEqBusinessDetailDto>();

            records = QueryTable.Count();
            var query = QueryTable.Select(c => new VideoEqBusinessDetailDto
            {
                Id = c.Id,
                ParkId = c.ParkId.Value,
                BerthsecId = c.BerthsecId.Value,
                ParkName = Context.Set<Park>().FirstOrDefault(entity => entity.Id == c.ParkId).ParkName,
                BerthsecName = Context.Set<Berthsecs.Berthsec>().FirstOrDefault(entity => entity.Id == c.BerthsecId).BerthsecName,
                BerthNumber = c.BerthNumber,
                VedioEqNumber = c.VedioEqNumber,
                PlateNumber = c.PlateNumber,
                Receivable = c.Receivable,
                CarInTime = c.CarInTime,
                CarOutTime = c.CarOutTime,
                StopTime = c.StopTime,
                OssPathURL = c.OssPathURL,
                Trust = c.Trust,
                OutOssPathURL = c.OutOssPathURL,
                VideoTypeName = ((VideoEqType)Context.Set<VideoEquip>().FirstOrDefault(x => x.VedioEqNumber == c.VedioEqNumber).VedioEqType).ToString(),
                DetailOssPathURL = c.DetailOssPathURL,
                FixOssPathURL = c.FixOssPathURL,
            }).Where(x => x.VideoTypeName == VideoEqType.路牙机.ToString() );
            records = query.Count();
            query = query.Orders(input).PageBy(input);
            return new GetAllVideoEqBusinessDetailOutput()
            {
                rows = query.ToList(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }



        public void UpdatePlate(CreateOrUpdateVideoEqBusDetail input,IAbpSession abpsession)
        {
            string sql = string.Empty;
            sql += $"update A set PlateNumber = '{input.PlateNumber}' from AbpBusinessDetail A left join AbpVideoEquipBusinessDetail B on A.[guid]=B.[guid] where A.Id = '{input.id}';";
            sql += $"update A set RelateNumber = '{input.PlateNumber}' from AbpBerths A left join AbpVideoEquipBusinessDetail B on A.[guid]=B.[guid] where A.Id = '{input.id}';";
            sql += $"update AbpVideoEquipBusinessDetail set PlateNumber = '{input.PlateNumber}' where Id = '{input.id}';";

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
                return ;
            }
            #endregion

            #region 发送微信出场信息给修改后的车主
            int IdTemp = Convert.ToInt32(input.id);
            var QueryTable = Table.FirstOrDefault(x=> x.Id == IdTemp);
            string ParkName = Context.Set<Berthsecs.Berthsec>().FirstOrDefault(entity => entity.Id == QueryTable.BerthsecId).BerthsecName;
            HttpClient client = new HttpClient();
            string url = "http://www.fxintel.top/weixin_guide/message/SendOutPark?carNumber=" + input.PlateNumber + "&parkName=" + ParkName + "&carOutTime=" + QueryTable.CarOutTime + "&carInTime=" + QueryTable.CarInTime;
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                InsertWeixinPushLog(new WeixinPushModel() { CreationTime = DateTime.Now, PlateNumber = input.PlateNumber, PushContent = url, PushType = "CarOutMsg", TenantId = Convert.ToInt32(abpsession.TenantId) });
            }
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


public class WeixinPushModel
{
    /// <summary>
    /// 车牌号
    /// </summary>
    public string PlateNumber { get; set; }

    /// <summary>
    /// 推送类型
    /// </summary>
    public string PushType { get; set; }

    /// <summary>
    /// 推送内容
    /// </summary>
    public string PushContent { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreationTime { get; set; }

    /// <summary>
    /// 商户代码
    /// </summary>
    public int TenantId { get; set; }
}