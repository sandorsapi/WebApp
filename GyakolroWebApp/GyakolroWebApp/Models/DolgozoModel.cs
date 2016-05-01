using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Data.Entity;

namespace GyakolroWebApp.Models
{
    public class DolgozoModel
    {
        [Display(Name = "Munkahely:")]
        [HiddenInput]
        public string mHely { get; set; }
        [Display(Name = "Név:")]
        [StringLength(100)]
        [Required]
        public string dolgozoNev { get; set; }
        [Display(Name = "Email:")]
        [StringLength(100)]
        [Required]
        public string email { get; set; }                   
    }   
}