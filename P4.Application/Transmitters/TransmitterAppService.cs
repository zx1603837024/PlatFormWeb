using Abp.Application.Services;
using Abp.Domain.Repositories;
using P4.Sensors;
using P4.Transmitters.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Linq.Extensions;
using Abp.AutoMapper;
using Abp.Domain.Uow;
namespace P4.Transmitters
{
    /// <summary>
    /// 基站
    /// </summary>
    public class TransmitterAppService : ApplicationService, ITransmitterAppService
    {
        #region Var
        private readonly IRepository<Transmitter> _abpTransmitterRepository;
        private readonly ITransmitterRepository  _transmitterRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        #endregion
        public TransmitterAppService(IRepository<Transmitter> abpTransmitterRepository, ITransmitterRepository transmitterRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _abpTransmitterRepository = abpTransmitterRepository;
            _transmitterRepository = transmitterRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }
        public GetAllTransmitterOutput GetAllTransmitter(TransmitterInput input)
        {
            //return _abpTransmitterRepository.GetAllList().MapTo<List<TransmitterDto>>().ToList();
            var records = _abpTransmitterRepository.GetAll().Filters(input).Count();
            return new GetAllTransmitterOutput()
            {
                rows = _abpTransmitterRepository.GetAll().Filters(input).Orders(input).PageBy(input).ToList().MapTo<List<TransmitterDto>>(),
                records = records,
                total = records / input.rows + (records % input.rows != 0 ? 1 : 0)
            };

        }
        public List<TransmitterDto> GetAllTransmitterList()
        {
            //return _abpTransmitterRepository.GetAllList().MapTo<List<TransmitterDto>>().ToList();

            return _abpTransmitterRepository.GetAll().MapTo<List<TransmitterDto>>().ToList();
               

        }
        public TransmitterDto GetTransmitterById(int id)
        {

            return _abpTransmitterRepository.Get(id).MapTo<TransmitterDto>();
        }

        public string Insert(CreateOrUpdateTransmitterInput input)
        {
            if (_abpTransmitterRepository.FirstOrDefault(dic => dic.TransmitterNumber == input.TransmitterNumber) != default(Transmitter))
            {
                return input.TransmitterNumber + "已经存在!";
            }
            if (_abpTransmitterRepository.FirstOrDefault(dic => dic.TransmitterName == input.TransmitterName) != default(Transmitter))
            {
                return input.TransmitterName + "已经存在!";
            }
            if (input.TransmitterNumber == null)
            {
                return "区域不能为空！";
            }
            Transmitter entity = new Transmitter();
            entity.TransmitterNumber = input.TransmitterNumber;
            entity.TransmitterName = input.TransmitterName;
            entity.VoltageCaution = input.VoltageCaution;
            entity.X = input.X;
            entity.Y = input.Y;
            entity.Address = input.Address;

            _abpTransmitterRepository.Insert(entity);
            return null;
        }

        public void Delete(int Id)
        {
            _abpTransmitterRepository.Delete(Id);
        }

        public string Modify(CreateOrUpdateTransmitterInput input)
        {
            var entity = _abpTransmitterRepository.Load(input.Id);
            if (_abpTransmitterRepository.FirstOrDefault(dic => dic.TransmitterNumber == input.TransmitterNumber) == default(Transmitter))
            {
                return input.TransmitterNumber + "不存在!";
            }
            if (input.TransmitterName!=entity.TransmitterName && _abpTransmitterRepository.FirstOrDefault(dic => dic.TransmitterName == input.TransmitterName) != default(Transmitter))
            {
                return input.TransmitterName + "重名!";
            }
            if (input.TransmitterNumber == null)
            {
                return "基站编号不能为空！";
            }

            entity.TransmitterNumber = input.TransmitterNumber;
            entity.TransmitterName = input.TransmitterName;
            entity.VoltageCaution = input.VoltageCaution;
            entity.X = input.X;
            entity.Y = input.Y;
            entity.Address = input.Address;


            _abpTransmitterRepository.Update(entity);
            return null;
        }

   

        public GetAllTransmitterOutput GetAllManageTransmitter(TransmitterInput input)
        {
            if (!AbpSession.TenantId.HasValue)
                _unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant);

            return _transmitterRepository.GetAllManageTransmitter(input);
        }
        

        public string ModifyManageTransmitter(CreateOrUpdateTransmitterInput input)
        {
            if (!AbpSession.TenantId.HasValue)
                _unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant);
            var entity = _abpTransmitterRepository.Load(input.Id);
            entity.TenantId =int.Parse( input.Name);
            _transmitterRepository.Update(entity);
            return null;

        }
    }
}
