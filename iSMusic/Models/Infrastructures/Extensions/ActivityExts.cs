using isMusic.Models.DTOs;
using isMusic.Models.ViewModels;
using iSMusic.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.SessionState;

namespace isMusic.Infrastructures.Extensions
{
    //public class ActivityExts
    //{

    //}
    public static class ActivityExts
    {


        public static Activity ToActivityEntity(this ActivityCreateDTO source)
        {

            return new Activity
            {
                //id = source.id,
                activityName = source.activityName,
                activityStartTime = source.activityStartTime,
                activityEndTime = source.activityEndTime,
                activityLocation = source.activityLocation,
                activityTypeId = source.activityTypeId,
                activityInfo = source.activityInfo,
                activityOrganizerId = source.activityOrganizerId,
                activityImagePath = source.activityImagePath,
                publishedStatus = source.publishedStatus,
                checkedById = source.checkedById,
            };
        }

        public static Activity ToActivityEntityForEdit(this ActivityEditDTO source)
        {

            return new Activity
            {
                //id = source.id,
                activityName = source.activityName,
                activityStartTime = source.activityStartTime,
                activityEndTime = source.activityEndTime,
                activityLocation = source.activityLocation,
                activityTypeId = source.activityTypeId,
                activityInfo = source.activityInfo,
                activityOrganizerId = source.activityOrganizerId,
                activityImagePath = source.activityImagePath,
                publishedStatus = source.publishedStatus,
                checkedById = source.checkedById,
            };
        }

        public static ActivityCreateDTO ToActivityCreateDTO(this ActivityCreateVM source)
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
                activityImagePath = source.activityImagePath,
                activityOrganizerId = source.activityOrganizerId,
                publishedStatus = source.publishedStatus,
                checkedById = source.checkedById,
            };
        }

        public static ActivityEditDTO ToActivityEditDTO(this ActivityEditVM source)
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

        public static ActivityEditDTO ToActivityEditDTOForEdit(this Activity source)
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