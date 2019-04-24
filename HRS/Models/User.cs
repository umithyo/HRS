using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static HRS.Helpers.Utils;

namespace HRS.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        [Range(11, 11, ErrorMessage = "Lütfen geçerli bir TC Kimlik No girin.")]
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public int TCKimlikNo { get; set; }
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public string Password { get; set; }
        public string Role { get; set; } = RoleConfig.User;
        //If doctor;
        public Clinic Clinics { get; set; }
        public Hospital Hospitals { get; set; }

        [Column(TypeName ="TIMESTAMP")]
        public DateTime CreatedAt { get; set; }
    }
}
