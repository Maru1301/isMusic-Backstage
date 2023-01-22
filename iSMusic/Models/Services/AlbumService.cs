using iSMusic.Models.DTOs;
using iSMusic.Models.Infrastructures.Repositories;
using iSMusic.Models.Services.Interfaces;
using iSMusic.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iSMusic.Models.Services
{
	public class AlbumService
	{
		private IAlbumRepository repository;

		public AlbumService(IAlbumRepository repo)
		{
			repository = repo;
		}

		public IEnumerable<AlbumIndexVM> Index()
		{
			return repository.GetAlbumIndexVMs();
		}

		public void AddNewAlbum(string coverPath, AlbumDTO dto)
		{
			if (ExistDupSong(dto.songIdList) == true) throw new Exception("專輯內有重複歌曲");
			if (dto.songIdList == null) throw new Exception("未選中演出者");
			//check if the song has existed in the database
			if (repository.Search(dto) != null) throw new Exception("專輯已存在");

			// Upload cover
			if (dto.CoverFile == null || string.IsNullOrEmpty(dto.CoverFile.FileName) || dto.CoverFile.ContentLength == 0)
			{
				dto.albumCoverPath = string.Empty;
			}
			else
			{
				// save uploaded file
				string fileName = System.IO.Path.GetFileName(dto.CoverFile.FileName); // "photo.jpg"

				// 取一個不重覆的新檔名
				string newFileName = GetNewFileName(coverPath, fileName); // <===

				string fullPath = System.IO.Path.Combine(coverPath, newFileName); // <=== "c:\sites\uploads\photo.jpg"
				dto.CoverFile.SaveAs(fullPath); // 將上傳的檔案存放到 server
				dto.albumCoverPath = newFileName;// <===
			}

			repository.AddNewAlbum(dto);

			var metadataRepository = new AlbumMetadataRepository();
			var album = repository.Search(dto);
			foreach (var songId in dto.songIdList)
			{
				metadataRepository.AddNewMetadata(album.id, songId);
			}
		}

		public void UpdateAlbum(string coverPath, AlbumDTO dto)
		{
			var album = repository.FindById(dto.id);
			if (ExistDupSong(dto.songIdList) == true) throw new Exception("專輯內有重複歌曲");
			if (dto.songIdList == null) throw new Exception("未選中演出者");
			if (repository.Search(dto, dto.id) != null) throw new Exception("擁有相同資料的專輯已存在");

			if (dto.CoverFile == null || string.IsNullOrEmpty(dto.CoverFile.FileName) || dto.CoverFile.ContentLength == 0)
			{
				dto.albumCoverPath = album.albumCoverPath;
			}
			else
			{
				// save uploaded file
				string fileName = System.IO.Path.GetFileName(dto.CoverFile.FileName); // "photo.jpg"

				// 取一個不重覆的新檔名
				string newFileName = GetNewFileName(coverPath, fileName); // <===

				string fullPath = System.IO.Path.Combine(coverPath, newFileName); // <=== "c:\sites\uploads\photo.jpg"
				dto.CoverFile.SaveAs(fullPath); // 將上傳的檔案存放到 server
				dto.albumCoverPath = newFileName;// <===
			}

			repository.UpdateAlbum(dto);

			UpdateAlbumMetadata(album.id, dto.songIdList);
		}

		public void DeleteAlbum(int id)
		{
			DeleteAlbumMetadata(id);
			repository.DeleteAlbum(id);
		}

		private void DeleteAlbumMetadata(int albumId)
		{
			var metadataRepository = new AlbumMetadataRepository();
			var data = metadataRepository.GetSongIdList(albumId).ToList();

			foreach (var sondId in data)
			{
				metadataRepository.DeleteMetadata(albumId, sondId);
			}
		}

		private bool ExistDupSong(List<int> songIdList)
		{
			List<int> songIds = new List<int>();
			foreach (int songId in songIdList)
			{
				if (songIds.Contains(songId))
				{
					return true;
				}
				else
				{
					songIds.Add(songId);
				}
			}

			return false;
		}

		private void UpdateAlbumMetadata(int albumId, List<int> newList)
		{
			var metadataRepository = new AlbumMetadataRepository();
			var oldList = metadataRepository.GetSongIdList(albumId).ToList();

			foreach (int id in newList)
			{
				if (oldList.Contains(id) == false)
				{
					metadataRepository.AddNewMetadata(albumId, id);
				}
				else
				{
					oldList.Remove(id);
				}
			}

			foreach (int id in oldList)
			{
				metadataRepository.DeleteMetadata(albumId, id);
			}
		}

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
	}
}