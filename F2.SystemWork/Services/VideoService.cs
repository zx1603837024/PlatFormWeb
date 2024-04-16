using F2.SystemWork.Daos;
using F2.SystemWork.Models;
using Newtonsoft.Json;
using P4.Berths;
using P4.Sensors.Dtos;
using P4.VideoEquipBusinessDetails.Dtos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace F2.SystemWork.Services
{
    public class VideoService
    {
        //查询VideoEquips唯一设备列表
        public Hashtable SelectVideoEquipsDistinct(VideoGraphParamModel input) {
            Hashtable result = new Hashtable();
            try
            {
                CommonService cs = new CommonService();
                Hashtable param = cs.InputAnalysis<VideoGraphParamModel>(input);
                VideoDao dao = new VideoDao();
                IList<VideoSettingModel> List = dao.SelectVideoEquipsDistinct(param);
                IList<Hashtable> List2 = dao.SelectVideoEquipsDistinctCount(param);
                int records = 0;
                if (List2.Count > 0)
                {
                   records = List2.Count;
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

        /**
         * 臻识使用查询设备接口
         * 传设备编号/查所有
         * 
         */
        public Hashtable getVideoEquips(VideoQuery input)
        {
            Hashtable result = new Hashtable();
            try
            {
                CommonService cs = new CommonService();
                Hashtable param = cs.InputAnalysis<VideoQuery>(input);
                VideoDao dao = new VideoDao();
                IList<VideoEquipsModel> list = dao.getVideoEquips(param);
                foreach (VideoEquipsModel view in list)
                {
                    Hashtable berthseParam = new Hashtable();
                    berthseParam.Add("VideoEqNumber", view.videoEqNumber);
                    IList<VideoBerthModel> berthseList = dao.getVideoBerth(berthseParam);
                    if(berthseList.Count>0)
                    {
                        List<string> berthseNumbers = new List<string>();
                        foreach (VideoBerthModel berth in berthseList)
                        {
                            if (berth.berthNumber != null)
                            {
                                berthseNumbers.Add(berth.berthNumber);
                            }
                        }
                        view.berthsecName = berthseList[0].berthsecName;
                        view.berthNumbers = berthseNumbers;
                    }
                    
                }
                result.Add("code", 200);
                result.Add("data", list);
                result.Add("msg", "成功");
            }
            catch (Exception e)
            {
                result.Add("code", 1001);
                result.Add("msg", "数据查询异常");
            }
            return result;
        }
        /**
         * 
         * 查询泊位段信息
         */
        public Hashtable getVideoBerthsec()
        {
            Hashtable result = new Hashtable();
            VideoDao dao = new VideoDao();
            IList<BerthsecView> list = dao.getVideoBerthsec();
            foreach (BerthsecView view in list)
            {
                Hashtable param = new Hashtable();
                param.Add("BerthsecId", view.berthCount);
                view.berthCount = (int)dao.getVideoBerthsecCount(param);
            }
            result.Add("code", 200);
            result.Add("data", list);
            result.Add("msg", "成功");
            return result;
        }
        /**
         * 臻识使用查询泊位接口
         * 传设备编号查询这个设备下的泊位
         * 传泊位编号查单个泊位
         *  
         */
        public Hashtable getVideoBerth(VideoQuery input)
        {
            Hashtable result = new Hashtable();
            try
            {
                CommonService cs = new CommonService();
                Hashtable param = cs.InputAnalysis<VideoQuery>(input);
                VideoDao dao = new VideoDao();
                IList<VideoBerthModel> list = dao.getVideoBerth(param);
                result.Add("code", 200);
                result.Add("data", list);
                result.Add("msg", "成功");
            }
            catch (Exception e)
            {
                result.Add("code", 1001);
                result.Add("msg", "数据查询异常");
            }
            return result;
            
        }
        
        public Hashtable capture(CaptureQuery query)
        {
            Hashtable result = new Hashtable();
            Hashtable data = new Hashtable();
            CommonService cs = new CommonService();
            Hashtable param = cs.InputAnalysis<CaptureQuery>(query);
            VideoDao dao = new VideoDao();
            try
            {
                IList<Hashtable> list = dao.capture(param);
                object total = dao.captureSize(param);
                result.Add("msg", "成功");
                result.Add("code", 200);
                data.Add("records", list);
                data.Add("total", total);
                result.Add("data", data);
            }catch(Exception e)
            {
                result.Add("msg", "数据查询失败");
                result.Add("code", 1001);
            }
            
            return result;
        }
        //查询VideoEquips列表
        public Hashtable SelectVideoEquips(VideoGraphParamModel input)
        {
            Hashtable result = new Hashtable();
            try {
                CommonService cs = new CommonService();
                Hashtable param = cs.InputAnalysis<VideoGraphParamModel>(input);
                VideoDao dao = new VideoDao();
                IList<VideoSettingModel> List = dao.SelectVideoEquipsList(param);
                IList<int> List2 = dao.SelectVideoEquipsCount(param);
                int records = 0;
                if (List2.Count > 0)
                {
                    records = List2[0];
                }
                //if (input.VedioEqType=="4") {
                    //高位摄像
                    foreach (VideoSettingModel ele in List) {
                        if (ele.BerthNumber== null) {
                            List.Remove(ele);
                            break;
                        }
                    }
                    records = records - 1;
                //}
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


        //查询停车场列表
        public Hashtable SelectParkInfoList()
        {
            Hashtable result = new Hashtable();
            try
            {
                VideoDao dao = new VideoDao();
                IList<VideoSettingModel> List = dao.SelectParkInfoList();
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
        //查询泊位段列表
        public Hashtable SelectBerthsecsInfoList(String input)
        {
            Hashtable result = new Hashtable();
            try
            {
                var param = JsonConvert.DeserializeObject<Hashtable>(input);
                VideoDao dao = new VideoDao();
                IList<VideoSettingModel> List = dao.SelectBerthsecsInfoList(param);
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
        //查询未绑定泊位
        public Hashtable SelectNoBerthsecsNumber(String input)
        {
            Hashtable result = new Hashtable();
            try
            {
                var param = JsonConvert.DeserializeObject<Hashtable>(input);
                VideoDao dao = new VideoDao();
                IList<VideoSettingModel> List = dao.SelectNoBerthsecsNumber(param);
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
        //视频设备解绑
        public Hashtable UpdateVideoEquipsNull(String input)
        {
            Hashtable result = new Hashtable();
            try
            {
                var param = JsonConvert.DeserializeObject<Hashtable>(input);
                VideoDao dao = new VideoDao();
                dao.UpdateVideoEquipsNull(param);
                result.Add("result", "success");
            }
            catch (Exception e)
            {
                result.Add("result", "false");
                result.Add("msg", e.ToString());
            }
            return result;
        }
        //视频设备绑定
        public Hashtable UpdateVideoEquipsBand(String input)
        {
            Hashtable result = new Hashtable();
            try
            {
                var param = JsonConvert.DeserializeObject<Hashtable>(input);
                VideoDao dao = new VideoDao();
                dao.UpdateVideoEquipsBand(param);
                result.Add("result", "success");
            }
            catch (Exception e)
            {
                result.Add("result", "false");
                result.Add("msg", e.ToString());
            }
            return result;
        }
        //视频设备编辑
        public Hashtable UpdateVideoEquips(VideoSettingModel input)
        {
            Hashtable result = new Hashtable();
            try
            {
                Hashtable param = new Hashtable();
                param.Add("Id", input.Id);
                param.Add("IsUse", input.IsUse);
                param.Add("VedioEqType", input.VedioEqType);
                param.Add("VedioEqNumber", input.VedioEqNumber);
                var Now = DateTime.Now.ToLocalTime().ToString();
                VideoDao dao = new VideoDao();

                dao.UpdateVideoEquips(param);
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
        //批量插入视频设备数据
        public Hashtable InsertVideoEquipsBatch(String input)
        {
            Hashtable result = new Hashtable();
            try
            {
                var param = JsonConvert.DeserializeObject<Hashtable>(input);
                var EqinfoR = Convert.ToString(param["Eqinfo"]).Replace(" ", "");
                var Eqinfo = EqinfoR.Replace("\n", ",");
                var Eqtype = Convert.ToString(param["Eqtype"]);
                var Now = DateTime.Now.ToLocalTime().ToString();
                string[] paramArr = Eqinfo.Split(',');
                var val = "";
                for (int i = 0; i < paramArr.Length; i++)
                {
                    if (val.Length == 0)
                    {
                        if (!string.IsNullOrEmpty(paramArr[i])) {
                            val = "('" + paramArr[i] + "','" + Eqtype + "','" + Now + "','1','" + Now + "')";
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(paramArr[i])) {
                            val = val + ",('" + paramArr[i] + "','" + Eqtype + "','" + Now + "','1','" + Now + "')";
                        }
                    }
                }
                Hashtable param2 = new Hashtable();
                param2.Add("Value", val);
                VideoDao dao = new VideoDao();
                if (Eqtype == "4")
                {//高位摄像批量新增 
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
                    paramDistinct.Add("VedioEqNumbers", vals);
                    IList<VideoSettingModel> List = dao.SelectVideoEquipsDistinct(paramDistinct);
                    if (List.Count() == 0)
                    {
                        dao.InsertVideoEquipsBatch(param2);
                        result.Add("result", "success");
                    }
                    else {
                        var str = "";
                        foreach (VideoSettingModel ele in List) {
                            str+=ele.VedioEqNumber+",";
                        }
                        result.Add("result", "false");
                        result.Add("msg", "视频设备"+ str + "已存在");
                    }
                }
                else {
                    dao.InsertVideoEquipsBatch(param2);
                    result.Add("result", "success");
                }
            }
            catch (Exception e)
            {
                result.Add("result", "false");
                result.Add("msg", e.ToString());
            }
            return result;
        }
        //视频设备删除
        public Hashtable DeleteVideoEquips(VideoSettingModel input)
        {
            Hashtable result = new Hashtable();
            try
            {
                Hashtable param = new Hashtable();
                param.Add("Id", input.Id);
                VideoDao dao = new VideoDao();
                dao.DeleteVideoEquips(param);
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
        //查询心跳列表
        public Hashtable SelectVideoHeartBeat(VideoGraphParamModel input)
        {
            Hashtable result = new Hashtable();
            try
            {
                CommonService cs = new CommonService();
                Hashtable param=cs.InputAnalysis<VideoGraphParamModel>(input);
                VideoDao dao = new VideoDao();
                IList<VideoGraphModel> List = dao.SelectVideoHeartBeatList(param);
                IList<int> List2 = dao.SelectVideoHeartBeatCount(param);
                int records = 0;
                if (List2.Count > 0)
                {
                    records = List2[0];
                }
                result.Add("result", "success");
                result.Add("rows", List);
                result.Add("records", records);
                if (input.rows!=null && input.rows != 0) {
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
        //首页统计图
        public Hashtable SelectVideoEqOnlineCount() {
            Hashtable result = new Hashtable();
            try
            {
                VideoDao dao = new VideoDao();
                Hashtable param = new Hashtable();
                param.Add("VedioEqType","1,2");
                IList<int> ListSpAll = dao.SelectVideoEqOnlineCount(param);//视频桩总数
                param = new Hashtable();
                param.Add("VedioEqType", "1,2");
                param.Add("IsOnlineValue", "1");
                IList<int> ListSpOnline = dao.SelectVideoEqOnlineCount(param);//视频桩在线
                param = new Hashtable();
                param.Add("VedioEqType", "4");
                IList<int> ListGwAll = dao.SelectVideoEqOnlineCount(param);//高位总数
                param = new Hashtable();
                param.Add("VedioEqType", "4");
                param.Add("IsOnlineValue", "1");
                IList<int> ListGwOnline = dao.SelectVideoEqOnlineCount(param);//高位在线
                result.Add("SpAll", ListSpAll[0]);
                result.Add("SpOnline", ListSpOnline[0]);
                result.Add("GwAll", ListGwAll[0]);
                result.Add("GwOnline", ListGwOnline[0]);
                result.Add("result", "success");
            }
            catch (Exception e)
            {
                result.Add("result", "false");
                result.Add("msg", e.ToString());
            }
            return result;
        }
        //欠费列表展示
        public Hashtable SelectArrearageDetail(ArrearageParamModel input) {
            Hashtable result = new Hashtable();
            try
            {
                CommonService cs = new CommonService();
                Hashtable param = cs.InputAnalysis(input);
                VideoDao dao = new VideoDao();
                IList<Hashtable> List= dao.SelectArrearageDetail(param);
                IList<int> List2 = dao.SelectArrearageDetailCount(param);
                int records = 0;
                if (List2.Count > 0)
                {
                    records = List2[0];
                }
                result.Add("result", "success");
                result.Add("rows", List);
                result.Add("records", records);
                var Arrearage = 0;
                Hashtable userdata = new Hashtable();
                userdata.Add("PlateNumber","合计");
                foreach (Hashtable ele in List) {
                    Arrearage = Arrearage + Convert.ToInt32(ele["Arrearage"]);
                }
                userdata.Add("Arrearage", Arrearage);
                result.Add("userdata", userdata);
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
        //查询收费员信息
        public Hashtable SelectEmployeesInfo(String input)
        {
            Hashtable result = new Hashtable();
            try
            {
                CommonService cs = new CommonService();
                var param = JsonConvert.DeserializeObject<Hashtable>(input);
                VideoDao dao = new VideoDao();
                IList<Hashtable> List = dao.SelectEmployeesInfo(param);
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
        //欠费追缴
        public Hashtable UpdateBusinessDetailArrearage(String input)
        {
            Hashtable result = new Hashtable();
            try
            {
                CommonService cs = new CommonService();
                var param = JsonConvert.DeserializeObject<Hashtable>(input);
                VideoDao dao = new VideoDao();
                dao.InsertBusinessDetailRecovery(param);
                dao.UpdateBusinessDetailArrearage(param);
                result.Add("result", "success");
            }
            catch (Exception e)
            {
                result.Add("result", "false");
                result.Add("msg", e.ToString());
            }
            return result;
        }
        //高位视频设备绑定
        public Hashtable BandVideoEquipsGW(String input)
        {
            Hashtable result = new Hashtable();
            try
            {
                var param = JsonConvert.DeserializeObject<Hashtable>(input);
                if (Convert.ToString(param["BeatDatetime"]).Length == 0)
                {
                    param["BeatDatetime"] = "null";
                }
                else {
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
                VideoDao dao = new VideoDao();
                IList<Hashtable> List= dao.SelectBerthsecsNumberGW(param);
                var Now = DateTime.Now.ToLocalTime().ToString();
                var pstr = "";
                foreach (Hashtable ele in List) {
                    pstr = pstr + "('" + ele["TenantId"] + "','"+ ele["CompanyId"] +"','" + ele["RegionId"] + "','" + ele["ParkId"] + "','" + ele["BerthsecId"] + "','" + ele["BerthNumber"] + "','" + param["VedioEqNumber"] + "','" + Now + "','" + param["VedioEqType"] + "'," + param["IsUse"] + ",'" + ele["Id"] + "'," + param["BeatDatetime"] + "," + param["IsOnlineValue"] + "),";
                }
                pstr = pstr.Substring(0, pstr.Length-1);
                Hashtable paramGW = new Hashtable();
                paramGW.Add("Value", pstr);
                dao.InsertVideoEquipsGW(paramGW);
                result.Add("result", "success");
            }
            catch (Exception e)
            {
                result.Add("result", "false");
                result.Add("msg", e.ToString());
            }
            return result;
        }
        //按视频设备编号删除设备
        public Hashtable DeleteVideoEquipsByEquips(VideoGWModel input)
        {
            Hashtable result = new Hashtable();
            try
            {
                Hashtable param = new Hashtable();
                string[] paramArr = input.id.Split(',');
                var val = "";
                for (int i = 0; i < paramArr.Length; i++)
                {
                    if (val.Length == 0)
                    {
                        val = "('" + paramArr[i] + "')";
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(paramArr[i]))
                        {
                            val = val + ",('" + paramArr[i] + "')";
                        }
                    }
                }
                param.Add("VedioEqNumber", val);
                VideoDao dao = new VideoDao();
                dao.DeleteVideoEquipsByEquips(param);
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
        
        //高位摄像编辑
        public Hashtable UpdateVideoEquipsGW(VideoGWModel input)
        {
            Hashtable result = new Hashtable();
            try
            {
                Hashtable param = new Hashtable();
                var Now = DateTime.Now.ToLocalTime().ToString();
                var IsUse =0;
                param.Add("VedioEqNumber", input.VedioEqNumber);
                VideoDao dao = new VideoDao();
                IList<VideoSettingModel> List = dao.SelectVideoEquipsDistinct(param);
                switch (input.IsUse)
                {
                    case "1":
                        IsUse=1;
                        if (List[0].Remark == input.Remark)
                        {
                            param.Add("EnableTime", Now);
                        }
                        param.Add("StopTime", null);
                        break;
                    case "0":
                        IsUse = 0;
                        if (List[0].Remark == input.Remark)
                        {
                            param.Add("StopTime", Now);
                        }
                        break;
                }
                var VedioEqType = 0;
                switch (input.VedioEqType)
                {
                    case "有线供电视频桩":
                        VedioEqType = 1;
                        break;
                    case "无源免布线视频桩":
                        VedioEqType = 2;
                        break;
                    case "路牙机":
                        VedioEqType = 3;
                        break;
                    case "高位摄像":
                        VedioEqType = 4;
                        break;
                }
                param.Add("IsUse", IsUse);
                param.Add("VedioEqType", VedioEqType);
                param.Add("Remark", input.Remark);
                param.Add("VedioEqNumber_s", input.id);
                dao.UpdateVideoEquipsGW(param);
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
        //视频设备停车数据
        public Hashtable GetAllVideoEqBusinessDetaillist(VideoEqBusinessDetailModel input) {
            Hashtable result = new Hashtable();
            try
            {
                CommonService cs = new CommonService();
                Hashtable param = cs.InputAnalysis<VideoEqBusinessDetailModel>(input);
                VideoDao dao = new VideoDao();
                IList<VideoEqBusinessDetailModel> List= dao.GetVideoEquipBusinessDetail(param);
                IList<int> List2 = dao.GetVideoEquipBusinessDetailCount(param);
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
        //订单状态审核
        public Hashtable UpdateBusinessDetailAuditStatus(String input)
        {
            Hashtable result = new Hashtable();
            try
            {
                var param = JsonConvert.DeserializeObject<Hashtable>(input);
                CommonService com = new CommonService();
                VideoDao dao = new VideoDao();
                CarVideoDao dao2 = new CarVideoDao();
                //华峰废弃订单发送
                var PicPath = "http://36.139.2.80:8090/VideoPic/";
                IList<Hashtable> UrlList= dao2.SelectAbpParamConfig(param);
                string url = Convert.ToString(UrlList[0]["Url"]);
                if (param["AuditStatus"].ToString()=="2") {
                    IList<Hashtable> res =dao.SelectFqDetail(param);
                    foreach (Hashtable ele in res) {
                        ele.Add("Evt", 617);
                        ele["OssPathURL"] = null;
                        ele["OutOssPathURL"] = null;
                        com.GetTokenByHF(url+ "/api/ev/ssl/receive", JsonConvert.SerializeObject(ele).ToString(), url);
                    }
                }
                if (string.IsNullOrEmpty(Convert.ToString(param["PostState"]))){
                    param["PostState"] = null;
                }
                dao.UpdateBusinessDetailAuditStatus(param);
                result.Add("result", "success");
            }
            catch (Exception e)
            {
                result.Add("result", "false");
                result.Add("msg", e.ToString());
            }
            return result;
        }
        //订单状态审核
        public Hashtable UpdateDetailPlateNumber(CreateOrUpdateVideoEqBusDetail input)
        {
            Hashtable result = new Hashtable();
            try
            {
                Hashtable param = new Hashtable();
                param.Add("Id", input.id);
                param.Add("PlateNumber", input.PlateNumber);
                param.Add("PostState", input.PostState);
                VideoDao dao = new VideoDao();
                dao.UpdateVideoDetailPlateNumber(param);
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
        //视频设备异常数据
        public Hashtable GetAllVideoEquipFaults(VideoEqBusinessDetailModel input)
        {
            Hashtable result = new Hashtable();
            try
            {
                CommonService cs = new CommonService();
                Hashtable param = cs.InputAnalysis<VideoEqBusinessDetailModel>(input);
                VideoDao dao = new VideoDao();
                IList<Hashtable> List = dao.GetVideoEquipFaults(param);
                IList<int> List2 = dao.GetVideoEquipFaultsCount(param);
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
        //订单重新发送
        public Hashtable RePostProcess(String input)
        {
            Hashtable result = new Hashtable();
            try
            {
                var param = JsonConvert.DeserializeObject<Hashtable>(input);
                VideoDao dao = new VideoDao();
                dao.UBusinessDetailPostState(param);
                result.Add("result", "success");
            }
            catch (Exception e)
            {
                result.Add("result", "false");
                result.Add("msg", e.ToString());
            }
            return result;
        }
        //视频设备超时停车数据
        public Hashtable SelectLongTimeOccpy(VideoEqBusinessDetailModel input)
        {
            Hashtable result = new Hashtable();
            try
            {
                CommonService cs = new CommonService();
                Hashtable param = cs.InputAnalysis<VideoEqBusinessDetailModel>(input);
                CarVideoDao dao2 = new CarVideoDao();
                Hashtable paramd = new Hashtable();
                IList<Hashtable> List3 = dao2.SelectAbpParamConfig(paramd);
                if (List3.Count > 0)
                {
                    var OverTime = Convert.ToInt64(List3[0]["OrderOverTime"]);
                    param["StartTime"] = DateTime.Now.AddDays(-OverTime).ToString("yyy-MM-dd 00:00:00");
                    param["EndTime"] = DateTime.Now.ToString("yyy-MM-dd 23:59:59");
                    VideoDao dao = new VideoDao();
                    IList<VideoEqBusinessDetailModel> List = dao.SelectLongTimeOccpy(param);
                    IList<int> List2 = dao.SelectLongTimeOccpyCount(param);
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

            }
            catch (Exception e)
            {
                result.Add("result", "false");
                result.Add("msg", e.ToString());
            }
            return result;
        }
        //华峰数据维护
        public Hashtable HFDataRedeem() {
            Hashtable result = new Hashtable();
            try
            {
                Hashtable param = new Hashtable();
                CommonService com = new CommonService();
                //华峰废弃订单发送
                param.Add("Evt",617);
                param.Add("OssPathURL", null);
                param.Add("OutOssPathURL", null);
                param.Add("BerthNumber", "821254");
                param.Add("CarOutTime", "");
                param.Add("PlateColor", 1);
                param.Add("VedioEqNumber", "f972b674-66692d63");
                param.Add("CarInTime", "2023-03-05 09:57:50");
                param.Add("PlateNumber", "粤S06K03");
                string url = "https://songshanhu.sslkxczhjt.cn";
                com.GetTokenByHF(url + "/api/ev/ssl/receive", JsonConvert.SerializeObject(param).ToString(), url);
                
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
