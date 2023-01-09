namespace iSMusic.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ForumFollow
    {
        public int id { get; set; }

        public int memberId { get; set; }

        public int forumCategoryId { get; set; }

        public virtual ForumCategory ForumCategory { get; set; }

        public virtual Member Member { get; set; }
    }
}
