using iSMusic.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iSMusic.Models.ViewModels
{
    public class AdminIndexVM
    {
        public int id { get; set; }

        [Display(Name ="部門")]
        public string departmentName { get; set; }

        [Display(Name = "帳號")]
        public string adminAccount { get; set; }

        [Display(Name = "身分")]
        public string roleName { get; set; }
    }
}