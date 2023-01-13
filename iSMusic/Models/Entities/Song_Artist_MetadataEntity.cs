using iSMusic.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iSMusic.Models.Entities
{
	public class Song_Artist_MetadataEntity
	{
		public int id { get; set; }

		public int songId { get; set; }

		public int artistId { get; set; }

		public ArtistEntity Artist { get; set; }
	}

	public static partial class Song_Artist_MetadataExts
	{
		public static Song_Artist_MetadataEntity ToEntity(this Song_Artist_Metadata source)
		{
			return new Song_Artist_MetadataEntity
			{
				id = source.id,
				songId = source.songId,
				artistId = source.artistId,
				Artist = source.Artist.ToEntity(),
			};
		}
	}
}