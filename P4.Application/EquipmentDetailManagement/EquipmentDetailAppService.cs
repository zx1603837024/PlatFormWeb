using Abp.Domain.Repositories;
using P4.EquipmentDetailManagement.Dtos;
using P4.EquipmentMaintain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Linq.Extensions;

namespace P4.EquipmentDetailManagement
{
    public class EquipmentDetailAppService:IEquipmentDetailAppService
    {
        /// <summary>
       /// 设备详情
       /// </summary>
       private readonly IRepository<EquipmentDetail> _abpEquipmentDetailRepository;

       public EquipmentDetailAppService(IRepository<EquipmentDetail> abpEquipmentDetailRepository)
       {
           _abpEquipmentDetailRepository = abpEquipmentDetailRepository;
       }

       /// <summary>
       /// 删除设备详情
       /// </summary>
       /// <param name="input"></param>
       public void Delete(CreateOrUpdateEquipmentDetailInput input)
       {
           _abpEquipmentDetailRepository.Delete(input.Id);
       }
        /// <summary>
        /// 查询设备详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
       public GetAllEquipmentDetailOutput GetAllEquipmentDetail(SearchEquipmentDetailInput input)
       {
           var entity = _abpEquipmentDetailRepository.GetAll().Where(entry => entry.EqId == input.EqId);
           int records = entity.Filters(input).ToList().Count;
           return new GetAllEquipmentDetailOutput()
           {
               rows = entity.Orders(input).ToList().MapTo<List<EquipmentDetailDto>>(),
               records = records,
               total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
           };
       }


        /// <summary>
        /// 添加设备详情
        /// </summary>
        /// <param name="input"></param>
       public int Insert(CreateOrUpdateEquipmentDetailInput input)
       {
           if (_abpEquipmentDetailRepository.FirstOrDefault(dic => dic.PartsId == input.PartsId) != default(EquipmentDetail))
               return 1;
           else
           {
               return _abpEquipmentDetailRepository.Insert(input.MapTo<EquipmentDetail>())==null?1:0;
           }
           
       }
        /// <summary>
        /// 修改设备详情
        /// </summary>
        /// <param name="input"></param>
       public int Update(CreateOrUpdateEquipmentDetailInput input)
       {
           if (_abpEquipmentDetailRepository.FirstOrDefault(dic => dic.PartsId == input.PartsId) != default(EquipmentDetail))
               return 1;
           var model = _abpEquipmentDetailRepository.Load(input.Id);
           //model.EqId = input.EqId;
           if (!string.IsNullOrWhiteSpace(input.PartsType))
           {
               model.PartsType = input.PartsType;
           }
           if (!string.IsNullOrWhiteSpace(input.PartsId))
           {
               model.PartsId = input.PartsId;
           }
           return _abpEquipmentDetailRepository.Update(model)==null?1:0;
       }

        /// <summary>
        /// 通过EqId查询PartsId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       public List<EquipmentDetail> GetPartsIdByEqId(string id)
       {
           return  _abpEquipmentDetailRepository.GetAllList(entity => entity.EqId == id);
       }
       

    }
}
