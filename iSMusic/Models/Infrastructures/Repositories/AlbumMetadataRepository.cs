using iSMusic.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iSMusic.Models.Infrastructures.Repositories
{
	public class AlbumMetadataRepository
	{
		private AppDbContext db;

		public AlbumMetadataRepository()
		{
			db = new AppDbContext();
		}

		public Album_Song_Metadata FindMetadata(int albumId, int songId)
		{
			return db.Album_Song_Metadata.SingleOrDefault(m => m.albumId == albumId && m.songId == songId);
		}

		public IEnumerable<int> GetSongIdList(int albumId)
		{
			return db.Album_Song_Metadata.Where(m => m.albumId == albumId).Select(x => x.songId);
		}

		public void AddNewMetadata(int albumId, int songId)
		{
			var metadata = new Album_Song_Metadata();
			metadata.albumId = albumId;
			metadata.songId = songId;

			db.Album_Song_Metadata.Add(metadata);
			db.SaveChanges();
		}

		public void DeleteMetadata(int albumId, int songId)
		{
			var metadata = FindMetadata(albumId, songId);

			db.Entry(metadata).State = System.Data.Entity.EntityState.Deleted;
			db.SaveChanges();
		}
	}
}