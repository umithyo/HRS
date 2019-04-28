using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRS.Models
{
    public class HospitalPolyclinic
    {
        public int HospitalId { get; set; }
        public Hospital Hospital { get; set; }
        public int PolyclinicId { get; set; }
        public Polyclinic Polyclinic { get; set; }
    }
}
