using System;
using System.Collections;
using F2.SystemWork.Services;
using F2.SystemWork.Models;
using F2.SystemWork.Daos;
using System.Collections.Generic;

namespace F2.OptionSystem.Service.impl
{
    public class TUserServiceImpl : TUserService
    {
        public Hashtable getTUser(TUserQuery query)
        {
            Hashtable result = new Hashtable();
            CommonService cs = new CommonService();
            Hashtable param = cs.InputAnalysis2<TUserQuery>(query);
            VideoDao dao = new VideoDao();
            try
            {
                object total = dao.getTUserSize(param);
                IList<Hashtable> list = dao.getTUser(param);

                result.Add("code", 200);
                result.Add("result", "成功");
                result.Add("records", total );
                result.Add("rows", list);
            }
            catch (Exception e)
            {
                result.Add("result", "数据查询失败");
                result.Add("code", 1001);
            }

            return result;
        }
    }
}
