using iSMusic.Models.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iSMusic.Models.DTOs
{
	public class AdminDTO
	{
		private string SALT = "!@#$%^*AWRH()&%^";

		public int Id { get; set; }

		public int departmentId { get; set; }

		public string DepartmentName { get; set; }

		public string adminAccount { get; set; }

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

		public IEnumerable<int> RoleIdList { get; set; }

		public string MainRoleName { get; set; }
	}
}