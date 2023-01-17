using iSMusic.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iSMusic.Models.DTOs
{
	public class DepartmentDTO
	{
		public int id { get; set; }

		public string departmentName { get; set; }
	}

	public static class DepartmentExts
	{
		public static DepartmentDTO ToDepartmentEntity(this Department source)
			=> new DepartmentDTO
			{
				id = source.id,
				departmentName = source.departmentName
			};
	}
}