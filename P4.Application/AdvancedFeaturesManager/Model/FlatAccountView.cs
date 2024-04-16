using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.AdvancedFeaturesManager.Model
{

    /// <summary>
    /// 平账
    /// </summary>
    public class FlatAccountView
    {
        /// <summary>
        /// 当前登录用户所属商户id
        /// </summary>
        public int? TenantId { get; set; }

        /// <summary>
        /// 当前登录用户所属分公司
        /// </summary>
        public List<int> CompanyIds { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? CompanyId { get; set; }

        /// <summary>
        /// 收费时间-开始
        /// </summary>
        public string operateDateBegin { get; set; }

        /// <summary>
        /// 收费时间-结束
        /// </summary>
        public string operateDateEnd { get; set; }

        /// <summary>
        /// 开始
        /// </summary>
        public DateTime begindt
        {
            get
            {
                return string.IsNullOrWhiteSpace(operateDateBegin) == true ? DateTime.Now.Date : DateTime.Parse(operateDateBegin);
            }
        }

        /// <summary>
        /// 结束
        /// </summary>
        public DateTime enddt
        {
            get
            {
                return string.IsNullOrWhiteSpace(operateDateEnd) == true ? DateTime.Now.AddDays(1).Date : DateTime.Parse(operateDateEnd);
            }
        }

        /// <summary>
        /// 是否删除（逻辑删除）
        /// </summary>
        public int? isdeleted
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// 泊位段
        /// </summary>
        public int BerthsecId { get; set; }

        /// <summary>
        /// 收费员工号
        /// </summary>
        public string InOperaId { get; set; }


        /// <summary>
        /// 支付状态（查询已支付）
        /// </summary>
        public int? PayStatus
        {
            get
            {
                return 1;
            }
        }

        /// <summary>
        /// 平账金额 总输入(应收)和实收金额之间的差额(例如：-5.00 、 5.00)
        /// </summary>
        public decimal AverageAccount { get; set; }
    }

    /// <summary>
    /// 收费员平账
    /// </summary>
    public class getBerthsecAllListByEmpIdInput
    {
        /// <summary>
        /// 当前登录用户所属商户id
        /// </summary>
        public int? TenantId { get; set; }

        /// <summary>
        /// 当前登录用户所属分公司
        /// </summary>
        public List<int> CompanyIds { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? CompanyId { get; set; }

        /// <summary>
        /// 收费员工号
        /// </summary>
        public string EmployeeId { get; set; }
    }
}
