using Abp.Domain.Repositories;
using P4.Authorization;
using P4.Roles.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Authorization.Users;
using Abp.Linq.Extensions;
using Abp.Application.Services;
using P4.Companys;
using System.Data;

namespace P4.Roles
{
    /// <summary>
    /// 角色管理
    /// </summary>
    public class RoleAppService : ApplicationService, IRoleAppService
    {
        #region Var
        private readonly IRepository<Role> _abpRoleRepository;
        private readonly IRepository<UserRole, long> _abpUserRoleRepository;
        private readonly IRepository<OperatorsCompany> _abpCompanyRepository;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="abpRoleRepository"></param>
        /// <param name="abpUserRoleRepository"></param>
        /// <param name="abpCompanyRepository"></param>
        public RoleAppService(IRepository<Role> abpRoleRepository, IRepository<UserRole, long> abpUserRoleRepository, IRepository<OperatorsCompany> abpCompanyRepository)
        {
            _abpRoleRepository = abpRoleRepository;
            _abpUserRoleRepository = abpUserRoleRepository;
            _abpCompanyRepository = abpCompanyRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Abp.Application.Services.Dto.ListResultOutput<RoleDto> GetAll()
        {
            return new Abp.Application.Services.Dto.ListResultOutput<RoleDto>()
            {
                Items = _abpRoleRepository.GetAllList().MapTo<List<RoleDto>>().ToList()
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Role GetRoleByName(String name)
        {
            return _abpRoleRepository.FirstOrDefault(dic => dic.Name == name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Role GetRoleById(int id)
        {
            return _abpRoleRepository.FirstOrDefault(dic => dic.Id == id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllRoleOutput GetAllRoleList(RoleInput input)
        {
            //超级管理员
            if (!AbpSession.TenantId.HasValue)
            {
                var sql = @"
SELECT TotalCount=COUNT(*) OVER (),
	   b.Name AS TenantName,
	   c.CompanyName AS CompanyName,
	   d.Name AS CreatorUserName,
	   e.Name AS LastModifierUserName,
	   a.*
FROM [AbpRoles] a WITH(NOLOCK)
left JOIN dbo.AbpTenants b WITH(NOLOCK)
ON a.TenantId=b.Id
left JOIN dbo.AbpOperatorsCompany c WITH(NOLOCK)
ON a.CompanyId=c.Id
LEFT JOIN dbo.AbpUsers d WITH(NOLOCK)
ON a.CreatorUserId =d.Id
LEFT JOIN dbo.AbpUsers e WITH(NOLOCK)
ON a.LastModifierUserId =e.Id
WHERE a.IsDeleted = 0
";
                //filter暂未实现
                sql += $"ORDER BY a.{input.sidx} {input.sord}  OFFSET {(input.page - 1) * input.rows} ROWS FETCH NEXT {input.rows} ROWS ONLY";
                var dataSet = Abp.Helper.SqlHelper.ExecuteDataset(Abp.Helper.SqlHelper.connectionString, CommandType.Text, sql);
                var table= Abp.DataProcessHelper.GetEntityFromTable<RoleDto_TotalCount>(dataSet.Tables[0]);
                int records = table.Count==0?0 :table.First().TotalCount;

                List<RoleDto> roleDtoList = new List<RoleDto>();
                if (records == 0)
                    return null;
                foreach (RoleDto_TotalCount rd in table)
                {
                    //RoleDto roledto = new RoleDto();
                    //roledto.Id = rd.Id;
                    //roledto.LastModifierUserName = rd.LastModifierUser == null ? null : rd.LastModifierUser.Name;
                    //roledto.TenantName = rd.Tenant == null ? null : rd.Tenant.Name;
                    //roledto.CreatorUserName = rd.CreatorUser == null ? null : rd.CreatorUser.Name;
                    //roledto.Name = rd.Name;
                    //roledto.CompanyName = rd.CompanyId.HasValue == true ? _abpCompanyRepository.Get(rd.CompanyId.Value).CompanyName : "";
                    //roledto.DisplayName = rd.DisplayName;
                    //roledto.IsStatic = rd.IsStatic;
                    //roledto.IsDefault = rd.IsDefault;
                    //roledto.CreationTime = rd.CreationTime;
                    //roledto.LastModificationTime = rd.LastModificationTime;
                    roleDtoList.Add(rd);
                }
                return new GetAllRoleOutput()
                {
                    rows = roleDtoList,
                    records = records,
                    total = records / input.rows + 1
                };
            }
            else
            {
                int records = _abpRoleRepository.GetAll().Filters(input).ToList().Count;

                List<RoleDto> roleDtoList = new List<RoleDto>();

                List<Role> roles = _abpRoleRepository.GetAll().Orders(input).Filters(input).PageBy(input).ToList();
                if (roles == null)
                    return null;
                foreach (Role rd in roles)
                {
                    RoleDto roledto = new RoleDto();
                    roledto.Id = rd.Id;
                    roledto.LastModifierUserName = rd.LastModifierUser == null ? null : rd.LastModifierUser.Name;
                    roledto.TenantName = rd.Tenant == null ? null : rd.Tenant.Name;
                    roledto.CreatorUserName = rd.CreatorUser == null ? null : rd.CreatorUser.Name;
                    roledto.Name = rd.Name;
                    roledto.CompanyName = rd.CompanyId.HasValue == true ? _abpCompanyRepository.Get(rd.CompanyId.Value).CompanyName : "";
                    roledto.DisplayName = rd.DisplayName;
                    roledto.IsStatic = rd.IsStatic;
                    roledto.IsDefault = rd.IsDefault;
                    roledto.CreationTime = rd.CreationTime;
                    roledto.TenantId = (int)rd.TenantId;
                    roledto.LastModificationTime = rd.LastModificationTime;
                    roleDtoList.Add(roledto);
                }
                return new GetAllRoleOutput()
                {
                    rows = roleDtoList,
                    records = records,
                    total = records / input.rows + 1
                };
            }
        }

        /// <summary>
        ///  角色新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public int RoleInsert(CreateOrUpdateRoleInput input)
        {
            if (_abpRoleRepository.FirstOrDefault(dic => dic.Name == input.Name) != default(Role))
            {
                return 1;
            }
            Role entity = new Role();
            entity.TenantId = string.IsNullOrWhiteSpace(input.TenantName) == true ? default(int?) : int.Parse(input.TenantName);
            entity.CompanyId = string.IsNullOrWhiteSpace(input.CompanyName) == true ? default(int?) : int.Parse(input.CompanyName);
            entity.Name = input.Name;
            entity.DisplayName = input.DisplayName;
            entity.IsStatic = input.IsStatic;
            entity.IsDefault = input.IsDefault;
            return _abpRoleRepository.Insert(entity) == null ? 1 : 0;
        }


        /// <summary>
        /// 角色信息更新
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public int RoleUpdate(CreateOrUpdateRoleInput input)
        {
            var entity = _abpRoleRepository.Load(input.Id);
            if (entity.Name != input.Name && _abpRoleRepository.FirstOrDefault(dic => dic.Name == input.Name) != default(Role) || entity.IsStatic == true)
                return 1;
            entity.TenantId = string.IsNullOrWhiteSpace(input.TenantName) == true ? default(int?) : int.Parse(input.TenantName);
            entity.CompanyId = string.IsNullOrWhiteSpace(input.CompanyName) == true ? default(int?) : int.Parse(input.CompanyName);
            entity.Name = input.Name;
            entity.DisplayName = input.DisplayName;
            entity.IsStatic = input.IsStatic;
            entity.IsDefault = input.IsDefault;
            return _abpRoleRepository.Update(entity) == null ? 1 : 0;

        }

        /// <summary>
        /// 如果角色已分配给了用户,返回值为1，不允许删除。未给该角色分配用户，返回值为0
        /// </summary>
        /// <param name="input"></param>
        public int RoleDelete(CreateOrUpdateRoleInput input)
        {
            var entity = _abpRoleRepository.Load(input.Id);
            if (_abpUserRoleRepository.FirstOrDefault(dic => dic.RoleId == input.Id) != default(UserRole) || entity.IsStatic == true)
                return 1;
            _abpRoleRepository.Delete(input.Id);
            return 0;
        }
    }
}
