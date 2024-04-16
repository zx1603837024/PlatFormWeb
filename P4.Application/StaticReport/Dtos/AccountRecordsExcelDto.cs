using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.StaticReport.Dtos
{
    public class AccountRecordsExcelDto
    {
        public string UserName { get; set; }

        public string Name { get; set; }

        public string InTimeStr { get; set; }

        public string CardNo { get; set; }

        public string TelePhone { get; set; }

        public string Remark { get; set; }

        public decimal Money { get; set; }

        public decimal Wallet { get; set; }

    }
}
