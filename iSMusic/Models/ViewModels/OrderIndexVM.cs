using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using iSMusic.Models.EFModels;

namespace iSMusic.Models.ViewModels
{
    public class OrderIndexVM
    {
        public int id { get; set; }

        [Display(Name = "訂購人")]
        public string memberNickName { get; set; }

        [Display(Name = "付款方式")]
        public string paymentName { get; set; }
        

        public bool orderStatus { get; set; }
        
        [Display(Name = "付款成功")]
        public bool paid { get; set; }
        
        [Display(Name = "訂單成立時間")]
        [Column(TypeName = "date")]
        public DateTime created { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "收件人")]
        public string receiver { get; set; }

        public decimal price { get; set; }


        public virtual Member Member { get; set; }
    }
}