
using iSMusic.Models.EFModels;
using iSMusic.Models.Infrastructures.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace isMusic.Models.DTOs
{
    public class ActivityDTO
    {
        public int id { get; set; }
        public string activityName { get; set; }

        public DateTime activityStartTime { get; set; }

        public DateTime activityEndTime { get; set; }
        public string activityLocation { get; set; }

        public ActivityTypeDTO activityType { get; set; }
        //public int activityTypeId { get; set; }

        public string activityInfo { get; set; }

        //public string member { get; set; }
        public MemberEntity member { get; set; }

        public string activityImagePath { get; set; }

        public bool publishedStatus { get; set; }

        //public string admin { get; set; }
        public AdminEntity admin { get; set; }


    }

    public static partial class ActivityExts
    {
        public static ActivityDTO ToActivityDTO(this Activity source)
        {
            return new ActivityDTO
            {
                id = source.id,
                activityName = source.activityName,
                activityStartTime = source.activityStartTime,
                activityEndTime = source.activityEndTime,
                activityLocation = source.activityLocation,
                activityType = source.ActivityType.ToActivityTypeDTO(),
                activityInfo = source.activityInfo,
                activityImagePath = source.activityImagePath,
                member = source.Member.ToMemeberDTO(),
                publishedStatus = source.publishedStatus,
                admin = source.Admin.ToAdminEntity(),
            };
        }

        public static Activity ToActivityEntity(this ActivityDTO source)
        {
            return new Activity
            {
                id = source.id,
                activityName = source.activityName,
                activityStartTime = source.activityStartTime,
                activityEndTime = source.activityEndTime,
                activityLocation = source.activityLocation,
                activityTypeId = source.activityType.id,
                activityInfo = source.activityInfo,
                activityImagePath = source.activityImagePath,
                activityOrganizerId = source.member.id,
                publishedStatus = source.publishedStatus,
                checkedById = source.admin.id,
            };
        }
    }
}