using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iSMusic.Models.DTOs
{
	public class AlbumDTO
	{
		public int id { get; set; }

		public string albumName { get; set; }

		public string albumCoverPath { get; set; }

		public HttpPostedFileBase CoverFile { get; set; }

		public int typeId { get; set; }

		public DateTime released { get; set; }

		public string description { get; set; }

		public int mainArtistId { get; set; }

		public List<int> songIdList { get; set; }
	}
}