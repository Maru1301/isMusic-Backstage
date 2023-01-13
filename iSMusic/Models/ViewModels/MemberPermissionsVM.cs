using iSMusic.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iSMusic.Models.ViewModels
{
	public class MemberPermissionsVM
	{
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public MemberPermissionsVM
()
		{
			SubscriptionRecords = new HashSet<SubscriptionRecord>();
		}

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int id { get; set; }

		[Required]
		[StringLength(50)]
		public string memberNickName { get; set; }

		[Required]
		[StringLength(50)]
		public string memberAccount { get; set; }

		[Required]
		[StringLength(50)]
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

		[Column(TypeName = "date")]
		public DateTime created { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<SubscriptionRecord> SubscriptionRecords { get; set; }
	}
}

