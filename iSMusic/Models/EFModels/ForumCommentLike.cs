namespace iSMusic.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ForumCommentLike
    {
        public int id { get; set; }

        public bool isLike { get; set; }

        public int commentId { get; set; }

        public int memberId { get; set; }

        public virtual ForumComment ForumComment { get; set; }

        public virtual Member Member { get; set; }
    }
}
