using iSMusic.Models.DTOs;
using iSMusic.Models.EFModels;
using iSMusic.Models.ViewModels;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iSMusic.Models.Infrastructures.Extensions
{
    public static partial class AdminExts
    {		
		public static AdminDTO ToAdminDTO(this Admin_Role_Metadata source)
        {
            return new AdminDTO
            {
                id = source.id,
                adminAccount = source.Admin.adminAccount,                
                //departmentId = source.Admin.departmentId,
                Department = source.Admin.Department,
                Role = source.Role
            };
        }
        
        public static AdminIndexVM ToAdminIndexVM(this AdminDTO source)
        {
            return new AdminIndexVM
            {
                id = source.id,
                adminAccount = source.adminAccount,                
                departmentName = source.Department.departmentName,
                roleName = source.Role.roleName
            };
        }
        
        public static Admin ToAdminEntity(this AdminDTO source)
        {
            return new Admin
            {                                
                id = source.id,
                adminAccount = source.adminAccount,
                departmentId = source.departmentId,
                adminEncryptedPassword = source.adminEncryptedPassword,                
            };			
		}		

		public static AdminDTO ToAdminCreateDTO(this AdminCreateVM source)
        {
            return new AdminDTO
            {                
                adminAccount = source.adminAccount,
                Password = source.Password,
                departmentId = source.departmentId,
                roleIdList = source.roleIdList
			};
        }

		public static AdminDTO ToAdminDTO(this Admin source)
		{
			return new AdminDTO
			{
				id = source.id,
                adminAccount = source.adminAccount,
				departmentId = source.departmentId,
                departmentName = source.Department.departmentName,
				//roleIdList = source.Admin_Role_Metadata.Where(m => m.adminId == source.id).Select(x => x.roleId),
			};
		}

		public static AdminEditVM ToAdminEditVM(this AdminDTO source)
		{
            return new AdminEditVM
            {
                id = source.id,
                adminAccount = source.adminAccount,
                departmentId = source.departmentId,
                departmentName = source.departmentName,
                roleIdList = source.roleIdList
			};
		}

		public static AdminDTO ToAdminEditDTO(this AdminEditVM source)
		{
            return new AdminDTO
            {
                id = source.id,
                adminAccount = source.adminAccount,
                departmentId = source.departmentId,
                departmentName = source.departmentName,
                roleIdList = source.roleIdList

			};
		}
		
	}
}