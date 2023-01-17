using iSMusic.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iSMusic.Models.DTOs
{
    public class RoleDTO
    {
        public int id { get; set; }

        public string roleName { get; set; }
        
    }
    public static class RoleDTOExts
    {
        public static RoleDTO ToRoleMetadataDTO(this Role source)
        {
            return new RoleDTO
            {
                id = source.id,
                roleName= source.roleName,
            };
        }
    }
}