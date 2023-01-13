using iSMusic.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace isMusic.Models.DTOs
{
    public class AdminDTO
    {
        public int id { get; set; }
        public string adminAccount { get; set; }
        public string adminEncryptedPassword { get; set; }
        public int departmentId { get; set; }
    }

    public static class AdminDTOExts
    {
        public static AdminDTO ToAdminDTO(this Admin source)
        {
            return new AdminDTO
            {
                id = source.id,
                adminAccount = source.adminAccount,
                adminEncryptedPassword = source.adminEncryptedPassword,
                departmentId = source.departmentId,
            };
        }

    }
}