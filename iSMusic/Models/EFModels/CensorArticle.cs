namespace iSMusic.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CensorArticle")]
    public partial class CensorArticle
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CensorArticle()
        {
            ArticlePunishemts = new HashSet<ArticlePunishemt>();
        }

        public int id { get; set; }

        public int articleId { get; set; }

        public bool status { get; set; }

        public int memberId { get; set; }

        public int adminId { get; set; }

        public DateTime censored { get; set; }

        public int censorReasonId { get; set; }

        public bool censorResult { get; set; }

        public virtual Admin Admin { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ArticlePunishemt> ArticlePunishemts { get; set; }

        public virtual CensorReason CensorReason { get; set; }

        public virtual ForumArticle ForumArticle { get; set; }

        public virtual Member Member { get; set; }
    }
}
