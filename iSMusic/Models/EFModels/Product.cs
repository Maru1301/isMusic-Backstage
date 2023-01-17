namespace iSMusic.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            CartItems = new HashSet<CartItem>();
            Order_Product_Metadata = new HashSet<Order_Product_Metadata>();
        }

        public int id { get; set; }

        [Display(Name = "語言")]
        public int productCategoryId { get; set; }

        [Display(Name = "價錢")]
        public decimal productPrice { get; set; }

        [Display(Name = "專輯")]
        public int albumId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "商品名稱")]
        public string productName { get; set; }

        [Display(Name = "庫存")]
        public int stock { get; set; }

        [Display(Name = "狀態")]
        public bool status { get; set; }

        public virtual Album Album { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CartItem> CartItems { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Product_Metadata> Order_Product_Metadata { get; set; }

        public virtual ProductCategory ProductCategory { get; set; }
    }
}
