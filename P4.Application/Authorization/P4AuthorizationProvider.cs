using Abp.Authorization;
using Abp.Localization;
using P4.OperationPermissions;
using System.Collections.Generic;
using System.Linq;

namespace P4.Authorization
{

    /// <summary>
    /// 
    /// </summary>
    public class P4AuthorizationProvider : AuthorizationProvider
    {
        #region Var
        private readonly IOperationPermissionsAppService _operationPermissionsAppService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationPermissionsAppService"></param>
        public P4AuthorizationProvider(IOperationPermissionsAppService operationPermissionsAppService)
        {
            _operationPermissionsAppService = operationPermissionsAppService;
        }

        #endregion

        /// <summary>
        /// 初始化操作权限
        /// </summary>
        /// <param name="context"></param>
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            var permissions = _operationPermissionsAppService.GetAllPermissions();//操作权限
            foreach (var model in permissions.rows.FindAll(entity => entity.FatherCode == "0"))
            {
                if (permissions.rows.Exists(entity => entity.FatherCode == model.Name))
                {
                    var PermissionModel = context.CreatePermission(model.Name, new FixedLocalizableString(model.Name), isFunction: model.IsFunction,
                        multiTenancySides: (Abp.MultiTenancy.MultiTenancySides)System.Enum.Parse(typeof(Abp.MultiTenancy.MultiTenancySides), model.MultiTenancySides.ToString()),
                        isGrantedByDefault: model.IsGrantedByDefault);
                    CreateChildRenPermissions(PermissionModel, permissions.rows, context);
                    //foreach (var childmodel in permissions.rows.FindAll(entity => entity.FatherCode == model.Name))
                    //{
                    //    PermissionModel.CreateChildPermission(childmodel.Name, new FixedLocalizableString(childmodel.Name), isFunction: childmodel.IsFunction,
                    //        multiTenancySides: (Abp.MultiTenancy.MultiTenancySides)System.Enum.Parse(typeof(Abp.MultiTenancy.MultiTenancySides), model.MultiTenancySides.ToString()),
                    //        isGrantedByDefault: childmodel.IsGrantedByDefault);
                    //}
                }
                else
                {
                    context.CreatePermission(model.Name, new FixedLocalizableString(model.Name), isFunction: model.IsFunction, multiTenancySides: (Abp.MultiTenancy.MultiTenancySides)System.Enum.Parse(typeof(Abp.MultiTenancy.MultiTenancySides), model.MultiTenancySides.ToString()),
                        isGrantedByDefault: model.IsGrantedByDefault);
                }
            }
        }

        private void CreateChildRenPermissions(Permission parent,List<OperationPermissions.Dtos.OperationPermissionsDto> permissionsLst, IPermissionDefinitionContext context)
        {
            var children = permissionsLst.Where(u => u.FatherCode == parent.Name);
            if (children.Any())
            {
                foreach(var childmodel in children)
                {
                    var childPermission= parent.CreateChildPermission(childmodel.Name, new FixedLocalizableString(childmodel.Name), isFunction: childmodel.IsFunction,
                            multiTenancySides: (Abp.MultiTenancy.MultiTenancySides)System.Enum.Parse(typeof(Abp.MultiTenancy.MultiTenancySides), parent.MultiTenancySides.ToString()),
                            isGrantedByDefault: childmodel.IsGrantedByDefault);
                    CreateChildRenPermissions(childPermission, permissionsLst, context);
                }
            }
        }

    }
}