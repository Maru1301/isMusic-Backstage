using Antlr.Runtime.Tree;
using isMusic.Models.DTOs;
using isMusic.Services.Interfaces;
using iSMusic.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace isMusic.Services
{
    public class ActivityService
    {
        private readonly IActivityRepository _repository;

        //這個最後要拿掉
        private AppDbContext db = new AppDbContext();

        public ActivityService(IActivityRepository repo)
        {
            this._repository = repo;
        }
        //public (bool IsSuccess, string ErrorMessage) CreateNewActivity(ActivityDTO dto)
        //{
        //    if (_repository.IsExist(dto.activityName))
        //    {
        //        return (false, "活動已存在");
        //    }

        //    _repository.Create(dto);
        //    return (true, null);
        //}

        //public IEnumerable<ActivityVM> GetAll()
        //{
        //    return _repository.GetAll();
        //}
        public IEnumerable<ActivityDTO> Search(int? activityId, string activityName)
            => _repository.Search(activityId, activityName, true);

        public ActivityDTO GetByActivityName(string currentActivityAccount)
            => _repository.GetByActivityName(currentActivityAccount);

        public ActivityDTO GetById(int id)
            => _repository.GetById(id);

        public void Create(string coverPath, ActivityCreateDTO dto)
        {
            if (ActivityExistsCheckForCreate(dto) == true)
                throw new Exception("此活動已存在");

			if (dto.File == null || string.IsNullOrEmpty(dto.File.FileName) || dto.File.ContentLength == 0)
			{
				dto.Path = string.Empty;
			}
			else
			{
				// save uploaded file
				string fileName = System.IO.Path.GetFileName(dto.File.FileName); // "photo.jpg"

				// 取一個不重覆的新檔名
				string newFileName = GetNewFileName(coverPath, fileName); // <===

				string fullPath = System.IO.Path.Combine(coverPath, newFileName); // <=== "c:\sites\uploads\photo.jpg"
				dto.File.SaveAs(fullPath); // 將上傳的檔案存放到 server
				dto.Path = newFileName;// <===
			}

			_repository.Create(dto);
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
		//public ActivityDTO GetByIdForEdit(int id)
		//    => _repository.GetByIdForEdit(id);

		public void Edit(ActivityEditDTO dto)
        {

            if (ActivityExistsCheckForEdit(dto) == true) throw new Exception("此活動已存在");

            _repository.Edit(dto);
        }

        private bool ActivityExistsCheckForCreate(ActivityCreateDTO dto)
        {
            var model = _repository.IsExistForCreate(dto);
            return model != null;
        }

        private bool ActivityExistsCheckForEdit(ActivityEditDTO dto)
        {
            var model = _repository.IsExistForEdit(dto);
            return model != null;
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}