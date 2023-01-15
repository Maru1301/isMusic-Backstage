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
				Id = source.Id,
				DepartmentName = source.DepartmentName,
				AdminAccount = source.adminAccount,
				RoleName = source.MainRoleName,
			};
		}

		public static AdminEditVM ToEditVM(this AdminDTO source)
			=> new AdminEditVM
			{
				Id = source.Id,
				adminAccount= source.adminAccount,
				departmentId= source.departmentId,
				RoleIdList = source.RoleIdList,
			};

		public static AdminDelVM ToDelVM(this AdminDTO source)
			=> new AdminDelVM
			{
				adminAccount = source.adminAccount,
				departmentName = source.DepartmentName,
				MainRoleName = source.MainRoleName,
			};

		public static AdminDTO ToDTO(this Admin source)
		{
			return new AdminDTO
			{
				Id = source.id,
				adminAccount = source.adminAccount,
				departmentId= source.departmentId,
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

		public static AdminDTO ToRequestDTO(this AdminEditVM source)
			=> new AdminDTO
			{
				Id = source.Id,
				departmentId = source.departmentId,
				adminAccount = source.adminAccount,
				RoleIdList = source.RoleIdList.Where(id => id != 0),
			};

		public static AdminDTO ToRequestDTO(this AdminDelVM source)
			=> new AdminDTO
			{
				Id = source.Id,
				adminAccount = source.adminAccount,
			};

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