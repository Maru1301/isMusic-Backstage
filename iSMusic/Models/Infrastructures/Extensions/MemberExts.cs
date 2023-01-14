using iSMusic.Models.DTOs;
using iSMusic.Models.EFModels;
using iSMusic.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iSMusic.Models.Infrastructures.Extensions
{
	public static class MemberExts
	{
		public static MemberDTO ToDto(this Member entity)
		{
			return entity == null
				? null
				: new MemberDTO
				{
					id = entity.id,
					Account = entity.memberAccount,
					EncryptedPassword = entity.memberEncryptedPassword,
					Email = entity.memberEmail,
					NickName = entity.memberNickName,
					Cellphone = entity.memberCellphone,
					isConfirmed = entity.isConfirmed,
					confirmCode = entity.confirmCode,
					Address = entity.memberAddress
				};
		}
		public static EditProfileVM ToEditProfileVM(this MemberDTO source)
		{
			return new EditProfileVM
			{
				Id = source.id,
				Account = source.Account,
				Address = source.Address,
				Email = source.Email,
				NickName = source.NickName,
				Cellphone = source.Cellphone,

			};
		}

		public static UpdateProfileDTO ToEditProfileDTO(this EditProfileVM source)
		{
			return new UpdateProfileDTO
			{
				Id = source.Id,
				Account = source.Account,
				Address = source.Address,
				Email = source.Email,
				NickName = source.NickName,
				Cellphone = source.Cellphone,

			};
		}
		public static UpdateProfileDTO ToEditProfileDTO(this Member source)
		{
			return new UpdateProfileDTO
			{
				Id = source.id,
				Account = source.memberAccount,
				Address = source.memberAddress,
				Email = source.memberEmail,
				NickName = source.memberNickName,
				Cellphone = source.memberCellphone,

			};
		}
	}
}
