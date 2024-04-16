
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
    public class CouponsServiceImpl : CouponsService
    {
        public Hashtable getCouponsPlanList(CouponsQueryList param)
        {
            Hashtable result = new Hashtable();
            CouponsDao couponsDao = new CouponsDao();
            CommonService cs = new CommonService();
            param.start = (param.page - 1) * param.rows;

            Hashtable query = cs.InputAnalysis2<CouponsQueryList>(param);
            IList<Hashtable> list= couponsDao.getCouponsPlanList(query);

            object records = couponsDao.getCouponsPlanListSize(query);
            result.Add("rows", list);
            result.Add("code", 200);
            int a = Convert.ToInt32(records);
            int b = Convert.ToInt32(query["rows"]);
            result.Add("total", (a + (b - 1)) / b);

            result.Add("records", records);
            result.Add("msg", "success");
            return result;
        }

        public Hashtable insertCouponsPlan(InsertPlanQuery param)
        {
            Hashtable result = new Hashtable();
            CommonService cs = new CommonService();
            if(param.StartTime == null)
            {
                result.Add("msg", "优惠券方案开始时间不可为空");
                result.Add("code", 501);
                return result;
            }
            param.GetNumber = 0;
            Hashtable query = cs.InputAnalysis<InsertPlanQuery>(param);

            try
            {

                query.Add("CreatTime", DateTime.Now.ToLocalTime().ToString());
                query.Add("IsDelete", 2);
                query.Add("Status", 1);
                CouponsDao couponsDao = new CouponsDao();
                couponsDao.insertCouponsPlan(query);
                result.Add("msg", "success");
                result.Add("code", 200);
            }
            catch(Exception e)
            {
                result.Add("msg", e.ToString());
                result.Add("code", 200);
            }
            
            return result;
        }

        public Hashtable couponsEdit(InsertPlanQuery param)
        {
            Hashtable result = new Hashtable();
            CommonService cs = new CommonService();
            CouponsDao couponsDao = new CouponsDao();
            if (param.id == null)
            {
                result.Add("Error", "优惠券方案的id不可为空");
                result.Add("Success", false);
                return result;
            }
            Hashtable query = new Hashtable();
            query.Add("Id", param.id);
            IList<Hashtable> list = couponsDao.getCouponsPlanList(query);
            if (list.Count == 0)
            {
                result.Add("Error", "不存在修改的优惠券方案");
                result.Add("Success", false);
                return result;
            }
            Hashtable sqlparma = cs.InputAnalysis<InsertPlanQuery>(param);
            couponsDao.updatePlane(sqlparma);
            result.Add("Error", "success");
            result.Add("Success", true);
            return result;
        }

        public Hashtable couponsDelete(InsertPlanQuery param)
        {
            Hashtable result = new Hashtable();
            CommonService cs = new CommonService();
            CouponsDao couponsDao = new CouponsDao();
            if (param.id == null)
            {
                result.Add("Error", "优惠券方案的id不可为空");
                result.Add("Success", false);
                return result;
            }
            Hashtable query = new Hashtable();
            query.Add("Id", param.id);
            IList<Hashtable> list = couponsDao.getCouponsPlanList(query);
            if (list.Count == 0)
            {
                result.Add("Error", "不存在优惠券方案");
                result.Add("Success", false);
                return result;
            }
            Hashtable sqlparma = cs.InputAnalysis<InsertPlanQuery>(param);
            couponsDao.deletePlane(sqlparma);
            result.Add("Error", "success");
            result.Add("Success", true);
            return result;
        }

        public Hashtable getCouponsDetail(CouponsDetailQuery param)
        {
            Hashtable result = new Hashtable();
            CommonService cs = new CommonService();
            CouponsDao couponsDao = new CouponsDao();
            param.start = (param.page - 1) * param.rows;
            Hashtable query = cs.InputAnalysis2<CouponsDetailQuery>(param);

            IList<Hashtable> list = couponsDao.getCouponsDetail(query);

            object records = couponsDao.getCouponsDetailSize(query);
            result.Add("rows", list);
            result.Add("code", 200);
            int a = Convert.ToInt32(records);
            int b = Convert.ToInt32(query["rows"]);
            result.Add("total", (a + (b - 1)) / b);

            result.Add("records", records);
            result.Add("msg", "success");
            return result;
            
        }

        public Hashtable grantCoupons(GrantCouponsQuery param)
        {
            Hashtable result = new Hashtable();
            CommonService cs = new CommonService();
            CouponsDao couponsDao = new CouponsDao();
            VideoDao video = new VideoDao();

            var Now = DateTime.Now.ToLocalTime().ToString();

            Hashtable getList = new Hashtable();
            getList.Add("Id", param.PlanId);
            IList<Hashtable> plan = couponsDao.getCouponsPlanList(getList);
            if (plan.Count==0)
            {
                result.Add("Error", "不存在的优惠券方案");
                result.Add("Success", false);
                return result;
                
            }
            if (Convert.ToInt32(plan[0]["Status"])!=1)
            {
                result.Add("Error", "优惠活动未启用或已结束");
                result.Add("Success", false);
                return result;
            }
            if (param.uid == null || param.uid == "")
            {
                result.Add("Error", "至少选择一个用户");
                result.Add("Success", false);
                return result;
            }
            string[] uidList = param.uid.Split(',');
            int couponNumnber = Convert.ToInt32(plan[0]["CouponNumber"]);
            int getNumber = Convert.ToInt32(plan[0]["GetNumber"]);
            if (couponNumnber !=-1 && couponNumnber-getNumber< uidList.Length)
            {
                result.Add("Error", "优惠券剩余发放数量不足");
                result.Add("Success", false);
                return result;
            }

            var queryVar = "";
            for (int i = 0; i < uidList.Length; i++)
            {
                if (queryVar.Length == 0)
                {
                    queryVar = "'" + uidList[i] + "'";
                }
                else
                {
                    queryVar = queryVar + ",'" + uidList[i] + "'";
                }
            }
            getList.Add("uids", queryVar);
            IList<Hashtable> userList = video.getTUser(getList);
            if (userList.Count == 0)
            {
                result.Add("Error", "未查询到用户信息");
                result.Add("Success", false);
                return result;
            }
            var val = "";
            
            foreach (Hashtable user in userList)
            {
                if (val.Length == 0)
                {
                    val = "('" + System.Guid.NewGuid().ToString()+"','"+param.PlanId + "','" + Now + "','1','" + user["uid"] + "')";
                }
                else
                {
                    val = val + ",('" + System.Guid.NewGuid().ToString() + "','" + param.PlanId + "','" + Now + "','1','" + user["uid"]  + "')";
                }
            }
            Hashtable ans = new Hashtable();
            ans.Add("Value", val);
            couponsDao.insertCouponsDetails(ans);
            result.Add("msg", "success");
            result.Add("code", 200);
            return result;
        }
    }
}
