using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iSMusic.Models.ViewModels
{
    public class ArtistVM
    {
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "表演者名稱*")]
        public string artistName { get; set; }

        [Display(Name = "是否為樂團*")]
        public bool isBand { get; set; }

        [Display(Name = "性別*")]
        public bool? artistGender { get; set; }

        [Required]
        [StringLength(500)]
        [Display(Name = "關於表演者*")]
        public string artistAbout { get; set; }

        [StringLength(100)]
        public string artistPicPath { get; set; }

        [Required]
        [Display(Name = "表演者圖片*")]
        public HttpPostedFileBase CoverFile { get; set; }
    }
}