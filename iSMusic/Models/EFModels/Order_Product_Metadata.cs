namespace iSMusic.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order_Product_Metadata
    {
        public int id { get; set; }

        public int orderId { get; set; }

        public int productId { get; set; }

        public decimal price { get; set; }

        [Required]
        [StringLength(50)]
        public string productName { get; set; }

        public int qty { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}
