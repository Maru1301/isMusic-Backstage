using iSMusic.Models.DTOs;
using iSMusic.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iSMusic.Models.Services.Interfaces
{
	public interface ISongGenreRepository
	{
		void Create(SongGenreDTO dto);

		List<SongGenre> FindAll(int pageSize, int recordStartIndex, string genreName);

		SongGenre FindById(int id);

		int GetTotalRecordsNum();

		void Edit(SongGenreDTO dto);

		void Delete(SongGenreDTO dto);
	}
}
