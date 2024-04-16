using Abp.Application.Services;
using Abp.Domain.Repositories;
using P4.SpecialLists.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;

namespace P4.SpecialLists
{
    public class SpecialListAppSevice : ApplicationService, ISpecialListAppSevice
    {
        #region Var
        private readonly IRepository<SpecialList> _abpSpecialListRepository;
        private readonly ISpecialRepository _specialListRepository;
        #endregion

        public SpecialListAppSevice(IRepository<SpecialList> abpSpecialListRepository, ISpecialRepository specialListRepository)
        {
            _abpSpecialListRepository = abpSpecialListRepository;
            _specialListRepository = specialListRepository;

        }


        public Dtos.GetAllSpecialListsOutput GetAllSpecialLists(Dtos.SpecialListInput input)
        {
            return _specialListRepository.GetAllSpecialList(input);
        }

        public string SpecialListsInsert(Dtos.CreateOrUpdateSpecialListsInput input)
        {
            SpecialList entity = new SpecialList();
            if (input.PlateNumber == null)
            {
                return "车牌不能为空！";
            }
            entity.VehicleType = input.VehicleType;
            entity.Discount = input.Discount;
            entity.IsActive = input.IsActive;
            entity.Remarks = input.Remarks;
            entity.CarOwner = input.CarOwner;
            entity.PlateNumber = input.PlateNumber;
            entity.SpecilType = input.SpecilType;
            entity.BeginDiscountTime = input.BeginDiscountTime;
            entity.EndDiscountTime = input.EndDiscountTime;
            entity.ParkId = input.ParkId;

            _abpSpecialListRepository.Insert(entity);
            return null;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string SpecialListsUpdate(Dtos.CreateOrUpdateSpecialListsInput input)
        {
            var entity = _abpSpecialListRepository.Load(input.Id);
            if (_abpSpecialListRepository.FirstOrDefault(dic => dic.PlateNumber == input.PlateNumber && dic.Id != input.Id) != default(SpecialList))
            {
                return input.PlateNumber + "车牌已经存在";
            }
            entity.VehicleType = input.VehicleType;
            entity.Discount = input.Discount;
            entity.IsActive = input.IsActive;
            entity.Remarks = input.Remarks;
            entity.CarOwner = input.CarOwner;
            entity.PlateNumber = input.PlateNumber;
            entity.SpecilType = input.SpecilType;
            entity.BeginDiscountTime = input.BeginDiscountTime;
            entity.EndDiscountTime = input.EndDiscountTime;
            entity.ParkId = input.ParkId;
            _abpSpecialListRepository.Update(entity);
            return null;



        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        public void SpecialListsDelete(Dtos.CreateOrUpdateSpecialListsInput input)
        {
            var entity = _abpSpecialListRepository.Load(input.Id);
            _abpSpecialListRepository.Delete(input.Id);
        }
        /// <summary>
        /// 收费员获取特殊车辆数据
        /// </summary>
        /// <returns></returns>
        public List<Dtos.SpecialListsDto> GetAllSpecialCar()
        {
            //if (AbpSession.TenantId != 1019)
            //{
            //    var parkid = AbpSession.ParkIds[0];
            //    var entry =
            //   _abpSpecialListRepository.GetAllList(entity => entity.ParkId == parkid);
            //    return entry.MapTo<List<Dtos.SpecialListsDto>>();
            //}
            ////景德镇错峰时间段
            //var tenantId = AbpSession.TenantId;
            //var entry1 =
            //    _abpSpecialListRepository.GetAllList(entity => entity.TenantId == tenantId&&entity.SpecilType==2);
            //return entry1.MapTo<List<Dtos.SpecialListsDto>>();
            var tenantId = AbpSession.TenantId;
            var parkid = AbpSession.ParkIds[0];
            return _specialListRepository.GetAllSpecialCar(tenantId, parkid);
        }
    }
}
