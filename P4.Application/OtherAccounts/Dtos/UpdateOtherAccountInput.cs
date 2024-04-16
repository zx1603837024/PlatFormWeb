using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.OtherAccounts.Dtos
{
    public class UpdateOtherAccountInput : EntityRequestInput, IInputDto
    {
        public string UserName { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string TelePhone { get; set; }

        public bool IsPhoneConfirmed { get; set; }
    }
}
