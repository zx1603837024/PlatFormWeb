using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.WebChat.Dtos
{
    public class StopCarDto
    {
        public Int64 Id { get; set; }

        public int ROW_NUMBER { get; set; }
        public string PlateNumber { get; set; }

        public string CarInTime { get; set; }

        public string CarOutTime { get; set; }

        public decimal Money { get; set; }

        public string EmployeeName { get; set; }
    }
}
