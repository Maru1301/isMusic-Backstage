using iSMusic.Models.DTOs;
using iSMusic.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iSMusic.Models.Infrastructures.Extensions
{
	public static class SongGenreExts
	{
		public static SongGenreDTO ToDTO(this SongGenre source)
		{
			return new SongGenreDTO
			{
				Id = source.id,
				GenreName = source.genreName,
			};
		}
		public static SongGenre ToEntity(this SongGenreDTO source)
		{
			return new SongGenre
			{
				id = source.Id,
				genreName = source.GenreName,
			};
		}
	}
}