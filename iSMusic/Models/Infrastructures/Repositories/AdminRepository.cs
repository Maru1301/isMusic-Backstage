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
using System.Data.Entity.Migrations;
using System.Web.Security;

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

		public AdminDTO GetByAccount(string account)
		{
			return db.Admins.FirstOrDefault(a => a.adminAccount == account).ToDTO();
		}

		public AdminDTO GetById(int id)
		{
			return db.Admins.Include("Admin_Role_Metadata").FirstOrDefault(a=> a.id== id).ToDTO();
		}

		public void Create(AdminDTO dto)
		{
			db.Admins.Add(dto.ToEntity());
			db.SaveChanges();
		}

		public void Update(AdminDTO dto)
		{
			var entity = db.Admins.SingleOrDefault(a => a.id == dto.Id);
			entity.adminAccount = dto.adminAccount;
			entity.departmentId= dto.departmentId;
			db.SaveChanges();
		}

		public void Delete(int id)
		{
			var entity = db.Admins.Find(id);
			db.Admins.Remove(entity);
			db.SaveChanges();
		}

		public void CreateMetadata(int adminId, int roleId)
		{
			var metadata = new Admin_Role_Metadata()
			{
				adminId = adminId,
				roleId = roleId,
			};
			db.Admin_Role_Metadata.Add(metadata);
			db.SaveChanges();
		}

		public void DeleteMetadata(int adminId, int roleId)
		{
			var metadata = db.Admin_Role_Metadata.SingleOrDefault(m=>m.adminId == adminId&& m.roleId == roleId);
			db.Admin_Role_Metadata.Remove(metadata);
			db.SaveChanges();
		}

		public Admin Load(string account)
		{
			Admin query = db.Admins.SingleOrDefault(x => x.adminAccount == account);

			return query;
		}

		public IQueryable<Admin_Role_Metadata> LoadMetadata(int adminId)
		{
			return db.Admin_Role_Metadata.Where(m => m.adminId == adminId);
		}

		public bool IsExisted(string account, int id = 0)
		{
			var admins = db.Admins;
			Admin entity;
			if(id == 0)
			{
				entity = db.Admins.SingleOrDefault(x => x.adminAccount == account);
			}
			else
			{
				entity = db.Admins.SingleOrDefault(x => x.adminAccount == account && x.id != id);
			}

			return (entity != null);

		}

		public IEnumerable<AdminDTO> Search(string adminAccount = null)
		{
			var data = db.Admin_Role_Metadata.Include("Admin").Include("Department").Include("Role").Where(x=> x.roleId != 1).Select(x => new
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