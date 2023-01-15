using iSMusic.Models.DTOs;
using iSMusic.Models.EFModels;
using iSMusic.Models.Infrastructures;
using iSMusic.Models.Services.Interfaces;
using iSMusic.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iSMusic.Models.Services
{
	public class AdminService
	{
		public IAdminRepository repository;

		private Dictionary<int, string> roleName = new Dictionary<int, string>()
		{
			[0] = "超級使用者",
			[1] = "訪客",
			[2] = "編輯者",
			[3] = "管理員",
			[52] = "權限編輯者",
			[53] = "權限管理員"
		};


		public AdminService(IAdminRepository repo)
		{
			this.repository = repo;
		}

		public IEnumerable<AdminDTO> FindAll()
		{
			return repository.FindAll();
		}

		public Admin GetByAccount(string account)
		{
			return repository.GetByAccount(account);
		}

		public IEnumerable<AdminDTO> Search()
		{
			var admins = repository.Search().ToList();

			for (int i = 0; i < admins.Count(); i++)
			{
				var roleIdList = admins[i].RoleIdList;
				//if (roleIdList.Contains(1))
				//{
				//	admins[i].MainRoleName = roleName[0];
				//}
				//else
				if (roleIdList.Contains(53))
				{
					admins[i].MainRoleName = roleName[53];
				}
				else if (roleIdList.Contains(52))
				{
					admins[i].MainRoleName = roleName[52];
				}
				else
				{
					var mainRoleId = roleIdList.First(id => id.ToString().Contains(admins[i].departmentId.ToString()));

					admins[i].MainRoleName = roleName[mainRoleId % 10];
				}

			}

			return admins;
		}

		public (bool issuccess, string errormessage) CreateNewAdmin(AdminDTO dto)
		{
			if (repository.IsExisted(dto.adminAccount))
			{
				return (false, "帳號已存在");
			}

			if (dto.RoleIdList.All(x => x == 0))
			{
				return (false, "未選擇權限");
			}

			//Permissions of others departments cannot be higher than the main department permission => todo
			var otherDepartmentRoleIdList = dto.RoleIdList.Where(Id => Id / 10 != dto.departmentId);
			var mainDepartmentRoleId = dto.RoleIdList.SingleOrDefault(single => single / 10 == dto.departmentId);

			if (mainDepartmentRoleId == 0)
			{
				return (false, "未選擇主部門權限");
			}

			if (otherDepartmentRoleIdList.Any(Id => (Id % 10) > (mainDepartmentRoleId % 10)))
			{
				return (false, "其他部門權限不得大於主部門權限");
			}

			repository.Create(dto);

			var admindata = repository.Load(dto.adminAccount);

			foreach(var roleId in dto.RoleIdList)
			{
				if(roleId != 0)
				{
					repository.CreateMetadata(admindata.id, roleId);
				}
			}

			return (true, null);
		}

		public (bool issuccess, string errormessage) EditAdmin(AdminDTO dto)
		{
			if (repository.IsExisted(dto.adminAccount, dto.Id))
			{
				return (false, "相同帳號已存在");
			}

			if (dto.RoleIdList.All(x => x == 0))
			{
				return (false, "未選擇權限");
			}

			//Permissions of others departments cannot be higher than the main department permission => todo
			var otherDepartmentRoleIdList = dto.RoleIdList.Where(Id => Id / 10 != dto.departmentId);
			var mainDepartmentRoleId = dto.RoleIdList.SingleOrDefault(single => single / 10 == dto.departmentId);

			if (mainDepartmentRoleId == 0)
			{
				return (false, "未選擇主部門權限");
			}

			if (otherDepartmentRoleIdList.Any(Id => (Id % 10) > (mainDepartmentRoleId % 10)))
			{
				return (false, "其他部門權限不得大於主部門權限");
			}

			repository.Update(dto);

			var oldRoleList = repository.LoadMetadata(dto.Id).Select(data=> data.roleId).ToList();

			var newRoleList = dto.RoleIdList.ToList();

			UpdateMetadata(oldRoleList, newRoleList, dto.Id);

			return (true, null);
		}

		public AdminDTO ShowDelete(int id)
		{
			var data = repository.GetById(id);

			var roleIdList = data.RoleIdList.Where(x => x != 0);

			if (roleIdList.Contains(53))
			{
				data.MainRoleName = roleName[53];
			}
			else if (roleIdList.Contains(52))
			{
				data.MainRoleName = roleName[52];
			}
			else
			{
				var mainRoleId = roleIdList.First(x => x.ToString().Contains(data.departmentId.ToString()));

				data.MainRoleName = roleName[mainRoleId % 10];
			}

			return data;
		}

		public (bool issuccess, string errormessage) Delete(AdminDTO dto)
		{
			var admin = repository.GetById(dto.Id);
			if (repository.IsExisted(admin.adminAccount) == false)
			{
				return (false, "帳號已不存在");
			}

			repository.Delete(admin.Id);

			var adminId = admin.Id;
			foreach(var roleId in admin.RoleIdList)
			{
				repository.DeleteMetadata(adminId, roleId);
			}

			return (true, null);
		}

		private void UpdateMetadata(List<int> oldIdList, List<int> newIdList, int adminId)
		{
			foreach(var id in newIdList)
			{
				if(oldIdList.Contains(id) == false) {
					repository.CreateMetadata(adminId, id);
				}
				else
				{
					oldIdList.Remove(id);
				}
			}

			foreach(var id in oldIdList)
			{
				repository.DeleteMetadata(adminId, id);
			}
		}

		public string GetNewAccount()
		{
			var lastAccount = repository.GetLastDefaultAccount();
			string result = "iSMusic";

			if (lastAccount == null)
			{
				result += "0001";
			}
			else
			{
				var number = int.Parse(lastAccount.Substring(7));
				number++;
				result += number.ToString().PadLeft(4, '0'); ;
			}

			return result;
		}

		public (bool issuccess, string errormessage) Login(string account, string password)
		{
			Admin admin = repository.Load(account);

			if (admin == null)
			{
				return (false, "帳密有誤");
			}

			string encryptedPwd = HashUtility.ToSHA256(password, Admin.SALT);

			return (String.CompareOrdinal(admin.adminEncryptedPassword, encryptedPwd) == 0)
				? (true, string.Empty)
				: (false, "帳密有誤");
		}
	}
}