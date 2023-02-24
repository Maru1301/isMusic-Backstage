namespace iSMusic.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Song
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Song()
        {
            LikedSongs = new HashSet<LikedSong>();
            Playlist_Song_Metadata = new HashSet<Playlist_Song_Metadata>();
            Queues = new HashSet<Queue>();
            QueueSongs = new HashSet<QueueSong>();
            Song_Artist_Metadata = new HashSet<Song_Artist_Metadata>();
            Song_Creator_Metadata = new HashSet<Song_Creator_Metadata>();
            SongPlayedRecords = new HashSet<SongPlayedRecord>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string songName { get; set; }

        public int genreId { get; set; }

        public int duration { get; set; }

        public bool isInstrumental { get; set; }

        [StringLength(50)]
        public string language { get; set; }

        public bool? isExplicit { get; set; }

        public DateTime released { get; set; }

        [Required]
        [StringLength(50)]
        public string songWriter { get; set; }

        [StringLength(2000)]
        public string lyric { get; set; }

        [Required]
        [StringLength(200)]
        public string songCoverPath { get; set; }

        [Required]
        [StringLength(200)]
        public string songPath { get; set; }

        public bool status { get; set; }

        public int? albumId { get; set; }

        public int? displayOrderInAlbum { get; set; }

        public virtual Album Album { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LikedSong> LikedSongs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Playlist_Song_Metadata> Playlist_Song_Metadata { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Queue> Queues { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QueueSong> QueueSongs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Song_Artist_Metadata> Song_Artist_Metadata { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Song_Creator_Metadata> Song_Creator_Metadata { get; set; }

        public virtual SongGenre SongGenre { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SongPlayedRecord> SongPlayedRecords { get; set; }
    }
}
