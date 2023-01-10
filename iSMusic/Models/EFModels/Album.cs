namespace iSMusic.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Album
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Album()
        {
            Album_Artist_Metadata = new HashSet<Album_Artist_Metadata>();
            Album_Song_Metadata = new HashSet<Album_Song_Metadata>();
            Library_Album_Metadata = new HashSet<Library_Album_Metadata>();
            LikedAlbums = new HashSet<LikedAlbum>();
            Products = new HashSet<Product>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string albumName { get; set; }

        [Required]
        [StringLength(50)]
        public string albumCoverPath { get; set; }

        [Column(TypeName = "date")]
        public DateTime released { get; set; }

        public DateTime created { get; set; }

        [Required]
        [StringLength(3000)]
        public string description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Album_Artist_Metadata> Album_Artist_Metadata { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Album_Song_Metadata> Album_Song_Metadata { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Library_Album_Metadata> Library_Album_Metadata { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LikedAlbum> LikedAlbums { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
