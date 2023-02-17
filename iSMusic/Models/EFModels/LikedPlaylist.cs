namespace iSMusic.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LikedPlaylist
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public int playlistId { get; set; }

        public int memberId { get; set; }

        public DateTime created { get; set; }

        public virtual Member Member { get; set; }

        public virtual Playlist Playlist { get; set; }
    }
}
