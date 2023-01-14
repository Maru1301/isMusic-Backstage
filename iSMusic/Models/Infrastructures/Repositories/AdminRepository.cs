using iSMusic.Models.Infrastructures.Extensions;
using iSMusic.Models.DTOs;
using iSMusic.Models.EFModels;
using iSMusic.Models.Services.Interfaces;
using iSMusic.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iSMusic.Infrastructures.Extensions;

namespace AdminManagement.Models.Repositories
{
	public class AdminRepository : IAdminRepository
	{
		private AppDbContext db;
		public AdminRepository()
		{
			db = new AppDbContext();
		}

		public IEnumerable<AdminDTO> FindAll()
		{
			return db.Admins.Select(a => a.ToDTO());
		}

		public Admin GetByAccount(string account)
		{
			return db.Admins.FirstOrDefault(a => a.adminAccount == account);
		}


		public void Create(AdminDTO dto)
		{
			db.Admins.Add(dto.ToEntity());
			db.SaveChanges();
		}

		public void CreateMetadata(int adminId, List<int> roleIdList)
		{
			foreach (var role in roleIdList)
			{
				if (role == 0) break;
				var metadata = new Admin_Role_Metadata()
				{
					adminId = adminId,
					roleId = role,
				};
				db.Admin_Role_Metadata.Add(metadata);
				db.SaveChanges();
			}
		}

		public Admin Load(string account)
		{
			Admin query = db.Admins.SingleOrDefault(x => x.adminAccount == account);

			return query;
		}

		public bool IsExisted(string account)
		{
			var entity = db.Admins.SingleOrDefault(x => x.adminAccount == account);

			return (entity != null);

		}

		public IEnumerable<AdminDTO> Search(string adminAccount = null)
		{
			var data = db.Admin_Role_Metadata.Include("Admin").Include("Department").Include("Role").Select(x => new
			{
				x.Admin.id,
				DepartmintId = x.Admin.departmentId,
				DepartmentName = x.Admin.Department.departmentName,
				x.Admin.adminAccount,
				roleId = new List<int> { x.roleId },
			}).GroupBy(x => x.id).ToList().Select(x => new AdminDTO
			{
				Id = x.First().id,
				departmentId = x.First().DepartmintId,
				DepartmentName = x.First().DepartmentName,
				adminAccount = x.First().adminAccount,
				RoleIdList = x.SelectMany(r => r.roleId).Distinct(),
			});

			return data;
		}

		public string GetLastDefaultAccount()
		{
			return db.Admins.Where(a => a.adminAccount.StartsWith("iSMusic")).OrderBy(a => a.adminAccount).Select(a => a.adminAccount).ToList().LastOrDefault();
		}
	}
}