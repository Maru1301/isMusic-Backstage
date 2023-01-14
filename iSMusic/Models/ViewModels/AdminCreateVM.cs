using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace isMusic.Models.ViewModels
{
	public class AdminCreateVM
	{
		[Required]
		[Display(Name = "部門")]
		public int departmentId { get; set; }

		[Required]
		[StringLength(30)]
		[Display(Name = "帳號")]
		public string adminAccount { get; set; }

		[Required]
		[StringLength(70)]
		[Display(Name = "密碼")]
		public string Password { get; set; }

		public List<int> RoleIdList { get; set; }
	}
}