using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using iSMusic.Models.EFModels;

namespace isMusic.Models.DTOs
{
    public class MemberDTO
    {
        public int id { get; set; }

        public string memberNickName { get; set; }

        public string memberAccount { get; set; }

        public string memberEncryptedPassword { get; set; }

        public string memberEmail { get; set; }

        public string memberAddress { get; set; }

        public string memberCellphone { get; set; }

        public DateTime? memberDateOfBirth { get; set; }

        public int? avatarId { get; set; }

        public bool memberReceivedMessage { get; set; }

        public bool memberSharedData { get; set; }

        public bool libraryPrivacy { get; set; }

        public bool calenderPrivacy { get; set; }

        public int? creditCardId { get; set; }

        public bool isConfirmed { get; set; }

        public string confirmCode { get; set; }

    }

    public static class MemberDTOExts
    {
        public static MemberDTO ToMemeberDTO(this Member source)
        {
            return new MemberDTO
            {
                id = source.id,
                memberNickName = source.memberNickName,
                memberAccount = source.memberAccount,
                memberEncryptedPassword = source.memberEncryptedPassword,
                memberEmail = source.memberEmail,
                memberAddress = source.memberAddress,
                memberCellphone = source.memberCellphone,
                memberDateOfBirth = source.memberDateOfBirth,
                avatarId = source.avatarId,
                memberReceivedMessage = source.memberReceivedMessage,
                memberSharedData = source.memberSharedData,
                calenderPrivacy = source.calenderPrivacy,
                creditCardId = source.creditCardId,
                isConfirmed = source.isConfirmed,
                confirmCode = source.confirmCode,
            };
        }
    }
}