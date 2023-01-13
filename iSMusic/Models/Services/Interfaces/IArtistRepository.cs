using iSMusic.Models.DTOs;
using iSMusic.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iSMusic.Models.Services.Interfaces
{
	public interface IArtistRepository
	{
		List<Artist> FindAll();

		Artist FindById(int id);

		IQueryable<Artist> GetQuery();

		Artist IsExisted(ArtistDTO dto);

		void Create(ArtistDTO dto);

		void Edit(ArtistDTO dto);

		void Delete(ArtistDTO dto);
	}
}
