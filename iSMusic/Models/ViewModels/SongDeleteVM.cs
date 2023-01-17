using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace iSMusic.Models.ViewModels
{
	public class SongDeleteVM
	{
		public int id { get; set; }

		[Display(Name = "歌曲名稱")]
		public string SongName { get; set; }

		[Display(Name = "演出者名稱")]
		public List<string> SingersName { get; set; }

		[Display(Name = "歌曲種類")]
		public string GenreName { get; set; }

		[Display(Name = "語言")]
		public string Language { get; set; }

		[Display(Name = "發行日期")]
		public DateTime Released { get; set; }

		[Display(Name = "上架狀態")]
		public bool Status { get; set; }
	}
}