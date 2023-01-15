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

		AdminDTO GetById(int id);

		IEnumerable<AdminDTO> Search(string adminAccount = null);

		Admin Load(string account);

		IQueryable<Admin_Role_Metadata> LoadMetadata(int id);

		bool IsExisted(string account, int id = 0);

		void Create(AdminDTO dto);

		void Update(AdminDTO dto);

		string GetLastDefaultAccount();

		void CreateMetadata(int adminId, int roleId);

		void DeleteMetadata(int adminId, int roleId);
	}
}