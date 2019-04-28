using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HRS.Models
{
    public class Hospital
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Hastane Adı")]
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public string Name { get; set; }
        [Display(Name = "Poliklinik Adresi")]
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public virtual City City { get; set; }
        public virtual Town Town { get; set; }
        public List<HospitalPolyclinic> HospitalPolyclinics { get; set; }

        [Column(TypeName = "TIMESTAMP")]
        public DateTime CreatedAt { get; set; }
    }
}
