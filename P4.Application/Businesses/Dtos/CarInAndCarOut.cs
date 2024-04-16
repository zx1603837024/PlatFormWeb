using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Businesses.Dtos
{
    /// <summary>
    /// 进出场完整记录
    /// </summary>
   public class CarInAndCarOut
    {
       /// <summary>
       /// 进场方法
       /// </summary>
       public string methodnameIn { get; set; }
       /// <summary>
       /// 出场方法
       /// </summary>
       public string methodnameOut { get; set; }
       /// <summary>
       /// 用户ID
       /// </summary>
       public double userid { get; set; }
       /// <summary>
       /// 进场json串
       /// </summary>
       public string parametersIn { get; set; }
       /// <summary>
       /// 出场json串
       /// </summary>
       public string parametersOut { get; set; }
    }
}
