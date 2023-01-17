using iSMusic.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iSMusic.Models.DTOs
{
	public class RoleMetadataDTO
	{
        public int id { get; set; }

        public int adminId { get; set; }

        public int roleId { get; set; }

        public virtual Admin Admin { get; set; }

        public virtual Role Role { get; set; }
    }
    public static class RoleMetadataDTOExts
    {
        public static RoleMetadataDTO ToRoleMetadataDTO(this Admin_Role_Metadata source)
        {
            return new RoleMetadataDTO
            {
                id = source.id,
                adminId = source.adminId,
                roleId = source.roleId,
            };
        }
    }
}