using iSMusic.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace iSMusic.Models.ViewModels
{
	public class SongCreateVM
	{
		public SongCreateVM()
		{
			Song_Artist_Metadata = new HashSet<Song_Artist_Metadata>();
			CoverFile = null;
			SongFile = null;
		}

		public int id { get; set; }

		[Required]
		[Display(Name = "歌曲名稱")]
		public string songName { get; set; }

		[Required(ErrorMessage = "{0} 必填")]
		[Display(Name = "演奏者/創作者")]
		public List<int> artistIdList { get; set; }

		[Required]
		[Display(Name = "歌曲種類")]
		public int genreId { get; set; }

		[Required]
		[Display(Name = "歌曲長度")]
		public int duration { get; set; }

		[Required]
		[Display(Name = "純音樂")]
		public bool isInstrumental { get; set; }

		[Required]
		[Display(Name = "語言")]
		public string language { get; set; }

		[Required]
		[Display(Name = "敏感字詞")]
		public bool? isExplicit { get; set; }

		[Required]
		[Display(Name = "上架日期")]
		public DateTime released { get; set; }

		[Required]
		[StringLength(50)]
		[Display(Name = "作曲/作詞")]
		public string songWriter { get; set; }

		[Required]
		[StringLength(2000)]
		[Display(Name = "歌詞")]
		public string lyric { get; set; }

		public string songCoverPath { get; set; }

		public string songPath { get; set; }

		[Required]
		[Display(Name = "音樂封面檔案")]
		public HttpPostedFileBase CoverFile { get; set; }

		[Required]
		[Display(Name = "歌曲檔案")]
		public HttpPostedFileBase SongFile { get; set; }

		public virtual ICollection<Song_Artist_Metadata> Song_Artist_Metadata { get; set; }

		public virtual SongGenre SongGenre { get; set; }
	}
}