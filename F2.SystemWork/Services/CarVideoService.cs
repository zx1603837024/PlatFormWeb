using F2.SystemWork.Daos;
using F2.SystemWork.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F2.SystemWork.Services
{
    public class CarVideoService
    {
        //巡检车信息列表查询
        public Hashtable SelectVideoCarDistinct(CarVideoParamModel input)
        {
            Hashtable result = new Hashtable();
            try
            {
                CommonService cs = new CommonService();
                Hashtable param = cs.InputAnalysis<CarVideoParamModel>(input);
                CarVideoDao dao = new CarVideoDao();
                IList<Hashtable> List = dao.SelectVideoCarDistinct(param);
                IList<int> List2 = dao.SelectVideoCarDistinctCount(param);
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
        //巡检车信息下拉列表
        public Hashtable SelectVideoCars(CarVideoParamModel input)
        {
            Hashtable result = new Hashtable();
            try
            {
                CommonService cs = new CommonService();
                Hashtable param = cs.InputAnalysis<CarVideoParamModel>(input);
                CarVideoDao dao = new CarVideoDao();
                IList<Hashtable> List = dao.SelectVideoCarList(param);
                foreach (Hashtable ele in List)
                {
                    if (ele["BerthNumber"] == null)
                    {
                        List.Remove(ele);
                        break;
                    }
                }
                result.Add("result", "success");
                result.Add("rows", List);
            }
            catch (Exception e)
            {
                result.Add("result", "false");
                result.Add("msg", e.ToString());
            }
            return result;
        }
        //上传巡检车信息
        public Hashtable InsertVideoCarBatch(String input)
        {
            Hashtable result = new Hashtable();
            try
            {
                var param = JsonConvert.DeserializeObject<Hashtable>(input);
                var Eqinfo = Convert.ToString(param["Eqinfo"]).Replace("\n", ",");
                var Eqtype = Convert.ToString(param["Eqtype"]);
                var Now = DateTime.Now.ToLocalTime().ToString();
                string[] paramArr = Eqinfo.Split(',');
                var val = "";
                for (int i = 0; i < paramArr.Length; i++)
                {
                    if (val.Length == 0)
                    {
                        val = "('" + paramArr[i] + "','" + Eqtype + "','" + Now + "')";
                    }
                    else
                    {
                        val = val + ",('" + paramArr[i] + "','" + Eqtype + "','" + Now + "')";
                    }
                }
                Hashtable param2 = new Hashtable();
                param2.Add("Value", val);
                CarVideoDao dao = new CarVideoDao();
                var vals = "";
                for (int i = 0; i < paramArr.Length; i++)
                {
                    if (vals.Length == 0)
                    {
                        vals = "'" + paramArr[i] + "'";
                    }
                    else
                    {
                        vals = vals + ",'" + paramArr[i] + "'";
                    }
                }
                Hashtable paramDistinct = new Hashtable();
                paramDistinct.Add("VideoCarNumber", vals);
                IList<Hashtable> List = dao.SelectVideoCarDistinct(paramDistinct);
                if (List.Count() == 0)
                {
                    dao.InsertVideoCarsBatch(param2);
                    result.Add("result", "success");
                }
                else
                {
                    result.Add("result", "false");
                    result.Add("msg", "巡检车信息已存在");
                }
            }
            catch (Exception e)
            {
                result.Add("result", "false");
                result.Add("msg", e.ToString());
            }
            return result;
        }
        //巡检车绑定
        public Hashtable BandVideoCar(String input)
        {
            Hashtable result = new Hashtable();
            try
            {
                var param = JsonConvert.DeserializeObject<Hashtable>(input);
                if (Convert.ToString(param["BeatDatetime"]).Length == 0)
                {
                    param["BeatDatetime"] = "null";
                }
                else
                {
                    param["BeatDatetime"] = "'" + param["BeatDatetime"] + "'";
                }
                if (Convert.ToString(param["IsUse"]).Length == 0)
                {
                    param["IsUse"] = "null";
                }
                else
                {
                    param["IsUse"] = "'" + param["IsUse"] + "'";
                }
                if (Convert.ToString(param["IsOnlineValue"]).Length == 0)
                {
                    param["IsOnlineValue"] = "null";
                }
                else
                {
                    param["IsOnlineValue"] = "'" + param["IsOnlineValue"] + "'";
                }
                CarVideoDao dao = new CarVideoDao();
                IList<Hashtable> List = dao.SelectBerthsecsNumberCar(param);
                var Now = DateTime.Now.ToLocalTime().ToString();
                var pstr = "";
                foreach (Hashtable ele in List)
                {
                    pstr = pstr + "('" + ele["TenantId"] + "','" + ele["CompanyId"] + "','" + ele["RegionId"] + "','" + ele["ParkId"] + "','" + ele["BerthsecId"] + "','" + ele["BerthNumber"] + "','" + param["VideoCarNumber"] + "','" + Now + "','" + param["VedioEqType"] + "'," + param["IsUse"] + ",'" + ele["Id"] + "'," + param["BeatDatetime"] + "," + param["IsOnlineValue"] + "),";
                }
                pstr = pstr.Substring(0, pstr.Length - 1);
                Hashtable paramGW = new Hashtable();
                paramGW.Add("Value", pstr);
                dao.InsertAbpVideoCars(paramGW);
                result.Add("result", "success");
            }
            catch (Exception e)
            {
                result.Add("result", "false");
                result.Add("msg", e.ToString());
            }
            return result;
        }
        //巡检车解绑
        public Hashtable DeleteVideoCars(String input)
        {
            Hashtable result = new Hashtable();
            try
            {
                var param = JsonConvert.DeserializeObject<Hashtable>(input);
                CarVideoDao dao = new CarVideoDao();
                dao.DeleteVideoCars(param);
                result.Add("result", "success");
            }
            catch (Exception e)
            {
                result.Add("result", "false");
                result.Add("msg", e.ToString());
            }
            return result;
        }
        //删除巡检车信息
        public Hashtable DeleteVideoCarsByNumber(CarVideoParamModel input)
        {
            Hashtable result = new Hashtable();
            try
            {
                CommonService cs = new CommonService();
                Hashtable param = cs.InputAnalysis<CarVideoParamModel>(input);
                CarVideoDao dao = new CarVideoDao();
                param["VideoCarNumber"] = param["id"];
                dao.DeleteVideoCarsByNumber(param);
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
        //巡检车信息修改
        public Hashtable UpdateVideoCars(CarVideoParamModel input)
        {
            Hashtable result = new Hashtable();
            try
            {
                Hashtable param = new Hashtable();
                var IsUse = 0;
                switch (input.IsUse)
                {
                    case "启用":
                        IsUse = 1;
                        break;
                    case "未启用":
                        IsUse = 0;
                        break;
                }
                var VedioEqType = 0;
                switch (input.VedioEqType)
                {
                    case "二轮巡检车":
                        VedioEqType = 1;
                        break;
                    case "四轮巡检车":
                        VedioEqType = 2;
                        break;
                }
                param.Add("IsUse", IsUse);
                param.Add("VedioEqType", VedioEqType);
                param.Add("Id", input.id);
                param.Add("VideoCarNumber", input.VideoCarNumber);
                CarVideoDao dao = new CarVideoDao();
                dao.UpdateVideoCars(param);
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
        //巡检车在线率列表
        public Hashtable SelectVideoCarHeartBeatList(CarVideoParamModel input)
        {
            Hashtable result = new Hashtable();
            try
            {
                CommonService cs = new CommonService();
                Hashtable param = cs.InputAnalysis<CarVideoParamModel>(input);
                CarVideoDao dao = new CarVideoDao();
                IList<Hashtable> List = dao.SelectVideoCarHeartBeatList(param);
                IList<int> List2 = dao.SelectVideoCarHeartBeatCount(param);
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
        //配置参数查询
        public Hashtable SelectAbpParamConfig(Hashtable input)
        {
            Hashtable result = new Hashtable();
            try
            {
                CommonService cs = new CommonService();
                CarVideoDao dao = new CarVideoDao();
                IList<Hashtable> List = dao.SelectAbpParamConfig(input);
                result.Add("result", "success");
                result.Add("rows", List);
            }
            catch (Exception e)
            {
                result.Add("result", "false");
                result.Add("msg", e.ToString());
            }
            return result;
        }
        //配置参数插入
        public Hashtable InsertAbpParamConfig(Hashtable input)
        {
            Hashtable result = new Hashtable();
            try
            {
                CommonService cs = new CommonService();
                CarVideoDao dao = new CarVideoDao();
                dao.InsertAbpParamConfig(input);
                result.Add("result", "success");
            }
            catch (Exception e)
            {
                result.Add("result", "false");
                result.Add("Message", e.ToString());
            }
            return result;
        }
        //配置参数更新
        public Hashtable UpdateAbpParamConfig(String input)
        {
            Hashtable result = new Hashtable();
            try
            {
                var param = JsonConvert.DeserializeObject<Hashtable>(input);
                CommonService cs = new CommonService();
                CarVideoDao dao = new CarVideoDao();
                dao.UpdateAbpParamConfig(param);
                result.Add("result", "success");
            }
            catch (Exception e)
            {
                result.Add("result", "false");
                result.Add("Message", e.ToString());
            }
            return result;
        }
        //首页控制台图表展示上
        public Hashtable GetEqDetalControlInfo(String input)
        {
            Hashtable result = new Hashtable();
            try
            {
                Hashtable rows = new Hashtable();
                var param = JsonConvert.DeserializeObject<Hashtable>(input);
                CommonService cs = new CommonService();
                CarVideoDao dao = new CarVideoDao();
                IList<int> DetailNum= dao.GetAllDetail(param);
                IList<int> BerthNum = dao.GetAllBerth(param);
                IList<int> EquipmentNum = dao.GetAllEquipment(param);
                IList<int> AlertNum = dao.GetAllAlert(param);
                rows.Add("DetailNum", DetailNum[0]);
                rows.Add("BerthNum", BerthNum[0]);
                rows.Add("EquipmentNum", EquipmentNum[0]);
                rows.Add("AlertNum", AlertNum[0]);
                result.Add("result", "success");
                result.Add("rows", rows);
            }
            catch (Exception e)
            {
                result.Add("result", "false");
                result.Add("msg", e.ToString());
            }
            return result;
        }
        //首页控制台图表展示下
        public Hashtable GetEqDetalChart()
        {
            Hashtable result = new Hashtable();
            try
            {
                Hashtable rows = new Hashtable();
                List<object> state = new List<object>();
                Hashtable param = new Hashtable();
                List<string> dateList = new List<string>();
                List<int> detailList = new List<int>();
                var dateStr = "";
                DateTime d = DateTime.Now;
                for (int i = 0; i < 10; i++)
                {
                    dateList.Add(d.AddDays(-i).ToString("yyyy-MM-dd"));
                    dateStr = dateStr + "'" + d.AddDays(-i).ToString("yyyy-MM-dd") + "',";
                    detailList.Add(0);
                }

                dateStr = dateStr.Substring(0, dateStr.Length - 1);
                dateList.Reverse();
                CommonService cs = new CommonService();
                CarVideoDao dao = new CarVideoDao();
                param.Add("CreationTime", dateStr);
                //全部订单
                IList<Hashtable> ListAll= dao.GetDetailGroup(param);
                foreach (Hashtable ele in ListAll) {
                    var idx=dateList.IndexOf(Convert.ToString(ele["CreationTime"]));
                    detailList[idx] = Convert.ToInt32(ele["Num"]);
                }
                state.Add(dateList);
                state.Add(detailList);
                rows.Add("Alldd", state);
                //有效订单
                detailList = new List<int>();
                for (int i = 0; i < 10; i++)
                {
                    detailList.Add(0);
                }
                param.Add("AuditStatus", 1);
                IList<Hashtable> ListYx = dao.GetDetailGroup(param);
                foreach (Hashtable ele in ListYx)
                {
                    var idx = dateList.IndexOf(Convert.ToString(ele["CreationTime"]));
                    detailList[idx] = Convert.ToInt32(ele["Num"]);
                }
                state = new List<object>();
                state.Add(dateList);
                state.Add(detailList);
                rows.Add("Yxdd", state);
                //无效订单
                detailList = new List<int>();
                for (int i = 0; i < 10; i++)
                {
                    detailList.Add(0);
                }
                param["AuditStatus"]=3;
                IList<Hashtable> ListWx = dao.GetDetailGroup(param);
                foreach (Hashtable ele in ListWx)
                {
                    var idx = dateList.IndexOf(Convert.ToString(ele["CreationTime"]));
                    detailList[idx] = Convert.ToInt32(ele["Num"]);
                }
                state = new List<object>();
                state.Add(dateList);
                state.Add(detailList);
                rows.Add("Wxdd", state);
                //
                result.Add("result", "success");
                result.Add("rows", rows);
            }
            catch (Exception e)
            {
                result.Add("result", "false");
                result.Add("msg", e.ToString());
            }
            return result;
        }

        public Hashtable UserInitialization(String input)
        {
            Hashtable result = new Hashtable();
            try
            {
                var param = JsonConvert.DeserializeObject<Hashtable>(input);
                CarVideoDao dao = new CarVideoDao();
                var Now = DateTime.Now.ToLocalTime().ToString();
                param.Add("now", Now);
                dao.DeleteAbpSettingById(param);
                dao.InsertAbpSetting(param);
                result.Add("result", "success");
            }
            catch (Exception e)
            {
                result.Add("result", "false");
                result.Add("msg", e.ToString());
            }
            return result;
        }
    }
}
