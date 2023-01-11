using iSMusic.Models.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iSMusic.Models.ViewModels
{
	public class EditProfileVM
	{
		public int Id { get; set; }

		
		[Display(Name = "暱稱")]
		public string NickName { get; set; }
		
		[Display(Name = "帳號")]
		public string Account { get; set; }

		[Display(Name = "電子信箱")]
		public string Email { get; set; }
		
		[Display(Name = "地址")]
		public string Address { get; set; }

		
		[Display(Name = "行動電話")]
		public string Cellphone { get; set; }
		

	}

}