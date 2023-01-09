using iSMusic.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iSMusic.Models.DTOs
{
	public class ArtistDTO
	{
		public ArtistDTO()
		{
			Song_Artist_Metadata = new HashSet<Song_Artist_Metadata>();
		}

		public int id { get; set; }

		public string artistName { get; set; }

		public bool isBand { get; set; }

		public bool? artistGender { get; set; }

		public string artistAbout { get; set; }

		public virtual ICollection<Song_Artist_Metadata> Song_Artist_Metadata { get; set; }
	}
}