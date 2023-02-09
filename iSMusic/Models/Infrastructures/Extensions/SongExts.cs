using iSMusic.Models.DTOs;
using iSMusic.Models.EFModels;
using iSMusic.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iSMusic.Models.Infrastructures.Extensions
{
	public static class SongExts
	{
		public static SongDTO ToDTO(this Song source)
		{
			return new SongDTO
			{
				id= source.id,
				songName = source.songName,
				artistList = source.Song_Artist_Metadata.Where(m=>m.songId == source.id).Select(m=> m.Artist.artistName).ToList(),
				genreName = source.SongGenre.genreName,
				duration = source.duration,
				isInstrumental = source.isInstrumental,
				language = source.language,
				isExplicit = source.isExplicit,
				released = source.released,
				songWriter = source.songWriter,
				songPath= source.songPath,
			};
		}

		public static SongDTO ToDTO(this SongCreateVM source)
		{
			return new SongDTO
			{
				songName = source.songName,
				artistIdList = source.artistIdList,
				genreId = source.genreId,
				duration = source.duration,
				isInstrumental = source.isInstrumental,
				language = source.language,
				isExplicit = source.isExplicit,
				released = source.released,
				songWriter = source.songWriter,
				CoverFile = source.CoverFile,
				SongFile = source.SongFile,
				lyric = source.lyric,
			};
		}

		public static SongDTO ToDTO(this SongEditVM source)
		{
			return new SongDTO
			{
				id = source.id,
				songName = source.songName,
				artistIdList = source.artistIdList,
				genreId = source.genreId,
				duration = source.duration,
				isInstrumental = source.isInstrumental,
				language = source.language,
				isExplicit = source.isExplicit,
				released = source.released,
				songWriter = source.songWriter,
				CoverFile = source.CoverFile,
				SongFile = source.SongFile,
				lyric = source.lyric,
			};
		}

		public static SongDTO ToDTO(this SongDeleteVM source)
		{
			return new SongDTO
			{
				id = source.id,
				songName = source.SongName,
				artistList = source.SingersName,
				genreName = source.GenreName,								
				language = source.Language,				
				released = source.Released,
				status = source.Status,
			};
		}

		public static Song ToEntity(this SongDTO source)
		{
			return new Song
			{
				id = source.id,
				songName = source.songName,
				//artist will be add in the song_artist_metadata
				//genreId will be add afterward
				genreId = source.genreId,
				duration = source.duration,
				isInstrumental = source.isInstrumental,
				language = source.language,
				isExplicit = source.isExplicit,
				released = source.released,
				songWriter = source.songWriter,
				lyric = source.lyric,
				songCoverPath = source.songCoverPath,
				songPath = source.songPath,
				status = source.status,
				timesOfPlay = source.timesOfPlay,
			};
		}

		public static SongIndexVM ToIndexVM(this SongDTO source)
		{
			return new SongIndexVM
			{
				id = source.id,
				songName = source.songName,
				songPath= source.songPath,
				artistList = source.Song_Artist_Metadata.Where(m=>m.songId == source.id).Select(x=> x.Artist.artistName).ToList(),
				genreName = source.songName,
				duration = source.duration,
				language = source.language,
				released= source.released,
				status= source.status,
			};
		}

		public static SongEditVM ToEditVM(this SongDTO source)
			=> new SongEditVM
			{
				id = source.id,
				songName = source.songName,
				genreId= source.genreId,
				artistIdList= source.artistIdList,
				released = source.released,
				status= source.status,
				language= source.language,
				lyric = source.lyric,
				songCoverPath= source.songCoverPath,
				songPath = source.songPath,
				songWriter = source.songWriter,
				duration=source.duration,
				isExplicit=source.isExplicit,
				isInstrumental=source.isInstrumental,
				timesOfPlay	= source.timesOfPlay,
			};

		public static SongDeleteVM ToDeleteVM(this SongDTO source)
		=> new SongDeleteVM
		{
			id = source.id,
			SongName= source.songName,
			SingersName= source.artistList,
			GenreName= source.genreName,
			Language= source.language,
			Released= source.released,
			Status= source.status,
		};
	}
}