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
            ArticlePunishemts = new HashSet<ArticlePunishemt>();
            ArtistFollows = new HashSet<ArtistFollow>();
            Carts = new HashSet<Cart>();
            CensorArticles = new HashSet<CensorArticle>();
            CensorComments = new HashSet<CensorComment>();
            CensorSongs = new HashSet<CensorSong>();
            CensorTags = new HashSet<CensorTag>();
            CommentPunishments = new HashSet<CommentPunishment>();
            Creators = new HashSet<Creator>();
            ForumArticleLikes = new HashSet<ForumArticleLike>();
            ForumArticles = new HashSet<ForumArticle>();
            ForumCollections = new HashSet<ForumCollection>();
            ForumCommentLikes = new HashSet<ForumCommentLike>();
            ForumComments = new HashSet<ForumComment>();
            ForumFollows = new HashSet<ForumFollow>();
            Libraries = new HashSet<Library>();
            LikedAlbums = new HashSet<LikedAlbum>();
            LikedSongs = new HashSet<LikedSong>();
            Orders = new HashSet<Order>();
            SubscriptionRecords = new HashSet<SubscriptionRecord>();
            TagPunishments = new HashSet<TagPunishment>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string memberNickName { get; set; }

        [Required]
        [StringLength(50)]
        public string memberAccount { get; set; }

        [Required]
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
        public virtual ICollection<ArticlePunishemt> ArticlePunishemts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ArtistFollow> ArtistFollows { get; set; }

        public virtual Avatar Avatar { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cart> Carts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CensorArticle> CensorArticles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CensorComment> CensorComments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CensorSong> CensorSongs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CensorTag> CensorTags { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CommentPunishment> CommentPunishments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Creator> Creators { get; set; }

        public virtual CreditCard CreditCard { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ForumArticleLike> ForumArticleLikes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ForumArticle> ForumArticles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ForumCollection> ForumCollections { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ForumCommentLike> ForumCommentLikes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ForumComment> ForumComments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ForumFollow> ForumFollows { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Library> Libraries { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LikedAlbum> LikedAlbums { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LikedSong> LikedSongs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubscriptionRecord> SubscriptionRecords { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TagPunishment> TagPunishments { get; set; }
    }
}
