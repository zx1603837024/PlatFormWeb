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
    public class OpinionDao
    {
        private ISqlMapper mappers;
        public OpinionDao()
        {
            CommonService common = new CommonService();
            var map = common.PropertiesInitialization();
            DomSqlMapBuilder builder = new DomSqlMapBuilder();
            mappers = builder.Configure(map);
        }
        public IList<Hashtable> getOpinionList(Hashtable param)
        {
            IList<Hashtable> res = mappers.QueryForList<Hashtable>("getOpinionList", param);
            return res;
        }

        public object getOpinionListSize(Hashtable param)
        {
            return mappers.QueryForObject("getOpinionListSize", param);
        }

        public void opinionHandle(Hashtable param)
        {
            mappers.Update("opinionHandle", param);
        }
        

    }
}
