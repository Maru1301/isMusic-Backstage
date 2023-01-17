using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iSMusic.Models.DTOs
{
	public class MemberDTO
	{
		public int id { get; set; }

		public string Account { get; set; }

		public string EncryptedPassword { get; set; }

		public string Email { get; set; }

		public string NickName { get; set; }

		public string Cellphone { get; set; }

		public bool isConfirmed { get; set; }

		public string confirmCode { get; set; }
		public string Address { get; set; }
		public bool ReceivedMessage { get; set; }
		public DateTime? DateOfBirth { get; set; }

	}
}
