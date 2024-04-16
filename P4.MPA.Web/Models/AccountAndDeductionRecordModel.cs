using P4.OtherAccounts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4.Web.Models
{
    public class AccountAndDeductionRecordModel
    {
        public List<AccountAndDeductionRecordsDto> AccountList { get; set; }

        public List<AccountAndDeductionRecordsDto> ThisMonthAccountList { get; set; }

        public List<AccountAndDeductionRecordsDto> LastMonthAccountList { get; set; }

        public List<AccountAndDeductionRecordsDto> NextMonthAccountList { get; set; }
    }
}