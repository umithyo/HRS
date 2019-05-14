using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HRS.Models
{
    public class Polyclinic
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Muayene Yeri")]
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public string Name { get; set; }
        [Display(Name = "Klinik")]
        public virtual Clinic Clinic { get; set; }
        [Display(Name = "Hastane")]
        public virtual Hospital Hospital { get; set; }
        public int? ClinicId { get; set; }
        public int? HospitalId { get; set; }
        [Column(TypeName = "TIMESTAMP")]
        public DateTime CreatedAt { get; set; }
    }
}
