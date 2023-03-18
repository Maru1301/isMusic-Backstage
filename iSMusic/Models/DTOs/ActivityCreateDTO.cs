using isMusic.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace isMusic.Models.DTOs
{
    public class ActivityCreateDTO
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
		public string Path { get; set; }
		public HttpPostedFileBase File { get; set; }
	}

    public static class ActivityCreateDTOExts
    {
        public static ActivityCreateDTO ToDTO(this ActivityCreateVM source)
        {

            return new ActivityCreateDTO
            {
                id = source.id,
                activityName = source.activityName,
                activityStartTime = source.activityStartTime,
                activityEndTime = source.activityEndTime,
                activityLocation = source.activityLocation,
                activityTypeId = source.activityTypeId,
                activityInfo = source.activityInfo,                
                activityOrganizerId = source.activityOrganizerId,
                publishedStatus = source.publishedStatus,
                checkedById = source.checkedById,
                File=source.File,                
            };
        }
    }
}