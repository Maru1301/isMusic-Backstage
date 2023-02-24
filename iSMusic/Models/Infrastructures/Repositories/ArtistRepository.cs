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
	public class ArtistRepository: IArtistRepository
	{
		private AppDbContext db;
		public ArtistRepository()
		{
			db = new AppDbContext();
		}

		public List<Artist> FindAll()
		{
			return db.Artists.ToList();
		}

		public Artist FindById(int id)
		{
			return db.Artists.Find(id);
		}

		public IQueryable<Artist> GetQuery()
		{
			return db.Artists;
		} 

		public void Create(ArtistDTO dto)
		{
			db.Artists.Add(dto.ToEntity());
			db.SaveChanges();
		}
		public void Edit(ArtistDTO dto)
		{
			var data = FindById(dto.id);
			db.Entry(data).State = EntityState.Detached;
			db.Entry(dto.ToEntity()).State = EntityState.Modified;
			db.SaveChanges();
		}

		public void Delete(ArtistDTO dto)
		{
			var artist = db.Artists.Find(dto.id);

			db.Artists.Remove(artist);
			db.SaveChanges();
		}

		public Artist IsExisted(ArtistDTO dto)
		{
			Artist model;
			if (dto.id == 0)
			{
				model = db.Artists.SingleOrDefault(a => a.artistName == dto.artistName && a.isBand == dto.isBand && a.artistGender == dto.artistGender && a.artistAbout == dto.artistAbout);
			}
			else
			{
				model = db.Artists.SingleOrDefault(a => a.id != dto.id && a.artistName == dto.artistName && a.isBand == dto.isBand && a.artistGender == dto.artistGender && a.artistAbout == dto.artistAbout);
			}

			return model;
		}
	}
}