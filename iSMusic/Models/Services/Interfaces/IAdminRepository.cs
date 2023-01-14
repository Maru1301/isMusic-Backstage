using iSMusic.Models.DTOs;
using iSMusic.Models.EFModels;
using iSMusic.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iSMusic.Models.Services.Interfaces
{
	public interface IAdminRepository
	{
		IEnumerable<AdminDTO> FindAll();

		Admin GetByAccount(string account);

		IEnumerable<AdminDTO> Search(string adminAccount = null);

		Admin Load(string account);

		bool IsExisted(string account);

		void Create(AdminDTO dto);

		string GetLastDefaultAccount();

		void CreateMetadata(int adminId, List<int> roleIdList);
	}
}