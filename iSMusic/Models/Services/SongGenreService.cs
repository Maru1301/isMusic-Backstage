using iSMusic.Models.DTOs;
using iSMusic.Models.EFModels;
using iSMusic.Models.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iSMusic.Models.Services
{
	public class SongGenreService
	{
		private ISongGenreRepository repository;
		public SongGenreService(ISongGenreRepository repo)
		{
			repository = repo;
		}

		public List<SongGenre> Index(int pageSize, int recordStartIndex, string genreName)
		{
			return repository.FindAll(pageSize, recordStartIndex, genreName);
		}
		public void Create(SongGenreDTO dto)
		{
			repository.Create(dto);
		}

		public SongGenre Edit(int id)
		{
			return repository.FindById(id);
		}

		public void Edit(SongGenreDTO dto)
		{
			repository.Edit(dto);
		}

		public SongGenre Delete(int id)
		{
			return repository.FindById(id);
		}

		public void Delete(SongGenreDTO dto)
		{
			repository.Delete(dto);
		}
	}
}