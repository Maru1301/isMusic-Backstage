using iSMusic.Models.DTOs;
using iSMusic.Models.EFModels;
using iSMusic.Models.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iSMusic.Models.Services
{
	public class ArtistService
	{
		private IArtistRepository repository;
		public ArtistService(IArtistRepository repo)
		{
			repository = repo;
		}

		public IEnumerable<Artist> FindAll()
		{
			return repository.FindAll();
			//return new ArtistDAO().FindAll();
		}

		public Artist FindById(int id)
		{
			return repository.FindById(id);
			//return new ArtistDAO().FindById(id);
		}

		public IQueryable<Artist> GetQuery()
		{
			return repository.GetQuery();
		}

		public void Create(ArtistDTO dto)
		{
			//check existence
			if (ArtistExists(dto) == true) throw new Exception("音樂家已存在");

			//EF
			repository.Create(dto);

			//ado.net
			//new ArtistDAO().Create(dto);
		}

		public void Edit(ArtistDTO dto)
		{
			//check if the artist has existed
			if (ArtistExists(dto) == true) throw new Exception("相同資料的表演者已存在");

			//mvc
			repository.Edit(dto);

			//ado.net
			//new ArtistDAO().Edit(dto);
		}

		public void Delete(ArtistDTO dto)
		{
			repository.Delete(dto);
			//new ArtistDAO().Delete(dto);
		}

		private bool ArtistExists(ArtistDTO dto)
		{
			var model = repository.IsExisted(dto);
			//var model = new ArtistDAO().ArtistExists(dto);

			return model != null;
		}
	}
}