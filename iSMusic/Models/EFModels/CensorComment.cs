namespace iSMusic.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CensorComment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CensorComment()
        {
            CommentPunishments = new HashSet<CommentPunishment>();
        }

        public int id { get; set; }

        public int commentId { get; set; }

        public bool status { get; set; }

        public int memberId { get; set; }

        public int adminId { get; set; }

        public DateTime creatde { get; set; }

        public DateTime censored { get; set; }

        public int censorReasonId { get; set; }

        public bool censorResult { get; set; }

        public virtual Admin Admin { get; set; }

        public virtual CensorReason CensorReason { get; set; }

        public virtual ForumComment ForumComment { get; set; }

        public virtual Member Member { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CommentPunishment> CommentPunishments { get; set; }
    }
}
