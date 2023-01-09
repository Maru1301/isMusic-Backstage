using iSMusic.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iSMusic.Models.Infrastructures.Repositories
{
	public class SongMetadataRepository
	{
		private AppDbContext _db;

		public SongMetadataRepository()
		{
			_db = new AppDbContext();
		}

		public IEnumerable<int> FindMetadata(int songId)
		{
			return _db.Song_Artist_Metadata.Where(m => m.songId == songId).Select(m => m.artistId);
		}

		public void CreateMetadata(int songId, int artistId)
		{
			var metadata = new Song_Artist_Metadata();
			metadata.songId = songId;
			metadata.artistId = artistId;
			_db.Song_Artist_Metadata.Add(metadata);
			_db.SaveChanges();
		}

		public void DeleteMetadata(int songId, int artistId)
		{
			var metadata = _db.Song_Artist_Metadata.SingleOrDefault(m => m.songId == songId && m.artistId == artistId);

			_db.Entry(metadata).State = System.Data.Entity.EntityState.Deleted;
			_db.SaveChanges();
		}
	}
}