using iSMusic.Models.DTOs;
using iSMusic.Models.EFModels;
using iSMusic.Models.Entities;
using iSMusic.Models.Infrastructures.Extensions;
using iSMusic.Models.Services.Interfaces;
using iSMusic.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using static iSMusic.Controllers.AlbumsController;
using static iSMusic.Controllers.SongsController;

namespace iSMusic.Models.Infrastructures.Repositories
{
	public class SongRepository: ISongRepository
	{
		private AppDbContext db;

		public SongRepository()
		{
			db = new AppDbContext();
		}

		public List<SongIndexVM> FindAll()
		{
			return db.Songs
				.Include("Song_Artist_Metadata")
				.Include("SongGenre")
				.Select(x => new
				{
					x.id,
					x.songName,
					artistList = x.Song_Artist_Metadata.Select(m => m.Artist).Select(a => a.artistName).ToList(),
					genreName = x.SongGenre.genreName,
					x.language,
					x.released,
					x.duration,
					x.songWriter,
					x.songPath,
					x.status,
				}).ToList()
			.Select(p => new SongIndexVM
			{
				id = p.id,
				songName = p.songName,
				artistList = p.artistList,
				genreName = p.genreName,
				language = p.language,
				released = p.released,
				duration = p.duration,
				songWriter = p.songWriter,
				songPath = "/Uploads/Songs/" + p.songPath,
				status = p.status,
			}).ToList();
		}

		public Song Find(int id)
		{
			return db.Songs.Find(id);
		}

		public void LaunchSong(Song song)
		{
			db.Entry(song).Property(s => s.status).IsModified = true;
			db.SaveChanges();
		}

		public void RecallSong(Song song)
		{
			db.Entry(song).Property(s => s.status).IsModified = true;
			db.SaveChanges();
		}

		public void AddNewSong(SongDTO dto)
		{
			db.Songs.Add(dto.ToEntity());
			db.SaveChanges();
		}

		public void EditSong(SongDTO dto)
		{
			var song = db.Songs.Find(dto.id);
			db.Entry(song).State = System.Data.Entity.EntityState.Detached;
			db.Entry(dto.ToEntity()).State = System.Data.Entity.EntityState.Modified;
			db.SaveChanges();
		}

		public Song Search(SongDTO dto)
		{
			Song model;
			if (dto.id == 0)
			{
				model = db.Songs.SingleOrDefault(x => x.songName == dto.songName && x.genreId == dto.genreId && x.duration == dto.duration);
			}
			else
			{
				model = db.Songs.SingleOrDefault(x => x.id != dto.id && x.songName == dto.songName && x.genreId == dto.genreId && x.duration == dto.duration);
			}

			return model;
		}

		public IEnumerable<SongEntity> Search(SongCriteria criteria, Controllers.SongsController.SortInfo sortInfo)
		{
			IQueryable<Song> query = db.Songs;

			// Searching
			query = criteria.ApplyCriteria(query);

			//Sorting
			query = sortInfo.ApplySort(query);

			return query.ToList().Select(q => q.ToEntity());
		}

		public SongDTO FindById(int id)
		{
			return db.Songs.Include("Song_Artist_Metadata").Select(x => new SongDTO()
			{
				id = x.id,
				songName = x.songName,
				artistList = x.Song_Artist_Metadata.Where(m=> m.songId == id).Select(m=> m.Artist.artistName).ToList(),
				artistIdList = x.Song_Artist_Metadata.Where(m => m.songId == id).Select(a => a.artistId).ToList(),
				genreId= x.genreId,
				genreName = x.SongGenre.genreName,
				duration = x.duration,
				isInstrumental = x.isInstrumental,
				language = x.language,
				isExplicit = x.isExplicit,
				released = x.released,
				songWriter = x.songWriter,
				lyric = x.lyric,
				songCoverPath = x.songCoverPath,
				songPath = x.songPath,
				status = x.status,
				timesOfPlay = x.timesOfPlay,
			}).SingleOrDefault(x => x.id == id);
		}

		public void DeleteSong(SongDTO dto)
		{
			db.Entry(dto.ToEntity()).State = System.Data.Entity.EntityState.Deleted;
			db.SaveChanges();
		}

		public IEnumerable<SongListItemVM> GetSongList(int artistId)
		{
			return db.Song_Artist_Metadata.Include("Song").Where(m => m.artistId == artistId).Select(m => new
			{
				m.songId,
				m.Song.songName
			}).Select(x => new SongListItemVM
			{
				songId = x.songId,
				songName = x.songName
			});
		}

		public IQueryable<SongEntity> GetQuery()
		{
			return db.Songs.Select(s=> s.ToEntity());
		}
	}
}