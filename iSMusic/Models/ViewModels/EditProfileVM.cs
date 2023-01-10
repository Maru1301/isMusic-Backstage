using iSMusic.Models.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iSMusic.Models.ViewModels
{
	public class EditProfileVM
	{
		public int Id { get; set; }

		[Required]
		[StringLength(50)]
		[Display(Name = "電子信箱")]
		public string Email { get; set; }

		[Required]
		[StringLength(50)]
		[Display(Name = "帳號")]
		public string Account { get; set; }

		[Required]
		[StringLength(100)]
		[Display(Name = "地址")]
		public string Address { get; set; }
				
		[Required]
		[StringLength(30)]
		public string NickName { get; set; }

		[StringLength(10)]
		public string Cellphone { get; set; }
	}
	public static class MemberDTOExts
	{
		public static EditProfileVM ToEditProfileVM(this MemberDTO source)
		{
			return new EditProfileVM
			{
				Id = source.id,
				Account = source.Account,
				Address=source.Address,
				Email = source.Email,
				NickName = source.NickName,
				Cellphone = source.Cellphone,
				
			};
		}

		public static UpdateProfileDTO ToDTO(this EditProfileVM source, string currentUserAccount)
		{
			return new UpdateProfileDTO
			{
				//CurrentUserAccount = currentUserAccount,
				Id =source.Id,
				Account = currentUserAccount,
				Address = source.Address,
				Email = source.Email,
				NickName = source.NickName,
				Cellphone = source.Cellphone,
				
			};
		}
	}
}