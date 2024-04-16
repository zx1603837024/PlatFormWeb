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
    public class CouponsDao
    {
        private ISqlMapper mappers;
        public CouponsDao()
        {
            CommonService common = new CommonService();
            var map = common.PropertiesInitialization();
            DomSqlMapBuilder builder = new DomSqlMapBuilder();
            mappers = builder.Configure(map);
        }
        public IList<Hashtable> getCouponsPlanList(Hashtable param)
        {
            IList<Hashtable> res = mappers.QueryForList<Hashtable>("getCouponsPlanList", param);
            return res;
        }

        public object getCouponsPlanListSize(Hashtable param)
        {
            return mappers.QueryForObject("getCouponsPlanListSize", param);
        }

        
        public void insertCouponsPlan(Hashtable query)
        {
            mappers.Insert("insertCouponsPlan", query);
        }

        public void updatePlane(Hashtable param)
        {
            mappers.Update("updatePlane", param);
        }
        public void deletePlane(Hashtable param)
        {
            mappers.Update("deletePlane", param);
        }
        public IList<Hashtable> getCouponsDetail(Hashtable param)
        {
            IList<Hashtable> res = mappers.QueryForList<Hashtable>("getCouponsDetail", param);
            return res;
        }

        public object getCouponsDetailSize(Hashtable param)
        {
            return mappers.QueryForObject("getCouponsDetailSize", param);
        }

        public void insertCouponsDetails(Hashtable query)
        {
            mappers.Insert("insertCouponsDetails", query);
        }

        
    }
}
