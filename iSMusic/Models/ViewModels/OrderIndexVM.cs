using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iSMusic.Models.ViewModels
{
    public class OrderIndexVM
    {
        public int id { get; set; }

        public int memberId { get; set; }

        public string paymentName { get; set; }

        public bool orderStatus { get; set; }

        public bool paid { get; set; }

        [Column(TypeName = "date")]
        public DateTime created { get; set; }

        [Required]
        [StringLength(30)]
        public string receiver { get; set; }
    }
}