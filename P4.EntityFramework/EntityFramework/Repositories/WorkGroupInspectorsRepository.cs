using Abp.Domain.Repositories;
using Abp.EntityFramework;
using P4.Companys;
using P4.MultiTenancy;
using System.Collections.Generic;
using System.Linq;
using Abp.Linq.Extensions;
using P4.Berthsecs.Dto;
using P4.Berthsecs;
using P4.Reports;
using P4.WorkGroupInspectors;
using P4.WorkGroupInspectors.Dto;
using P4.Inspectors;
using P4.Inspectors.Dtos;

namespace P4.EntityFramework.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class WorkGroupInspectorsRepository : P4RepositoryBase<WorkGroupsInspectors>, IWorkGroupInspectorsRepository
    {
        #region var
        private readonly IRepository<WorkGroupInspectors.WorkGroupInspectors> _abpworkGroupInspectorsRepository;
        private readonly IRepository<WorkGroupInspectorsBerthsecs> _abpworkGroupBerthsecRepository;
        private readonly IRepository<WorkGroupsInspectors> _abpWorkGroupRepository;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContextProvider"></param>
        /// <param name="abpworkGroupInspectorsRepository"></param>
        /// <param name="abpworkGroupBerthsecRepository"></param>
        /// <param name="abpWorkGroupRepository"></param>
        public WorkGroupInspectorsRepository(IDbContextProvider<P4DbContext> dbContextProvider, IRepository<WorkGroupInspectors.WorkGroupInspectors> abpworkGroupInspectorsRepository,
            IRepository<WorkGroupInspectorsBerthsecs> abpworkGroupBerthsecRepository, IRepository<WorkGroupsInspectors> abpWorkGroupRepository)
            : base(dbContextProvider)
        {
            _abpworkGroupInspectorsRepository = abpworkGroupInspectorsRepository;
            _abpworkGroupBerthsecRepository = abpworkGroupBerthsecRepository;
            _abpWorkGroupRepository = abpWorkGroupRepository;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllWorkGroupInspectorsOutput GetAllWorkGroupList(WorkGroupInspectorsInput input)
        {

            WorkGroupsInspectorsDto workGroupDto = new WorkGroupsInspectorsDto();


            int records = Table.Count();
            return new GetAllWorkGroupInspectorsOutput()
            {
                rows = Table.Select(c => new WorkGroupsInspectorsDto
                {
                    Id = c.Id,
                    WorkGroupName = c.WorkGroupName,
                    TenantId = c.TenantId,
                    TenantName = Context.Set<Tenant>().FirstOrDefault(t => t.Id == c.TenantId).Name,
                    CompanyId = c.CompanyId,
                    CompanyName = Context.Set<OperatorsCompany>().FirstOrDefault(t => t.Id == c.CompanyId).CompanyName,
                    LastModificationTime = c.LastModificationTime,
                    LastModifierUserId = c.LastModifierUserId,
                    LastModifierUserName = c.LastModifierUser.UserName,
                    CreationTime = c.CreationTime,
                    CreatorUserId = c.CreatorUserId,
                    CreatorUserName = c.CreatorUser.UserName,
                    Inspectors = Context.Set<WorkGroupInspectors.WorkGroupInspectors>().Where(t => t.WorkGroupId == c.Id).Select(entity =>
                        new InspectorDto
                        {
                            Id = entity.Inspector.Id,
                            TrueName = entity.Inspector.TrueName + "(" + entity.Inspector.UserName + ")"
                        }).ToList(),
                    Berthsecs = Context.Set<WorkGroupInspectorsBerthsecs>().Where(t => t.WorkGroupId == c.Id).Select(entity =>
                        new BerthsecDto
                        {
                            Id = entity.Berthsec.Id,
                            BerthsecName = entity.Berthsec.BerthsecName
                                //isselected=false
                            }).ToList(),
                    IsActive = c.IsActive

                }).Orders(input).PageBy(input).Filters(input).ToList(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }
        /// <summary>
        /// 获取所有未分配工作组的收费员和泊位段
        /// </summary>
        /// <returns></returns>
        public WorkGroupsInspectorsDto GetWorkGroupList(int workgroupId)
        {

            WorkGroupsInspectorsDto workgroupdto = new WorkGroupsInspectorsDto();
            WorkGroupsInspectors workgroup = _abpWorkGroupRepository.FirstOrDefault(t => t.Id == workgroupId);
            if (workgroup != null)
            {
                workgroupdto.WorkGroupName = workgroup.WorkGroupName;
                workgroupdto.TenantId = workgroup.TenantId;
                workgroupdto.TenantName = Context.Set<Tenant>().FirstOrDefault(t => t.Id == workgroup.TenantId).Name;
                workgroupdto.CompanyId = workgroup.CompanyId;
                workgroupdto.CompanyName = Context.Set<OperatorsCompany>().FirstOrDefault(t => t.Id == workgroup.CompanyId).CompanyName;
                workgroupdto.IsActive = workgroup.IsActive;
            }
            IQueryable<Inspector> employee = Context.Set<Inspector>();
            IQueryable<Berthsec> berthsec = Context.Set<Berthsec>();
            IQueryable<WorkGroupInspectors.WorkGroupInspectors> workGroupEmployee = Context.Set<WorkGroupInspectors.WorkGroupInspectors>();
            IQueryable<WorkGroupInspectorsBerthsecs> workGroupBerthsec = Context.Set<WorkGroupInspectorsBerthsecs>();

            var emquery = from em in employee
                          let entity = workGroupEmployee.FirstOrDefault(entity => entity.InspectorsId == em.Id)
                          where entity == default(WorkGroupInspectors.WorkGroupInspectors)
                          select new InspectorDto
                          {
                              Id = em.Id,
                              TrueName = em.TrueName,
                              UserName = em.UserName
                          };

            List<InspectorDto> workgroupInspector = _abpworkGroupInspectorsRepository.GetAll().Where(t => t.WorkGroupId == workgroupId).Select(entity =>
                              new InspectorDto
                              {
                                  Id = entity.Inspector.Id,
                                  TrueName = entity.Inspector.TrueName,
                                  UserName = entity.Inspector.UserName,
                                  isselected = "selected"
                              }).ToList();
            workgroupdto.Inspectors = emquery.ToList();
            workgroupdto.Inspectors.AddRange(workgroupInspector);

            var bsquery = from bs in berthsec
                          let entity = workGroupBerthsec.FirstOrDefault(entity => entity.BerthsecId == bs.Id)
                          where entity == default(WorkGroupInspectorsBerthsecs)
                          select new BerthsecDto
                          {
                              Id = bs.Id,
                              BerthsecName = bs.BerthsecName
                          };
            List<BerthsecDto> workgroupBerthsec = _abpworkGroupBerthsecRepository.GetAll().Where(t => t.WorkGroupId == workgroupId).Select(entity =>
                           new BerthsecDto
                           {
                               Id = entity.Berthsec.Id,
                               BerthsecName = entity.Berthsec.BerthsecName,
                               isselected = "selected"
                           }).ToList();

            workgroupdto.Berthsecs = bsquery.ToList();
            workgroupdto.Berthsecs.AddRange(workgroupBerthsec);
            return workgroupdto;
        }

        /// <summary>
        /// 工作组报表Echar数据  统计数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<WorkGroupInspectorsEcharsDto> GetWorkGroupReportToList(WorkGroupInspectorsInput input)
        {
            IQueryable<WorkGroupInspectorsEcharsDto> WorkGB = Context.Set<WorkGroupInspectorsEcharsDto>();
            IQueryable<WorkGroupsInspectors> WorkG = Context.Set<WorkGroupsInspectors>();
            IQueryable<BerthsecReport> BerthsecR = Context.Set<BerthsecReport>();
            //IQueryable<WorkGroupEcharsDto> WgbWg=Context.Set<WorkGroupEcharsDto>();
            var WgbWg = from wgb in WorkGB
                        join                  //one
                        wg in WorkG on wgb.WorkGroupId equals wg.Id
                        into wgbwg
                        from r1 in wgbwg.DefaultIfEmpty()
                        select new
                        {
                            BerthsecId = wgb.BerthsecId,
                            WorkGroupId = wgb.WorkGroupId,
                            WorkGroupName = r1.WorkGroupName
                        };
            //var yyy = new
            //{
            //    BerthsecId = 1,
            //    WorkGroupId = 1,
            //    WorkGroupName = "suibian"
            //};
            //List<yyy> a = WgbWg.ToList();
            var berthsecreport = from br in BerthsecR
                                 where br.Time >= input.begindt && br.Time <= input.enddt
                                 group br by br.BerthsecId into g
                                 select new
                                 {
                                     BerthsecId = g.Key,
                                     SumPrepaid = g.Sum(br => br.Prepaid),
                                     SumReceivable = g.Sum(br => br.Receivable),
                                     SumFactReceive = g.Sum(br => br.FactReceive),
                                     SumArrearage = g.Sum(br => br.Arrearage),
                                     SumCash = g.Sum(br => br.Cash),
                                     SumByCard = g.Sum(br => br.FactReceive) - g.Sum(br => br.Cash)
                                 };
            var chen = from a in WgbWg
                       join
                        b in berthsecreport on a.BerthsecId equals b.BerthsecId
                           into ab
                       from r1 in ab.DefaultIfEmpty()
                       select new
                       {
                           BerthsecId = r1.BerthsecId,
                           WorkGroupId = a.WorkGroupId,
                           WorkGroupName = a.WorkGroupName,
                           SumPrepaid = r1.SumPrepaid,
                           SumReceivable = r1.SumReceivable,
                           SumFactReceive = r1.SumFactReceive,
                           SumArrearage = r1.SumArrearage,
                           SumCash = r1.SumCash,
                           SumByCard = r1.SumByCard
                       };

          //  var parks = _abpBerthsecReportRepository.GetAll().Where(ber => ber.Time >= getmoneyinput.BeginTime && ber.Time <= getmoneyinput.EndTime).GroupBy(ber => ber.ParkId).OrderBy(ber => ber.Key).PageBy(input).ToList();
            
            var chendewang = from a in chen
                             orderby a.WorkGroupId
                             group a by new { WorkGroupId = a.WorkGroupId, WorkGroupName = a.WorkGroupName } into g
                             select new WorkGroupInspectorsEcharsDto
                             {
                                 WorkGroupId = g.Key.WorkGroupId,
                                 WorkGroupName = g.Key.WorkGroupName,
                                 WorkGroupPrepaid = g.Sum(a => a.SumPrepaid) == null ? 0 : g.Sum(a => a.SumPrepaid),
                                 WorkGroupReceivable = g.Sum(a => a.SumReceivable) == null ? 0 : g.Sum(a => a.SumReceivable),
                                 WorkGroupFactReceive = g.Sum(a => a.SumFactReceive) == null ? 0 : g.Sum(a => a.SumFactReceive),
                                 WorkGroupArrearage = g.Sum(a => a.SumArrearage) == null ? 0 : g.Sum(a => a.SumArrearage),
                                 WorkGroupCash = g.Sum(a => a.SumCash) == null ? 0 : g.Sum(a => a.SumCash),
                                 WorkGroupByCard = g.Sum(a => a.SumByCard) == null ? 0 : g.Sum(a => a.SumByCard)
                             };
           return chendewang.OrderBy(c => c.WorkGroupId).PageBy(input).ToList();
            //return x;
            //return chendewang.OrderBy(ber => ber.Id).PageBy(input).ToList();
        }

        /// <summary>
        /// 工作组报表JqGrid数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllWorkGroupInspectorsReport GetWorkGroupReportJqGrid(WorkGroupInspectorsInput input)
        {
            IQueryable<WorkGroupInspectorsBerthsecs> WorkGB = Context.Set<WorkGroupInspectorsBerthsecs>();
            IQueryable<WorkGroupsInspectors> WorkG = Context.Set<WorkGroupsInspectors>();
            IQueryable<BerthsecReport> BerthsecR = Context.Set<BerthsecReport>();
            //IQueryable<WorkGroupEcharsDto> WgbWg=Context.Set<WorkGroupEcharsDto>();
            var WgbWg = from wgb in WorkGB
                        join                  //one
                        wg in WorkG on wgb.WorkGroupId equals wg.Id
                        into wgbwg
                        from r1 in wgbwg.DefaultIfEmpty()
                        select new
                        {
                            BerthsecId = wgb.BerthsecId,
                            WorkGroupId = wgb.WorkGroupId,
                            WorkGroupName = r1.WorkGroupName
                        };
            //var yyy = new
            //{
            //    BerthsecId = 1,
            //    WorkGroupId = 1,
            //    WorkGroupName = "suibian"
            //};
            //List<yyy> a = WgbWg.ToList();
            var berthsecreport = from br in BerthsecR
                                 where br.Time >= input.begindt && br.Time <= input.enddt
                                 group br by br.BerthsecId into g
                                 select new
                                 {
                                     BerthsecId = g.Key,
                                     SumPrepaid = g.Sum(br => br.Prepaid),
                                     SumReceivable = g.Sum(br => br.Receivable),
                                     SumFactReceive = g.Sum(br => br.FactReceive),
                                     SumArrearage = g.Sum(br => br.Arrearage),
                                     SumCash = g.Sum(br => br.Cash),
                                     SumByCard = g.Sum(br => br.FactReceive) - g.Sum(br => br.Cash)
                                 };
            var chen = from a in WgbWg
                       join
                        b in berthsecreport on a.BerthsecId equals b.BerthsecId
                           into ab
                       from r1 in ab.DefaultIfEmpty()
                       select new
                       {
                           BerthsecId = r1.BerthsecId,
                           WorkGroupId = a.WorkGroupId,
                           WorkGroupName = a.WorkGroupName,
                           SumPrepaid = r1.SumPrepaid,
                           SumReceivable = r1.SumReceivable,
                           SumFactReceive = r1.SumFactReceive,
                           SumArrearage = r1.SumArrearage,
                           SumCash = r1.SumCash,
                           SumByCard = r1.SumByCard
                       };

            //  var parks = _abpBerthsecReportRepository.GetAll().Where(ber => ber.Time >= getmoneyinput.BeginTime && ber.Time <= getmoneyinput.EndTime).GroupBy(ber => ber.ParkId).OrderBy(ber => ber.Key).PageBy(input).ToList();

            var chendewang = from a in chen
                             orderby a.WorkGroupId
                             group a by new { WorkGroupId = a.WorkGroupId, WorkGroupName = a.WorkGroupName } into g
                             select new WorkGroupInspectorsEcharsDto
                             {
                                 WorkGroupId = g.Key.WorkGroupId,
                                 WorkGroupName = g.Key.WorkGroupName,
                                 WorkGroupPrepaid = g.Sum(a => a.SumPrepaid) == null ? 0 : g.Sum(a => a.SumPrepaid),
                                 WorkGroupReceivable = g.Sum(a => a.SumReceivable) == null ? 0 : g.Sum(a => a.SumReceivable),
                                 WorkGroupFactReceive = g.Sum(a => a.SumFactReceive) == null ? 0 : g.Sum(a => a.SumFactReceive),
                                 WorkGroupArrearage = g.Sum(a => a.SumArrearage) == null ? 0 : g.Sum(a => a.SumArrearage),
                                 WorkGroupCash = g.Sum(a => a.SumCash) == null ? 0 : g.Sum(a => a.SumCash),
                                 WorkGroupByCard = g.Sum(a => a.SumByCard) == null ? 0 : g.Sum(a => a.SumByCard)
                             };
            int records = chendewang.ToList().Count();
            List<WorkGroupInspectorsEcharsDto> sdfd = GetWorkGroupReportToList(input);
           
            return new GetAllWorkGroupInspectorsReport()
            {
                rows=sdfd,
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }


    }
}
