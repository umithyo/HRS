using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRS.Models.ViewModels
{
    public class FilterVM
    {
        public int? CityId { get; set; }
        public int? TownId { get; set; }
        public int? HospitalId { get; set; }
        public int? ClinicId { get; set; }
    }
}
