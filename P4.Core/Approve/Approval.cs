using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Approve
{
    /// <summary>
    /// 审批记录
    /// </summary>
    [Table("AbpApproval")]
    public class Approval : Entity<long>
    {

    }
}
