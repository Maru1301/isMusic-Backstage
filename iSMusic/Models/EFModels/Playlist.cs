namespace iSMusic.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Playlist
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Playlist()
        {
            LikedPlaylists = new HashSet<LikedPlaylist>();
            Playlist_Song_Metadata = new HashSet<Playlist_Song_Metadata>();
            Queues = new HashSet<Queue>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string listName { get; set; }

        [StringLength(200)]
        public string playlistCoverPath { get; set; }

        public int memberId { get; set; }

        [StringLength(300)]
        public string description { get; set; }

        public bool isPublic { get; set; }

        public DateTime created { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LikedPlaylist> LikedPlaylists { get; set; }

        public virtual Member Member { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Playlist_Song_Metadata> Playlist_Song_Metadata { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Queue> Queues { get; set; }
    }
}
