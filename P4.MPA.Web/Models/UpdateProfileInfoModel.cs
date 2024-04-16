using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P4.Web.Models
{
    public class UpdateProfileInfoModel
    {
        public string NewPassord { get; set; }

        public string ConfirmPassword { get; set; }

        public bool Gender { get; set; }

        public string UserName { get; set; }

        public DateTime Birthday { get; set; }

        public string Surname { get; set; }

        public string EmailAddress { get; set; }

        public string Telephone { get; set; }
    }
}