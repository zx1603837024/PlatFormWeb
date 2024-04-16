using System;

namespace P4.SystemManager.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class DBerthDto
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid guid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ParkId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CardNo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Prepaid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BerthNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SensorNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BerthsecName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BerthStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ParkName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RegionName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RelateNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? InCarTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? OutCarTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string InCarTimeStr { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OutCarTimeStr { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int64 Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CarType { get; set; }
    }
}
