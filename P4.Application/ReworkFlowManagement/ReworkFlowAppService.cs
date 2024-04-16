using Abp.Domain.Repositories;
using P4.CompanyCustomerExpressManagement.Dto;
using P4.EquipmentMaintain;
using P4.ReworkFlowManagement.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using P4.EquipmentDetailManagement.Dtos;
using Abp.Linq.Extensions;
using P4.CompanyCustomerExpressManagement;

namespace P4.ReworkFlowManagement
{
    public class ReworkFlowAppService : IReworkFlowAppService
    {
        private readonly IRepository<ReworkFlow> _abpReworkFlowRepository;
        private readonly IRepository<CompanyCustomerExpress> _abpCompanyCustomerExpressRepository;
        private readonly IRepository<EquipmentsM> _abpEquipmentsMRepository;
        private readonly IRepository<ReworkPicture> _reworkPictureRepository;
        private readonly IRepository<CompanyFactoryExpress> _abpCompanyFactoryExpressRepository;
        /// <summary>
        /// 构造函数
        /// </summary>
        public ReworkFlowAppService(IRepository<ReworkFlow> abpReworkFlowRepository, IRepository<CompanyCustomerExpress> abpCompanyCustomerExpressRepository, IRepository<EquipmentsM> abpEquipmentsMRepository, IRepository<ReworkPicture> reworkPictureRepository, IRepository<CompanyFactoryExpress> abpCompanyFactoryExpressRepository)
        {
            _abpReworkFlowRepository = abpReworkFlowRepository;
            _abpCompanyCustomerExpressRepository = abpCompanyCustomerExpressRepository;
            _abpEquipmentsMRepository = abpEquipmentsMRepository;
            _reworkPictureRepository = reworkPictureRepository;
            _abpCompanyFactoryExpressRepository = abpCompanyFactoryExpressRepository;
        }


        /// <summary>
        /// 添加返修流程表
        /// </summary>
        /// <param name="input"></param>
        /// <param name="expressinput"></param>
        /// <returns></returns>
        public int Insert(CreateOrUpdateReworkFlowInput input)
        {
            //判断批次号在公司客户快递表中重复(该批次已发货)
            if (_abpCompanyCustomerExpressRepository.FirstOrDefault(dic => dic.BatchNum == input.BatchNum) != default(CompanyCustomerExpress))
                return 1;
            //判断EqId是否存在
            else if (_abpEquipmentsMRepository.FirstOrDefault(dic => dic.EqId == input.EqId) == default(EquipmentsM))
                return 2;
            else
            {
                _abpReworkFlowRepository.Insert(input.MapTo<ReworkFlow>());
                //_abpCompanyCustomerExpressRepository.Insert(expressinput.MapTo<CompanyCustomerExpress>());
                return 0;
            }
        }

        /// <summary>
        /// 判断图片是否已经上传
        /// </summary>
        /// <param name="reworkPictureId"></param>
        /// <returns>true:存在，false:不存在</returns>
        public bool GetReworkPicture(int reworkPictureId)
        {
            if (_reworkPictureRepository.Count(entity => entity.ReworkPictureId == reworkPictureId ) > 0)
                return true;
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public void InsertPicture(ReworkPictureDto model)
        {
            ReworkPicture entity = new ReworkPicture();
            entity.ReworkPictureId = model.ReworkPictureId;
            entity.Image = model.Image;
            _reworkPictureRepository.Insert(entity);
        }

        public int CreatePicture(ReworkPictureDto entity)
        {
            Abp.Helper.MongoDbHelper.InsertOne<ReworkPictureDto>("ReworkPictureDto", entity);
            return 1;
        }

        /// <summary>
        /// 查询返修图片
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<ReworkPictureDto> GetPictureList(int id)
        {
            return _reworkPictureRepository.GetAll().Where(entity => entity.ReworkPictureId == id).ToList().MapTo<List<ReworkPictureDto>>();
        }

        public ReworkPictureDto GetPicture(int id)
        {
            ReworkPictureDto entity = new ReworkPictureDto();
            var model = _reworkPictureRepository.FirstOrDefault(entry => entry.ReworkPictureId == id);
            if (model != default(ReworkPicture))
            {
                entity.Image = model.Image;
            }
            return entity;
        }

        /// <summary>
        /// 查询返修流程表详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetAllReworkFlowOutput GetReworkFlowList(SearchReworkFlowInput input)
        {
            int records = _abpReworkFlowRepository.GetAll().Filters(input).ToList().Count;
            return new GetAllReworkFlowOutput()
            {
                rows = _abpReworkFlowRepository.GetAll().Orders(input).Filters(input).PageBy(input).ToList().MapTo<List<ReworkFlowDto>>(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };
        }

        /// <summary>
        /// 通过id查询EqId，PartsId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ReworkFlowDto GetReworkFlowById(int id)
        {
            ReworkFlow reworkflow = _abpReworkFlowRepository.FirstOrDefault(dic => dic.Id == id);
            ReworkFlowDto reworkflowdto = new ReworkFlowDto();
            reworkflowdto.Id = reworkflow.Id;
            reworkflowdto.EqId = reworkflow.EqId;
            reworkflowdto.PartsId = reworkflow.PartsId;
            reworkflowdto.MaintainDescription = reworkflow.MaintainDescription;
            return reworkflowdto;
        }

        /// <summary>
        /// 添加维修描述
        /// </summary>
        /// <param name="input"></param>
        public int UpdateMaintainDescription(CreateOrUpdateReworkFlowInput input)
        {
            ReworkFlow reworkflow = _abpReworkFlowRepository.Load(input.Id);
            if (_abpCompanyFactoryExpressRepository.FirstOrDefault(dic => dic.BatchNum == reworkflow.BatchNum) != default(CompanyFactoryExpress))
            {
                if (_abpCompanyFactoryExpressRepository.FirstOrDefault(dic => dic.BatchNum == reworkflow.BatchNum).CompanyFactoryExpressState == "工厂已收货" && _abpCompanyFactoryExpressRepository.GetAllList(dic => dic.BatchNum == reworkflow.BatchNum).Count() <= 1)
                {
                    reworkflow.MaintainDescription = input.MaintainDescription;
                    reworkflow.MaintainState = input.MaintainState;
                    return 0;
                }
                //工厂已将返修设备发回公司后，不能更改维修描述
                else if (_abpCompanyFactoryExpressRepository.GetAllList(dic => dic.BatchNum == reworkflow.BatchNum).Count() > 1)
                {
                    return 2;
                }
                return 1;
            }
            else
            {
                //该批次还未到达工厂，不能添加维修描述
                return 1;
            }
        }


    }
}
