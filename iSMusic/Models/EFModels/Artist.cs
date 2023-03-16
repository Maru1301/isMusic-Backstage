namespace iSMusic.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.CompilerServices;

    public partial class Artist
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Artist()
        {
            Albums = new HashSet<Album>();
            ArtistFollows = new HashSet<ArtistFollow>();
            Library_Artist_Metadata = new HashSet<Library_Artist_Metadata>();
            Song_Artist_Metadata = new HashSet<Song_Artist_Metadata>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "�m�W*")]
        public string artistName { get; set; }

        [Display(Name = "�O�_���ֹ�*")]
        public bool isBand { get; set; }

        [Display(Name = "�ʧO*")]
        public bool? artistGender { get; set; }

        [Required]
        [StringLength(500)]
        [Display(Name = "����*")]
        public string artistAbout { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Album> Albums { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ArtistFollow> ArtistFollows { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Library_Artist_Metadata> Library_Artist_Metadata { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Song_Artist_Metadata> Song_Artist_Metadata { get; set; }
    }
}
