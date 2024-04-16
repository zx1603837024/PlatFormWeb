using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Businesses.Dtos
{
    /// <summary>
    /// 费用补缴  
    /// </summary>
  public  class FeeBackBusiness
    {
      public string id { get; set; }
      public string parameters { get; set; }

      public double userid { get; set; }

      public int  tenantid { get; set; }
    }
}
