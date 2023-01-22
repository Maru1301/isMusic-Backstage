using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iSMusic.Models.ViewModels
{
	public class AlbumIndexVM
	{
		public int id { get; set; }

		[Display(Name = "專輯名稱")]
		public string albumName { get; set; }

		public int albumTypeId { get; set; }

		[Display(Name = "專輯類型")]
		public string typeName { get; set; }

		[Display(Name = "專輯音樂種類")]
		public string albumGenreName { get; set; }

		[Display(Name = "發行時間")]
		[DataType(DataType.Date)]
		public DateTime released { get; set; }

		[Display(Name = "主要演出者")]
		public string mainArtistName { get; set; }
	}
}