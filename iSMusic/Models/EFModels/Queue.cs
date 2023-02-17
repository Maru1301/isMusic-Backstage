namespace iSMusic.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Queue
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Queue()
        {
            QueueSongs = new HashSet<QueueSong>();
        }

        public int id { get; set; }

        public int memberId { get; set; }

        public int? currentSongId { get; set; }

        public int? currentSongTime { get; set; }

        public bool isShuffle { get; set; }

        public bool? isRepeat { get; set; }

        public int? albumId { get; set; }

        public int? playlistId { get; set; }

        public int? artistId { get; set; }

        public virtual Album Album { get; set; }

        public virtual Artist Artist { get; set; }

        public virtual Member Member { get; set; }

        public virtual Playlist Playlist { get; set; }

        public virtual Song Song { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QueueSong> QueueSongs { get; set; }
    }
}
