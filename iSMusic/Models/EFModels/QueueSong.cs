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

        public int displayOrder { get; set; }

        public bool fromPlaylist { get; set; }

        public int shuffleOrder { get; set; }

        public virtual Queue Queue { get; set; }

        public virtual Song Song { get; set; }
    }
}
