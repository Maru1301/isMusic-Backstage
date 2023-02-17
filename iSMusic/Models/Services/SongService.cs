using iSMusic.Models.DTOs;
using iSMusic.Models.Entities;
using iSMusic.Models.Infrastructures.Extensions;
using iSMusic.Models.Infrastructures.Repositories;
using iSMusic.Models.Services.Interfaces;
using iSMusic.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebGrease.Css.Ast.MediaQuery;
using static iSMusic.Controllers.SongsController;

namespace iSMusic.Models.Services
{
	public class SongService
	{
		private ISongRepository repository;
		private int pageSize;
		public SongService(ISongRepository repo)
		{
			repository = repo;
			pageSize = 2;
		}

		public List<SongIndexVM> Index()
		{
			return repository.FindAll();
		}

		public IEnumerable<SongEntity> Search(SongCriteria criteria, SortInfo sortInfo, int pageNumber, out Models.Infrastructures.PaginationInfo paginationInfo)
		{
			var entities = repository.Search(criteria, sortInfo);
			int totalRecords = entities.Count();

			paginationInfo = new Models.Infrastructures.PaginationInfo(totalRecords, this.pageSize, pageNumber);

			var list = paginationInfo.GetPagedData(entities);

			return list;
		}

		public string LaunchSong(List<int> songIds)
		{
			foreach(int songId in songIds)
			{
				var song = repository.Find(songId);

				if (song == null)
				{
					throw new Exception($"查無ID為{songId}的歌");
				}

				song.status = true;

				repository.LaunchSong(song);
			}

			return "上架成功";
		}

		public string RecallSong(List<int> songIdList)
		{
			foreach (int songId in songIdList)
			{
				var song = repository.Find(songId);

				if (song == null)
				{
					throw new Exception($"查無ID為{songId}的歌");
				}

				song.status = false;

				repository.RecallSong(song);
			}

			return "下架成功";
		}

		public void AddNewSong(string coverPath, string songPath, SongDTO dto)
		{
			if (dto.artistIdList == null) throw new Exception("未選中表演者");
			if (dupArtist(dto.artistIdList) == true) throw new Exception("表演者重複");
			//check if the song has existed in the database
			if (repository.Search(dto) != null) throw new Exception("歌曲已存在");

			// Upload cover
			if (dto.CoverFile == null || string.IsNullOrEmpty(dto.CoverFile.FileName) || dto.CoverFile.ContentLength == 0)
			{
				dto.songCoverPath = string.Empty;
			}
			else
			{
				// save uploaded file
				string fileName = System.IO.Path.GetFileName(dto.CoverFile.FileName); // "photo.jpg"

				// 取一個不重覆的新檔名
				string newFileName = GetNewFileName(coverPath, fileName); // <===

				string fullPath = System.IO.Path.Combine(coverPath, newFileName); // <=== "c:\sites\uploads\photo.jpg"
				dto.CoverFile.SaveAs(fullPath); // 將上傳的檔案存放到 server
				dto.songCoverPath = newFileName;// <===
			}

			// Upload song
			if (dto.SongFile == null || string.IsNullOrEmpty(dto.SongFile.FileName) || dto.SongFile.ContentLength == 0)
			{
				dto.songPath = string.Empty;
			}
			else
			{
				// save uploaded file
				string fileName = System.IO.Path.GetFileName(dto.SongFile.FileName); // "song.mp3"

				// 取一個不重覆的新檔名
				string newFileName = GetNewFileName(songPath, fileName); // <===

				string fullPath = System.IO.Path.Combine(songPath, newFileName); // <=== "c:\sites\uploads\song.mp3"
				dto.SongFile.SaveAs(fullPath); // 將上傳的檔案存放到 server
				dto.songPath = newFileName;// <===
			}

			if (dto.released <= DateTime.Now)
			{
				dto.status = true;
			}

			dto.timesOfPlay = 0;

			//create new song data in the database
			repository.AddNewSong(dto);

			//create new song metadata in the database
			var metadataRepo = new SongMetadataRepository();
			var song = repository.Search(dto);
			foreach (int id in dto.artistIdList)
			{
				metadataRepo.CreateMetadata(song.id, id);
			}
		}

		public void EditSong(string coverPath, string songPath, SongDTO dto)
		{
			var song = repository.FindById(dto.id);
			if (song == null) throw new Exception("此歌曲已不存在");

			dto.id = song.id;
			if (dto.artistIdList == null) throw new Exception("未選中表演者");
			// check if there are duplicated artists
			if (dupArtist(dto.artistIdList) == true) throw new Exception("表演者重複");
			//check if the song has existed in the database
			if (repository.Search(dto) != null) throw new Exception("同樣歌曲已存在");

			// Upload cover
			if (dto.CoverFile == null || string.IsNullOrEmpty(dto.CoverFile.FileName) || dto.CoverFile.ContentLength == 0)
			{
				dto.songCoverPath = song.songCoverPath;
			}
			else
			{
				// save uploaded file
				string fileName = System.IO.Path.GetFileName(dto.CoverFile.FileName); // "photo.jpg"

				// 取一個不重覆的新檔名
				string newFileName = GetNewFileName(coverPath, fileName); // <===

				string fullPath = System.IO.Path.Combine(coverPath, newFileName); // <=== "c:\sites\uploads\photo.jpg"
				dto.CoverFile.SaveAs(fullPath); // 將上傳的檔案存放到 server
				dto.songCoverPath = newFileName;// <===
			}

			// Upload song
			if (dto.SongFile == null || string.IsNullOrEmpty(dto.SongFile.FileName) || dto.SongFile.ContentLength == 0)
			{
				dto.songPath = song.songPath;
			}
			else
			{
				// save uploaded file
				string fileName = System.IO.Path.GetFileName(dto.SongFile.FileName); // "song.mp3"

				// 取一個不重覆的新檔名
				string newFileName = GetNewFileName(songPath, fileName); // <===

				string fullPath = System.IO.Path.Combine(songPath, newFileName); // <=== "c:\sites\uploads\song.mp3"
				dto.SongFile.SaveAs(fullPath); // 將上傳的檔案存放到 server
				dto.songPath = newFileName;// <===
			}

			dto.status = song.status;
			dto.timesOfPlay = song.timesOfPlay;

			//create new song data in the database
			repository.EditSong(dto);

			//update song artist metadata
			UpdateMetadata(song.id, dto.artistIdList);
		}

		private void UpdateMetadata(int songId, List<int> newList)
		{
			var metadataRepo = new SongMetadataRepository();
			var oldList = metadataRepo.FindMetadata(songId).ToList();

			foreach (int id in newList)
			{
				if (oldList.Contains(id) == false)
				{
					metadataRepo.CreateMetadata(songId, id);
				}
				else
				{
					oldList.Remove(id);
				}
			}

			foreach (int id in oldList)
			{
				metadataRepo.DeleteMetadata(songId, id);
			}
		}

		public void DeleteSong(SongDTO dto)
		{
			var song = repository.FindById(dto.id);
			if (song == null) throw new Exception("查無此歌");

			//if this song is in any album, this song is not able to be deleted the song
			if (IsInAlbum(dto.id) == true) throw new Exception("有一張專輯包含了此歌");

			SongMetadataRepository metadataRepository = new SongMetadataRepository();
			foreach(int artistId in song.artistIdList)
			{
				metadataRepository.DeleteMetadata(dto.id, artistId);
			}
			
			repository.DeleteSong(song);
		}

		private bool IsInAlbum(int id)
		{
			var albumRepo = new AlbumRepository();
			var album = albumRepo.FindById(id);

			return album != null;
		}

		private bool dupArtist(List<int> artistIdList)
		{
			List<int> ids = new List<int>();

			foreach (int id in artistIdList)
			{
				if (ids.Contains(id))
				{
					return true;
				}

				ids.Add(id);
			}

			return false;
		}

		/// <summary>
		/// 取一個在 path裡, 唯一的新檔名
		/// </summary>
		/// <param name="path"></param>
		/// <param name="fileName"></param>
		/// <returns></returns>
		private string GetNewFileName(string path, string fileName)
		{
			string ext = System.IO.Path.GetExtension(fileName); // 取得副檔名,例如".jpg"
			string newFileName = string.Empty;
			string fullPath = string.Empty;

			// todo use song name + artists name instead of guid, so when uploading the new file it will replace the old one.
			do
			{
				newFileName = Guid.NewGuid().ToString("N") + ext;
				fullPath = System.IO.Path.Combine(path, newFileName);
			} while (System.IO.File.Exists(fullPath) == true); // 如果同檔名的檔案已存在,就重新再取一個新檔名

			return newFileName;
		}

		public IEnumerable<SongListItemVM> GetSongList(int artistId)
		{
			return repository.GetSongList(artistId);
		}
	}
}