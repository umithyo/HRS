using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRS.Models
{
    public class HospitalClinic
    {
        public int HospitalId { get; set; }
        public Hospital Hospital { get; set; }
        public int ClinicId { get; set; }
        public Clinic Clinic { get; set; }
    }
}
