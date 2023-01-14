using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iSMusic.Models.DTOs
{
	public class UpdateProfileDTO
	{
		public int Id { get; set; }
		public string NickName { get; set; }
		public string Account { get; set; }
		public string EncryptedPassword { get; set; }
		public string Email { get; set; }
		public string Address { get; set; }
		public string Cellphone { get; set; }
		public DateTime? memberDateOfBirth { get; set; }
		public int? avatarId { get; set; }
		public bool memberReceivedMessage { get; set; }

		public bool memberSharedData { get; set; }

		public bool libraryPrivacy { get; set; }

		public bool calenderPrivacy { get; set; }

		public int? creditCardId { get; set; }

		public bool isConfirmed { get; set; }

		public string confirmCode { get; set; }

	}
}