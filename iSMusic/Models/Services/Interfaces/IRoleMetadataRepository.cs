using iSMusic.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iSMusic.Models.Services.Interfaces
{
	public interface IRoleMetadataRepository
	{
		void RoleMetadataCreate(int accountId, int roleId);

		void RoleMetadataDelete(int accountId, int roleId);

		 IEnumerable<int> GetRoleIdList(int albumId);
	}
}
