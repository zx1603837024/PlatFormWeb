using F2.SystemWork.Models;
using F2.SystemWork.Services;
using IBatisNet.DataMapper;
using IBatisNet.DataMapper.Configuration;
using P4.Sensors.Dtos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F2.SystemWork.Daos
{
    public class VideoDao
    {
        private ISqlMapper mappers;
        public VideoDao()
        {
            CommonService common = new CommonService();
            var map=common.PropertiesInitialization();
            DomSqlMapBuilder builder = new DomSqlMapBuilder();
            mappers = builder.Configure(map);
        }
        public IList<VideoSettingModel> SelectVideoEquipsDistinct(Hashtable param)
        {
            IList<VideoSettingModel> res = mappers.QueryForList<VideoSettingModel>("SelectVideoEquipsDistinct", param);
            return res;
        }

        public IList<VideoEquipsModel> getVideoEquips(Hashtable param)
        {
            IList<VideoEquipsModel> res = mappers.QueryForList<VideoEquipsModel>("getVideoEquips", param);
            return res;
        }


        public IList<VideoBerthModel> getVideoBerth(Hashtable param)
        {
            IList<VideoBerthModel> res = mappers.QueryForList<VideoBerthModel>("getVideoBerth", param);
            return res;
        }
        public IList<Hashtable> capture(Hashtable param)
        {
            IList<Hashtable> res = mappers.QueryForList<Hashtable>("capture", param);
            return res;
        }

        public object captureSize(Hashtable param)
        {
            return mappers.QueryForObject("captureSize", param);
        }

        public IList<BerthsecView> getVideoBerthsec()
        {
            Hashtable param=new Hashtable();
            return mappers.QueryForList<BerthsecView>("getVideoBerthsec", param);
        }

        public object getVideoBerthsecCount(Hashtable param)
        {
            return mappers.QueryForObject("getVideoBerthsecCount", param);
        }

        public IList<Hashtable> SelectVideoEquipsDistinctCount(Hashtable param)
        {
            IList<Hashtable> res = mappers.QueryForList<Hashtable>("SelectVideoEquipsDistinctCount", param);
            return res;
        }
        public IList<VideoSettingModel> SelectVideoEquipsList(Hashtable param)
        {
            IList<VideoSettingModel> res = mappers.QueryForList<VideoSettingModel>("SelectVideoEquipsList", param);
            return res;
        }
        public IList<int> SelectVideoEquipsCount(Hashtable param)
        {
            IList<int> res = mappers.QueryForList<int>("SelectVideoEquipsCount", param);
            return res;
        }
        public IList<VideoSettingModel> SelectParkInfoList()
        {
            IList<VideoSettingModel> res = mappers.QueryForList<VideoSettingModel>("SelectParkInfoList", null);
            return res;
        }
        public IList<VideoSettingModel> SelectBerthsecsInfoList(Hashtable param)
        {
            IList<VideoSettingModel> res = mappers.QueryForList<VideoSettingModel>("SelectBerthsecsInfoList", param);
            return res;
        }
        public IList<VideoSettingModel> SelectNoBerthsecsNumber(Hashtable param)
        {
            IList<VideoSettingModel> res = mappers.QueryForList<VideoSettingModel>("SelectNoBerthsecsNumber", param);
            return res;
        }
        public void UpdateVideoEquipsNull(Hashtable param)
        {
            mappers.Update("UpdateVideoEquipsNull", param);
        }
        public void UpdateVideoEquipsBand(Hashtable param)
        {
            mappers.Update("UpdateVideoEquipsBand", param);
        }
        public void UpdateVideoEquips(Hashtable param)
        {
            mappers.Update("UpdateVideoEquips", param);
        }
        public void InsertVideoEquipsBatch(Hashtable param)
        {
            mappers.Insert("InsertVideoEquipsBatch", param);
        }
        public void DeleteVideoEquips(Hashtable param)
        {
            mappers.Delete("DeleteVideoEquips", param);
        }
        public IList<VideoGraphModel> SelectVideoHeartBeatList(Hashtable param)
        {
            IList<VideoGraphModel> res = mappers.QueryForList<VideoGraphModel>("SelectVideoHeartBeatList", param);
            return res;
        }
        public IList<int> SelectVideoHeartBeatCount(Hashtable param)
        {
            IList<int> res = mappers.QueryForList<int>("SelectVideoHeartBeatCount", param);
            return res;
        }
        public IList<int> SelectVideoEqOnlineCount(Hashtable param)
        {
            IList<int> res = mappers.QueryForList<int>("SelectVideoEqOnlineCount", param);
            return res;
        }
        public IList<Hashtable> SelectEmployeesInfo(Hashtable param)
        {
            IList<Hashtable> res = mappers.QueryForList<Hashtable>("SelectEmployeesInfo", param);
            return res;
        }
        public IList<Hashtable> SelectArrearageDetail(Hashtable param)
        {
            IList<Hashtable> res = mappers.QueryForList<Hashtable>("SelectArrearageDetail", param);
            return res;
        }
        public IList<int> SelectArrearageDetailCount(Hashtable param)
        {
            IList<int> res = mappers.QueryForList<int>("SelectArrearageDetailCount", param);
            return res;
        }
        public void UpdateBusinessDetailArrearage(Hashtable param)
        {
            mappers.Update("UpdateBusinessDetailArrearage", param);
        }
        public void InsertBusinessDetailRecovery(Hashtable param)
        {
            mappers.Insert("InsertBusinessDetailRecovery", param);
        }
        public IList<Hashtable> SelectBerthsecsNumberGW(Hashtable param)
        {
            IList<Hashtable> res = mappers.QueryForList<Hashtable>("SelectBerthsecsNumberGW", param);
            return res;
        }
        public void InsertVideoEquipsGW(Hashtable param)
        {
            mappers.Insert("InsertVideoEquipsGW", param);
        }
        public void DeleteVideoEquipsByEquips(Hashtable param)
        {
            mappers.Delete("DeleteVideoEquipsByEquips", param);
        }

        public void UpdateVideoEquipsGW(Hashtable param)
        {
            mappers.Update("UpdateVideoEquipsGW", param);
        }
        public IList<VideoEqBusinessDetailModel> GetVideoEquipBusinessDetail(Hashtable param)
        {
            IList<VideoEqBusinessDetailModel> res = mappers.QueryForList<VideoEqBusinessDetailModel>("GetVideoEquipBusinessDetail", param);
            return res;
        }
        public IList<int> GetVideoEquipBusinessDetailCount(Hashtable param)
        {
            IList<int> res = mappers.QueryForList<int>("GetVideoEquipBusinessDetailCount", param);
            return res;
        }
        public void UpdateBusinessDetailAuditStatus(Hashtable param)
        {
            mappers.Update("UpdateBusinessDetailAuditStatus", param);
        }
        public void UpdateVideoDetailPlateNumber(Hashtable param)
        {
            mappers.Update("UpdateVideoDetailPlateNumber", param);
        }
        public IList<Hashtable> GetVideoEquipFaults(Hashtable param)
        {
            IList<Hashtable> res = mappers.QueryForList<Hashtable>("GetVideoEquipFaults", param);
            return res;
        }
        public IList<int> GetVideoEquipFaultsCount(Hashtable param)
        {
            IList<int> res = mappers.QueryForList<int>("GetVideoEquipFaultsCount", param);
            return res;
        }
        public void UBusinessDetailPostState(Hashtable param)
        {
            mappers.Update("UBusinessDetailPostState", param);
        }
        public IList<Hashtable> SelectFqDetail(Hashtable param)
        {
            IList<Hashtable> res = mappers.QueryForList<Hashtable>("SelectFqDetail", param);
            return res;
        }

        public IList<Hashtable> getTUser(Hashtable param)
        {
            IList<Hashtable> res = mappers.QueryForList<Hashtable>("GetTUser", param);
            return res;
        }

        public object getTUserSize(Hashtable param)
        {
            return mappers.QueryForObject("GetTUserSize", param);
        }

        public IList<VideoEqBusinessDetailModel> SelectLongTimeOccpy(Hashtable param)
        {
            IList<VideoEqBusinessDetailModel> res = mappers.QueryForList<VideoEqBusinessDetailModel>("SelectLongTimeOccpy", param);
            return res;
        }
        public IList<int> SelectLongTimeOccpyCount(Hashtable param)
        {
            IList<int> res = mappers.QueryForList<int>("SelectLongTimeOccpyCount", param);
            return res;
        }
    }
}
