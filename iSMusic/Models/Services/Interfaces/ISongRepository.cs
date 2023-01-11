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
	public interface ISongRepository
	{
		List<SongIndexVM> FindAll();

		Song Find(int id);

		void LaunchSong(Song song);

		void RecallSong(Song song);

		SongEditVM FindById(int id);

		void AddNewSong(SongDTO dto);

		void EditSong(SongDTO dto);

		Song Search(SongDTO dto);

		void DeleteSong(SongDTO dto);

		IEnumerable<SongListItemVM> GetSongList(int artistId);
	}
}
