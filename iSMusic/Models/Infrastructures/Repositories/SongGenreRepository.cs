using iSMusic.Models.DTOs;
using iSMusic.Models.EFModels;
using iSMusic.Models.Infrastructures.Extensions;
using iSMusic.Models.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace iSMusic.Models.Infrastructures.Repositories
{
	public class SongGenreRepository: ISongGenreRepository
	{
		private AppDbContext db;
		public SongGenreRepository()
		{
			db = new AppDbContext();
		}

		public IEnumerable<SongGenre> GetAll()
		{
			return db.SongGenres.ToList();
		}

		public List<SongGenre> FindAll(int pageSize, int recordStartIndex, string genreName)
		{
			var query = db.SongGenres;

			if (string.IsNullOrEmpty(genreName) == false) query = (DbSet<SongGenre>)query.Where(p => p.genreName.Contains(genreName));

			int totalRecords = query.Count();

			var data = query.OrderBy(t => t.id)
				.Skip(recordStartIndex).Take(pageSize).ToList();

			return data;
		}
		public SongGenre FindById(int id)
		{
			return db.SongGenres.Find(id);
		}

		public int GetTotalRecordsNum()
		{
			return db.SongGenres.Count();
		}
		public void Create(SongGenreDTO dto)
		{
			db.SongGenres.Add(dto.ToEntity());
			db.SaveChanges();
		}

		public void Edit(SongGenreDTO dto)
		{
			db.Entry(dto.ToEntity()).State = System.Data.Entity.EntityState.Modified;
			db.SaveChanges();
		}

		public void Delete(SongGenreDTO dto)
		{
			var genre = db.SongGenres.Find(dto.Id);
			db.SongGenres.Remove(genre);
			db.SaveChanges();
		}
	}
}