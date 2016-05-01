using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Data.Entity;

namespace GyakolroWebApp.Models
{
    public class MunkahelyModel
    {
        [Display(Name = "Főkategória:")]
        [HiddenInput]
        public string fkNev { get; set; }
        [Display(Name = "Munkahely neve:")]
        [StringLength(100)]
        [Required]
        public string munkahelyNev { get; set; } 
    }
}