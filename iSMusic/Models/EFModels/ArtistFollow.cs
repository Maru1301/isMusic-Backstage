namespace iSMusic.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ArtistFollow
    {
        public int id { get; set; }

        public int artistId { get; set; }

        public int memberId { get; set; }

        public virtual Artist Artist { get; set; }

        public virtual Member Member { get; set; }
    }
}
