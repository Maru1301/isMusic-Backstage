namespace iSMusic.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CommentPunishment
    {
        public int id { get; set; }

        public int memberId { get; set; }

        [Column(TypeName = "date")]
        public DateTime duration { get; set; }

        public int censorCommentId { get; set; }

        public virtual CensorComment CensorComment { get; set; }

        public virtual Member Member { get; set; }
    }
}
