namespace iSMusic.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Member
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Member()
        {
            Activities = new HashSet<Activity>();
            ActivityFollows = new HashSet<ActivityFollow>();
            ArtistFollows = new HashSet<ArtistFollow>();
            Carts = new HashSet<Cart>();
            CreatorFollows = new HashSet<CreatorFollow>();
            Creators = new HashSet<Creator>();
            LikedActivities = new HashSet<LikedActivity>();
            LikedAlbums = new HashSet<LikedAlbum>();
            LikedPlaylists = new HashSet<LikedPlaylist>();
            LikedSongs = new HashSet<LikedSong>();
            Orders = new HashSet<Order>();
            Playlists = new HashSet<Playlist>();
            Queues = new HashSet<Queue>();
            SongPlayedRecords = new HashSet<SongPlayedRecord>();
            SubscriptionRecords = new HashSet<SubscriptionRecord>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string memberNickName { get; set; }

        [Required]
        [StringLength(50)]
        public string memberAccount { get; set; }

        [StringLength(100)]
        public string memberEncryptedPassword { get; set; }

        [Required]
        [StringLength(50)]
        public string memberEmail { get; set; }

        [StringLength(100)]
        public string memberAddress { get; set; }

        [StringLength(10)]
        public string memberCellphone { get; set; }

        [Column(TypeName = "date")]
        public DateTime? memberDateOfBirth { get; set; }

        public int? avatarId { get; set; }

        public bool memberReceivedMessage { get; set; }

        public bool memberSharedData { get; set; }

        public bool libraryPrivacy { get; set; }

        public bool calenderPrivacy { get; set; }

        public int? creditCardId { get; set; }

        public bool isConfirmed { get; set; }

        [StringLength(50)]
        public string confirmCode { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Activity> Activities { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActivityFollow> ActivityFollows { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ArtistFollow> ArtistFollows { get; set; }

        public virtual Avatar Avatar { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cart> Carts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CreatorFollow> CreatorFollows { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Creator> Creators { get; set; }

        public virtual CreditCard CreditCard { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LikedActivity> LikedActivities { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LikedAlbum> LikedAlbums { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LikedPlaylist> LikedPlaylists { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LikedSong> LikedSongs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Playlist> Playlists { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Queue> Queues { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SongPlayedRecord> SongPlayedRecords { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubscriptionRecord> SubscriptionRecords { get; set; }
    }
}
