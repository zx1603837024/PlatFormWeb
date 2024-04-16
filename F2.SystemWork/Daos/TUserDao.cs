using F2.SystemWork.Services;
using IBatisNet.DataMapper;
using IBatisNet.DataMapper.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F2.SystemWork.Daos
{
    public class TUserDao
    {
        private ISqlMapper mappers;
        public TUserDao()
        {
            CommonService common = new CommonService();
            var map = common.PropertiesInitialization();
            DomSqlMapBuilder builder = new DomSqlMapBuilder();
            mappers = builder.Configure(map);
        }
        public IList<Hashtable> getTUser(Hashtable param)
        {
            IList<Hashtable> res = mappers.QueryForList<Hashtable>("getTUser", param);
            return res;
        }

        public object getTUserSize(Hashtable param)
        {
            return mappers.QueryForObject("getTUserSize", param);
        }
    }
}
