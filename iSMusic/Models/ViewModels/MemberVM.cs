using iSMusic.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Xml.Linq;

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
		[Display(Name = "暱稱")]
		public string memberNickName { get; set; }
		[Display(Name = "帳號")]
		public string memberAccount { get; set; }
		[Display(Name = "電子信箱")]
		public string memberEmail { get; set; }
		[Display(Name = "地址")]
		public string memberAddress { get; set; }
		[Display(Name = "行動電話")]
		public string memberCellphone { get; set; }
		[Display(Name = "生日")]
		public DateTime? memberDateOfBirth { get; set; }
		[Display(Name = "驗證狀態")]
		public bool isConfirmed { get; set; }




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

			};
		}
	}
}