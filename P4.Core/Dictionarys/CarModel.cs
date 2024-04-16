using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P4.Dictionarys
{
    /// <summary>
    /// 车辆类型
    /// </summary>
    [Table("AbpCarModels")]
    public class CarModel : Entity<int>, IPassivable
    {
        /// <summary>
        /// 
        /// </summary>
        public int ModelValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(20)]
        public string ModelName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsActive { get; set; }
    }
}