using isMusic.Models.ViewModels;
using iSMusic.Models.DTOs;
using iSMusic.Models.EFModels;
using iSMusic.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iSMusic.Infrastructures.Extensions
{
	public static class AdminExts
	{
		public static AdminIndexVM ToVM(this AdminDTO source)
		{
			return new AdminIndexVM
			{
				DepartmentName = source.DepartmentName,
				adminAccount = source.adminAccount,
				roleName = source.MainRoleName,
			};
		}

		public static AdminDTO ToDTO(this Admin source)
		{
			return new AdminDTO
			{
				Id = source.id,
				adminAccount = source.adminAccount,
				DepartmentName = source.Department.departmentName,
				RoleIdList = source.Admin_Role_Metadata.Where(m => m.adminId == source.id).Select(x => x.roleId),
			};
		}

		public static AdminDTO ToRequestDTO(this AdminCreateVM source)
		{
			return new AdminDTO
			{
				departmentId = source.departmentId,
				adminAccount = source.adminAccount,
				Password = source.Password,
				RoleIdList = source.RoleIdList.Where(id => id != 0),
			};
		}

		public static Admin ToEntity(this AdminDTO source)
		{
			return new Admin
			{
				departmentId = source.departmentId,
				adminAccount = source.adminAccount,
				adminEncryptedPassword = source.adminEncryptedPassword,
			};
		}
	}
}