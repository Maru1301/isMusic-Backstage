using iSMusic.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iSMusic.Models.Entities
{
	public class SongGenreEntity
	{
		public int id { get; set; }

		public string genreName { get; set; }
	}

	public static partial class SongGenreExts
	{
		public static SongGenreEntity ToEntity(this SongGenre source)
		{
			return new SongGenreEntity() 
			{ 
				id = source.id, 
				genreName = source.genreName
			};
		}
	}
}