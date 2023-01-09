namespace iSMusic.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ForumArticleLike
    {
        public int id { get; set; }

        public int articleId { get; set; }

        public int memberId { get; set; }

        public bool isLiked { get; set; }

        public virtual ForumArticle ForumArticle { get; set; }

        public virtual Member Member { get; set; }
    }
}
