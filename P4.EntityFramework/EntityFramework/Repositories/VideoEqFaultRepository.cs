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

namespace P4.EntityFramework.Repositories
{
    public class VideoEqFaultRepository : P4RepositoryBase<VideoEquipFaults, long>, IVideoEqFaultRepository
    {
        public VideoEqFaultRepository(IDbContextProvider<P4DbContext> dbContextProvider)
: base(dbContextProvider)
        {

        }

        public VeqFaultOutput GetAllVideoEqFaultsList(VeqFaultInput input, IAbpSession abpsession)
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
                        case "VideoNumber":
                            QueryTable = QueryTable.Where(entry => entry.VedioEqNumber == entity.data);
                            break;
                        default:
                            break;
                    }
                }
            }


            List<VideoEqFaultDto> query = new List<VideoEqFaultDto>();

            records = QueryTable.Count();
            query = QueryTable.Select(c => new VideoEqFaultDto
            {
                Id = c.Id,
                ParkId = c.ParkId.Value,
                BerthsecId = c.BerthsecId.Value,
                ParkName = Context.Set<Park>().FirstOrDefault(entity => entity.Id == c.ParkId).ParkName,
                BerthsecName = Context.Set<Berthsecs.Berthsec>().FirstOrDefault(entity => entity.Id == c.BerthsecId).BerthsecName,
                BerthNumber = c.BerthNumber,
                VideoNumber = c.VedioEqNumber,
                StatusTime = c.StatusTime,
                OssPathURL = c.OssPathURL,
                Remark = c.Remark,
                VideoTypeName = ((VideoEqType)Context.Set<VideoEquip>().FirstOrDefault(x => x.VedioEqNumber == c.VedioEqNumber).VedioEqType).ToString(),
            }).Orders(input).PageBy(input).ToList();

            query = query.Where(entry => entry.VideoTypeName == VideoEqType.高位摄像.ToString()).ToList();
            records = query.Count();

            return new VeqFaultOutput()
            {
                rows = query.ToList(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }

        public VeqFaultOutput GetAllVideoEqFaultsList1(VeqFaultInput input, IAbpSession abpsession)
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
                        case "VideoNumber":
                            QueryTable = QueryTable.Where(entry => entry.VedioEqNumber == entity.data);
                            break;
                        default:
                            break;
                    }
                }
            }


            List<VideoEqFaultDto> query = new List<VideoEqFaultDto>();

            records = QueryTable.Count();
            query = QueryTable.Select(c => new VideoEqFaultDto
            {
                Id = c.Id,
                ParkId = c.ParkId.Value,
                BerthsecId = c.BerthsecId.Value,
                ParkName = Context.Set<Park>().FirstOrDefault(entity => entity.Id == c.ParkId).ParkName,
                BerthsecName = Context.Set<Berthsecs.Berthsec>().FirstOrDefault(entity => entity.Id == c.BerthsecId).BerthsecName,
                BerthNumber = c.BerthNumber,
                VideoNumber = c.VedioEqNumber,
                StatusTime = c.StatusTime,
                OssPathURL = c.OssPathURL,
                Remark = c.Remark,
                VideoTypeName = ((VideoEqType)Context.Set<VideoEquip>().FirstOrDefault(x => x.VedioEqNumber == c.VedioEqNumber).VedioEqType).ToString(),
            }).Orders(input).PageBy(input).ToList();

            query = query.Where(entry => entry.VideoTypeName == VideoEqType.无源免布线视频桩.ToString() || entry.VideoTypeName == VideoEqType.有线供电视频桩.ToString()).ToList();
            records = query.Count();

            return new VeqFaultOutput()
            {
                rows = query.ToList(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }

        public VeqFaultOutput GetAllVideoEqFaultsList2(VeqFaultInput input, IAbpSession abpsession)
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
                        case "VideoNumber":
                            QueryTable = QueryTable.Where(entry => entry.VedioEqNumber == entity.data);
                            break;
                        default:
                            break;
                    }
                }
            }


            List<VideoEqFaultDto> query = new List<VideoEqFaultDto>();

            records = QueryTable.Count();
            query = QueryTable.Select(c => new VideoEqFaultDto
            {
                Id = c.Id,
                ParkId = c.ParkId.Value,
                BerthsecId = c.BerthsecId.Value,
                ParkName = Context.Set<Park>().FirstOrDefault(entity => entity.Id == c.ParkId).ParkName,
                BerthsecName = Context.Set<Berthsecs.Berthsec>().FirstOrDefault(entity => entity.Id == c.BerthsecId).BerthsecName,
                BerthNumber = c.BerthNumber,
                VideoNumber = c.VedioEqNumber,
                StatusTime = c.StatusTime,
                OssPathURL = c.OssPathURL,
                Remark = c.Remark,
                VideoTypeName = ((VideoEqType)Context.Set<VideoEquip>().FirstOrDefault(x => x.VedioEqNumber == c.VedioEqNumber).VedioEqType).ToString(),
            }).Orders(input).PageBy(input).ToList();

            query = query.Where(entry => entry.VideoTypeName == VideoEqType.路牙机.ToString()).ToList();
            records = query.Count();

            return new VeqFaultOutput()
            {
                rows = query.ToList(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }
    }
}
