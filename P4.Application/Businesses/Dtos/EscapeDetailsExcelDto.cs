using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Businesses.Dtos
{
    public class EscapeDetailsExcelDto
    {
        public string EdeTenant { get; set; }

        public string EdeRegionName { get; set; }

        public string EdeParkName { get; set; }

        public string EdeBerthsecName { get; set; }

        public string EdeBerthNumber { get; set; }

        public string EdePlateNumber { get; set; }

        public string EdeCarInTimeString { get; set; }

        public string EdeCarOutTimeString { get; set; }

        public string EdeStopTimes { get; set; }

        public decimal EdePrepaid { get; set; }

        public decimal EdeReceivable { get; set; }

        public decimal EdeArrearage { get; set; }

        public string EdeInEmployeeName { get; set; }

        public string EdeInDeviceCode { get; set; }

        public string EdeOutEmployeeName { get; set; }

        public string EdeOutDeviceCode { get; set; }

        public string EdeEscapeEmployeeName { get; set; }

        public string EdeEscapeDeviceCode { get; set; }

        public string EdeStatusName { get; set; }

        public decimal? EdeRepayment { get; set; }

        public DateTime? EdeCarRepaymentTime { get; set; }
    }
}
