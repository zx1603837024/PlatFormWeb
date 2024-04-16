
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
    public class PatrolCarServiceImpl : PatrolCarService
    {

        
        public Hashtable getPatrolCarList(Hashtable query)
        {
            Hashtable result  = new Hashtable();
            CarVideoDao carVideoDao = new CarVideoDao();
            IList<Hashtable> list =  carVideoDao.getPatrolCarList(query);
            object records = carVideoDao.getPatrolCarListSize(query);
            result.Add("rows", list);
            result.Add("code", 200);
            int a = Convert.ToInt32(records);
            int b = Convert.ToInt32(query["rows"]);
            result.Add("total", (a + (b - 1)) / b);
            
            result.Add("records", records);
            result.Add("msg", "success");
            return result;
        }
        //巡检车批量新增
        public Hashtable InsertPatrolCar(string input)
        {
            Hashtable result = new Hashtable();
            var param = JsonConvert.DeserializeObject<Hashtable>(input);
            if (param["list"] == null)
            {
                result.Add("code", 501);
                result.Add("msg", "至少添加一个设备");
                return result;
            }
            if(param["PatrolType"] == null)
            {
                result.Add("code", 501);
                result.Add("msg", "车辆类型不可为空");
                return result;
            }
            var list = Convert.ToString(param["list"]).Replace("\n", ",");
            string[] paramArr = list.Split(',');
            var Now = DateTime.Now.ToLocalTime().ToString();
            var queryVar = "";
            for(int i = 0; i < paramArr.Length; i++)
            {
                if (queryVar.Length == 0)
                {
                    queryVar =  "'"+paramArr[i] +"'";
                }
                else
                {
                    queryVar = queryVar + ",'" + paramArr[i]+"'" ;
                }
            }
            var val = "";
            Hashtable query = new Hashtable();
            query.Add("PatrolCarNumber", queryVar);
            CarVideoDao carVideoDao = new CarVideoDao();
            IList<Hashtable> carList = carVideoDao.getPatrolCarList(query);
            if (carList.Count > 0)
            {
                result.Add("code", 501);
                result.Add("msg", "以下巡检车已经录入过,请勿重复上传:"+string.Join(",", carList.Select(t=>t["PatrolCarNumber"]).Distinct().ToList()));
                return result;
            }
            
            for (int i = 0; i < paramArr.Length; i++)
            {
                if (val.Length == 0)
                {
                    val = "('" + paramArr[i] + "','" + Now + "','1','" + param["PatrolType"] +"','"+ Now + "')";
                }
                else
                {
                    val = val + ",('" + paramArr[i] + "','" + Now + "','1','" + param["PatrolType"] +"','"+Now+ "')";
                }
            }
            query.Add("Value", val);
            result.Add( "code", 200);
            result.Add("msg", "success");
            carVideoDao.InsertPatrolCar(query);
            return result;
        }
        public Hashtable bindingCar(BindingCarQuery param)
        {
            CommonService cs = new CommonService();
            
            Hashtable result = new Hashtable();
            if (param.PatrolCarNumber == null || param.PatrolCarNumber == "")
            {
                result.Add("code", 501);
                result.Add("msg", "绑定巡检车编码不可为空");
                return result;
            }
            if (param.BerthsId == null || param.BerthsId == "")
            {
                result.Add("code", 501);
                result.Add("msg", "绑定巡检车泊位号不可为空");
                return result;
            }
            Hashtable hashtable = new Hashtable();
            hashtable.Add("PatrolCarNumber", "'"+ param.PatrolCarNumber+"'");
            CarVideoDao carVideoDao = new CarVideoDao();
            IList<Hashtable> carList = carVideoDao.getPatrolCarList(hashtable);
            if (carList.Count == 0)
            {
                result.Add("code", 501);
                result.Add("msg", "未录入巡检车无法绑定");
                return result;
            }
            Hashtable query = cs.InputAnalysis2<BindingCarQuery>(param);
            IList<Hashtable> carList2 = carVideoDao.getBindingList(query);
            if(carList2.Count > 0)
            {
                result.Add("code", 501);
                result.Add("msg", "请勿重复绑定相同泊位");
                return result;
            }
            Hashtable Berths = new Hashtable();
            Berths.Add("BerthsId", param.BerthsId);
            IList<Hashtable> BerthsList = carVideoDao.getBindingList(Berths);
            if (BerthsList.Count > 0)
            {
                result.Add("code", 501);
                result.Add("msg", "绑定的泊位已经被如下巡检车绑定"+ string.Join(",", BerthsList.Select(t => t["PatrolCarNumber"]).Distinct().ToList()));
                return result;
            }
            query.Add("CreateTime", DateTime.Now);
            carVideoDao.bindingCar(query);
            result.Add("code", 200);
            result.Add("msg", "success");
            return result;
        }

        public Hashtable PatrolEdit(PatrolCarModel input)
        {
            Hashtable result = new Hashtable();
            CarVideoDao dao = new CarVideoDao();
            try
            {
                Hashtable param = new Hashtable();
                if (input.id == null)
                {
                    result.Add("Success", false);
                    result.Add("Error", "需要修改的巡检车Id不可为空");
                    return result;
                }
                Hashtable query2 = new Hashtable();
                query2.Add("Id", input.id);
                IList<Hashtable> carList2 = dao.getPatrolCarList(query2);
                if (carList2.Count == 0)
                {
                    result.Add("Success", false);
                    result.Add("Error", "需要修改的巡检车系统中不存在");
                    return result;
                }
                Hashtable query = new Hashtable();
                query.Add("PatrolCarNumber", "'" + input.PatrolCarNumber + "'");
                IList<Hashtable> carList = dao.getPatrolCarList(query);
                if (carList.Count > 0)
                {
                    result.Add("Success", false);
                    result.Add("Error", "修改后的巡检车在系统中已录入");
                    return result;
                }
                //参数填补
                var PatrolCarNumber_s = carList2[0]["PatrolCarNumber"];
                var Now = DateTime.Now.ToLocalTime().ToString();
                param.Add("PatrolCarNumber", input.PatrolCarNumber);
                param.Add("PatrolCarNumber_s", PatrolCarNumber_s);
                param.Add("IsUse", input.IsUse);
                param.Add("Remark", input.Remark);
                if (input.PatrolType != null)
                {
                    param.Add("PatrolType", input.PatrolType);
                }
                if(input.IsUse == 0)
                {
                    param["StopTime"] = DateTime.Now;
                    param.Add("IsUse", 0);
                }
                else if (input.IsUse == 1)
                {
                    param["EnableTime"] = DateTime.Now;
                    param["StopTime"] = "";
                    param.Add("IsUse", 1);
                }
                if (param["PatrolCarNumber"] == null)
                {
                    param["PatrolCarNumber"] = param["PatrolCarNumber_s"];
                }
                dao.updatePatrolCar(param);
                dao.updatePatrolBerth(param);
                result.Add("Success", true);
            }
            catch (Exception e)
            {
                result.Add("Success", false);
                Hashtable msg = new Hashtable();
                msg.Add("Message", e.ToString());
                result.Add("Error", msg);
            }
            return result;
        }

        public Hashtable PatrolDelete(PatrolCarModel input)
        {
            Hashtable result = new Hashtable();
            CarVideoDao carDao = new CarVideoDao();
            try
            {
                if(input.id == null || input.id == "")
                {
                    result.Add("Success", false);
                    result.Add("Error", "巡检车设备id不可为空");
                    return result;
                }
                Hashtable query2 = new Hashtable();

                string[] paramArr = input.id.Split(',');
                var queryVar = "";
                for (int i = 0; i < paramArr.Length; i++)
                {
                    if (queryVar.Length == 0)
                    {
                        queryVar = "'" + paramArr[i] + "'";
                    }
                    else
                    {
                        queryVar = queryVar + ",'" + paramArr[i] + "'";
                    }
                }

                query2.Add("Ids", queryVar);

                IList<Hashtable> carList2 = carDao.getPatrolCarList(query2);
                if (carList2.Count == 0)
                {
                    result.Add("Success", false);
                    result.Add("Error", "需要删除的巡检车系统中不存在");
                    return result;
                }
                /*param.Add("PatrolCarNumber", carList2[0]["PatrolCarNumber"]);*/
                foreach(Hashtable car in carList2)
                {
                    Hashtable param = new Hashtable();
                    param.Add("PatrolCarNumber", car["PatrolCarNumber"]);
                    carDao.deletPatrolCar(param);
                    carDao.deletPatrolBerths(param);
                }
                
                result.Add("Success", true);
            }
            catch (Exception e)
            {
                result.Add("Success", false);
                Hashtable msg = new Hashtable();
                msg.Add("Message", e.ToString());
                result.Add("Error", msg);
            }
            return result;
        }

        /// <summary>
        /// 查询关联巡检车
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public Hashtable getPatrolBerthsList(PatrolBerthsQuery param )
        {
            Hashtable result = new Hashtable();
            try
            {
                param.start = (param.page - 1) * param.rows;
                
                CarVideoDao carVideoDao = new CarVideoDao();
                CommonService cs = new CommonService();
                Hashtable query = cs.InputAnalysis2<PatrolBerthsQuery>(param);
                IList<Hashtable> list = carVideoDao.getPatrolBerthsList(query);
                object records = carVideoDao.getPatrolBerthsListSize(query);
                result.Add("rows", list);
                result.Add("code", 200);
                int a = Convert.ToInt32(records);
                int b = Convert.ToInt32(query["rows"]);
                result.Add("total", (a + (b - 1)) / b);
                
                result.Add("records", records);
                result.Add("msg", "success");
            }catch(Exception e)
            {
                result.Add("code", 501);
                result.Add("msg", e.ToString());
            }
            return result;
        }

        public Hashtable deletePatrolBerths(DeletePatrolBerhsQuery parmas)
        {
            Hashtable result = new Hashtable();
            if (parmas.PatrolCarNumber==null || parmas.BerthsId==null)
            {
                result.Add("code", 501);
                result.Add("msg", "解绑巡检车 绑定车辆编号 和路段id 不可为空");
                return result;
            }
            CarVideoDao carVideoDao = new CarVideoDao();
            CommonService cs = new CommonService();
            Hashtable query = cs.InputAnalysis2<DeletePatrolBerhsQuery>(parmas);
            carVideoDao.deletPatrolBerths(query);
            result.Add("code", 200);
            result.Add("msg", "success");
            return result;
        }

        //巡检车订单数据
        public Hashtable GetPatrolCarBusinessDetail(PatrolCarModel input)
        {
            Hashtable result = new Hashtable();
            try
            {
                CommonService cs = new CommonService();
                Hashtable param = cs.InputAnalysis<PatrolCarModel>(input);
                CarVideoDao dao = new CarVideoDao();
                IList<Hashtable> List = dao.GetPatrolCarBusinessDetail(param);
                IList<int> List2 = dao.GetPatrolCarBusinessDetailCount(param);
                int records = 0;
                if (List2.Count > 0)
                {
                    records = List2[0];
                }
                result.Add("result", "success");
                result.Add("rows", List);
                result.Add("records", records);
                if (input.rows != null && input.rows != 0)
                {
                    result.Add("total", (int)Math.Ceiling((double)records / (double)input.rows));
                }
            }
            catch (Exception e)
            {
                result.Add("result", "false");
                result.Add("msg", e.ToString());
            }
            return result;
        }
        //巡检车订单审核
        public Hashtable UPatrolCarBusinessDetailAuditStatus(string input)
        {
            Hashtable result = new Hashtable();
            try
            {
                var param = JsonConvert.DeserializeObject<Hashtable>(input);
                CommonService com = new CommonService();
                CarVideoDao dao = new CarVideoDao();
                dao.UPatrolCarBusinessDetailAuditStatus(param);
                result.Add("result", "success");
            }
            catch (Exception e)
            {
                result.Add("result", "false");
                result.Add("msg", e.ToString());
            }
            return result;
        }
        
        /// <summary>
        /// 巡检车订单编辑
        /// </summary>
        /// <returns></returns>q
        public Hashtable UPatrolCarBusinessDetail(PatrolCarModel input)
        {
            Hashtable result = new Hashtable();
            try
            {
                CommonService cs = new CommonService();
                Hashtable param = cs.InputAnalysis<PatrolCarModel>(input);
                CarVideoDao dao = new CarVideoDao();
                dao.UPatrolCarBusinessDetail(param);
                result.Add("Success", true);
            }
            catch (Exception e)
            {
                result.Add("Success", false);
                Hashtable msg = new Hashtable();
                msg.Add("Message", e.ToString());
                result.Add("Error", msg);
            }
            return result;
        }

		public Hashtable getAlarmDataList(AlarmDataQuery param)
        {
            Hashtable result = new Hashtable();
            try
            {
                param.start = (param.page - 1) * param.rows;

                CarVideoDao carVideoDao = new CarVideoDao();
                CommonService cs = new CommonService();
                Hashtable query = cs.InputAnalysis2<AlarmDataQuery>(param);
                IList<Hashtable> list = carVideoDao.getAlarmDataList(query);
                object records = carVideoDao.getAlarmDataListSize(query);
                result.Add("rows", list);
                result.Add("code", 200);
                int a = Convert.ToInt32(records);
                int b = Convert.ToInt32(query["rows"]);
                result.Add("total", (a + (b - 1)) / b);

                result.Add("records", records);
                result.Add("msg", "success");
            }
            catch (Exception e)
            {
                result.Add("code", 501);
                result.Add("msg", e.ToString());
            }
            return result;
        }


    }
}
