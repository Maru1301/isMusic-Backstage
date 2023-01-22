namespace iSMusic.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Admin
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Admin()
        {
            Activities = new HashSet<Activity>();
            Admin_Role_Metadata = new HashSet<Admin_Role_Metadata>();
            CensorArticles = new HashSet<CensorArticle>();
            CensorComments = new HashSet<CensorComment>();
            CensorSongs = new HashSet<CensorSong>();
            CensorTags = new HashSet<CensorTag>();
        }
		public static string SALT = "!@#$%^*AWRH()&%^";

		public int id { get; set; }

        [Required]
        [StringLength(30)]
        public string adminAccount { get; set; }

        [Required]
        [StringLength(70)]
        public string adminEncryptedPassword { get; set; }

        public int departmentId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Activity> Activities { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Admin_Role_Metadata> Admin_Role_Metadata { get; set; }

        public virtual Department Department { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CensorArticle> CensorArticles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CensorComment> CensorComments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CensorSong> CensorSongs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CensorTag> CensorTags { get; set; }
    }
}
