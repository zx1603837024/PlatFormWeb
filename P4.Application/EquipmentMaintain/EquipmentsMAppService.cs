using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.AutoMapper;
using P4.EquipmentMaintain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using P4.EquipmentDetailManagement.Dtos;

namespace P4.EquipmentMaintain
{
    /// <summary>
    /// 
    /// </summary>
    public class EquipmentsMAppService : IEquipmentsMAppService
    {
        /// <summary>
        /// 设备管理
        /// </summary>
        private readonly IRepository<EquipmentsM> _abpEquipmentsMRepository;
        private readonly IEquipmentsMRepository _equipmentsMRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="abpEquipmentsMRepository"></param>
        /// <param name="equipmentsMRepository"></param>
        public EquipmentsMAppService(IRepository<EquipmentsM> abpEquipmentsMRepository,IEquipmentsMRepository equipmentsMRepository)
        {
            _abpEquipmentsMRepository = abpEquipmentsMRepository;
            _equipmentsMRepository = equipmentsMRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        public void Delete(CreateOrUpdateEquipmentsMInput input)
        {
            _abpEquipmentsMRepository.Delete(input.Id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllEquipmentsMOutput GetAllEquipmentsM(SearchEquipmentsMInput input)
        {
            int records = _abpEquipmentsMRepository.GetAll().Filters(input).ToList().Count;
            return new GetAllEquipmentsMOutput()
            {
                rows = _abpEquipmentsMRepository.GetAll().Orders(input).Filters(input).PageBy(input).ToList().MapTo<List<EquipmentsMDto>>(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }
        /// <summary>
        /// 两表查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllEquipmentsMOutput GetAllEquipmentsMList(SearchEquipmentsMInput input)
        {
            return _equipmentsMRepository.GetAllEquipmentsMList(input);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        public void Insert(CreateOrUpdateEquipmentsMInput input)
        {
            _abpEquipmentsMRepository.Insert(input.MapTo<EquipmentsM>());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        public void Update(CreateOrUpdateEquipmentsMInput input)
        {

            var model = _abpEquipmentsMRepository.Load(input.Id);
            //model.EqId = input.EqId;
            model.EqName = input.EqName;
            model.EqFactory = input.EqFactory;
            model.BatchNum = input.BatchNum;
            model.EqVersion = input.EqVersion;
            _abpEquipmentsMRepository.Update(model);
        }

        /// <summary>
        /// 获取设备详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public EquipmentsMDto GetEquipmentDetailInfo(int Id)
        {
            return _abpEquipmentsMRepository.FirstOrDefault(Id).MapTo<EquipmentsMDto>();
        }






        /// <summary>
        /// 查询Eqld是否存在
        /// </summary>
        /// <param name="eqld"></param>
        /// <returns></returns>
        public string GetEqld(string eqld)
        {
            if (_abpEquipmentsMRepository.FirstOrDefault(entity => entity.EqId == eqld) == null)
            {
                return "true";
            }
            else
            {
                return "false";
            }
        }

        /// <summary>
        /// 通过批次号查询设备编号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<EquipmentsM> GetEqIdByBatchNum(int id)
        {
            return _abpEquipmentsMRepository.GetAllList(entity => entity.BatchNum == id);
        }
    }
}
