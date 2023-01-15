using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace isMusic.Models.ViewModels
{
	public class AdminIndexVM
	{
		public int Id { get; set; }

		[Display(Name = "部門")]
		public string DepartmentName { get; set; }

		[Display(Name = "帳號")]
		public string AdminAccount { get; set; }

		[Display(Name = "身分")]
		public string RoleName { get; set; }
	}
}