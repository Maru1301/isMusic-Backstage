namespace iSMusic.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ForumArticle
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ForumArticle()
        {
            Article_Tag_Metadata = new HashSet<Article_Tag_Metadata>();
            CensorArticles = new HashSet<CensorArticle>();
            ForumArticleLikes = new HashSet<ForumArticleLike>();
            ForumCollections = new HashSet<ForumCollection>();
            ForumComments = new HashSet<ForumComment>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public int forumCategoryId { get; set; }

        public int memberId { get; set; }

        [Required]
        [StringLength(50)]
        public string articleTitle { get; set; }

        [Required]
        [StringLength(4000)]
        public string articleContent { get; set; }

        public DateTime created { get; set; }

        public DateTime updated { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Article_Tag_Metadata> Article_Tag_Metadata { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CensorArticle> CensorArticles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ForumArticleLike> ForumArticleLikes { get; set; }

        public virtual Member Member { get; set; }

        public virtual ForumCategory ForumCategory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ForumCollection> ForumCollections { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ForumComment> ForumComments { get; set; }
    }
}
