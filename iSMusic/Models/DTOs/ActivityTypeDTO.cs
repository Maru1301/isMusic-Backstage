using isMusic.Models.ViewModels;
using iSMusic.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace isMusic.Models.DTOs
{
    public class ActivityTypeDTO
    {
        public int id { get; set; }

        public string typeName { get; set; }
    }

    public static class ActivityTypeDTOExts
    {
        public static ActivityTypeDTO ToActivityTypeDTO(this ActivityType source)
        {
            return new ActivityTypeDTO
            {
                id = source.id,
                typeName = source.typeName,
            };
        }

        public static ActivityTypeDTO ToTypeDTO(this ActivityCreateVM source)
        {
            return new ActivityTypeDTO
            {
                id = source.activityTypeId
            };
        }
    }
}