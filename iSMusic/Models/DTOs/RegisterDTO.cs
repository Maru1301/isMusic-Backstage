using iSMusic.Models.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iSMusic.Models.DTOs
{
	public class RegisterDTO
	{
		public RegisterDTO()
		{
			this.created= DateTime.Now;
		}
		public const string SALT = "!@#$$DGTEGYT";
		public string Account { get; set; }

		/// <summary>
		/// 密碼,明碼
		/// </summary>
		public string Password { get; set; }

		/// <summary>
		/// 加密之後的密碼
		/// </summary>
		public string EncryptedPassword
		{
			get
			{
				string salt = SALT;
				string result = HashUtility.ToSHA256(this.Password, salt);
				return result;
			}
		}

		public string Email { get; set; }

		public string NickName { get; set; }
				
		public string ConfirmCode { get; set; }
		public DateTime created { get; set; }
	}
}