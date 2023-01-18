using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace iSMusic.Models.ViewModels
{
	public class AdminEditVM
	{
		public int id { get; set; }

		public int departmentId { get; set; }

		[Display(Name = "部門")]
		public string departmentName { get; set; }

		[Display(Name = "帳號")]
		public string adminAccount { get; set; }

		public IEnumerable<int> roleIdList { get; set; }
	}
}