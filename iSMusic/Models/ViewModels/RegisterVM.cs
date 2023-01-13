using iSMusic.Models.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iSMusic.Models.ViewModels
{
	public class RegisterVM
	{
		public int id { get; set; }
		[Required]
		[StringLength(50)]
		[Display(Name = "暱稱")]
		public string NickName { get; set; }
		[Required]
		[StringLength(50)]
		[Display(Name = "帳號")]
		public string Account { get; set; }
		[Required]
		[StringLength(50)]
		[DataType(DataType.Password)]
		[Display(Name = "密碼")]
		public string Password { get; set; }
		[Required]
		[StringLength(50)]
		[Compare(nameof(Password))]
		[DataType(DataType.Password)]
		[Display(Name = "確認密碼")]
		public string ConfirmPassword { get; set; }
		[Required]
		[StringLength(50)]
		[Display(Name = "電子信箱")]
		public string Email { get; set; }
	}
	public static class RegisterVMExts
	{
		public static RegisterDTO ToRequestDto(this RegisterVM source)
		{
			return new RegisterDTO
			{
				Account = source.Account,
				Password = source.Password,
				Email = source.Email,
				NickName = source.NickName,

			};
		}
	}
}