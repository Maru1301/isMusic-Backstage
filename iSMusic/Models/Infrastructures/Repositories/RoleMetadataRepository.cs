using iSMusic.Models.DTOs;
using iSMusic.Models.EFModels;
using iSMusic.Models.Services.Interfaces;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace iSMusic.Models.Infrastructures.Repositories
{
	public class RoleMetadataRepository : IRoleMetadataRepository
	{
		private readonly AppDbContext _db;

		public RoleMetadataRepository()
		{
			AppDbContext db = new AppDbContext();
			_db = db;
		}
		public void RoleMetadataCreate(int accountId, int roleId)
		{
				Admin_Role_Metadata adminRoleMetadata = new Admin_Role_Metadata
				{
					adminId = accountId,
					roleId = roleId
				};
				_db.Admin_Role_Metadata.Add(adminRoleMetadata);
				_db.SaveChanges();			
		}

		public void RoleMetadataDelete(int accountId, int roleId)
		{
			Admin_Role_Metadata adminRoleMetadata = new Admin_Role_Metadata
			{
				adminId = accountId,
				roleId = roleId
			};
			_db.Admin_Role_Metadata.Remove(adminRoleMetadata);
			_db.SaveChanges();
		}

		public IEnumerable<int> GetRoleIdList(int adminId)
		{
			return _db.Admin_Role_Metadata.Where(m => m.adminId == adminId).Select(x => x.adminId);
		}
	}
}