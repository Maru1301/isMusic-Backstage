using iSMusic.Models.DTOs;
using iSMusic.Models.EFModels;
using iSMusic.Models.Services.Interfaces;
using iSMusic.Models.Infrastructures.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iSMusic.Models.Entities;
using isMusic.Models.DTOs;

namespace iSMusic.Models.Infrastructures.Repositories
{
	public class AdminRepository : IAdminRepository
	{
		private AppDbContext _db;
		public AdminRepository(AppDbContext db)
		{
			_db = db;
		}
		public IEnumerable<AdminDTO> Search(int? adminId, string adminAccount)
		{
			IEnumerable<Admin_Role_Metadata> query = _db.Admin_Role_Metadata;

			query = query.OrderBy(x => x.Admin.adminAccount);

			return query.Select(x => x.ToAdminDTO());
		}

		public bool IsExisted(string account)
		{
			Admin model;
			var admins = _db.Admins;
			model = admins.SingleOrDefault(x => x.adminAccount == account);

			return model != null;
		}

		public void adminCreate(AdminDTO dto)
		{
			_db.Admins.Add(dto.ToAdminEntity());
			_db.SaveChanges();
		}

		public void roleMedataCreate(int adminId, List<int> roleIdList)
		{
			foreach (var role in roleIdList)
			{
				if (role == 0) break;
				var metadata = new Admin_Role_Metadata()
				{
					adminId = adminId,
					roleId = role,
				};

				_db.Admin_Role_Metadata.Add(metadata);
				_db.SaveChanges();
			}
		}

		public AdminDTO GetById(int id)
		{
			return _db.Admins.FirstOrDefault(a => a.id == id).ToAdminDTO();
		}

		public AdminDTO GetByAccount(string account)
		{
			//return _db.Admins.FirstOrDefault(a => a.adminAccount == account).ToAdminDTO();
			return _db.Admins.SingleOrDefault(a => a.adminAccount == account).ToAdminDTO();
		}

		public Admin Load(string account)
		{
			Admin query = _db.Admins.SingleOrDefault(x => x.adminAccount == account);

			return query;
		}

		public void Edit(AdminDTO dto)
		{
			var data = _db.Admins.Find(dto.id);
			data.adminAccount = dto.adminAccount;
			data.departmentId = dto.departmentId;

			_db.SaveChanges();

		}

		public void roleMedataEdit(int adminId, List<int> roleIdList)
		{
			var oldRoleMetadata = _db.Admin_Role_Metadata.Where(x => x.adminId == adminId).ToList();

			foreach (var role in oldRoleMetadata)
			{
				_db.Admin_Role_Metadata.Remove(role);
			}
			_db.SaveChanges();

			roleMedataCreate(adminId, roleIdList);
			_db.SaveChanges();

		}

		public void Delete(string adminAccount)
		{
			var admin = _db.Admins.SingleOrDefault(x => x.adminAccount == adminAccount);
			_db.Admins.Remove(admin);
			_db.SaveChanges();
		}

		public int FindAdminId(string adminAccount)
		{
			var adminId = _db.Admins.SingleOrDefault(x => x.adminAccount == adminAccount).id;
			return adminId;
		}

		public void roleDelete(int adminId)
		{
			var oldRoleMetadata = _db.Admin_Role_Metadata.Where(x => x.adminId == adminId).ToList();

			foreach (var role in oldRoleMetadata)
			{
				_db.Admin_Role_Metadata.Remove(role);
			}
			_db.SaveChanges();
		}
	}
}