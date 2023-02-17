using isMusic.Models.DTOs;
using iSMusic.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iSMusic.Models.Entities
{
	public class SongPlayedRecordEntity
	{
		public int id { get; set; }

		public int songId { get; set; }

		public int memberId { get; set; }

		public DateTime playedDate { get; set; }

		public MemberEntity Member { get; set; }
	}

	public static partial class SongPlayedRecordExts
	{
		public static SongPlayedRecordEntity ToEntity(this SongPlayedRecord source)
		{
			return new SongPlayedRecordEntity
			{
				id = source.id,
				songId = source.songId,
				memberId = source.memberId,
				playedDate = source.playedDate,
				Member = source.Member.ToMemeberDTO(),
			};
		}
	}
}