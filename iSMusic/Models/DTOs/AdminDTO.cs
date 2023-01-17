using iSMusic.Models.EFModels;
using iSMusic.Models.Infrastructures;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iSMusic.Models.DTOs
{
	public class AdminDTO
	{
		public static string SALT = "!@#$%^*AWRH()&%^";

		public int id { get; set; }

		public string adminAccount { get; set; }

		public int departmentId { get; set; }

		public string departmentName { get; set; }

		public string Password { get; set; }

		public string adminEncryptedPassword
		{
			get
			{
				string salt = SALT;
				string result = HashUtility.ToSHA256(this.Password, salt);
				return result;
			}
		}

		public virtual Admin Admin { get; set; }

		public virtual Role Role { get; set; }

		public virtual Department Department { get; set; }

		public IEnumerable<int> roleIdList { get; set; }
	}
}