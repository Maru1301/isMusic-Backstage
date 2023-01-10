using iSMusic.Models.DTOs;
using iSMusic.Models.Services.Interfaces;
using iSMusic.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iSMusic.Models.Services
{
	public class MemberService
	{
		private readonly IMemberRepository _repository;

		public MemberService(IMemberRepository repository)
		{
			_repository = repository;
		}

		public IEnumerable<MemberVM> GetAll()
		{
			return _repository.GetAll();
		}
		public (bool IsSuccess, string ErrorMessage) CreateNewMember(RegisterDTO dto)
		{
			
			// todo 判斷各欄位是否正確

			// 判斷帳號是否已存在
			if (_repository.IsExist(dto.Account))
			{
				return (false, "帳號已存在");
			}

			#region 建立一個會員記錄

			//	 建立 ConfirmCode
			dto.ConfirmCode = Guid.NewGuid().ToString("N");

			_repository.Create(dto);

			#endregion

			return (true, null);
		}
	}
}