namespace iSMusic.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class QueueSong
    {
        public int id { get; set; }

        public int queueId { get; set; }

        public int songId { get; set; }

        public int? nextQueueSong { get; set; }

        public bool fromAlbumOrPlaylist { get; set; }

        public virtual Queue Queue { get; set; }
    }
}
