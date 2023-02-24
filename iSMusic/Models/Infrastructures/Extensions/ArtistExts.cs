using iSMusic.Models.DTOs;
using iSMusic.Models.EFModels;
using iSMusic.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iSMusic.Models.Infrastructures.Extensions
{
	public static class ArtistExts
	{
		public static ArtistVM ToVM(this Artist source)
		{
			return new ArtistVM
            {
				id = source.id,
				artistName = source.artistName,
				isBand = source.isBand,
				artistGender = source.artistGender,
				artistAbout = source.artistAbout,
				artistPicPath= source.artistPicPath,
			};
		}

        public static ArtistDTO ToDTO(this ArtistVM source)
        {
            return new ArtistDTO
            {
                id = source.id,
                artistName = source.artistName,
                isBand = source.isBand,
                artistGender = source.artistGender,
                artistAbout = source.artistAbout,
				artistPicPath= source.artistPicPath,
				CoverFile = source.CoverFile,
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
				artistPicPath= source.artistPicPath,
			};
		}
	}
}