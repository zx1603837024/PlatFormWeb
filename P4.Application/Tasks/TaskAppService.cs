using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Linq.Extensions;
using P4.Tasks.Dto;
using Abp.Application.Services;

namespace P4.Tasks
{
    /// <summary>
    /// 
    /// </summary>
    public class TaskAppService : ApplicationService, ITaskAppService
    {
        #region Var
        private readonly IRepository<Task, int> _abpTaskRepository;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="abpTaskRepository"></param>
        public TaskAppService(IRepository<Task, int> abpTaskRepository)
        {
            _abpTaskRepository = abpTaskRepository;
        }
        public Abp.Application.Services.Dto.ListResultOutput<Dto.TaskDto> GetAll()
        {
            return new Abp.Application.Services.Dto.ListResultOutput<Dto.TaskDto>()
            {
                Items = _abpTaskRepository.GetAll().Where(task => task.IsRead == false && task.ReadUserId == AbpSession.UserId.Value).OrderByDescending(p=>p.CreationTime).Take(5).ToList().MapTo<List<Dto.TaskDto>>()
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Dto.GetAllTaskOutput GetAll(Dto.TaskInput input)
        {
            int records = _abpTaskRepository.Count(entity => entity.ReadUserId == AbpSession.UserId.Value);
            return new Dto.GetAllTaskOutput()
            {
                rows = _abpTaskRepository.GetAll().Where(entity => entity.ReadUserId == AbpSession.UserId.Value).OrderBy(p=>p.ReadTime).PageBy(input).ToList().MapTo<List<TaskDto>>(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetAllCount()
        {
            return _abpTaskRepository.Count(task => task.IsRead == false && task.ReadUserId == AbpSession.UserId.Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        public bool ReadTask(List<int> Ids)
        {
            foreach (var v in Ids)
            {
                var entity = _abpTaskRepository.Load(v);
                entity.IsRead = true;
                entity.ReadTime = DateTime.Now;
                _abpTaskRepository.Update(entity);
            }
            return true;
        }
    }
}
