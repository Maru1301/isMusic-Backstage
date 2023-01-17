namespace iSMusic.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Coupon
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Coupon()
        {
            Orders = new HashSet<Order>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        [Display (Name="折扣內容")]
        public string couponText { get; set; }

        [Display(Name = "開始日期")]
        public DateTime startDate { get; set; }

        [Display(Name = "結束日期")]
        public DateTime expiredDate { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "折扣")]
        public string discounts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
