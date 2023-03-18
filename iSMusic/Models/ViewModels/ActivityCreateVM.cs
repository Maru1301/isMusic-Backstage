using isMusic.Models.DTOs;
using iSMusic.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace isMusic.Models.ViewModels
{
    public class ActivityCreateVM
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
        public int activityTypeId { get; set; }

        [Display(Name = "活動資訊")]
        [Required]
        [StringLength(4000)]
        public string activityInfo { get; set; }

        [Display(Name = "活動發起者")]
        public int activityOrganizerId { get; set; }


        //[Display(Name = "活動照片")]
        //[Required]
        //[StringLength(50)]
        //public string activityImagePath { get; set; }

        [Display(Name = "審核完成")]
        public bool publishedStatus { get; set; }

        [Display(Name = "審核者")]
        public int checkedById { get; set; }
		[Required]
		[Display(Name = "活動封面檔案*")]
		public HttpPostedFileBase File { get; set; }


		//public virtual ActivityType ActivityType { get; set; }

		//public virtual Admin Admin { get; set; }

		//public virtual Member Member { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActivityFollow> ActivityFollows { get; set; }
    }


    public static class ActivityCreateVMExts
    {
        public static ActivityCreateVM ToActivityCreateVM(this ActivityDTO source)
        {
            return new ActivityCreateVM
            {
                id = source.id,
                activityName = source.activityName,
                activityStartTime = source.activityStartTime,
                activityEndTime = source.activityEndTime,
                activityLocation = source.activityLocation,
                activityTypeId = source.activityType.id,
                activityInfo = source.activityInfo,
                //activityImagePath = source.activityImagePath,
                activityOrganizerId = source.member.id,
                publishedStatus = source.publishedStatus,
                checkedById = source.admin.id,
                
            };
        }
    }
}
