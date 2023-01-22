namespace iSMusic.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PlayList_Song_Metadata
    {
        public int id { get; set; }

        public int playListId { get; set; }

        public int songId { get; set; }

        public int displayOrder { get; set; }

        [Column(TypeName = "date")]
        public DateTime addedTime { get; set; }

        public virtual Song Song { get; set; }

        public virtual Playlist Playlist { get; set; }
    }
}
