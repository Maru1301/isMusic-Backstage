using iSMusic.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace iSMusic.Models.ViewModels
{
	public class AlbumCreateVM
	{
		[Required]
		[Display(Name = "專輯類別*")]
		public int typeId { get; set; }

		[Required]
		[Display(Name = "專輯音樂種類*")]
		public int albumGenreId { get; set; }

		[Required]
		[StringLength(50)]
		[Display(Name = "專輯名稱*")]
		public string albumName { get; set; }

		[Display(Name = "音樂封面檔案*")]
		public HttpPostedFileBase CoverFile { get; set; }

		[Column(TypeName = "date")]
		[Display(Name = "發行時間*")]
		public DateTime released { get; set; }

		[Required]
		[StringLength(3000)]
		[Display(Name = "描述*")]
		public string description { get; set; }

		[Display(Name = "主要表演者*")]
		public int mainArtistId { get; set; }

		[Display(Name = "歌曲*")]
		public List<int> songIdList { get; set; }

        [Display(Name = "製作人*")]
        public string albumProducer { get; set; }

        [Display(Name = "發行公司*")]
        public string albumCompany { get; set; }
    }
}