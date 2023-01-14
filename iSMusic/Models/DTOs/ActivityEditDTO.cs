using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace isMusic.Models.DTOs
{
    public class ActivityEditDTO
    {
        public int id { get; set; }
        public string activityName { get; set; }

        public DateTime activityStartTime { get; set; }

        public DateTime activityEndTime { get; set; }
        public string activityLocation { get; set; }

        public int activityTypeId { get; set; }

        public string activityInfo { get; set; }

        public int activityOrganizerId { get; set; }

        public string activityImagePath { get; set; }

        public bool publishedStatus { get; set; }

        public int checkedById { get; set; }
    }

    public static class ActivityEditVMExts
    {
        public static ActivityEditDTO ToActivityEditDTO(this ActivityEditDTO source)
        {
            return new ActivityEditDTO
            {
                id = source.id,
                activityName = source.activityName,
                activityStartTime = source.activityStartTime,
                activityEndTime = source.activityEndTime,
                activityLocation = source.activityLocation,
                activityTypeId = source.activityTypeId,
                activityInfo = source.activityInfo,
                activityImagePath = source.activityImagePath,
                activityOrganizerId = source.activityOrganizerId,
                publishedStatus = source.publishedStatus,
                checkedById = source.checkedById,
            };
        }
    }
}