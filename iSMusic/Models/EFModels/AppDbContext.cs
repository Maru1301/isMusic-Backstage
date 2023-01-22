using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace iSMusic.Models.EFModels
{
	public partial class AppDbContext : DbContext
	{
		public AppDbContext()
			: base("name=AppConnStr")
		{
		}

		public virtual DbSet<Activity> Activities { get; set; }
		public virtual DbSet<ActivityFollow> ActivityFollows { get; set; }
		public virtual DbSet<ActivityType> ActivityTypes { get; set; }
		public virtual DbSet<Admin_Role_Metadata> Admin_Role_Metadata { get; set; }
		public virtual DbSet<Admin> Admins { get; set; }
		public virtual DbSet<Album_Song_Metadata> Album_Song_Metadata { get; set; }
		public virtual DbSet<Album> Albums { get; set; }
		public virtual DbSet<AlbumType> AlbumTypes { get; set; }
		public virtual DbSet<Article_Tag_Metadata> Article_Tag_Metadata { get; set; }
		public virtual DbSet<ArticlePunishemt> ArticlePunishemts { get; set; }
		public virtual DbSet<ArtistFollow> ArtistFollows { get; set; }
		public virtual DbSet<Artist> Artists { get; set; }
		public virtual DbSet<Avatar> Avatars { get; set; }
		public virtual DbSet<CartItem> CartItems { get; set; }
		public virtual DbSet<Cart> Carts { get; set; }
		public virtual DbSet<CensorArticle> CensorArticles { get; set; }
		public virtual DbSet<CensorComment> CensorComments { get; set; }
		public virtual DbSet<CensorReason> CensorReasons { get; set; }
		public virtual DbSet<CensorSong> CensorSongs { get; set; }
		public virtual DbSet<CensorTag> CensorTags { get; set; }
		public virtual DbSet<CommentPunishment> CommentPunishments { get; set; }
		public virtual DbSet<Coupon> Coupons { get; set; }
		public virtual DbSet<Creator> Creators { get; set; }
		public virtual DbSet<CreditCard> CreditCards { get; set; }
		public virtual DbSet<Department> Departments { get; set; }
		public virtual DbSet<ForumArticleLike> ForumArticleLikes { get; set; }
		public virtual DbSet<ForumArticle> ForumArticles { get; set; }
		public virtual DbSet<ForumCategory> ForumCategories { get; set; }
		public virtual DbSet<ForumCollection> ForumCollections { get; set; }
		public virtual DbSet<ForumCommentLike> ForumCommentLikes { get; set; }
		public virtual DbSet<ForumComment> ForumComments { get; set; }
		public virtual DbSet<ForumFollow> ForumFollows { get; set; }
		public virtual DbSet<Library> Libraries { get; set; }
		public virtual DbSet<Library_Album_Metadata> Library_Album_Metadata { get; set; }
		public virtual DbSet<Library_Artist_Metadata> Library_Artist_Metadata { get; set; }
		public virtual DbSet<Library_PlayList_Metadata> Library_PlayList_Metadata { get; set; }
		public virtual DbSet<LikedAlbum> LikedAlbums { get; set; }
		public virtual DbSet<LikedSong> LikedSongs { get; set; }
		public virtual DbSet<Member> Members { get; set; }
		public virtual DbSet<Order_Product_Metadata> Order_Product_Metadata { get; set; }
		public virtual DbSet<Order> Orders { get; set; }
		public virtual DbSet<PlayList_Song_Metadata> PlayList_Song_Metadata { get; set; }
		public virtual DbSet<Playlist> Playlists { get; set; }
		public virtual DbSet<ProductCategory> ProductCategories { get; set; }
		public virtual DbSet<Product> Products { get; set; }
		public virtual DbSet<Queue> Queues { get; set; }
		public virtual DbSet<QueueSong> QueueSongs { get; set; }
		public virtual DbSet<Role> Roles { get; set; }
		public virtual DbSet<Song_Artist_Metadata> Song_Artist_Metadata { get; set; }
		public virtual DbSet<Song_Creator_Metadata> Song_Creator_Metadata { get; set; }
		public virtual DbSet<SongGenre> SongGenres { get; set; }
		public virtual DbSet<Song> Songs { get; set; }
		public virtual DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
		public virtual DbSet<SubscriptionRecord> SubscriptionRecords { get; set; }
		public virtual DbSet<TagPunishment> TagPunishments { get; set; }
		public virtual DbSet<Tag> Tags { get; set; }
		public virtual DbSet<BanWord> BanWords { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Activity>()
				.Property(e => e.activityImagePath)
				.IsUnicode(false);

			modelBuilder.Entity<Activity>()
				.HasMany(e => e.ActivityFollows)
				.WithRequired(e => e.Activity)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<ActivityType>()
				.HasMany(e => e.Activities)
				.WithRequired(e => e.ActivityType)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Admin>()
				.Property(e => e.adminAccount)
				.IsUnicode(false);

			modelBuilder.Entity<Admin>()
				.Property(e => e.adminEncryptedPassword)
				.IsUnicode(false);

			modelBuilder.Entity<Admin>()
				.HasMany(e => e.Activities)
				.WithRequired(e => e.Admin)
				.HasForeignKey(e => e.checkedById)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Admin>()
				.HasMany(e => e.Admin_Role_Metadata)
				.WithRequired(e => e.Admin)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Admin>()
				.HasMany(e => e.CensorArticles)
				.WithRequired(e => e.Admin)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Admin>()
				.HasMany(e => e.CensorComments)
				.WithRequired(e => e.Admin)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Admin>()
				.HasMany(e => e.CensorSongs)
				.WithRequired(e => e.Admin)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Admin>()
				.HasMany(e => e.CensorTags)
				.WithRequired(e => e.Admin)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Album>()
				.HasMany(e => e.Album_Song_Metadata)
				.WithRequired(e => e.Album)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Album>()
				.HasMany(e => e.Library_Album_Metadata)
				.WithRequired(e => e.Album)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Album>()
				.HasMany(e => e.LikedAlbums)
				.WithRequired(e => e.Album)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Album>()
				.HasMany(e => e.Products)
				.WithRequired(e => e.Album)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<AlbumType>()
				.HasMany(e => e.Albums)
				.WithRequired(e => e.AlbumType)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Artist>()
				.HasMany(e => e.Albums)
				.WithRequired(e => e.Artist)
				.HasForeignKey(e => e.mainArtistId)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Artist>()
				.HasMany(e => e.ArtistFollows)
				.WithRequired(e => e.Artist)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Artist>()
				.HasMany(e => e.Library_Artist_Metadata)
				.WithRequired(e => e.Artist)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Artist>()
				.HasMany(e => e.Song_Artist_Metadata)
				.WithRequired(e => e.Artist)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Cart>()
				.HasMany(e => e.CartItems)
				.WithRequired(e => e.Cart)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<CensorArticle>()
				.HasMany(e => e.ArticlePunishemts)
				.WithRequired(e => e.CensorArticle)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<CensorComment>()
				.HasMany(e => e.CommentPunishments)
				.WithRequired(e => e.CensorComment)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<CensorReason>()
				.HasMany(e => e.CensorArticles)
				.WithRequired(e => e.CensorReason)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<CensorReason>()
				.HasMany(e => e.CensorComments)
				.WithRequired(e => e.CensorReason)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<CensorReason>()
				.HasMany(e => e.CensorSongs)
				.WithRequired(e => e.CensorReason)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<CensorReason>()
				.HasMany(e => e.CensorTags)
				.WithRequired(e => e.CensorReason)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<CensorTag>()
				.HasMany(e => e.TagPunishments)
				.WithRequired(e => e.CensorTag)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Coupon>()
				.HasMany(e => e.Orders)
				.WithRequired(e => e.Coupon)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Creator>()
				.HasMany(e => e.Song_Creator_Metadata)
				.WithRequired(e => e.Creator)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Department>()
				.HasMany(e => e.Admins)
				.WithRequired(e => e.Department)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<ForumArticle>()
				.HasMany(e => e.Article_Tag_Metadata)
				.WithRequired(e => e.ForumArticle)
				.HasForeignKey(e => e.articleId)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<ForumArticle>()
				.HasMany(e => e.CensorArticles)
				.WithRequired(e => e.ForumArticle)
				.HasForeignKey(e => e.articleId)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<ForumArticle>()
				.HasMany(e => e.ForumArticleLikes)
				.WithRequired(e => e.ForumArticle)
				.HasForeignKey(e => e.articleId)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<ForumArticle>()
				.HasMany(e => e.ForumCollections)
				.WithRequired(e => e.ForumArticle)
				.HasForeignKey(e => e.articleId)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<ForumArticle>()
				.HasMany(e => e.ForumComments)
				.WithRequired(e => e.ForumArticle)
				.HasForeignKey(e => e.articleId)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<ForumCategory>()
				.HasMany(e => e.ForumArticles)
				.WithRequired(e => e.ForumCategory)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<ForumCategory>()
				.HasMany(e => e.ForumFollows)
				.WithRequired(e => e.ForumCategory)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<ForumComment>()
				.HasMany(e => e.CensorComments)
				.WithRequired(e => e.ForumComment)
				.HasForeignKey(e => e.commentId)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<ForumComment>()
				.HasMany(e => e.ForumCommentLikes)
				.WithRequired(e => e.ForumComment)
				.HasForeignKey(e => e.commentId)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Library>()
				.HasMany(e => e.Library_Album_Metadata)
				.WithRequired(e => e.Library)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Library>()
				.HasMany(e => e.Library_Artist_Metadata)
				.WithRequired(e => e.Library)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Library>()
				.HasMany(e => e.Library_PlayList_Metadata)
				.WithRequired(e => e.Library)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Member>()
				.Property(e => e.memberCellphone)
				.IsUnicode(false);

			modelBuilder.Entity<Member>()
				.Property(e => e.confirmCode)
				.IsUnicode(false);

			modelBuilder.Entity<Member>()
				.HasMany(e => e.Activities)
				.WithRequired(e => e.Member)
				.HasForeignKey(e => e.activityOrganizerId)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Member>()
				.HasMany(e => e.ActivityFollows)
				.WithRequired(e => e.Member)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Member>()
				.HasMany(e => e.ArticlePunishemts)
				.WithRequired(e => e.Member)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Member>()
				.HasMany(e => e.ArtistFollows)
				.WithRequired(e => e.Member)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Member>()
				.HasMany(e => e.Carts)
				.WithRequired(e => e.Member)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Member>()
				.HasMany(e => e.CensorArticles)
				.WithRequired(e => e.Member)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Member>()
				.HasMany(e => e.CensorComments)
				.WithRequired(e => e.Member)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Member>()
				.HasMany(e => e.CensorSongs)
				.WithRequired(e => e.Member)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Member>()
				.HasMany(e => e.CensorTags)
				.WithRequired(e => e.Member)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Member>()
				.HasMany(e => e.CommentPunishments)
				.WithRequired(e => e.Member)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Member>()
				.HasMany(e => e.Creators)
				.WithRequired(e => e.Member)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Member>()
				.HasMany(e => e.ForumArticleLikes)
				.WithRequired(e => e.Member)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Member>()
				.HasMany(e => e.ForumArticles)
				.WithRequired(e => e.Member)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Member>()
				.HasMany(e => e.ForumCollections)
				.WithRequired(e => e.Member)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Member>()
				.HasMany(e => e.ForumCommentLikes)
				.WithRequired(e => e.Member)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Member>()
				.HasMany(e => e.ForumComments)
				.WithRequired(e => e.Member)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Member>()
				.HasMany(e => e.ForumFollows)
				.WithRequired(e => e.Member)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Member>()
				.HasMany(e => e.Libraries)
				.WithRequired(e => e.Member)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Member>()
				.HasMany(e => e.LikedAlbums)
				.WithRequired(e => e.Member)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Member>()
				.HasMany(e => e.LikedSongs)
				.WithRequired(e => e.Member)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Member>()
				.HasMany(e => e.Orders)
				.WithRequired(e => e.Member)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Member>()
				.HasMany(e => e.SubscriptionRecords)
				.WithRequired(e => e.Member)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Member>()
				.HasMany(e => e.TagPunishments)
				.WithRequired(e => e.Member)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Order_Product_Metadata>()
				.Property(e => e.price)
				.HasPrecision(10, 0);

			modelBuilder.Entity<Order>()
				.Property(e => e.cellphone)
				.IsUnicode(false);

			modelBuilder.Entity<Order>()
				.HasMany(e => e.Order_Product_Metadata)
				.WithRequired(e => e.Order)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Playlist>()
				.HasMany(e => e.Library_PlayList_Metadata)
				.WithRequired(e => e.Playlist)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Playlist>()
				.HasMany(e => e.PlayList_Song_Metadata)
				.WithRequired(e => e.Playlist)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<ProductCategory>()
				.HasMany(e => e.Products)
				.WithRequired(e => e.ProductCategory)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Product>()
				.Property(e => e.productPrice)
				.HasPrecision(7, 0);

			modelBuilder.Entity<Product>()
				.HasMany(e => e.CartItems)
				.WithRequired(e => e.Product)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Product>()
				.HasMany(e => e.Order_Product_Metadata)
				.WithRequired(e => e.Product)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Queue>()
				.HasMany(e => e.QueueSongs)
				.WithRequired(e => e.Queue)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Role>()
				.HasMany(e => e.Admin_Role_Metadata)
				.WithRequired(e => e.Role)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<SongGenre>()
				.HasMany(e => e.Albums)
				.WithRequired(e => e.SongGenre)
				.HasForeignKey(e => e.albumGenreId)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<SongGenre>()
				.HasMany(e => e.Songs)
				.WithRequired(e => e.SongGenre)
				.HasForeignKey(e => e.genreId)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Song>()
				.HasMany(e => e.Album_Song_Metadata)
				.WithRequired(e => e.Song)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Song>()
				.HasMany(e => e.CensorSongs)
				.WithRequired(e => e.Song)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Song>()
				.HasMany(e => e.LikedSongs)
				.WithRequired(e => e.Song)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Song>()
				.HasMany(e => e.PlayList_Song_Metadata)
				.WithRequired(e => e.Song)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Song>()
				.HasMany(e => e.Queues)
				.WithRequired(e => e.Song)
				.HasForeignKey(e => e.currentSongId)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Song>()
				.HasMany(e => e.Song_Artist_Metadata)
				.WithRequired(e => e.Song)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Song>()
				.HasMany(e => e.Song_Creator_Metadata)
				.WithRequired(e => e.Song)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<SubscriptionPlan>()
				.Property(e => e.price)
				.HasPrecision(7, 0);

			modelBuilder.Entity<SubscriptionPlan>()
				.HasMany(e => e.SubscriptionRecords)
				.WithRequired(e => e.SubscriptionPlan)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Tag>()
				.HasMany(e => e.Article_Tag_Metadata)
				.WithRequired(e => e.Tag)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Tag>()
				.HasMany(e => e.CensorTags)
				.WithRequired(e => e.Tag)
				.WillCascadeOnDelete(false);
		}
	}
}
