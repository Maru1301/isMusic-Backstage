using iSMusic.Models.DTOs;
using iSMusic.Models.EFModels;
using iSMusic.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iSMusic.Models.Services.Interfaces
{
	public interface IAlbumRepository
	{
		IEnumerable<AlbumIndexVM> GetAlbumIndexVMs();

		AlbumEditVM FindById(int id);

		Album Search(AlbumDTO dto, int albumId = 0);

		IQueryable<Album> GetQuery();

		void AddNewAlbum(AlbumDTO dto);

		void UpdateAlbum(AlbumDTO dto);

		void DeleteAlbum(int id);
	}
}
