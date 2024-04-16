using Abp.Application.Editions;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using P4.Editions.Dtos;
using Abp.Application.Features;
using P4.Permissions.Dto;
using Abp.Linq.Extensions;
using Abp.Authorization;
using System.Xml;
using P4.Common;
using P4.Authorization;

namespace P4.Editions
{
    /// <summary>
    /// 版本管理
    /// </summary>
    public class EditionAppService : P4AppServiceBase, IEditionAppService
    {
        #region Var
        private readonly IRepository<Edition> _abpEditionRepository;
        private readonly IRepository<EditionFeatureSetting, long> _abpEditionFeatureSettingRepository;
        private readonly IRepository<Menus.Menu> _abpMenuRepository;
        private readonly IRepository<OperationPermissions.OperationPermission> _abpOperationPermissionRepository;
        private readonly IRepository<PermissionSetting, long> _abpPermissionRepository;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="abpEditionRepository"></param>
        /// <param name="abpMenuRepository"></param>
        /// <param name="abpOperationPermissionRepository"></param>
        /// <param name="abpEditionFeatureSettingRepository"></param>
        /// <param name="abpPermissionRepository"></param>
        public EditionAppService(IRepository<Edition> abpEditionRepository,
                                 IRepository<Menus.Menu> abpMenuRepository,
                                 IRepository<OperationPermissions.OperationPermission> abpOperationPermissionRepository,
                                 IRepository<EditionFeatureSetting, long> abpEditionFeatureSettingRepository,
                                 IRepository<PermissionSetting, long> abpPermissionRepository
                                 )
        {
            _abpEditionRepository = abpEditionRepository;
            _abpMenuRepository = abpMenuRepository;
            _abpOperationPermissionRepository = abpOperationPermissionRepository;
            _abpEditionFeatureSettingRepository = abpEditionFeatureSettingRepository;
            _abpPermissionRepository = abpPermissionRepository;
        }
        /// <summary>
        /// 获取所有版本列表
        /// </summary>
        /// <returns></returns>
        public List<Dtos.EditionDto> GetAll()
        {
            return _abpEditionRepository.GetAll().MapTo<List<EditionDto>>();
        }

        /// <summary>
        /// 基础权限关键词
        /// </summary>
        private List<string> powerKeys = new List<string>() { "Insert", "Update", "Delete", "Info", "Search", "Refresh" };

        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <param name="editionId"></param>
        /// <returns></returns>
        public List<ElTreeDto> GetMenuTreeForEle(int editionId)
        {
            //权限
            var EditionFeature = _abpEditionFeatureSettingRepository.GetAllList(entity => entity.EditionId == editionId);
            //菜单
            var menu = _abpMenuRepository.GetAllList(u => u.IsDeleted == false).OrderBy(u => u.Level).ThenBy(u => u.Order).ToList();
            //权限
            var operation = _abpOperationPermissionRepository.GetAllList(entity => entity.IsFunction == false);
            List<ElTreeDto> result = new List<ElTreeDto>();
            result.AddRange(menu.Where(u => u.Level == 1).Select(u =>
            {
                var allPower = operation.Where(t => t.Name.IndexOf(u.Name + ".") > -1).Select(t => new ElTreeDto_Power { 
                    code=t.Name.Replace(u.Name + ".", ""),
                    desc=t.Description
                }).ToList();
                return new ElTreeDto
                {
                    key = u.Id,
                    code = u.Name,
                    name = (u.LocalizableString),
                    order = u.Order,
                    basePowers = allPower.Where(t=>powerKeys.Contains(t.code)).Select(t=>t.code).ToList(),
                    customPower = allPower.Where(t => !powerKeys.Contains(t.code)).ToList(),
                    pId = u.FatherCode,
                    url = u.Url,
                    children = new List<ElTreeDto>()
                };
            }));
            result.ForEach(e => { FillMenuChildren(e, menu, operation); });
            return result;
        }

        /// <summary>
        /// 填充子级菜单
        /// </summary>
        /// <param name="node"></param>
        /// <param name="menuList"></param>
        /// <param name="operation"></param>
        private void FillMenuChildren(ElTreeDto node, List<Menus.Menu> menuList, List<OperationPermissions.OperationPermission> operation)
        {
            var children = menuList.Where(u => u.FatherCode == node.code);
            if (children.Any())
            {
                node.children = children.Select(u =>
                {
                    var allPower = operation.Where(t => t.Name.IndexOf(u.Name + ".") > -1).Select(t => new ElTreeDto_Power
                    {
                        code = t.Name.Replace(u.Name + ".", ""),
                        desc = t.Description
                    }).ToList();
                    return new ElTreeDto
                    {
                        key = u.Id,
                        code = u.Name,
                        name = (u.LocalizableString),
                        order = u.Order,
                        basePowers = allPower.Where(t => powerKeys.Contains(t.code)).Select(t => t.code).ToList(),
                        customPower = allPower.Where(t => !powerKeys.Contains(t.code)).ToList(),
                        pId = u.FatherCode,
                        url = u.Url,
                        children = new List<ElTreeDto>()
                    };

                }).ToList();
                node.children.ForEach(e =>
                {
                    FillMenuChildren(e, menuList, operation);
                });
            }
        }

        /// <summary>
        /// 保存菜单
        /// </summary>
        /// <param name="input"></param>
        public bool SaveMenu(SaveMenuInput input )
        {
            //新增
            if (input.menuId == 0)
            {
                Menus.Menu parent = null;
                if (input.menuParentCode != "0")
                {
                    parent = _abpMenuRepository.FirstOrDefault(u => u.IsDeleted == false && u.Name == input.menuParentCode);
                    if (parent == null)
                    {
                        throw new Exception($"菜单:{input.menuParentCode}不存在");
                    }
                }
              
               
                var menuFind = _abpMenuRepository.FirstOrDefault(u => u.Name == input.menuCode && u.IsDeleted == false);
                if (menuFind != null)
                {
                    throw new Exception($"菜单:{input.menuCode}已存在，无法新增");
                }
                //插入表AbpMenus
                var menu = new Menus.Menu
                {
                    Name = input.menuCode,
                    FatherCode = input.menuParentCode,
                    Level = (parent?.Level??0) + 1,
                    LocalizableString = input.menuName,
                    Icon = "icon-dashboard",
                    Url = input.menuUrl,
                    RequiresAuthentication = true,
                    RequiredPermissionName = input.menuCode,
                    Order = input.order,
                    CreationTime = DateTime.Now,
                    IsDeleted = false,
                    TenantId = AbpSession.TenantId ?? 0,
                    Discriminator = parent==null? input.menuCode:(parent.Discriminator + "," + input.menuCode),
                    CreatorUserId = AbpSession.UserId,
                    IsActive = true,
                    IsStatic = true,
                    IsFunction = true,
                    MenuType = "Road"
                };
                _abpMenuRepository.Insert(menu);
                //插入表AbpFeatures
                var efInsert = new List<EditionFeatureSetting>
                {
                    new EditionFeatureSetting{
                         CreationTime=DateTime.Now,
                         CreatorUserId=AbpSession.UserId,
                         EditionId=input.editionId,
                         Name=input.menuCode,
                         Value="true"
                    }
                };
                //插入表AbpOperationPermissions
                var opInsert = new List<OperationPermissions.OperationPermission>
                {
                    new OperationPermissions.OperationPermission
                    {
                         Name= $"{input.menuCode}",
                         IsGrantedByDefault=false,
                         Description=$"{input.menuName}",
                         MultiTenancySides=3,
                         FatherCode=input.menuParentCode,
                         IsFunction=true
                    },                   
                };
                foreach(var power in input.menuPowers)
                {
                    efInsert.Add(new EditionFeatureSetting
                    {
                        CreationTime = DateTime.Now,
                        CreatorUserId = AbpSession.UserId,
                        EditionId = input.editionId,
                        Name = $"{input.menuCode}.{power.code}",
                        Value = "true"
                    });
                    opInsert.Add(new OperationPermissions.OperationPermission
                    {
                        Name = $"{input.menuCode}.{power.code}",
                        IsGrantedByDefault = false,
                        Description = power.desc,
                        MultiTenancySides = 3,
                        FatherCode = input.menuCode,
                        IsFunction = false
                    });
                }
                efInsert.ForEach(e =>
                {
                    _abpEditionFeatureSettingRepository.Insert(e);
                });
                opInsert.ForEach(e =>
                {
                    _abpOperationPermissionRepository.Insert(e);
                });
                //XMLHelper xmlHelper = new XMLHelper();
                //xmlHelper.UpdateP4NodeEN(menu.Name, menu.Name);
                //xmlHelper.UpdateP4NodeZh(menu.Name, menu.LocalizableString);
                //GC.Collect();
            }
            //更新
            else
            {
                var menu = _abpMenuRepository.FirstOrDefault(u => u.Id == input.menuId && u.IsDeleted == false);
                if (menu == null)
                {
                    throw new Exception($"菜单:{input.menuId}不存在");
                }
                Menus.Menu parent = null;
                if (input.menuParentCode != "0")
                {
                    parent = _abpMenuRepository.FirstOrDefault(u => u.IsDeleted == false && u.Name == input.menuParentCode);
                    if (parent == null)
                    {
                        throw new Exception($"菜单:{input.menuParentCode}不存在");
                    }
                }
                menu.Url = input.menuUrl??string.Empty;
                menu.LocalizableString = input.menuName;
                menu.FatherCode = input.menuParentCode;
                menu.Level = (parent?.Level ?? 0) + 1;
                menu.Discriminator = parent == null ? menu.Name : (parent.Discriminator + "," + menu.Name);
                //更新菜单
                _abpMenuRepository.Update(menu);
                //功能管理
                var EditionFeature = _abpEditionFeatureSettingRepository.GetAllList(entity => entity.EditionId == input.editionId && entity.Name.Contains(menu.Name+"."));
                //系统操作权限
                var operationPermission = _abpOperationPermissionRepository.GetAllList(entity => entity.Name.Contains(menu.Name+"."));
                var efInsert = new List<EditionFeatureSetting>();
                var opInsert = new List<OperationPermissions.OperationPermission>();
                var efUpdate = new List<EditionFeatureSetting>();
                var opUpdate = new List<OperationPermissions.OperationPermission>();
                foreach (var power in input.menuPowers??new List<ElTreeDto_Power>())
                {
                    var powerkey = $"{menu.Name}.{power.code}";
                    var efFind = EditionFeature.FirstOrDefault(u => u.Name == powerkey);
                    var opFind = operationPermission.FirstOrDefault(u => u.Name == powerkey);
                    if (efFind != null)
                    {
                        efFind.Name = powerkey;
                        efUpdate.Add(efFind);
                        EditionFeature.Remove(efFind);
                    }
                    else
                    {
                        efInsert.Add(new EditionFeatureSetting
                        {
                            CreationTime = DateTime.Now,
                            CreatorUserId = AbpSession.UserId,
                            EditionId = input.editionId,
                            Name = powerkey,
                            Value = "true"
                        });
                    }
                    if (opFind != null)
                    {
                        opFind.Name = powerkey;
                        opFind.Description = power.desc;
                        opUpdate.Add(opFind);
                        operationPermission.Remove(opFind);
                    }
                    else
                    {
                        opInsert.Add(new OperationPermissions.OperationPermission
                        {
                            Name = powerkey,
                            IsGrantedByDefault = false,
                            Description = power.desc,
                            MultiTenancySides = 3,
                            FatherCode = input.menuCode,
                            IsFunction = false
                        });
                    }
                }
                if (EditionFeature.Any())
                {
                    EditionFeature.ForEach(e => {
                        _abpEditionFeatureSettingRepository.Delete(e);
                    });
                }
                if (operationPermission.Any())
                {
                    operationPermission.ForEach(e =>
                    {
                        _abpOperationPermissionRepository.Delete(e);
                    });
                }
                efInsert.ForEach(e =>
                {
                    _abpEditionFeatureSettingRepository.Insert(e);
                });
                opInsert.ForEach(e =>
                {
                    _abpOperationPermissionRepository.Insert(e);
                });
                efUpdate.ForEach(e =>
                {
                    _abpEditionFeatureSettingRepository.Update(e);
                });
                opUpdate.ForEach(e =>
                {
                    _abpOperationPermissionRepository.Update(e);
                });
                //XMLHelper xmlHelper = new XMLHelper();
                //xmlHelper.UpdateP4NodeEN(menu.Name, menu.Name);
                //xmlHelper.UpdateP4NodeZh(menu.Name, menu.LocalizableString);
                //GC.Collect();
            }


            return true;
        }


        /// <summary>
        /// 获取版本权限
        /// </summary>
        /// <param name="editionId"></param>
        /// <returns></returns>
        public string SystemPermission(int editionId)
        {
            var EditionFeature = _abpEditionFeatureSettingRepository.GetAllList(entity => entity.EditionId == editionId);
            var menu = _abpMenuRepository.GetAllList();
            string menumodel = "{6} id: '{0}', pId: '{1}', name: '{2}', checked: {3}, doCheck: {4},open: {5} {7}";
            StringBuilder strs = new StringBuilder();
            foreach (var v in menu)
            {
                strs.AppendFormat(menumodel, v.RequiredPermissionName, v.FatherCode, L(v.Name), EditionFeature.Exists(entry => entry.Name == v.RequiredPermissionName) == true ? "true" : "false", "true", "false", "{", "},");
            }

            var operation = _abpOperationPermissionRepository.GetAllList(entity => entity.IsFunction == false);

            foreach (var v in operation)
            {
                if (menu.Exists(e => e.RequiredPermissionName == v.FatherCode))
                    strs.AppendFormat(menumodel, v.Name, v.FatherCode, v.Name.Contains(".Field") == true ? L(v.Name) : L(v.Name.Split('.')[1]), EditionFeature.Exists(entry => entry.Name == v.Name) == true ? "true" : "false", "true", "false", "{", "},");
            }
            return strs.ToString(0, strs.Length - 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="editionId"></param>
        /// <returns></returns>
        public List<EditionFeatureSetting> GetEditionFeature(int editionId)
        {
            return _abpEditionFeatureSettingRepository.GetAllList(entity => entity.EditionId == editionId);
        }

        /// <summary>
        /// 保存权限需要清除缓存
        /// </summary>
        /// <param name="savetype"></param>
        /// <param name="chooseeditionid"></param>
        /// <param name="nodes"></param>
        /// <returns></returns>
        public bool SaveEditionFeature(string savetype, int chooseeditionid, string nodes)
        {
            nodes = nodes.Replace("checked", "check");
            List<ZTree> zlist = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ZTree>>(nodes);
            StringBuilder sql = new StringBuilder();
            var EditionFeature = _abpEditionFeatureSettingRepository.GetAllList(entity => entity.EditionId == chooseeditionid);
            foreach (var e in EditionFeature)
            {
                if (!zlist.Exists(p => p.id == e.Name))
                {
                    sql.Append(" delete from AbpFeatures where Discriminator = 'EditionFeatureSetting' and name = '" + e.Name + "' and EditionId = " + chooseeditionid);
                }
            }

            foreach (var v in zlist)
            {
                if (!EditionFeature.Exists(p => p.Name == v.id))
                {
                    sql.Append(" insert into AbpFeatures(name, Value, CreationTime, CreatorUserId, EditionId, Discriminator) values('" + v.id + "', 'true', getdate(), " + AbpSession.UserId + ", " + chooseeditionid + ", 'EditionFeatureSetting')");
                }
            }
            if (sql.Length > 0)
                return (Abp.Helper.SqlHelper.ExecuteNonQuery(Abp.Helper.SqlHelper.connectionString, System.Data.CommandType.Text, sql.ToString()) > 0 ? true : false);
            else
                return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string Add(CreateOrUpdateEditionInput input)
        {
            _abpEditionRepository.Insert(input.MapTo<Edition>());
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="editionId"></param>
        /// <returns></returns>
        public string Delete(int editionId)
        {
            _abpEditionRepository.Delete(editionId);
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string Update(CreateOrUpdateEditionInput input)
        {
            _abpEditionRepository.Update(input.MapTo<Edition>());
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllEditionOutput GetAll(SearchEditionInput input)
        {
            int records = _abpEditionRepository.Count();
            return new GetAllEditionOutput()
            {
                rows = _abpEditionRepository.GetAll().OrderByDescending(dic => dic.Id).Filters(input).PageBy(input).ToList().MapTo<List<EditionDto>>(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }
    }
}
