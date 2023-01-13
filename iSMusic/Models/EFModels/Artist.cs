namespace iSMusic.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Artist
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Artist()
        {
            Album_Artist_Metadata = new HashSet<Album_Artist_Metadata>();
            ArtistFollows = new HashSet<ArtistFollow>();
            Library_Artist_Metadata = new HashSet<Library_Artist_Metadata>();
            Song_Artist_Metadata = new HashSet<Song_Artist_Metadata>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string artistName { get; set; }

        public bool isBand { get; set; }

        public bool? artistGender { get; set; }

        [Required]
        [StringLength(500)]
        public string artistAbout { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Album_Artist_Metadata> Album_Artist_Metadata { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ArtistFollow> ArtistFollows { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Library_Artist_Metadata> Library_Artist_Metadata { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Song_Artist_Metadata> Song_Artist_Metadata { get; set; }
    }
}
