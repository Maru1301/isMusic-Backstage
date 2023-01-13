using iSMusic.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iSMusic.Models.DTOs
{
	public class SongDTO
	{
		public SongDTO()
		{
			Song_Artist_Metadata = new HashSet<Song_Artist_Metadata>();
		}
		public int id { get; set; }

		public string songName { get; set; }

		public List<int> artistIdList { get; set; }

		public List<string> artistList { get; set; }

		public int genreId { get; set; }

		public string genreName { get; set; }

		public int duration { get; set; }

		public bool isInstrumental { get; set; }

		public string language { get; set; }

		public bool? isExplicit { get; set; }

		public DateTime released { get; set; }

		public string songWriter { get; set; }

		public string lyric { get; set; }

		public string songCoverPath { get; set; }

		public string songPath { get; set; }

		public HttpPostedFileBase CoverFile { get; set; }

		public HttpPostedFileBase SongFile { get; set; }

		public bool status { get; set; }

		public int timeOfPlay { get; set; }

		public virtual ICollection<Song_Artist_Metadata> Song_Artist_Metadata { get; set; }

		public virtual SongGenre SongGenre { get; set; }
	}
}