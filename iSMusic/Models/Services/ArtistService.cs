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

		public void Create(ArtistDTO dto, string coverPath)
		{
			//check existence
			if (ArtistExists(dto) == true) throw new Exception("音樂家已存在");

            if (dto.CoverFile == null || string.IsNullOrEmpty(dto.CoverFile.FileName) || dto.CoverFile.ContentLength == 0)
            {
                dto.artistPicPath = string.Empty;
            }
            else
            {
                // save uploaded file
                string fileName = System.IO.Path.GetFileName(dto.CoverFile.FileName); // "photo.jpg"

                // 取一個不重覆的新檔名
                string newFileName = GetNewFileName(coverPath, fileName); // <===

                string fullPath = System.IO.Path.Combine(coverPath, newFileName); // <=== "c:\sites\uploads\photo.jpg"
                dto.CoverFile.SaveAs(fullPath); // 將上傳的檔案存放到 server
                dto.artistPicPath = newFileName;// <===
            }

            repository.Create(dto);
		}

        private string GetNewFileName(string path, string fileName)
        {
            string ext = System.IO.Path.GetExtension(fileName); // 取得副檔名,例如".jpg"
            string newFileName;
            string fullPath;
            // todo use song name + artists name instead of guid, so when uploading the new file it will replace the old one.
            do
            {
                newFileName = Guid.NewGuid().ToString("N") + ext;
                fullPath = System.IO.Path.Combine(path, newFileName);
            } while (System.IO.File.Exists(fullPath) == true); // 如果同檔名的檔案已存在,就重新再取一個新檔名

            return newFileName;
        }

        public void Edit(ArtistDTO dto, string coverPath)
		{
			//check if the artist has existed
			if (ArtistExists(dto) == true) throw new Exception("相同資料的表演者已存在");

            if (dto.CoverFile == null || string.IsNullOrEmpty(dto.CoverFile.FileName) || dto.CoverFile.ContentLength == 0)
            {
                dto.artistPicPath = string.Empty;
            }
            else
            {
                // save uploaded file
                string fileName = System.IO.Path.GetFileName(dto.CoverFile.FileName); // "photo.jpg"

                // 取一個不重覆的新檔名
                string newFileName = GetNewFileName(coverPath, fileName);

                string fullPath = System.IO.Path.Combine(coverPath, newFileName);
                dto.CoverFile.SaveAs(fullPath); // 將上傳的檔案存放到 server
                dto.artistPicPath = newFileName;
            }

            repository.Edit(dto);
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