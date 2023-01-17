namespace iSMusic.Models.EFModels
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            Order_Product_Metadata = new HashSet<Order_Product_Metadata>();
        }
        public string[] PaymentList = { "apple pay", "Line pay" };

        public int id { get; set; }

        [Display(Name ="訂購人")]
        public int memberId { get; set; }

        public int couponId { get; set; }

        [Display(Name = "付款方式")]
        public int payments { get; set; }

        [Display(Name = "訂單狀態")]
        public bool orderStatus { get; set; }
        
        [Display(Name = "付款狀態")]
        public bool paid { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd HH:mm:ss}",ApplyFormatInEditMode = true)]
        [Display(Name = "訂單生成時間")]
        public DateTime created { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name ="收件者")]
        public string receiver { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name ="地址")]
        public string address { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name ="連絡電話")]
        public string cellphone { get; set; }

        public virtual Coupon Coupon { get; set; }

        public virtual Member Member { get; set; }

        //public virtual Product Product { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Product_Metadata> Order_Product_Metadata { get; set; }
    }
}
