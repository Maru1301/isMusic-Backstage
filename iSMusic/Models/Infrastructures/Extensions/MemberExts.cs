using iSMusic.Models.DTOs;
using iSMusic.Models.EFModels;
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
					confirmCode = entity.confirmCode
				};
		}
	}
}