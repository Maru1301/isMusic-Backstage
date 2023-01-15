using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace iSMusic.Models.ViewModels
{
	public class AdminDelVM
	{
		public int Id { get; set; }

		[Display(Name = "部門")]
		public string departmentName { get; set; }

		[Display(Name = "帳號")]
		public string adminAccount { get; set; }

		[Display(Name ="身分")]
		public string MainRoleName { get; set; }
	}
}