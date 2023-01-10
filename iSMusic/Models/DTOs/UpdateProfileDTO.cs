using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iSMusic.Models.DTOs
{
	public class UpdateProfileDTO
	{
		public int Id { get; set; }
		public string NickName { get; set; }
		public string Account { get; set; }
		public string Email { get; set; }
		public string Address { get; set; }
		public string Cellphone { get; set; }
		
	}
}