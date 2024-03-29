﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HRS.Models
{
    public class UserInfo
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [Display(Name = "İsim", Prompt = "İsim")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [Display(Name = "Soyisim", Prompt = "Soyisim")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Lütfen geçerli bir email adresi girin.")]
        [Display(Name = "Email", Prompt = "Email adresi")]
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber, ErrorMessage = "Lütfen geçerli bir telefon numarası girin.")]
        [Display(Name = "Telefon", Prompt = "Telefon numarası")]
        public string Phone { get; set; }
        [Display(Name="Cinsiyet")]
        public string Gender { get; set; }
        //If doctor;
        public virtual Clinic Clinic { get; set; }
        public virtual Hospital Hospital { get; set; }

        public int? ClinicId { get; set; }
        public int? HospitalId { get; set; }

        [Column(TypeName = "TIMESTAMP")]
        public DateTime CreatedAt { get; set; }
    }
}
