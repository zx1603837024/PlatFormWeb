using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 地磁进场
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_sensorcarin_Click(object sender, EventArgs e)
        {
            string temp = "";
            bool flag = false;
            if (string.IsNullOrWhiteSpace(tx_url.Text))
            {
                temp += "服务器地址,";
                flag = true;
            }
            if (string.IsNullOrWhiteSpace(tx_sensor.Text))
            {
                temp += "地磁编号,";
                flag = true;
            }
            if (string.IsNullOrWhiteSpace(tx_Indicate.Text))
            {
                temp += "流水号,";
                flag = true;
            }
            if (flag)
            {
                MessageBox.Show(temp + "不能为空！");
                return;
            }
            #region MyRegion
            //HttpClient client = new HttpClient();
            //INmotionDto dto = new INmotionDto();
            //dto.cmd = "sendParkStatu";//进出场标记
            //INmotionBody body = new INmotionBody();
            //body.deviceID = tx_sensor.Text.Trim();
            //body.time = tx_carintime.Text.Trim();
            //body.parkID = "1";
            //body.battary = "100";
            //body.token = "C26E1B53DFF8069B8A1DC9F1CBAFC676";
            //body.rssi = "26";
            //body.passTime = "0";
            //body.sequence = tx_Indicate.Text.Trim();
            //body.parkingStatu = "1";
            //dto.body = body;
            //HttpContent httpContent = new StringContent("{\"dto\":" + JsonConvert.SerializeObject(dto) + "}");
            //string url = tx_url.Text.Trim() + "/api/services/f2/Sensor/SendDeviceByINmotion";
            //string result = string.Empty;
            //// //异步Post
            //HttpResponseMessage response = client.PostAsync(url, httpContent).Result;
            ////输出Http响应状态码
            //string statusCode = response.StatusCode.ToString();
            ////确保Http响应成功
            //if (response.IsSuccessStatusCode)
            //{
            //    //异步读取json
            //    result = response.Content.ReadAsStringAsync().Result;
            //}
            //string url = tx_url.Text.Trim() + "/api/services/f2/Sensor/SendDeviceByINmotionfa?cmd=sendParkStatu&deviceID=" + tx_sensor.Text.Trim() + "&time=" + tx_carintime.Text.Trim() + "&parkID=1&battary=100&token=C26E1B53DFF8069B8A1DC9F1CBAFC676&rssi=26&passTime=0&sequence=" + tx_Indicate.Text.Trim() + "&parkingStatu=1&flag=" + flag + "";
            #endregion
            string result = string.Empty;
            string url = tx_url.Text.Trim() + "/api/services/f2/Sensor/SendDeviceByINmotion";
            Dictionary<string, string> keyValues = new Dictionary<string, string>
                {
                { "cmd", "sendParkStatu" },
                { "deviceID", "" + tx_sensor.Text.Trim() + "" },
                { "time", "" + tx_carintime.Text.Trim() + "" },
                { "parkID", "1" },
                { "battary","100"},
                { "token", "C26E1B53DFF8069B8A1DC9F1CBAFC676"},
                { "rssi","26"},
                { "passTime","0"},
                { "sequence","" + tx_Indicate.Text.Trim() + "" },
                { "parkingStatu","1"},
                {"flag" ,""+flag+""}
                };
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "Post";
            request.ContentType = "application/json";//"application/x-www-form-urlencoded";
            #region Post请求参数
            StringBuilder builder = new StringBuilder();
            int i = 0;
            foreach (var item in keyValues)
            {
                if (i > 0)
                    builder.Append("&");
                builder.AppendFormat("{0}={1}", item.Key, item.Value);
                i++;
            }
            byte[] data = Encoding.UTF8.GetBytes(builder.ToString());
            request.ContentLength = data.Length;
            using (Stream reqStream = request.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();
            }
            #endregion
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                result = reader.ReadToEnd(); //结果
            }
        }

        /// <summary>
        /// 地磁出场
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_sensorcarout_Click(object sender, EventArgs e)
        {
            string temp = "";
            bool flag = false;
            if (string.IsNullOrWhiteSpace(tx_url.Text))
            {
                temp += "服务器地址,";
                flag = true;
            }
            if (string.IsNullOrWhiteSpace(tx_sensor.Text))
            {
                temp += "地磁编号,";
                flag = true;
            }
            if (string.IsNullOrWhiteSpace(tx_Indicate.Text))
            {
                temp += "流水号,";
                flag = true;
            }
            if (flag)
            {
                MessageBox.Show(temp + "不能为空！");
                return;
            }
            string result = string.Empty;
            string url = tx_url.Text.Trim() + "/api/services/f2/Sensor/SendDeviceByINmotion";
            Dictionary<string, string> keyValues = new Dictionary<string, string>
                {
                { "cmd", "sendParkStatu" },
                { "deviceID", "" + tx_sensor.Text.Trim() + "" },
                { "time", "" + tx_carouttime.Text.Trim() + "" },
                { "parkID", "1" },
                { "battary","100"},
                { "token", "C26E1B53DFF8069B8A1DC9F1CBAFC676"},
                { "rssi","26"},
                { "passTime","0"},
                { "sequence","" + tx_Indicate.Text.Trim() + "" },
                { "parkingStatu","0"},
                { "flag" ,"" + flag + ""}
                };
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "Post";
            request.ContentType = "application/json";//"application/x-www-form-urlencoded";
            #region Post请求参数
            StringBuilder builder = new StringBuilder();
            int i = 0;
            foreach (var item in keyValues)
            {
                if (i > 0)
                    builder.Append("&");
                builder.AppendFormat("{0}={1}", item.Key, item.Value);
                i++;
            }
            byte[] data = Encoding.UTF8.GetBytes(builder.ToString());
            request.ContentLength = data.Length;
            using (Stream reqStream = request.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();
            }
            #endregion
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                result = reader.ReadToEnd(); //结果
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            tx_carintime.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
            tx_carouttime.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
        }
    }
}
