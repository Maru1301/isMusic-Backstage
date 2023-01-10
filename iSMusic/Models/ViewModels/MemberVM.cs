using iSMusic.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace iSMusic.Models.ViewModels
{
	public class MemberVM
	{
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public MemberVM()
		{
			SubscriptionRecords = new HashSet<SubscriptionRecord>();
		}

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int id { get; set; }

		public string memberNickName { get; set; }

		public string memberAccount { get; set; }

		public string memberEmail { get; set; }

		public string memberAddress { get; set; }

		public string memberCellphone { get; set; }

		public DateTime? memberDateOfBirth { get; set; }

		public bool isConfirmed { get; set; }


		public DateTime created { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<SubscriptionRecord> SubscriptionRecords { get; set; }
	}
	public static partial class MemberDtoExts
	{
		public static MemberVM ToMemberVM(this Member source)
		{
			return new MemberVM
			{
				id = source.id,
				memberNickName = source.memberNickName,
				memberAccount = source.memberAccount,
				memberEmail = source.memberEmail,
				memberAddress = source.memberAddress,
				memberCellphone = source.memberCellphone,
				memberDateOfBirth = source.memberDateOfBirth,
				isConfirmed = source.isConfirmed,
				created = source.created,
			};
		}
	}
}