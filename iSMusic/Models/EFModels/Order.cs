namespace iSMusic.Models.EFModels
{
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

        public int memberId { get; set; }

        public int couponId { get; set; }

        public int payments { get; set; }

        public bool orderStatus { get; set; }

        public bool paid { get; set; }

        public DateTime created { get; set; }

        [Required]
        [StringLength(30)]
        public string receiver { get; set; }

        [Required]
        [StringLength(200)]
        public string address { get; set; }

        [Required]
        [StringLength(10)]
        public string cellphone { get; set; }

        public virtual Coupon Coupon { get; set; }

        public virtual Member Member { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Product_Metadata> Order_Product_Metadata { get; set; }
    }
}
