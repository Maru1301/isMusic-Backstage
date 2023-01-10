using iSMusic.Models.DTOs;
using iSMusic.Models.EFModels;
using iSMusic.Models.Infrastructures.Extensions;
using iSMusic.Models.Services.Interfaces;
using iSMusic.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iSMusic.Models.Infrastructures.Repositories
{
	public class MemberRepository : IMemberRepository
	{
		private AppDbContext db = new AppDbContext();

		public IEnumerable<MemberVM> GetAll()
		{
			IEnumerable<Member> data = db.Members;

			return data.Select(x => x.ToMemberVM()); ;
		}
		public void Create(RegisterDTO dto)
		{
			Member member = new Member
			{
				memberAccount = dto.Account,
				memberEncryptedPassword = dto.EncryptedPassword,
				memberEmail = dto.Email,
				memberNickName = dto.NickName,
				
				isConfirmed = false, //預設是未確認的會員
				confirmCode = dto.ConfirmCode
			};

			db.Members.Add(member);
			db.SaveChanges();
		}
		public bool IsExist(string account)
		{
			var entity = db.Members.SingleOrDefault(x => x.memberAccount == account);

			return (entity != null);

		}
		public MemberDTO GetByAccount(string account)
		{
			return db.Members
				.SingleOrDefault(x => x.memberAccount == account)
				.ToDto();
		}
		public void Update(MemberDTO entity)
		{
			Member member = db.Members.Find(entity.id);

			member.memberEmail = entity.Email;
			member.memberNickName = entity.NickName;
			member.memberCellphone = entity.Cellphone;
			db.SaveChanges();
		}
	}
}