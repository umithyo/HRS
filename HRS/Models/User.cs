using HRS.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static HRS.Data.Constants;

namespace HRS.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Lütfen geçerli bir TC Kimlik No girin.")]
        
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Lütfen geçerli bir TC Kimlik No girin.")]
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [Display(Name = "TC Kimlik No", Prompt = "TC Kimlik No")]
        public string TCKimlikNo { get; set; }
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [Display(Name = "Şifre", Prompt = "Şifre")] 
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Role { get; set; } = RoleConfig.User;
        public virtual UserInfo UserInfo { get; set; }

        [Column(TypeName ="TIMESTAMP")]
        public DateTime CreatedAt { get; set; }
    }
}
