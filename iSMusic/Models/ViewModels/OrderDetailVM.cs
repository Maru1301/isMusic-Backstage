using iSMusic.Models.EFModels;
using iSMusic.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace iSMusic.Models.ViewModels
{
    public class OrderDetailVM
    {
        //public Order()
        //{
        //    Order_Product_Metadata = new HashSet<Order_Product_Metadata>();
        //}

        public int id { get; set; }

        public string memberId { get; set; }

        public string couponId { get; set; }

        public int payments { get; set; }

        public bool orderStatus { get; set; }

        public bool paid { get; set; }

        [Column(TypeName = "date")]
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


        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Order_Product_Metadata> Order_Product_Metadata { get; set; }

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
    public static class Extensions
    {
        public static OrderDetailVM ToDetailVM(this Order source)
        {
            return new OrderDetailVM
            {
                id = source.id,
                memberId = source.Member.memberNickName,
                couponId = source.Coupon.couponText,
                payments = source.payments,
                orderStatus = source.orderStatus,
                paid = source.paid,
                created = source.created,
                receiver = source.receiver,
                address = source.address,
                cellphone = source.cellphone,
                //orderId = source.Order_Product_Metadata.FirstOrDefault(x => x.orderId == source.id).orderId,
                //productId = source.Order_Product_Metadata.FirstOrDefault(x => x.orderId == source.id).productId,
                price = source.Order_Product_Metadata.FirstOrDefault(x => x.orderId == source.id).price,
                productName = source.Order_Product_Metadata.FirstOrDefault(x => x.orderId == source.id).productName,
                qty = source.Order_Product_Metadata.FirstOrDefault(x => x.orderId == source.id).qty,



            };

        }
    }

}
