using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iSMusic.Models.ViewModels
{
	public class AlbumIndexVM
	{
		public int id { get; set; }
		public string albumName { get; set; }

		public DateTime released { get; set; }

		public string mainArtistName { get; set; }
	}
}