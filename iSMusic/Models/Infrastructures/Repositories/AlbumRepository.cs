using iSMusic.Models.DTOs;
using iSMusic.Models.EFModels;
using iSMusic.Models.Infrastructures.Extensions;
using iSMusic.Models.Services.Interfaces;
using iSMusic.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iSMusic.Models.Infrastructures.Repositories
{
	public class AlbumRepository: IAlbumRepository
	{
		private AppDbContext db;

		public AlbumRepository()
		{
			db = new AppDbContext();
		}

		public void AddNewAlbum(AlbumDTO dto)
		{
			db.Albums.Add(dto.ToEntity());
			db.SaveChanges();
		}

		public void DeleteAlbum(int id)
		{
			var album = db.Albums.Find(id);
			db.Albums.Remove(album);
			db.SaveChanges();
		}

		public AlbumEditVM FindById(int id)
		{
			return db.Albums.Include("Album_Song_Metadata").Select(a => new AlbumEditVM
			{
				id = a.id,
				albumName = a.albumName,
				albumCoverPath = a.albumCoverPath,
				released = a.released,
				description = a.description,
				mainArtistId = a.mainArtistId,
				songIdList = a.Album_Song_Metadata.Where(m => m.albumId == id).Select(x => x.songId).ToList()
			}).SingleOrDefault(x => x.id == id);
		}

		public IEnumerable<AlbumIndexVM> GetAlbumIndexVMs()
		{
			return db.Albums.Include("Artist").Select(a => new
			{
				a.id,
				a.albumName,
				a.released,
				mainArtistName = a.Artist.artistName,
			}).Select(a => new AlbumIndexVM
			{
				id = a.id,
				albumName = a.albumName,
				released = a.released,
				mainArtistName = a.mainArtistName
			});
		}

		public Album Search(AlbumDTO dto, int albumId = 0)
		{
			if (albumId == 0)
			{
				return db.Albums.SingleOrDefault(a => a.albumName == dto.albumName && a.mainArtistId == dto.mainArtistId && a.description == dto.description);
			}

			return db.Albums.SingleOrDefault(a => a.id != albumId && a.albumName == dto.albumName && a.mainArtistId == dto.mainArtistId && a.description == dto.description);
		}

		public void UpdateAlbum(AlbumDTO dto)
		{
			db.Entry(dto.ToEntity()).State = System.Data.Entity.EntityState.Modified;
			db.SaveChanges();
		}
	}
}