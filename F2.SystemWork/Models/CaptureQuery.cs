using System;

namespace F2.SystemWork.Models
{
    public class CaptureQuery
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public string BerthNumber { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int start { get; set; }
    }
}
