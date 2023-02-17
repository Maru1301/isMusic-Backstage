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
            LikedAlbums = new HashSet<LikedAlbum>();
            Products = new HashSet<Product>();
            Songs = new HashSet<Song>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string albumName { get; set; }

        [Required]
        [StringLength(200)]
        public string albumCoverPath { get; set; }

        public int albumTypeId { get; set; }

        public int albumGenreId { get; set; }

        [Column(TypeName = "date")]
        public DateTime released { get; set; }

        [Required]
        [StringLength(3000)]
        public string description { get; set; }

        public int mainArtistId { get; set; }

        public int? mainCreatorId { get; set; }

        [StringLength(50)]
        public string albumProducer { get; set; }

        [StringLength(50)]
        public string albumCompany { get; set; }

        public virtual AlbumType AlbumType { get; set; }

        public virtual Artist Artist { get; set; }

        public virtual SongGenre SongGenre { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LikedAlbum> LikedAlbums { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Song> Songs { get; set; }
    }
}
