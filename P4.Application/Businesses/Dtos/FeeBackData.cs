using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Businesses.Dtos
{
    public class FeeBackData
    {
        public int id { get; set; }
        public decimal Repayment { get; set; }
        public DateTime CarRepaymentTime { get; set; }
        public bool IsEscapePay { get; set; }
        public short EscapePayStatus { get; set; }
        public short PaymentType { get; set; }
        public string CardNo { get; set; }
        public decimal BeforeDiscount { get; set; }
        public decimal DiscountMoney { get; set; }
        public double DiscountRate { get; set; }
        public long UserId { get; set; }
        public string DeviceCode { get; set; }
        public int TenantId { get; set; }

    }
}
