namespace iSMusic.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LikedAlbum
    {
        public int id { get; set; }

        public int albumId { get; set; }

        public int memberId { get; set; }

        public virtual Album Album { get; set; }

        public virtual Member Member { get; set; }
    }
}
