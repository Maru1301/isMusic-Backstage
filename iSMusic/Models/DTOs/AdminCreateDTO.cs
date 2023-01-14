using iSMusic.Models.Infrastructures;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace isMusic.Models.DTOs
{
	public class AdminCreateDTO
	{
		private string SALT = "!@#$%^*AWRH()&%^";

		public int departmentId { get; set; }

		public string adminAccount { get; set; }

		public string Password { get; set; }

		public string EncryptedPassword
		{
			get
			{
				string salt = SALT;
				string result = HashUtility.ToSHA256(this.Password, salt);
				return result;
			}
		}

		public List<int> RoleIdList { get; set; }
	}
}