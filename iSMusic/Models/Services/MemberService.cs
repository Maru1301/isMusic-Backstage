using iSMusic.Models.DTOs;
using iSMusic.Models.Infrastructures;
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
		public (bool IsSuccess, string ErrorMessage) Login(string account, string password)
		{
			MemberDTO member = _repository.GetByAccount(account);

			if (member == null)
			{
				return (false, "帳密有誤");
			}

			//if (member.IsConfirmed.HasValue == false || member.IsConfirmed.HasValue && member.IsConfirmed.Value == false)
			//{
			//	return (false, "會員資格尚未確認");
			//}

			string encryptedPwd = HashUtility.ToSHA256(password, RegisterDTO.SALT);

			return (String.CompareOrdinal(member.EncryptedPassword, encryptedPwd) == 0)
				? (true, null)
				: (false, "帳密有誤");
		}
		public void UpdateProfile(UpdateProfileDTO request)
		{
			// todo 驗證傳入的屬性值是否正確

			// 取得在db裡的原始記錄
			MemberDTO entity = _repository.GetByAccount(request.Account);
			if (entity == null) throw new Exception("找不到要修改的會員記錄");

			// 更新記錄
			entity.NickName = request.NickName;
			entity.Email = request.Email;
			entity.Cellphone = request.Cellphone;
			entity.Account = request.Account;
			entity.Address = request.Address;
			

			_repository.Update(entity);

		}
	}
}