namespace iSMusic.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ArticlePunishemt
    {
        public int id { get; set; }

        public int memberId { get; set; }

        [Column(TypeName = "date")]
        public DateTime duration { get; set; }

        public int censorArticleId { get; set; }

        public virtual CensorArticle CensorArticle { get; set; }

        public virtual Member Member { get; set; }
    }
}
