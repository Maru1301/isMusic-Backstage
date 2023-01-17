using iSMusic.Models.DTOs;
using iSMusic.Models.EFModels;
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
        public IAdminRepository adminRepository;

        public AdminService(IAdminRepository repo)
        {
            adminRepository = repo;
        }

        public IEnumerable<AdminDTO> Search(int? adminId, string adminAccount)
        {
            var admins = adminRepository.Search(adminId, adminAccount);

            //var result1 = admins.Where(x => x.Role.id == adminId).ToString().Contains("3");
            //var result2 = admins.Where(x => x.Role.id == adminId).ToString().Substring(0, 1).Contains("5");
            //if(result1 && result2)
            //{
            //    return admins;
            //}
            //else
            //{
            //    return admins.Where(x => x.Role.id ==  x.departmentId * 10 +2 );
            //}
            return admins;
        }

        public (bool, string) adminCreate(AdminDTO dto)
        {
            if (adminRepository.IsExisted(dto.adminAccount))
            {
                return (false, "帳號已存在");
            }
            adminRepository.adminCreate(dto);

			var admindata = adminRepository.Load(dto.adminAccount);

			adminRepository.roleMedataCreate(admindata.id, dto.roleIdList.ToList());

			return (true, null);
		}

        public AdminDTO GetByAccount(string account)
		 => adminRepository.GetByAccount(account);

		public AdminDTO GetById(int id)
		 => adminRepository.GetById(id);

		public void Edit(AdminDTO dto)
        {
            //if (adminRepository.IsExisted(dto.adminAccount)) throw new Exception("此帳號已存在");

            adminRepository.Edit(dto);
			var admindata = adminRepository.Load(dto.adminAccount);

			adminRepository.roleMedataEdit(admindata.id, dto.roleIdList.ToList());
		}

        public void Delete(AdminDTO dto)
        {            
			var admindata = adminRepository.Load(dto.adminAccount);            
			adminRepository.roleDelete(admindata.id);

			adminRepository.Delete(dto.adminAccount);
		}
	}
}