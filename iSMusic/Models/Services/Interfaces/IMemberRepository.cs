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
	}
}
