using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Businesses.Dtos
{
    /// <summary>
    /// 出场实体
    /// </summary>
   public class InsertCarOutModel
    {
       public string receivable { get; set; }
       public string factReceive { get; set; }
       public string arrearage { get; set; }
       public string carOutTime { get; set; }
       public string carPayTime { get; set; }
       public string outOperaId { get; set; }
       public string guid { get; set; }
       public string sensorsOutCarTime { get; set; }
       public string sensorsStopTime { get; set; }
       public string sensorsReceivable { get; set; }
       public string payStatus { get; set; }
       public string isPay { get; set; }
       public string feeType { get; set; }
       public string stopTime { get; set; }
       public string money { get; set; }
       public string cardNo { get; set; }
       public string userid { get; set; }

    }
}
