using iSMusic.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace iSMusic.Models.ViewModels
{
	public class SongIndexVM
	{
		public SongIndexVM()
		{
			Song_Artist_Metadata = new HashSet<Song_Artist_Metadata>();
		}
		public int id { get; set; }

		[Display(Name = "歌曲名稱")]
		public string songName { get; set; }

		[Display(Name = "表演者")]
		public List<string> artistList { get; set; }

		[Display(Name = "音樂種類")]
		public string genreName { get; set; }

		[Display(Name = "歌曲長度")]
		public int duration { get; set; }

		[Display(Name = "語言")]
		public string language { get; set; }

		[Display(Name = "發行日期")]
		public DateTime released { get; set; }

		[Display(Name = "作曲/作詞")]
		public string songWriter { get; set; }

		public string songPath { get; set; }

		public virtual ICollection<Song_Artist_Metadata> Song_Artist_Metadata { get; set; }

		public virtual SongGenre SongGenre { get; set; }
	}
}