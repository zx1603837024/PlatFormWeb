using Abp.Application.Services;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;

namespace P4.OperationPermissions
{
    public class OperationPermissionsAppService: ApplicationService, IOperationPermissionsAppService
    {
        #region Var
        private readonly IRepository<OperationPermission> _operationPermissionAppService;
        #endregion

        public OperationPermissionsAppService(IRepository<OperationPermission> operationPermissionAppService)
        {
            _operationPermissionAppService = operationPermissionAppService;
        }

        public Dtos.GetOperationPermissionsList GetAllPermissions()
        {

            //var test = _operationPermissionAppService.GetAllList().MapTo<List<OperationPermissions.Dtos.OperationPermissionsDto>>();

            //foreach (var model in test.FindAll(entity => entity.FatherCode == "0"))
            //{
            //    if (_menuAppService.FirstOrDefault(entity => entity.FatherCode == model.Name) == default(Menus.Menu))
            //    {
            //        _operationPermissionAppService.Insert(new OperationPermission() { FatherCode = model.Name, MultiTenancySides = 1, Name = model.Name + ".Insert" });
            //        _operationPermissionAppService.Insert(new OperationPermission() { FatherCode = model.Name, MultiTenancySides = 1, Name = model.Name + ".Update" });
            //        _operationPermissionAppService.Insert(new OperationPermission() { FatherCode = model.Name, MultiTenancySides = 1, Name = model.Name + ".Delete" });
            //        _operationPermissionAppService.Insert(new OperationPermission() { FatherCode = model.Name, MultiTenancySides = 1, Name = model.Name + ".Info" });
            //        _operationPermissionAppService.Insert(new OperationPermission() { FatherCode = model.Name, MultiTenancySides = 1, Name = model.Name + ".Search" });
            //        _operationPermissionAppService.Insert(new OperationPermission() { FatherCode = model.Name, MultiTenancySides = 1, Name = model.Name + ".Refresh" });
            //    }
            //}


            return new Dtos.GetOperationPermissionsList()
            {
                rows = _operationPermissionAppService.GetAll().Select(entity => new OperationPermissions.Dtos.OperationPermissionsDto()
                {
                    Id = entity.Id,
                    Description = entity.Description,
                    FatherCode = entity.FatherCode,
                    IsFunction = entity.IsFunction,
                    IsGrantedByDefault = entity.IsGrantedByDefault,
                    MultiTenancySides = entity.MultiTenancySides,
                    Name = entity.Name
                }).ToList()
            };
        }
    }
}
