using iSMusic.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace isMusic.Models.DTOs
{
    public class AdminEntity
    {
        public int id { get; set; }
        public string adminAccount { get; set; }
        public string adminEncryptedPassword { get; set; }
        public int departmentId { get; set; }
    }

    public static class AdminDTOExts
    {
        public static AdminEntity ToAdminEntity(this Admin source)
        {
            return new AdminEntity
            {
                id = source.id,
                adminAccount = source.adminAccount,
                adminEncryptedPassword = source.adminEncryptedPassword,
                departmentId = source.departmentId,
            };
        }

    }
}