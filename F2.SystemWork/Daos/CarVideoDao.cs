using F2.SystemWork.Services;
using IBatisNet.DataMapper.Configuration;
using IBatisNet.DataMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace F2.SystemWork.Daos
{
    public class CarVideoDao
    {
        private ISqlMapper mappers;
        public CarVideoDao()
        {
            CommonService common = new CommonService();
            var map = common.PropertiesInitialization();
            DomSqlMapBuilder builder = new DomSqlMapBuilder();
            mappers = builder.Configure(map);
        }
        public IList<Hashtable> SelectVideoCarDistinct(Hashtable param)
        {
            IList<Hashtable> res = mappers.QueryForList<Hashtable>("SelectVideoCarDistinct", param);
            return res;
        }
        public IList<int> SelectVideoCarDistinctCount(Hashtable param)
        {
            IList<int> res = mappers.QueryForList<int>("SelectVideoCarDistinctCount", param);
            return res;
        }
        public IList<Hashtable> SelectVideoCarList(Hashtable param)
        {
            IList<Hashtable> res = mappers.QueryForList<Hashtable>("SelectVideoCarList", param);
            return res;
        }
        public IList<int> SelectVideoCarCount(Hashtable param)
        {
            IList<int> res = mappers.QueryForList<int>("SelectVideoCarCount", param);
            return res;
        }
        public void InsertVideoCarsBatch(Hashtable param)
        {
            mappers.Insert("InsertVideoCarsBatch", param);
        }
        public void InsertAbpVideoCars(Hashtable param)
        {
            mappers.Insert("InsertAbpVideoCars", param);
        }
        public IList<Hashtable> SelectBerthsecsNumberCar(Hashtable param)
        {
            IList<Hashtable> res = mappers.QueryForList<Hashtable>("SelectBerthsecsNumberCar", param);
            return res;
        }
        public void DeleteVideoCars(Hashtable param)
        {
            mappers.Delete("DeleteVideoCars", param);
        }
        public void DeleteVideoCarsByNumber(Hashtable param)
        {
            mappers.Delete("DeleteVideoCarsByNumber", param);
        }

        public void UpdateVideoCars(Hashtable param)
        {
            mappers.Update("UpdateVideoCars", param);
        }
        public IList<Hashtable> SelectVideoCarHeartBeatList(Hashtable param)
        {
            IList<Hashtable> res = mappers.QueryForList<Hashtable>("SelectVideoCarHeartBeatList", param);
            return res;
        }
        public IList<int> SelectVideoCarHeartBeatCount(Hashtable param)
        {
            IList<int> res = mappers.QueryForList<int>("SelectVideoCarHeartBeatCount", param);
            return res;
        }
        public IList<Hashtable> SelectAbpParamConfig(Hashtable param)
        {
            IList<Hashtable> res = mappers.QueryForList<Hashtable>("SelectAbpParamConfig", param);
            return res;
        }
        public void UpdateAbpParamConfig(Hashtable param)
        {
            mappers.Update("UpdateAbpParamConfig", param);
        }
        public void InsertAbpParamConfig(Hashtable param)
        {
            mappers.Insert("InsertAbpParamConfig", param);
        }
        public IList<int> GetAllDetail(Hashtable param)
        {
            IList<int> res = mappers.QueryForList<int>("GetAllDetail", param);
            return res;
        }
        public IList<int> GetAllBerth(Hashtable param)
        {
            IList<int> res = mappers.QueryForList<int>("GetAllBerth", param);
            return res;
        }
        public IList<int> GetAllEquipment(Hashtable param)
        {
            IList<int> res = mappers.QueryForList<int>("GetAllEquipment", param);
            return res;
        }
        public IList<int> GetAllAlert(Hashtable param)
        {
            IList<int> res = mappers.QueryForList<int>("GetAllAlert", param);
            return res;
        }
        public IList<Hashtable> GetDetailGroup(Hashtable param)
        {
            IList<Hashtable> res = mappers.QueryForList<Hashtable>("GetDetailGroup", param);
            return res;
        }
        public void InsertAbpSetting(Hashtable param)
        {
            mappers.Insert("InsertAbpSetting", param);
        }
        public void DeleteAbpSettingById(Hashtable param)
        {
            mappers.Delete("DeleteAbpSettingById", param);
        }

        public IList<Hashtable> getPatrolCarList(Hashtable param)
        {
            IList<Hashtable> res = mappers.QueryForList<Hashtable>("getPatrolCarList", param);
            return res;
        }
        public object getPatrolCarListSize(Hashtable param)
        {
            return mappers.QueryForObject("getPatrolCarListSize", param);     
        }
        
        public void InsertPatrolCar(Hashtable query)
        {
            mappers.Insert("InsertPatrolCar", query);
        }

        public void bindingCar(Hashtable query)
        {
            mappers.Insert("bindingCar", query);
        }
        public IList<Hashtable> getBindingList(Hashtable param)
        {
            IList<Hashtable> res = mappers.QueryForList<Hashtable>("getBindingList", param);
            return res;
        }
        public void deletPatrolCar(Hashtable param)
        {
            mappers.Delete("deletPatrolCar", param);
        }
        public void deletPatrolBerths(Hashtable param)
        {
            mappers.Delete("deletPatrolBerths", param);
        }
        
        public void updatePatrolCar(Hashtable param)
        {
            mappers.Update("updatePatrolCar", param);
        }
        public void updatePatrolBerth(Hashtable param)
        {
            mappers.Update("updatePatrolBerth", param);
        }
        public IList<Hashtable> getPatrolBerthsList(Hashtable param)
        {
            IList<Hashtable> res = mappers.QueryForList<Hashtable>("getPatrolBerthsList", param);
            return res;
        }
        
        public object getPatrolBerthsListSize(Hashtable param)
        {
            return mappers.QueryForObject("getPatrolBerthsListSize", param);
        }
		public IList<Hashtable> GetPatrolCarBusinessDetail(Hashtable param)
        {
            IList<Hashtable> res = mappers.QueryForList<Hashtable>("GetPatrolCarBusinessDetail", param);
            return res;
        }
        public IList<int> GetPatrolCarBusinessDetailCount(Hashtable param)
        {
            IList<int> res = mappers.QueryForList<int>("GetPatrolCarBusinessDetailCount", param);
            return res;
        }
        public void UPatrolCarBusinessDetailAuditStatus(Hashtable param)
        {
            mappers.Update("UPatrolCarBusinessDetailAuditStatus", param);
        }
        
        public void UPatrolCarBusinessDetail(Hashtable param)
        {
            mappers.Update("UPatrolCarBusinessDetail", param);
        }

		public IList<Hashtable> getAlarmDataList(Hashtable param)
        {
            IList<Hashtable> res = mappers.QueryForList<Hashtable>("getAlarmDataList", param);
            return res;
        }

        public object getAlarmDataListSize(Hashtable param)
        {
            return mappers.QueryForObject("getAlarmDataListSize", param);
        }

    }
}
