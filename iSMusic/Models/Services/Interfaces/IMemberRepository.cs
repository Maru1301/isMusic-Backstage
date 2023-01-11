using iSMusic.Models.DTOs;
using iSMusic.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iSMusic.Models.Services.Interfaces
{
	public interface IMemberRepository
	{
		IEnumerable<MemberVM> GetAll();
		bool IsExist(string account);
		void Create(RegisterDTO dto);
		MemberDTO GetByAccount(string Account);
		MemberDTO GetById(int id);
		void Update(MemberDTO entity);
		void Delete(MemberDTO entity);
		
	}
}
