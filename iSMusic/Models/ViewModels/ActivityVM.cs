using isMusic.Models.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace isMusic.ViewModels
{
    public class ActivityVM
    {
        public int id { get; set; }

        [Display(Name = "活動名稱")]
        [Required]
        [StringLength(30)]
        public string activityName { get; set; }

        [Display(Name = "開始時間"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime activityStartTime { get; set; }

        [Display(Name = "結束時間"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime activityEndTime { get; set; }

        [Display(Name = "活動地點")]
        [Required]
        [StringLength(100)]
        public string activityLocation { get; set; }

        [Display(Name = "活動類型")]
        public string activityType { get; set; }

        [Display(Name = "活動資訊")]
        [Required]
        [StringLength(4000)]
        public string activityInfo { get; set; }

        [Display(Name = "活動發起者")]
        public string memberAccount { get; set; }


        [Display(Name = "活動照片")]

        [Required]
        [StringLength(50)]
        public string activityImagePath { get; set; }

        [Display(Name = "審核完成")]
        public bool publishedStatus { get; set; }

        [Display(Name = "審核者")]
        public string checkedBy { get; set; }

    }

    public static class ActivityVMExts
    {
        public static ActivityVM ToActivityVM(this ActivityDTO source)
        {
            return new ActivityVM
            {
                id = source.id,
                activityName = source.activityName,
                activityStartTime = source.activityStartTime,
                activityEndTime = source.activityEndTime,
                activityLocation = source.activityLocation,
                activityType = source.activityType.typeName,
                activityInfo = source.activityInfo,
                activityImagePath = source.activityImagePath,
                memberAccount = source.member.memberAccount,
                publishedStatus = source.publishedStatus,
                checkedBy = source.admin.adminAccount,
            };
        }
    }


}