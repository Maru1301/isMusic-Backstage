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

			var albumId = db.Albums.OrderByDescending(album => album.id).First().id;

			int order = 0;
			foreach(var songId in dto.songIdList)
			{
				var song = db.Songs.Single(s => s.id == songId);

				song.albumId = albumId;
				song.displayOrderInAlbum = order++;
			}

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
			return db.Albums.Include("Album_Song_Metadata").Include("Artist").Select(a => new AlbumEditVM
			{
				id = a.id,
				typeId = a.albumTypeId,
				albumName = a.albumName,
				albumGenreId= a.albumGenreId,
				albumCoverPath = a.albumCoverPath,
				released = a.released,
				description = a.description,
				mainArtistId = a.mainArtistId.Value,
				ArtistName = a.Artist.artistName,
				songIdList = a.Songs.Where(m => m.albumId == id).Select(x => x.id).ToList()
			}).SingleOrDefault(x => x.id == id);
		}

		public IEnumerable<AlbumIndexVM> GetAlbumIndexVMs()
		{
			return db.Albums.Include("Artist").Include("AlbumType").Select(a => new
			{
				a.id,
				a.albumName,
                a.AlbumType.typeName,
				a.released,
				mainArtistName = a.Artist.artistName,
			}).Select(a => new AlbumIndexVM
			{
				id = a.id,
				albumName = a.albumName,
				typeName = a.typeName,
				released = a.released,
				mainArtistName = a.mainArtistName
			});
		}

		public IQueryable<AlbumIndexVM> GetQuery()
		{
			return db.Albums.Include("Artist").Include("SongGenre").Select(a=> new AlbumIndexVM
			{
				id = a.id,
				albumName = a.albumName,
				released= a.released,
				mainArtistName = a.Artist.artistName,
				typeName = a.AlbumType.typeName,
				albumTypeId = a.albumTypeId,
				albumGenreName = a.SongGenre.genreName
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

            int order = 0;
            foreach (var songId in dto.songIdList)
            {
                var song = db.Songs.Single(s => s.id == songId);
                song.displayOrderInAlbum = order++;
            }

            db.SaveChanges();
		}
	}
}