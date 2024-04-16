using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Businesses.Dtos
{
    /// <summary>
    /// 接口执行结果以及错误详细
    /// </summary>
   public class BMessage
    {
       /// <summary>
       /// 是否成功  true false
       /// </summary>
       public bool B_success { get; set; }
       /// <summary>
       /// 错误码
       /// </summary>
       public int Code { get; set; }
       /// <summary>
       /// 错误信息
       /// </summary>
       public string ErrorMessage { get; set; }
       /// <summary>
       /// 成功信息
       /// </summary>
       public string SuccessMessage { get; set; }

        /// <summary>
        /// 
        /// </summary>
       public byte[] Image { get; set; }
    }
}
