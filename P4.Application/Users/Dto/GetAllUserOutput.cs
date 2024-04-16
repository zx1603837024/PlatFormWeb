using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Users.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public  class GetAllUserOutput : PagedResultOutput<User>, IOutputDto
    {
        /// <summary>
        /// 
        /// </summary>
        public List<UserDto> rows { get; set; }
    }
}
