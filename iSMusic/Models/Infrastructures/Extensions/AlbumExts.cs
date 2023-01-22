using iSMusic.Models.DTOs;
using iSMusic.Models.EFModels;
using iSMusic.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iSMusic.Models.Infrastructures.Extensions
{
	public static class AlbumExts
	{
		public static AlbumDTO ToDTO(this AlbumCreateVM source)
		{
			return new AlbumDTO
			{
				albumName = source.albumName,
				typeId= source.typeId,
				GenreId = source.albumGenreId,
				songIdList = source.songIdList,
				description = source.description,
				mainArtistId = source.mainArtistId,
				released = source.released,
				CoverFile = source.CoverFile,
			};
		}

		public static AlbumDTO ToDTO(this AlbumEditVM source)
		{
			return new AlbumDTO
			{
				id = source.id,
				albumName = source.albumName,
				typeId = source.typeId,
				GenreId = source.albumGenreId,
				songIdList = source.songIdList,
				description = source.description,
				mainArtistId = source.mainArtistId,
				released = source.released,
				CoverFile = source.CoverFile,
			};
		}

		public static Album ToEntity(this AlbumDTO source)
		{
			return new Album
			{
				id = source.id,
				albumName = source.albumName,
				albumTypeId = source.typeId,
				albumGenreId = source.GenreId,
				albumCoverPath = source.albumCoverPath,
				released = source.released,
				description = source.description,
				mainArtistId = source.mainArtistId,
			};
		}
	}
}