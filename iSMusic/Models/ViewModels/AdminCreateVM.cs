using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace iSMusic.Models.ViewModels
{
    public class AdminCreateVM
    {        

        [Required(ErrorMessage = "尚未選擇部門")]
        [Display(Name = "部門")]
        public int departmentId { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "帳號")]
        public string adminAccount { get; set; }

        [Required]
        [StringLength(70)]
        [DataType(DataType.Password)]
        [Display(Name = "密碼")]
        public string Password { get; set; }

        [Required]
        [StringLength(70)]
        [DataType(DataType.Password)]
        [Display(Name = "確認密碼")]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        public string ConfirmPassword { get; set; }

        public List<int> roleIdList { get; set; }
    }
}