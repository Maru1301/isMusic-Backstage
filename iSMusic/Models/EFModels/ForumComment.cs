namespace iSMusic.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ForumComment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ForumComment()
        {
            CensorComments = new HashSet<CensorComment>();
            ForumCommentLikes = new HashSet<ForumCommentLike>();
        }

        public int id { get; set; }

        public int articleId { get; set; }

        public int memberId { get; set; }

        [Required]
        [StringLength(3000)]
        public string commentContent { get; set; }

        public int? commentId { get; set; }

        public DateTime created { get; set; }

        public DateTime updated { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CensorComment> CensorComments { get; set; }

        public virtual ForumArticle ForumArticle { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ForumCommentLike> ForumCommentLikes { get; set; }

        public virtual Member Member { get; set; }
    }
}
