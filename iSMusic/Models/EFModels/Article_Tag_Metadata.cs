namespace iSMusic.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Article_Tag_Metadata
    {
        public int id { get; set; }

        public int articleId { get; set; }

        public int tagId { get; set; }

        public virtual ForumArticle ForumArticle { get; set; }

        public virtual Tag Tag { get; set; }
    }
}
