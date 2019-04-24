using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HRS.Models
{
    public class County
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "İlçe Adı")]
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public string Name { get; set; }
        [Display(Name = "İl")]
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public City City { get; set; }     
    }
}
