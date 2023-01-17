using iSMusic.Models.DTOs;
using iSMusic.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iSMusic.Models.Services.Interfaces
{
	public interface IAdminRepository
	{
		IEnumerable<AdminDTO> Search(int? activityId, string activityName);

		bool IsExisted(string account);

		void adminCreate(AdminDTO dto);

		void roleMedataCreate(int adminId, List<int> roleIdList);

		AdminDTO GetById(int id);

		AdminDTO GetByAccount(string account);

		void Edit(AdminDTO dto);

		void roleMedataEdit(int adminId, List<int> roleIdList);

		Admin Load(string account);


		void Delete(string adminAccount);

		int FindAdminId(string adminAccount);

		void roleDelete(int adminId);


	}
}
