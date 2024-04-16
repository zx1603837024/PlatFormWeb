using Abp.Domain.Repositories;
using Abp.EntityFramework;
using P4.Dictionarys;
using P4.Tenants;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.EntityFramework.Repositories
{
    public class TenantRepository : P4RepositoryBase<DictionaryType>, ITenantRepository
    {

        #region Var

        private readonly IRepository<DictionaryTypeTemplate> _abpDictionaryTypeTemplateRepository;
        private readonly IRepository<DictionaryValueTemplate> _abpDictionaryValueTemplateRepository;

        private readonly IRepository<DictionaryType> _abpDictionaryTypeRepository;
        private readonly IRepository<DictionaryValue> _abpDictionaryValueRepository;

        #endregion

        public TenantRepository(IDbContextProvider<P4DbContext> dbContextProvider,
            IRepository<DictionaryTypeTemplate> abpDictionaryTypeTemplateRepository,
            IRepository<DictionaryValueTemplate> abpDictionaryValueTemplateRepository,
            IRepository<DictionaryType> abpDictionaryTypeRepository,
            IRepository<DictionaryValue> abpDictionaryValueRepository)
            : base(dbContextProvider)
        {
            _abpDictionaryTypeTemplateRepository = abpDictionaryTypeTemplateRepository;
            _abpDictionaryValueTemplateRepository = abpDictionaryValueTemplateRepository;
            _abpDictionaryTypeRepository = abpDictionaryTypeRepository;
            _abpDictionaryValueRepository = abpDictionaryValueRepository;
        }

        /// <summary>
        /// 插入字典数据模板
        /// </summary>
        /// <param name="TenantId"></param>
        public void InsertDictionaryTemplate(int? TenantId)
        {
            foreach (var model in _abpDictionaryTypeTemplateRepository.GetAll().Where(entry => entry.TenantId == 1).ToList())
            {

                Context.Database.ExecuteSqlCommand("INSERT INTO [dbo].[AbpDictionaryType] ([TypeCode], [TypeValue], [TenantId] , [IsActive] ,[Remark] ,[IsDefault]) VALUES(@TypeCode, @TypeValue, @TenantId, @IsActive, @Remark, @IsDefault)",
                    new SqlParameter[] { 
                        new SqlParameter("@TypeCode", model.TypeCode),
                        new SqlParameter("@TypeValue", model.TypeValue),
                        new SqlParameter("@TenantId", TenantId),
                        new SqlParameter("@IsActive", model.IsActive),
                        new SqlParameter("@Remark", model.Remark),
                        new SqlParameter("@IsDefault", model.IsDefault)
                    });
            }
            foreach (var model in _abpDictionaryValueTemplateRepository.GetAll().Where(entry => entry.TenantId == 1).ToList())
            {
                Context.Database.ExecuteSqlCommand("INSERT INTO [dbo].[AbpDictionaryValue] ([ValueCode] ,[TypeCode] ,[ValueData] ,[TenantId] ,[IsActive] ,[Remark] ,[IsDefault] ,[Order]) VALUES(@ValueCode, @TypeCode, @ValueData, @TenantId, @IsActive, @Remark, @IsDefault, @Order)",
                   new SqlParameter[] { 
                        new SqlParameter("@ValueCode", model.ValueCode),
                        new SqlParameter("@TypeCode", model.TypeCode),
                        new SqlParameter("@TenantId", TenantId),
                        new SqlParameter("@IsActive", model.IsActive),
                        new SqlParameter("@ValueData", model.ValueData),
                        new SqlParameter("@Order", model.Order),
                        new SqlParameter("@Remark", model.Remark),
                        new SqlParameter("@IsDefault", model.IsDefault)
                    });
            }


        }
    }
}
