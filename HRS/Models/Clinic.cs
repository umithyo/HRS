using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HRS.Models
{
    public class Clinic
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Muayene Yeri")]
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public string Name { get; set; }
        [Display(Name = "Poliklinik")]
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public virtual Polyclinic Polyclinic { get; set; }
        [Display(Name = "Hastane")]
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public virtual Hospital Hospital { get; set; }
        [Column(TypeName = "TIMESTAMP")]
        public DateTime CreatedAt { get; set; }
    }
}
