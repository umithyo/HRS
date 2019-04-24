using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HRS.Models
{
    public class City
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "İl Adı")]
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public string Name { get; set; }
    }
}
