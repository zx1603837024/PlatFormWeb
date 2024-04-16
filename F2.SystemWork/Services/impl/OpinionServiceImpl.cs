
using F2.SystemWork.Daos;
using F2.SystemWork.Models;
using F2.SystemWork.Query;
using F2.SystemWork.Services;
using Newtonsoft.Json;
using P4.Parks.Dtos;  
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F2.SystemWork.Services.impl
{
    public class OpinionServiceImpl : OpinionService
    {
        public Hashtable getOpinionList(OpinionListQuery param)
        {
            Hashtable result = new Hashtable();
            OpinionDao opinion = new OpinionDao();
            CommonService cs = new CommonService();
            param.start = (param.page - 1) * param.rows;

            Hashtable query = cs.InputAnalysis2<OpinionListQuery>(param);
            IList<Hashtable> list= opinion.getOpinionList(query);

            object records = opinion.getOpinionListSize(query);
            result.Add("rows", list);
            result.Add("code", 200);
            int a = Convert.ToInt32(records);
            int b = Convert.ToInt32(query["rows"]);
            result.Add("total", (a + (b - 1)) / b);

            result.Add("records", records);
            result.Add("msg", "success");
            return result;
        }

        public Hashtable opinionHandle(OpinionHandleQuery param)
        {

            Hashtable result = new Hashtable();
            OpinionDao opinion = new OpinionDao();
            CommonService cs = new CommonService();


            
            Hashtable query = new Hashtable();
            query.Add("Id", param.Id);
            IList<Hashtable> list = opinion.getOpinionList(query);

            if(list.Count == 0)
            {
                result.Add("code", 501);
                result.Add("msg", "没有查询到该意见");
                return result;
            }
            Hashtable handle = cs.InputAnalysis2<OpinionHandleQuery>(param);
            handle.Add("opinionTime", DateTime.Now);
            opinion.opinionHandle(handle);

            result.Add("code", 200);
            result.Add("msg", "success");
            return result;
        }
    }
}
