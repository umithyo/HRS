using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HRS.Models
{
    public class Appointment
    {
        [Key]
        public Guid Id { get; set; }
        [Display(Name = "Hasta")]
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public virtual User Patient { get; set; }
        [Display(Name = "Doktor")]
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public virtual User Doctor { get; set; }
        [Column(TypeName = "TIMESTAMP")]
        [Display(Name = "Tarih")]
        [DataType(DataType.DateTime, ErrorMessage = "Geçerli bir tarih giriniz.")]
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public DateTime Time { get; set; }
        [Column(TypeName = "TIMESTAMP")]
        public DateTime CreatedAt { get; set; }
    }
}
