namespace iSMusic.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LikedSong
    {
        public int id { get; set; }

        public int songId { get; set; }

        public int memberId { get; set; }

        public virtual Member Member { get; set; }

        public virtual Song Song { get; set; }
    }
}
