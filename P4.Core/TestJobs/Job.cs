using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.TestJobs
{
    [Table("AbpJobs")]
    public class Job : Entity
    {
        /// <summary>
        /// 
        /// </summary>
        [MaxLength(20)]
        public virtual string JobContent { get; set; }
        public virtual DateTime InTime { get; set; }

    }
}
