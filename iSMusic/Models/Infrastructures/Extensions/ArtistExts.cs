using iSMusic.Models.DTOs;
using iSMusic.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iSMusic.Models.Infrastructures.Extensions
{
	public static class ArtistExts
	{
		public static ArtistDTO ToDTO(this Artist source)
		{
			return new ArtistDTO
			{
				id = source.id,
				artistName = source.artistName,
				isBand = source.isBand,
				artistGender = source.artistGender,
				artistAbout = source.artistAbout,
			};
		}

		public static Artist ToEntity(this ArtistDTO source)
		{
			return new Artist
			{
				id = source.id,
				artistName = source.artistName,
				isBand = source.isBand,
				artistGender = source.artistGender,
				artistAbout = source.artistAbout,
			};
		}
	}
}