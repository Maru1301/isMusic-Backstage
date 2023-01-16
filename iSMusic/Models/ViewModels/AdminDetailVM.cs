using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace iSMusic.Models.ViewModels
{
	public class AdminDetailVM
	{
		public int Id { get; set; }

		[Required]
		[Display(Name = "部門")]
		public string departmentName { get; set; }

		[Required]
		[StringLength(30)]
		[Display(Name = "帳號")]
		public string adminAccount { get; set; }

		[Display(Name = "權限列表")]
		public IEnumerable<string> RoleNameList { get; set; }
	}
}