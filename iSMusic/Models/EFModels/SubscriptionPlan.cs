namespace iSMusic.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SubscriptionPlan")]
    public partial class SubscriptionPlan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SubscriptionPlan()
        {
            Members = new HashSet<Member>();
            SubscriptionRecords = new HashSet<SubscriptionRecord>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string planName { get; set; }

        public decimal price { get; set; }

        public byte numberOfUsers { get; set; }

        [Required]
        [StringLength(500)]
        public string description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Member> Members { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubscriptionRecord> SubscriptionRecords { get; set; }
    }
}
