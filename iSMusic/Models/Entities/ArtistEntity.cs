using iSMusic.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iSMusic.Models.Entities
{
	public class ArtistEntity
	{
		public int id { get; set; }

		public string artistName { get; set; }

		public bool isBand { get; set; }

		public bool? artistGender { get; set; }

		public string artistAbout { get; set; }
	}

	public static partial class ArtistExts
	{
		public static ArtistEntity ToEntity(this Artist source)
		{
			return new ArtistEntity()
			{
				id = source.id,
				artistName = source.artistName,
				artistGender = source.artistGender,
				artistAbout = source.artistAbout,
			};
		}
	}
}