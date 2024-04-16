using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;

using P4.Sensors;
using P4.Sensors.Dtos;
using Abp;


namespace P4.Inducibles
{
    /// <summary>
    /// 诱导接口管理
    /// </summary>
    public class InducibleAppService : P4AppServiceBase, IInducibleAppService
    {
        #region Var

        private readonly IRepository<Inducible> _inducibleRepository;
        private readonly IRepository<InducibleToPark> _inducibleToParkRepository;
        private readonly IRepository<InducibleToAD> _inducibleToAdRepository;
        private readonly IRepository<Sensor> _sensorRepository;

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inducibleRepository"></param>
        /// <param name="inducibleToParkRepository"></param>
        /// <param name="sensorRepository"></param>
        /// <param name="inducibleToAdRepository"></param>
        public InducibleAppService(IRepository<Inducible> inducibleRepository,
            IRepository<InducibleToPark> inducibleToParkRepository,
            IRepository<Sensor> sensorRepository, IRepository<InducibleToAD> inducibleToAdRepository)
        {
            _inducibleRepository = inducibleRepository;
            _inducibleToParkRepository = inducibleToParkRepository;
            _inducibleToAdRepository = inducibleToAdRepository;
            _sensorRepository = sensorRepository;
        }

        /// <summary>
        /// 获取诱导数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Dtos.GetAllInducibleListOutput GetAllInducibleList(Dtos.SearchInducibleInput input)
        {
            var query = _inducibleRepository.GetAll();
            if (input.IsActive)
                query = query.Where(entity => entity.IsActive == true);
            if (input.InducibleType != null)
                query = query.Where(entity => entity.InducibleType == input.InducibleType);
            return new Dtos.GetAllInducibleListOutput()
            {
                rows = query.MapTo<List<Dtos.InducibleDto>>()
            };
        }

        /// <summary>
        /// 获取诱导数据（自动过滤数据权限）
        /// </summary>
        /// <returns></returns>
        public List<Dtos.InducibleDto> GetAllInducible()
        {
            return _inducibleRepository.GetAll().MapTo<List<Dtos.InducibleDto>>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Dtos.InducibleToADDto> GetAllInducibleToAd()
        {
            return _inducibleToAdRepository.GetAll().MapTo<List<Dtos.InducibleToADDto>>();
        }

        /// <summary>
        /// 更新诱导
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public int Update(Dtos.InsertOrUpdateInducibleInput input)
        {
            return _inducibleRepository.Update(input.MapTo<Inducible>()).Id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inducibleDto"></param>
        /// <returns></returns>
        public int SaveInducible(Dtos.InducibleDto inducibleDto)
        {
            //Inducible model = _inducibleRepository.Load(inducibleDto.Id).MapTo<Inducible>();
            Inducible inducible = _inducibleRepository.Load(inducibleDto.Id);
            if (inducibleDto.InducibleName != inducible.InducibleName &&
                _inducibleRepository.FirstOrDefault(ind => ind.InducibleName == inducibleDto.InducibleName) !=
                default(Inducible))
                return 1;
            //if (_abpBerthsecRepository.FirstOrDefault(dic => dic.RateId == model.Id) != default(Berthsec) && model.IsActive == false)
            //    return 2;
            inducible.InducibleName = inducibleDto.InducibleName;
            inducible.X = inducibleDto.X;
            inducible.Y = inducibleDto.Y;
            inducible.Address = inducibleDto.Address;
            _inducibleRepository.Update(inducible);

            if (inducibleDto.advert != null && inducibleDto.advert.Count > 0)
            {
                foreach (InducibleToAD model in inducibleDto.advert)
                {
                    InducibleToAD ad = _inducibleToAdRepository.Load(model.Id);
                    if (String.Compare(model.AD, ad.AD) != 0)
                    {
                        ad.IsActive = false;
                        _inducibleToAdRepository.Update(ad);
                    }
                }
            }

            if (inducibleDto.parklist != null && inducibleDto.parklist.Count > 0)
            {
                foreach (var item in inducibleDto.parklist)
                {
                    InducibleToPark ip = new InducibleToPark();
                    ip.ParkId = item.ParkId;
                    ip.Id = item.Id;
                    ip.InducibleId = item.InducibleId;
                    _inducibleToParkRepository.Update(ip);
                }
            }
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<SensorDto> GetEmplyBerthsecs()
        {
            return _sensorRepository.GetAll().Where(s => s.ParkStatus == 0).MapTo<List<SensorDto>>();
        }

        /// <summary>
        /// 获取停车场空泊位(车检器)
        /// </summary>
        /// <returns></returns>
        public List<SensorToParkDto> GetParkToSensor()
        {
            string str = "-1";
            foreach (var i in AbpSession.ParkIds)
            {
                str += "," + i.ToString();
            }
            string sql = "select ParkId, sum(case when ParkStatus = 0 then 1 else 0 end) as EmptyCount, count(Id) as BerthCount from AbpSensors where ParkId in (" + str + ") group by ParkId ";

            return DataProcessHelper.GetEntityFromTable<SensorToParkDto>(Abp.Helper.SqlHelper.ExecuteDataset(Abp.Helper.SqlHelper.connectionString, System.Data.CommandType.Text, sql, null).Tables[0]);
        }

        /// <summary>
        /// 更新诱导
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <returns></returns>
        public InducibleToAD UpdateADStutas(string equipmentId)
        {
            InducibleToAD ad = _inducibleToAdRepository.FirstOrDefault(entity => entity.EquipmentId == equipmentId);
            ad.IsActive = true;
            return _inducibleToAdRepository.Update(ad);
        }
    }
}
