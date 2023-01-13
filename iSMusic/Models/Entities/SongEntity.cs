using iSMusic.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iSMusic.Models.Entities
{
	public class SongEntity
	{
		public int id { get; set; }

		public string songName { get; set; }

		public IEnumerable<Song_Artist_MetadataEntity> Song_Artist_Metadata { get; set; }

		public SongGenreEntity SongGenre { get; set; }

		public int duration { get; set; }

		public bool isInstrumental { get; set; }

		public string language { get; set; }

		public bool? isExplicit { get; set; }

		public DateTime released { get; set; }

		public string songWriter { get; set; }

		public string lyric { get; set; }

		public string songCoverPath { get; set; }

		public string songPath { get; set; }

		public int timesOfPlay { get; set; }

		public bool status { get; set; }
	}

	public static partial class SongExts
	{
		public static SongEntity ToEntity(this Song source)
		{
			return new SongEntity()
			{
				id = source.id,
				songName = source.songName,
				Song_Artist_Metadata = source.Song_Artist_Metadata.Select(m=> m.ToEntity()),
				SongGenre = source.SongGenre.ToEntity(),
				duration= source.duration,
				isInstrumental = source.isInstrumental,
				language = source.language,
				isExplicit= source.isExplicit,
				released= source.released,
				songWriter = source.songWriter,
				lyric = source.lyric,
				songCoverPath= source.songCoverPath,
				songPath= source.songPath,
				timesOfPlay = source.timesOfPlay,
				status = source.status,
			};
		}
	}
}